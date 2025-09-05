using Backend.Dtos.Workshop;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IWorkshopRepository
    {
        Task<List<Workshop>> GetAllAsync();
        Task<Workshop?> GetByIdAsync(int id);
        Task<Workshop> CreateAsync(Workshop workshopModel);
        Task<Workshop?> UpdateAsync(int id, Workshop workshopModel);
        Task<Workshop?> DeleteAsync(int id);
    }
}
