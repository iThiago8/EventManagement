using apis.Dtos.Address;
using apis.Models;

namespace apis.Mappers
{
    public static class AddressMappers
    {
        public static AddressDto ToAddressDto(this Address addressModel)
        {
            return new AddressDto
            {
                Id = addressModel.Id,
                Street = addressModel.Street,
                Number = addressModel.Number,
                Complement = addressModel.Complement,
                Neighborhood = addressModel.Neighborhood,
                City = addressModel.City,
                State = addressModel.State,
                Country = addressModel.Country,
                PostalCode = addressModel.PostalCode
            };
        }

        public static Address ToAddressFromCreateDto(this CreateAddressRequestDto addressDto)
        {
            return new Address
            {
                Street = addressDto.Street,
                Number = addressDto.Number,
                Complement = addressDto.Complement,
                Neighborhood = addressDto.Neighborhood,
                City = addressDto.City,
                State = addressDto.State,
                Country = addressDto.Country,
                PostalCode = addressDto.PostalCode
            };
        }
    }
}
