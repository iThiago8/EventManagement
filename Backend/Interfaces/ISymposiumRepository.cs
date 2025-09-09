using Core.Models;

namespace Backend.Interfaces
{
    public interface ISymposiumRepository
    {
        Task<List<Symposium>> GetAllAsync();
        Task<Symposium?> GetByIdAsync(int id);
        Task<Symposium> CreateAsync(Symposium symposiumModel);
        Task<Symposium?> UpdateAsync(int id, Symposium updateModel);
        Task<Symposium?> DeleteAsync(int id);
    }
}
