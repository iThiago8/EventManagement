using Backend.Data;
using Backend.Dtos.ScientificCommittee;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Backend.Repositories
{
    public class ScientificCommitteeRepository(ApplicationDbContext context) : IScientificCommitteeRepository
    {
        public async Task<bool> ScientificCommitteeExists(int id)
        {
            return await context.ScientificCommittee.AnyAsync(sc => sc.Id == id);
        }

        public async Task<ScientificCommittee> CreateAsync(ScientificCommittee scientificCommitteeModel)
        {
            Subject? subjectModel = await context.Subject.FindAsync(scientificCommitteeModel.SubjectId);

            scientificCommitteeModel.Subject = subjectModel!;

            await context.ScientificCommittee.AddAsync(scientificCommitteeModel);
            await context.SaveChangesAsync();

            return scientificCommitteeModel;
        }

        public async Task<ScientificCommittee?> DeleteAsync(int id)
        {
            ScientificCommittee? scientificCommitteeModel = await context.ScientificCommittee.FindAsync(id);

            if (scientificCommitteeModel == null)
                return null;

            context.ScientificCommittee.Remove(scientificCommitteeModel);
            await context.SaveChangesAsync();

            return scientificCommitteeModel;
        }

        public async Task<List<ScientificCommittee>> GetAllAsync()
        {
            return await context.ScientificCommittee.Include(sc => sc.Subject).ToListAsync();
        }

        public async Task<ScientificCommittee?> GetByIdAsync(int id)
        {
            return await context.ScientificCommittee.Include(sc => sc.Subject).FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<ScientificCommittee?> UpdateAsync(int id, ScientificCommittee scientificCommitteeModel)
        {
            ScientificCommittee? existingScientificCommittee = await context.ScientificCommittee.FindAsync(id);

            if (existingScientificCommittee == null)
                return null;

            Subject? newSubject = await context.Subject.FindAsync(scientificCommitteeModel.SubjectId);

            existingScientificCommittee.Name = scientificCommitteeModel.Name;
            existingScientificCommittee.SubjectId = scientificCommitteeModel.SubjectId;
            existingScientificCommittee.Subject = newSubject!;

            await context.SaveChangesAsync();

            return existingScientificCommittee;
        }
    }
}
