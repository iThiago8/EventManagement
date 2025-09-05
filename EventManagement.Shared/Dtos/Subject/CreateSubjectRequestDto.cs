using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Subject
{
    public class CreateSubjectRequestDto
    {
        [Required(ErrorMessage = "Subject name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Subject name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}