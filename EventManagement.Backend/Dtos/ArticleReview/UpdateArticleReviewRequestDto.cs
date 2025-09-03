using System.ComponentModel.DataAnnotations;

namespace apis.Dtos.ArticleReview
{
    public class UpdateArticleReviewRequestDto
    {
        [Required]
        public float Grade { get; set; }

        [Required]
        public string Review { get; set; } = string.Empty;

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}
