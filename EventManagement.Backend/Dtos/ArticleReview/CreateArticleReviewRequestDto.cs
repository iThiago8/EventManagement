using System.ComponentModel.DataAnnotations;

namespace apis.Dtos.ArticleReview
{
    public class CreateArticleReviewRequestDto
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int ScientificCommitteeId { get; set; }

        [Required]
        public float Grade { get; set; }

        [Required]
        public string Review { get; set; } = string.Empty;

        [Required]
        public DateTime ReviewDate { get; set; }
    }
}
