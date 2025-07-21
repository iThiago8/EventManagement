using apis.Dtos.Person;
using apis.Models;

namespace apis.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person personModel);
        Task<Person?> UpdateAsync(int id, UpdatePersonRequestDto personDto);
        Task<Person?> DeleteAsync(int id);
    }
}
