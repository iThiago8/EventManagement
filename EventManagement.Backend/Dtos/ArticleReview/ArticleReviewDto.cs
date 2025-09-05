using Backend.Dtos;

namespace Backend.Dtos.ArticleReview
{
    public class ArticleReviewDto
    {
        public int ArticleId { get; set; }
        public Article.ArticleDto Article { get; set; } = new();
        public int ScientificCommitteeId { get; set; }
        public ScientificCommittee.ScientificCommitteeDto ScientificCommittee { get; set; } = new();
        public float Grade { get; set; }
        public string Review { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
    }
}
