namespace apis.Models
{
    public class ArticleReview
    {
        public ArticleReview(Article article, ScientificCommittee scientificCommittee, float grade, string review, DateTime reviewDate)
        {
            Article = article;
            ArticleId = article.Id;
            ScientificCommittee = scientificCommittee;
            ScientificCommitteeId = scientificCommittee.Id;
            Grade = grade;
            Review = review;
            ReviewDate = reviewDate;
        }

        public ArticleReview()
        {
            Article = null!;
            ScientificCommittee = null!;
            Review = string.Empty;
            ReviewDate = default;
        }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int ScientificCommitteeId { get; set; }
        public ScientificCommittee ScientificCommittee { get; set; }
        public float Grade { get;set;}
        public string Review { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
