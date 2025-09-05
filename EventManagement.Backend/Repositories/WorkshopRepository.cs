using Backend.Data;
using Backend.Dtos.Workshop;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class WorkshopRepository(ApplicationDbContext context) : IWorkshopRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Workshop> CreateAsync(Workshop workshopModel)
        {
            Subject? subjectModel = await _context.Subject.FindAsync(workshopModel.SubjectId);

            workshopModel.Subject = subjectModel!;

            await _context.Workshop.AddAsync(workshopModel);
            await _context.SaveChangesAsync();

            return workshopModel;
        }

        public async Task<Workshop?> DeleteAsync(int id)
        {
            Workshop? workshopModel = await GetByIdAsync(id);

            if (workshopModel == null)
                return null;

            _context.Workshop.Remove(workshopModel);
            await _context.SaveChangesAsync();

            return (workshopModel);
        }

        public async Task<List<Workshop>> GetAllAsync()
        {
            return await _context.Workshop.Include(w => w.Subject).ToListAsync();
        }

        public async Task<Workshop?> GetByIdAsync(int id)
        {
            return await _context.Workshop.Include(w => w.Subject).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Workshop?> UpdateAsync(int id, Workshop workshopModel)
        {
            Workshop? existingWorkshop = await _context.Workshop.Include(w => w.Subject).FirstOrDefaultAsync(w => w.Id == id);

            if (existingWorkshop == null)
                return null;

            Subject? newSubject = await _context.Subject.FindAsync(existingWorkshop.SubjectId);

            existingWorkshop.Name = workshopModel.Name;
            existingWorkshop.Hours = workshopModel.Hours;
            existingWorkshop.SubjectId = workshopModel.SubjectId;
            existingWorkshop.Subject = newSubject!;

            await _context.SaveChangesAsync();

            return existingWorkshop;
        }   
    }
}
