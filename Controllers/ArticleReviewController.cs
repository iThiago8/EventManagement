using apis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/articlereview")]
    [ApiController]
    public class ArticleReviewController(IArticleReviewRepository articleReviewRepo) : ControllerBase
    {
        [HttpGet("{articleId}/reviews")]
        [Authorize]
        public async Task<IActionResult> GetArticleReviews([FromRoute] int articleId)
        {
            var articleReviews = await articleReviewRepo.GetArticleReviewsAsync(articleId);

            if (articleReviews == null)
                return NotFound();

            return Ok(articleReviews);
        }
    }
}
