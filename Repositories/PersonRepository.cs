using apis.Data;
using apis.Dtos.Person;
using apis.Helpers.QueryObjects;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
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

        public async Task<Person?> UpdateAsync(int id, UpdatePersonRequestDto personDto)
        {
            Person? existingPerson = await context.Person.FirstOrDefaultAsync(p => p.Id == id);

            if (existingPerson == null)
                return null;

            existingPerson.Cpf = personDto.Cpf;
            existingPerson.Name = personDto.Name;
            existingPerson.Email = personDto.Email;
            existingPerson.PhoneNumber = personDto.PhoneNumber;
            existingPerson.BirthDate = personDto.BirthDate;

            await context.SaveChangesAsync();

            return existingPerson;
        }
    }
}