using apis.Models;

namespace apis.Mappers
{
    public class CreatePersonRequestDto
    {
        public string Cpf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime Data_Nascimento { get; set; }
    }
}
