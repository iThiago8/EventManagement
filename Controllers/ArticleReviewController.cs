using apis.Dtos.ArticleReview;
using apis.Helpers.QueryObjects;
using apis.Interfaces;
using apis.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/articlereview")]
    [ApiController]
    public class ArticleReviewController(IArticleReviewRepository articleReviewRepo, IArticleRepository articleRepo, IScientificCommitteeRepository scientificCommitteeRepo) : ControllerBase
    {
        [HttpGet("article/reviews")]
        [Authorize]
        public async Task<IActionResult> GetArticleReviews([FromQuery] ArticleReviewQueryObject query)
        {
            var articlesReviews = await articleReviewRepo.GetAllArticleReviewsAsync(query);

            if (articlesReviews == null)
                return NotFound();
            else
                return Ok(articlesReviews.Select(ar => ar.ToArticleReviewDto()));
        }

        [HttpGet("{articleId}/reviews")]
        [Authorize]
        public async Task<IActionResult> GetArticleReviewsById([FromRoute] int articleId)
        {
            var articleReviews = await articleReviewRepo.GetArticleReviewsByIdAsync(articleId);

            if (articleReviews == null)
                return NotFound();

            return Ok(articleReviews.Select(ar => ar.ToArticleReviewDto()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateArticleReviewRequestDto articleReviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await articleRepo.ArticleExists(articleReviewDto.ArticleId))
                return BadRequest("Article does not exist.");

            if (!await scientificCommitteeRepo.ScientificCommitteeExists(articleReviewDto.ScientificCommitteeId))
                return BadRequest("Scientific committee does not exist");

            var articleReviewModel = articleReviewDto.ToArticleReviewFromCreateDto();

            await articleReviewRepo.CreateAsync(articleReviewModel);

            // TODO: mudar isso para CreatedAtAction
            return Ok("Objeto criado");
        }
    }
}
