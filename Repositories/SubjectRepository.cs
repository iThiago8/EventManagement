using apis.Data;
using apis.Dtos.Subject;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class SubjectRepository(ApplicationDbContext context) : ISubjectRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Subject> CreateAsync(Subject subjectModel)
        {
            await _context.Subject.AddAsync(subjectModel);
            await _context.SaveChangesAsync();

            return subjectModel;

        }

        public async Task<Subject?> DeleteAsync(int id)
        {
            var subjectModel = await _context.Subject.FirstOrDefaultAsync(s => s.Id == id);

            if (subjectModel == null)
                return null;

            _context.Subject.Remove(subjectModel);
            await _context.SaveChangesAsync();

            return subjectModel;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subject.ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subject.FindAsync(id);
        }

        public async Task<Subject?> UpdateAsync(int id, UpdateSubjectRequestDto subjectDto)
        {
            var existingSubject = await _context.Subject.FirstOrDefaultAsync(s => s.Id == id);

            if (existingSubject == null)
                return null;

            existingSubject.Name = subjectDto.Name;
            await _context.SaveChangesAsync();

            return existingSubject;
        }
    }
}
