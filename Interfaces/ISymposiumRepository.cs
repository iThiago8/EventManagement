using apis.Dtos.Symposium;
using apis.Models;

namespace apis.Interfaces
{
    public interface ISymposiumRepository
    {
        Task<List<Symposium>> GetAllAsync();
        Task<Symposium?> GetByIdAsync(int id);
        Task<Symposium?> CreateAsync(Symposium symposiumModel);
        Task<Symposium?> UpdateAsync(int id, UpdateSymposiumRequestDto updateDto);
        Task<Symposium?> DeleteAsync(int id);
    }
}
