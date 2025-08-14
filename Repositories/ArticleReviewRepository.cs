using apis.Data;
using apis.Dtos.ArticleReview;
using apis.Interfaces;
using apis.Mappers;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class ArticleReviewRepository(ApplicationDbContext context) : IArticleReviewRepository
    {
        public async Task<List<ArticleReviewDto>?> GetArticleReviewsAsync(int articleId)
        {
            /*var article = await context.Article
                .FindAsync(articleId);*/

            var article = await context.Article
                .Where(a => a.Id == articleId)
                .Include(a => a.ArticleReview)
                .ThenInclude(sc => sc.ScientificCommittee)
                .ThenInclude(sc => sc.Subject)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync();

            if (article == null)
                return null;

            /*var articleReviews = await context.ArticleReview
                .Where(ar => ar.ArticleId == articleId)
                .Include(a => a.Article)
                .ThenInclude(a => a.Subject)
                .Include(ar => ar.ScientificCommittee)
                .ThenInclude(sc => sc.Subject)
                .Select(ar => new ArticleReviewDto
                {
                    ArticleId = ar.ArticleId,
                    Article = ar.Article.ToArticleDto(),
                    ScientificCommitteeId = ar.ScientificCommitteeId,
                    ScientificCommittee = ar.ScientificCommittee.ToScientificCommitteeDto(),
                    Grade = ar.Grade,
                    Review = ar.Review,
                    ReviewDate = ar.ReviewDate
                })
                .ToListAsync();*/

            var articleReviews = article.ArticleReview
                .Select(review => new ArticleReviewDto
                {
                    ArticleId = article.Id,
                    Article = article.ToArticleDto(),
                    ScientificCommitteeId = review.ScientificCommitteeId,
                    ScientificCommittee = review.ScientificCommittee.ToScientificCommitteeDto(),
                    Grade = review.Grade,
                    Review = review.Review,
                    ReviewDate = review.ReviewDate
                    
                })
                .ToList();

            return articleReviews;
        }
    }
}
