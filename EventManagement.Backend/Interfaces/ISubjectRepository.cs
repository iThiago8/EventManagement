using Backend.Dtos.Subject;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task<Subject> CreateAsync(Subject subjectModel);
        Task<Subject?> UpdateAsync(int id, Subject subjectModel);
        Task<Subject?> DeleteAsync(int id);
        Task<bool> SubjectExists(int id);
    }
}
