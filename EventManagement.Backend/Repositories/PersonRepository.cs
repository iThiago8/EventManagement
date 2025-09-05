using Backend.Data;
using Backend.Dtos.Person;
using Backend.Helpers.QueryObjects;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class PersonRepository(ApplicationDbContext context) : IPersonRepository
    {
        public async Task<Person> CreateAsync(Person personModel)
        {
            await context.Person.AddAsync(personModel);
            await context.SaveChangesAsync();

            return personModel;
        }

        public async Task<Person?> DeleteAsync(int id)
        {
            Person? personModel = await context.Person.FirstOrDefaultAsync(p => p.Id == id);

            if (personModel == null)
                return null;

            context.Person.Remove(personModel);
            await context.SaveChangesAsync();

            return personModel;
        }

        public async Task<List<Person>> GetAllAsync(PersonQueryObject query)
        {
            IQueryable<Person> people = context.Person.AsQueryable();

            if (!string.IsNullOrEmpty(query.Cpf))
            {
                people = people.Where(p => p.Cpf.Contains(query.Cpf));
            }
            
            if (!string.IsNullOrEmpty(query.Name))
            {
                people = people.Where(p => p.Name.Contains(query.Name));
            }
            
            if (!string.IsNullOrEmpty(query.Email))
            {
                people = people.Where(p => p.Email.Contains(query.Email));
            }
            
            if (!string.IsNullOrEmpty(query.PhoneNumber))
            {
                people = people.Where(p => p.PhoneNumber.Contains(query.PhoneNumber));
            }
            
            if (query.BirthDate != null)
            {
               people = people.Where(p => p.BirthDate == query.BirthDate);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await people.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await context.Person.FindAsync(id);
        }

        public async Task<Person?> UpdateAsync(int id, Person personModel)
        {
            Person? existingPerson = await context.Person.FirstOrDefaultAsync(p => p.Id == id);

            if (existingPerson == null)
                return null;

            existingPerson.Cpf = personModel.Cpf;
            existingPerson.Name = personModel.Name;
            existingPerson.Email = personModel.Email;
            existingPerson.PhoneNumber = personModel.PhoneNumber;
            existingPerson.BirthDate = personModel.BirthDate;

            await context.SaveChangesAsync();

            return existingPerson;
        }
    }
}