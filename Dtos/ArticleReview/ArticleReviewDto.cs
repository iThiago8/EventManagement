namespace apis.Dtos.ArticleReview
{
    public class ArticleReviewDto
    {
        public int ArticleId { get; set; }
        public Models.Article Article { get; set; } = new();
        public int ScientificCommitteeId { get; set; }
        public Models.ScientificCommittee ScientificCommittee { get; set; } = new();
        public float Grade { get; set; }
        public string Review { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
    }
}
