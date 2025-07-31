using apis.Dtos.Person;
using apis.Helpers.QueryObjects;
using apis.Models;

namespace apis.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(PersonQueryObject query);
        Task<Person?> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person personModel);
        Task<Person?> UpdateAsync(int id, UpdatePersonRequestDto personDto);
        Task<Person?> DeleteAsync(int id);
    }
}
