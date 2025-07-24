using apis.Dtos.Workshop;
using apis.Models;

namespace apis.Interfaces
{
    public interface IWorkshopRepository
    {
        Task<List<Workshop>> GetAllAsync();
        Task<Workshop?> GetByIdAsync(int id);
        Task<Workshop?> CreateAsync(Workshop workshopModel);
        Task<Workshop?> UpdateAsync(int id, UpdateWorkshopRequestDto workshopDto);
        Task<Workshop?> DeleteAsync(int id);
    }
}
