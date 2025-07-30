using System.ComponentModel.DataAnnotations;

namespace apis.Dtos.Article
{
    public class UpdateArticleRequestDto
    {
        [Required(ErrorMessage = "Article name is required.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Article name must be between 5 and 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publication date is required.")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "Abstract is required.")]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Abstract must be between 50 and 1000 characters.")]
        public string Abstract { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Subject ID must be a positive number.")]
        public int SubjectId { get; set; }
        
    }
}

