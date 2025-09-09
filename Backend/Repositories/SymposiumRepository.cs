using Backend.Data;
using Core.Dtos.Symposium;
using Backend.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class SymposiumRepository(ApplicationDbContext context) : ISymposiumRepository
    {
        public async Task<Symposium> CreateAsync(Symposium symposiumModel)
        {
            await context.Symposium.AddAsync(symposiumModel);
            await context.SaveChangesAsync();

            return (await GetByIdAsync(symposiumModel.Id))!;
        }

        public async Task<Symposium?> DeleteAsync(int id)
        {
            Symposium? symposiumModel = await context.Symposium.FindAsync(id);

            if (symposiumModel == null)
                return null;

            context.Remove(symposiumModel);
            await context.SaveChangesAsync();

            return symposiumModel;
        }

        public async Task<List<Symposium>> GetAllAsync()
        {
            return await context.Symposium.Include(s => s.LocationAddress).ToListAsync();
        }

        public async Task<Symposium?> GetByIdAsync(int id)
        {
            return await context.Symposium.Include(s => s.LocationAddress).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Symposium?> UpdateAsync(int id, Symposium symposiumModel)
        {
            Symposium? existingSymposium = await context.Symposium.Include(s => s.LocationAddress).FirstOrDefaultAsync(s => s.Id == id);

            if (existingSymposium == null)
                return null;

            existingSymposium.Name = symposiumModel.Name;
            existingSymposium.StartDate = symposiumModel.StartDate;
            existingSymposium.EndDate = symposiumModel.EndDate;
            existingSymposium.LocationAddressId = symposiumModel.LocationAddressId;
            existingSymposium.Description = symposiumModel.Description;

            await context.SaveChangesAsync();

            return (await GetByIdAsync(symposiumModel.Id))!;
        }
    }
}
