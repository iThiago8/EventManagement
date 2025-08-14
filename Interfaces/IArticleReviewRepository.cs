using apis.Dtos.ArticleReview;
using apis.Helpers.QueryObjects;

namespace apis.Interfaces
{
    public interface IArticleReviewRepository
    {
        Task<List<ArticleReviewDto>?> GetAllArticleReviewsAsync(ArticleReviewQueryObject query);
        Task<List<ArticleReviewDto>?> GetArticleReviewsByIdAsync(int articleId); 
    }
}
