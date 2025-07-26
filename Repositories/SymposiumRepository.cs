using apis.Data;
using apis.Dtos.Symposium;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class SymposiumRepository(ApplicationDbContext context) : ISymposiumRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Symposium?> CreateAsync(Symposium symposiumModel)
        {
            var addressModel = await _context.Address.FindAsync(symposiumModel.LocationAddressId);

            symposiumModel.LocationAddress = addressModel!;

            await _context.Symposium.AddAsync(symposiumModel);
            await _context.SaveChangesAsync();

            return symposiumModel;
        }

        public async Task<Symposium?> DeleteAsync(int id)
        {
            var symposiumModel = await _context.Symposium.FindAsync(id);

            if (symposiumModel == null)
                return null;

            _context.Remove(symposiumModel);
            await _context.SaveChangesAsync();

            return symposiumModel;
        }

        public async Task<List<Symposium>> GetAllAsync()
        {
            return await _context.Symposium.Include(s => s.LocationAddress).ToListAsync();
        }

        public async Task<Symposium?> GetByIdAsync(int id)
        {
            return await _context.Symposium.Include(s => s.LocationAddress).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Symposium?> UpdateAsync(int id, UpdateSymposiumRequestDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
