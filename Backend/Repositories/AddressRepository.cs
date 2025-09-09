using Backend.Data;
using Core.Dtos.Address;
using Backend.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class AddressRepository(ApplicationDbContext context) : IAddressRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<bool> AddressExists(int id)
        {
            return await _context.Address.AnyAsync(a => a.Id == id);
        }

        public async Task<Address> CreateAsync(Address addressModel)
        {
            await _context.Address.AddAsync(addressModel);
            await _context.SaveChangesAsync();

            return addressModel;
        }

        public async Task<Address?> DeleteAsync(int id)
        {
            Address? addressModel = await GetByIdAsync(id);

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

        public async Task<Address?> UpdateAsync(int id, Address addressModel)
        {
            Address? existingAddress = await GetByIdAsync(id);

            if (existingAddress == null)
                return null;

            existingAddress.Street = addressModel.Street;
            existingAddress.Number = addressModel.Number;
            existingAddress.Complement = addressModel.Complement;
            existingAddress.Neighborhood = addressModel.Neighborhood;
            existingAddress.City = addressModel.City;
            existingAddress.State = addressModel.State;
            existingAddress.Country = addressModel.Country;
            existingAddress.PostalCode = addressModel.PostalCode;

            await _context.SaveChangesAsync();

            return existingAddress;
        }
    }
}
