using apis.Dtos.Subject;
using apis.Models;

namespace apis.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task<Subject> CreateAsync(Subject subjectModel);
        Task<Subject?> UpdateAsync(int id, UpdateSubjectRequestDto subjectDto);
        Task<Subject?> DeleteAsync(int id);
        Task<bool> SubjectExists(int id);
    }
}
