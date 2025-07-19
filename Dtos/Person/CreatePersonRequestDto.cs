using apis.Models;

namespace apis.Dtos.Person
{
    public class CreatePersonRequestDto
    {
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
