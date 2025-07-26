using apis.Dtos.Address;
using apis.Models;

namespace apis.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<Address> CreateAsync(Address addressModel);
        Task<Address?> UpdateAsync(int id, UpdateAddressRequestDto addressDto);
        Task<Address?> DeleteAsync(int id);
        Task<bool> AddressExists(int id);
    }
}
