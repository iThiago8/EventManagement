using apis.Data;
using apis.Dtos.Person;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class PersonRepository(ApplicationDbContext context) : IPersonRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Person> CreateAsync(Person personModel)
        {
            await _context.Person.AddAsync(personModel);
            await _context.SaveChangesAsync();

            return personModel;
        }

        public async Task<Person?> DeleteAsync(int id)
        {
            var personModel = await _context.Person.FirstOrDefaultAsync(p => p.Id == id);

            if (personModel == null)
                return null;

            _context.Person.Remove(personModel);
            await _context.SaveChangesAsync();

            return personModel;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Person.FindAsync(id);
        }

        public async Task<Person?> UpdateAsync(int id, UpdatePersonRequestDto personDto)
        {
            var existingPerson = await _context.Person.FirstOrDefaultAsync(p => p.Id == id);

            if (existingPerson == null)
                return null;

            existingPerson.Cpf = personDto.Cpf;
            existingPerson.Name = personDto.Name;
            existingPerson.Email = personDto.Email;
            existingPerson.PhoneNumber = personDto.PhoneNumber;
            existingPerson.BirthDate = personDto.BirthDate;

            await _context.SaveChangesAsync();

            return existingPerson;
        }
    }
}