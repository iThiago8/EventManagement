using apis.Dtos.ArticleReview;
using apis.Helpers.QueryObjects;
using apis.Models;

namespace apis.Interfaces
{
    public interface IArticleReviewRepository
    {
        Task<List<ArticleReview>?> GetAllArticleReviewsAsync(ArticleReviewQueryObject query);
        Task<List<ArticleReview>?> GetArticleReviewsByIdAsync(int articleId);
        Task<ArticleReview> CreateAsync(ArticleReview articleReviewModel);
        Task<ArticleReview?> UpdateAsync(int articleId, int scientificCommitteeId, ArticleReview articleReviewModel);
        Task<ArticleReview?> DeleteAsync(int articleId, int scientificCommitteeId);
    }
}
