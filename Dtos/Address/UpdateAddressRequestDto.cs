using System.ComponentModel.DataAnnotations;

namespace apis.Dtos.Address
{
    public class UpdateAddressRequestDto
    {
        [Required(ErrorMessage = "Street is required.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Street must be between 3 and 200 characters.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Number is required.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Number must be between 1 and 20 characters.")]
        public string Number { get; set; } = string.Empty;

        public string? Complement { get; set; }

        [Required(ErrorMessage = "Neighborhood is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Neighborhood must be between 3 and 100 characters.")]
        public string Neighborhood { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "State must be between 2 and 100 characters.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Country must be between 3 and 100 characters.")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Postal Code must be between 3 and 15 characters.")]
        public string PostalCode { get; set; } = string.Empty;
    }
}
