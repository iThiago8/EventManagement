using apis.Data;
using apis.Dtos.ArticleReview;
using apis.Exceptions;
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
        public async Task<List<ArticleReview>?> GetAllArticleReviewsAsync(ArticleReviewQueryObject query)
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
                .Select(review => new ArticleReview
                    {
                        ArticleId = review.ArticleId,
                        Article = review.Article,
                        ScientificCommitteeId = review.ScientificCommitteeId,
                        ScientificCommittee = review.ScientificCommittee,
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

        public async Task<List<ArticleReview>?> GetArticleReviewsByIdAsync(int articleId)
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
                .Select(review => new ArticleReview
                {
                    ArticleId = article.Id,
                    Article = article,
                    ScientificCommitteeId = review.ScientificCommitteeId,
                    ScientificCommittee = review.ScientificCommittee,
                    Grade = review.Grade,
                    Review = review.Review,
                    ReviewDate = review.ReviewDate
                    
                })
                .ToList();

            return articleReviews;
        }

        public async Task<ArticleReview?> CreateAsync(ArticleReview articleReviewModel)
        {
            var articleReviewExists = await ArticleReviewExists(articleReviewModel.ArticleId, articleReviewModel.ScientificCommitteeId);

            if (articleReviewExists)
            {
                throw new DuplicateRecordException("This combination of article and scientific committee already exists.");
            }


            var article = await context.Article.FindAsync(articleReviewModel.ArticleId);

            var scientificCommittee = await context.ScientificCommittee.FindAsync(articleReviewModel.ScientificCommitteeId);

            articleReviewModel.Article = article!;

            articleReviewModel.ScientificCommittee = scientificCommittee!;

            await context.ArticleReview.AddAsync(articleReviewModel);
            await context.SaveChangesAsync();

            return articleReviewModel;
        }

        public async Task<ArticleReview?> UpdateAsync(int articleId, int scientificCommitteeId, ArticleReview articleReviewModel)
        {
            var articleReview = await context.ArticleReview
                .Include(ar => ar.Article)
                .ThenInclude(a => a.Subject)
                .Include(ar => ar.ScientificCommittee)
                .ThenInclude(sc => sc.Subject)
                .FirstOrDefaultAsync(ar => 
                    ar.ArticleId == articleId 
                    && ar.ScientificCommitteeId == scientificCommitteeId
                );

            if (articleReview == null)
                return null;

            articleReview.Grade = articleReviewModel.Grade;
            articleReview.Review = articleReviewModel.Review;
            articleReview.ReviewDate = articleReviewModel.ReviewDate;

            await context.SaveChangesAsync();
            
            return articleReview;
        }

        public async Task<ArticleReview?> DeleteAsync(int articleId, int scientificCommitteeId)
        {
            var articleReview = await context.ArticleReview.FirstOrDefaultAsync(ar => ar.ArticleId == articleId && ar.ScientificCommitteeId == scientificCommitteeId);

            if (articleReview == null)
                return null;

            context.ArticleReview.Remove(articleReview);
            await context.SaveChangesAsync();

            return articleReview;
        }

        public async Task<bool> ArticleReviewExists(int articleId, int scientificCommitteeId)
        {
            var existingArticleReview = await context.ArticleReview
                .AnyAsync(ar => 
                    ar.ArticleId == articleId
                    && ar.ScientificCommitteeId == scientificCommitteeId
                );

            return existingArticleReview;
        }
    }
}
