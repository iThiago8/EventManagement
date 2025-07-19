using apis.Dtos.Person;
using apis.Models;

namespace apis.Mappers
{
    public static class PersonMappers
    {
        public static PersonDto ToPersonDto(this Person personModel)
        {
            return new PersonDto
            {
                Id = personModel.Id,
                Cpf = personModel.Cpf,
                Name = personModel.Name,
                Email = personModel.Email,
                PhoneNumber = personModel.PhoneNumber,
                BirthDate = personModel.BirthDate
            };
        }
        public static Person ToPersonFromCreateDto(this CreatePersonRequestDto personDto)
        {
            return new Person
            {
                Cpf = personDto.Cpf,
                Name = personDto.Name,
                Email = personDto.Email,
                PhoneNumber = personDto.PhoneNumber,
                BirthDate = personDto.BirthDate
            };
        }
    }
}
