using apis.Data;
using apis.Dtos.Article;
using apis.Dtos.ArticleReview;
using apis.Interfaces;
using apis.Models;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class ArticleReviewRepository(ApplicationDbContext context) : IArticleReviewRepository
    {
        public async Task<List<ArticleReviewDto>?> GetArticleReviewsAsync(int articleId)
        {
            var article = await context.Article.FindAsync(articleId);

            if (article == null)
                return null;

            var articleReviews = await context.ArticleReview
                .Where(ar => ar.ArticleId == articleId)
                .Include(ar => ar.ScientificCommittee)
                .Select(ar => new ArticleReviewDto
                {
                    ArticleId = ar.ArticleId,
                    Article = ar.Article,
                    ScientificCommitteeId = ar.ScientificCommitteeId,
                    ScientificCommittee = ar.ScientificCommittee,
                    Grade = ar.Grade,
                    Review = ar.Review,
                    ReviewDate = ar.ReviewDate
                })
                .ToListAsync();

            return articleReviews;
        }
    }
}
