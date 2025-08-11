using apis.Data;
using apis.Dtos.Article;
using apis.Dtos.ArticleReview;
using apis.Interfaces;
using apis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace apis.Repositories
{
    public class ArticleReviewRepository(ApplicationDbContext context) : IArticleReviewRepository
    {
        public async Task<List<ReviewDto>?> GetArticleReviewsAsync(int articleId)
        {
            var articleReviews = new List<ReviewDto>();
            
            var article = await context.Article.FindAsync(articleId);

            if (article == null)
                return null;



            return articleReviews;
        }
    }
}
