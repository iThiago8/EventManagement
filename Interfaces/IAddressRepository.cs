using apis.Dtos.Address;
using apis.Models;

namespace apis.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<Address> CreateAsync(Address addressModel);
        Task<Address?> UpdateAsync(UpdateAddressRequestDto addressDto, int id);
        Task<Address?> DeleteAsync(int id);
    }
}
