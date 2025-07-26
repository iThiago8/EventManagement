using apis.Dtos.Symposium;
using apis.Models;

namespace apis.Mappers
{
    public static class SymposiumMappers
    {
        public static SymposiumDto ToSymposiumDto(this Symposium symposiumModel)
        {
            return new SymposiumDto
            {
                Id = symposiumModel.Id,
                Name = symposiumModel.Name,
                Description = symposiumModel.Description,
                StartDate = symposiumModel.StartDate,
                EndDate = symposiumModel.EndDate,
                LocationAddressId = symposiumModel.LocationAddressId,
                LocationAddress = symposiumModel.LocationAddress
            };
        }

        public static Symposium ToSymposiumFromCreateDto(this CreateSymposiumRequestDto symposiumDto)
        {
            return new Symposium
            {
                Name = symposiumDto.Name,
                Description = symposiumDto.Description,
                StartDate = symposiumDto.StartDate,
                EndDate = symposiumDto.EndDate,
                LocationAddressId = symposiumDto.LocationAddressId
            };
        }
    }
}