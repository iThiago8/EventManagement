using apis.Data;
using apis.Dtos.Address;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace apis.Repositories
{
    public class AddressRepository(ApplicationDbContext context) : IAddressRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Address> CreateAsync(Address addressModel)
        {
            await _context.Address.AddAsync(addressModel);
            await _context.SaveChangesAsync();

            return addressModel;
        }

        public async Task<Address?> DeleteAsync(int id)
        {
            var addressModel = await _context.Address.FirstOrDefaultAsync(a => a.Id == id);

            if (addressModel == null)
                return null;

            _context.Address.Remove(addressModel);
            await _context.SaveChangesAsync();

            return addressModel;
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _context.Address.ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Address.FindAsync(id);
        }

        public async Task<Address?> UpdateAsync(UpdateAddressRequestDto addressDto, int id)
        {
            var existingAddress = await _context.Address.FirstOrDefaultAsync(a => a.Id == id);

            if (existingAddress == null)
                return null;

            existingAddress.Street = addressDto.Street;
            existingAddress.Number = addressDto.Number;
            existingAddress.Complement = addressDto.Complement;
            existingAddress.Neighborhood = addressDto.Neighborhood;
            existingAddress.City = addressDto.City;
            existingAddress.State = addressDto.State;
            existingAddress.Country = addressDto.Country;
            existingAddress.PostalCode = addressDto.PostalCode;

            await _context.SaveChangesAsync();

            return existingAddress;
        }
    }
}
