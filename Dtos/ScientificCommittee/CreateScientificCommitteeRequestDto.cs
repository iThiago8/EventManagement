using System.ComponentModel.DataAnnotations;

namespace apis.Dtos.ScientificCommittee
{
    public class CreateScientificCommitteeRequestDto
    {
        [Required(ErrorMessage = "Scientific Committee name is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Committee name must be between 5 and 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be a positive number.")]
        public int SubjectId { get; set; }
    }
}