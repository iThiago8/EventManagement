using apis.Dtos.ArticleReview;

namespace apis.Interfaces
{
    public interface IArticleReviewRepository
    {
        Task<List<ReviewDto>?> GetArticleReviewsAsync(int articleId); 
    }
}
