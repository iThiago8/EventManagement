using System.ComponentModel.DataAnnotations;

namespace apis.Dtos.Symposium
{
    public class UpdateSymposiumRequestDto
    {
        [Required(ErrorMessage = "Symposium name is required.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Symposium name must be between 5 and 250 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Location address ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Location address ID must be a positive number.")]
        public int LocationAddressId { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }
    }
}