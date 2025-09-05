using Backend.Dtos.ArticleReview;
using Backend.Helpers.QueryObjects;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IArticleReviewRepository
    {
        Task<List<ArticleReview>?> GetAllArticleReviewsAsync(ArticleReviewQueryObject query);
        Task<List<ArticleReview>?> GetArticleReviewsByIdAsync(int articleId);
        Task<ArticleReview?> GetArticleReviewByCompositeId(int articleId, int scientificCommitteeId);
        Task<ArticleReview> CreateAsync(ArticleReview articleReviewModel);
        Task<ArticleReview?> UpdateAsync(int articleId, int scientificCommitteeId, ArticleReview articleReviewModel);
        Task<ArticleReview?> DeleteAsync(int articleId, int scientificCommitteeId);
        Task<bool> ArticleReviewExists(int articleId, int scientificCommitteeId);
    }
}
