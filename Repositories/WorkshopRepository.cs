using apis.Data;
using apis.Dtos.Workshop;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class WorkshopRepository(ApplicationDbContext context) : IWorkshopRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Workshop?> CreateAsync(Workshop workshopModel)
        {
            var subjectModel = await _context.Subject.FindAsync(workshopModel.SubjectId);

            workshopModel.Subject = subjectModel!;

            await _context.Workshop.AddAsync(workshopModel);
            await _context.SaveChangesAsync();

            return workshopModel;
        }

        public async Task<Workshop?> DeleteAsync(int id)
        {
            var workshopModel = await GetByIdAsync(id);

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

        public async Task<Workshop?> UpdateAsync(int id, UpdateWorkshopRequestDto workshopDto)
        {
            var existingWorkshop = await _context.Workshop.Include(w => w.Subject).FirstOrDefaultAsync(w => w.Id == id);

            if (existingWorkshop == null)
                return null;

            var newSubject = await _context.Subject.FindAsync(existingWorkshop.SubjectId);

            existingWorkshop.Name = workshopDto.Name;
            existingWorkshop.Hours = workshopDto.Hours;
            existingWorkshop.SubjectId = workshopDto.SubjectId;
            existingWorkshop.Subject = newSubject!;

            await _context.SaveChangesAsync();

            return existingWorkshop;
        }   
    }
}
