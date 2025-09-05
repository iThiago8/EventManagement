using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Workshop
{
    public class CreateWorkshopRequestDto
    {
        [Required(ErrorMessage = "Workshop name is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Workshop name must be between 3 and 150 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Workshop hours are required.")]
        [Range(1, 500, ErrorMessage = "Workshop hours must be between 1 and 500.")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Subject ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be a positive number.")]
        public int SubjectId { get; set; }
    }
}