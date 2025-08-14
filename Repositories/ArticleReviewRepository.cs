using apis.Data;
using apis.Dtos.ArticleReview;
using apis.Helpers.QueryObjects;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace apis.Repositories
{
    public class ArticleReviewRepository(ApplicationDbContext context) : IArticleReviewRepository
    {
        public async Task<List<ArticleReviewDto>?> GetAllArticleReviewsAsync(ArticleReviewQueryObject query)
        {
            var articles = await context.Article
                .Include(a => a.ArticleReview)
                .ThenInclude(sc => sc.ScientificCommittee)
                .ThenInclude(sc => sc.Subject)
                .Include(s => s.Subject)
                .ToListAsync();

            if (articles == null)
                return null;

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            var articleReviews = articles
                .SelectMany(article => article.ArticleReview)
                .Select(review => new ArticleReviewDto
                    {
                        ArticleId = review.ArticleId,
                        Article = review.Article.ToArticleDto(),
                        ScientificCommitteeId = review.ScientificCommitteeId,
                        ScientificCommittee = review.ScientificCommittee.ToScientificCommitteeDto(),
                        Grade = review.Grade,
                        Review = review.Review,
                        ReviewDate = review.ReviewDate
                    } 
                )
                .Skip(skipNumber)
                .Take(query.PageSize)
                .ToList();

            return articleReviews;

        }

        public async Task<List<ArticleReviewDto>?> GetArticleReviewsByIdAsync(int articleId)
        {
            var article = await context.Article
                .Where(a => a.Id == articleId)
                .Include(a => a.ArticleReview)
                .ThenInclude(sc => sc.ScientificCommittee)
                .ThenInclude(sc => sc.Subject)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync();

            if (article == null)
                return null;

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
