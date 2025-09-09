using Core.Models;

namespace Backend.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<Address> CreateAsync(Address addressModel);
        Task<Address?> UpdateAsync(int id, Address addressModel);
        Task<Address?> DeleteAsync(int id);
        Task<bool> AddressExists(int id);
    }
}
