using apis.Helpers.QueryObjects;
using apis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/articlereview")]
    [ApiController]
    public class ArticleReviewController(IArticleReviewRepository articleReviewRepo) : ControllerBase
    {
        [HttpGet("article/reviews")]
        [Authorize]
        public async Task<IActionResult> GetArticleReviews([FromQuery] ArticleReviewQueryObject query)
        {
            var articlesReviews = await articleReviewRepo.GetAllArticleReviewsAsync(query);

            if (articlesReviews == null)
                return NotFound();
            else
                return Ok(articlesReviews);
        }

        [HttpGet("{articleId}/reviews")]
        [Authorize]
        public async Task<IActionResult> GetArticleReviewsById([FromRoute] int articleId)
        {
            var articleReviews = await articleReviewRepo.GetArticleReviewsByIdAsync(articleId);

            if (articleReviews == null)
                return NotFound();

            return Ok(articleReviews);
        }
    }
}
