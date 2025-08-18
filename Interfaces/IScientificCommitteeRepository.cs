using apis.Dtos.ScientificCommittee;
using apis.Models;

namespace apis.Interfaces
{
    public interface IScientificCommitteeRepository
    {
        Task<List<ScientificCommittee>> GetAllAsync();
        Task<ScientificCommittee?> GetByIdAsync(int id);
        Task<ScientificCommittee> CreateAsync(ScientificCommittee scientificCommitteeModel);
        Task<ScientificCommittee?> UpdateAsync(int id, ScientificCommittee scientificCommitteeModel);
        Task<ScientificCommittee?> DeleteAsync(int id);
    }
}
