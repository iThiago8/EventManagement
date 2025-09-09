using Backend.Helpers.QueryObjects;
using Core.Models;

namespace Backend.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(PersonQueryObject query);
        Task<Person?> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person personModel);
        Task<Person?> UpdateAsync(int id, Person personModel);
        Task<Person?> DeleteAsync(int id);
    }
}
