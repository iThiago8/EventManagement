using apis.Data;
using apis.Dtos.ScientificCommittee;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace apis.Repositories
{
    public class ScientificCommitteeRepository(ApplicationDbContext context) : IScientificCommitteeRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ScientificCommittee> CreateAsync(ScientificCommittee scientificCommitteeModel)
        {
            Subject? subjectModel = await _context.Subject.FindAsync(scientificCommitteeModel.SubjectId);

            scientificCommitteeModel.Subject = subjectModel!;

            await _context.ScientificCommittee.AddAsync(scientificCommitteeModel);
            await _context.SaveChangesAsync();

            return scientificCommitteeModel;
        }

        public async Task<ScientificCommittee?> DeleteAsync(int id)
        {
            ScientificCommittee? scientificCommitteeModel = await _context.ScientificCommittee.FindAsync(id);

            if (scientificCommitteeModel == null)
                return null;

            _context.ScientificCommittee.Remove(scientificCommitteeModel);
            await _context.SaveChangesAsync();

            return scientificCommitteeModel;
        }

        public async Task<List<ScientificCommittee>> GetAllAsync()
        {
            return await _context.ScientificCommittee.Include(sc => sc.Subject).ToListAsync();
        }

        public async Task<ScientificCommittee?> GetByIdAsync(int id)
        {
            return await _context.ScientificCommittee.Include(sc => sc.Subject).FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<ScientificCommittee?> UpdateAsync(int id, UpdateScientificCommitteeResquestDto scientificCommitteeDto)
        {
            ScientificCommittee? existingScientificCommittee = await _context.ScientificCommittee.FindAsync(id);

            if (existingScientificCommittee == null)
                return null;

            Subject? newSubject = await _context.Subject.FindAsync(scientificCommitteeDto.SubjectId);

            existingScientificCommittee.Name = scientificCommitteeDto.Name;
            existingScientificCommittee.SubjectId = scientificCommitteeDto.SubjectId;
            existingScientificCommittee.Subject = newSubject!;

            await _context.SaveChangesAsync();

            return existingScientificCommittee;
        }
    }
}
