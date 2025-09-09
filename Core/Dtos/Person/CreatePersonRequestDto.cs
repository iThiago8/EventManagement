using System.ComponentModel.DataAnnotations;
using Core.Models; 

namespace Core.Dtos.Person
{
    public class CreatePersonRequestDto
    {
        [Required(ErrorMessage = "CPF is required.")]
        [StringLength(11, ErrorMessage = "The CPF length must be 11 characters")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(150, ErrorMessage = "Email address cannot exceed 150 characters.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth date is required.")]
        public DateTime BirthDate { get; set; }
    }
}