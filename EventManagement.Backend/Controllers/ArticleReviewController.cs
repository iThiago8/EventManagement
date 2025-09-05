using Backend.Dtos.ArticleReview;
using Backend.Exceptions;
using Backend.Helpers.QueryObjects;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/articlereview")]
    [ApiController]
    public class ArticleReviewController(IArticleReviewRepository articleReviewRepo, IArticleRepository articleRepo, IScientificCommitteeRepository scientificCommitteeRepo) : ControllerBase
    {
        [HttpGet("article/reviews")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ArticleReviewQueryObject query)
        {
            var articlesReviews = await articleReviewRepo.GetAllArticleReviewsAsync(query);

            if (articlesReviews == null)
                return NotFound();
            else
                return Ok(articlesReviews.Select(ar => ar.ToArticleReviewDto()));
        }

        [HttpGet("{articleId}/reviews")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int articleId)
        {
            var articleReviews = await articleReviewRepo.GetArticleReviewsByIdAsync(articleId);

            if (articleReviews == null)
                return NotFound();

            return Ok(articleReviews.Select(ar => ar.ToArticleReviewDto()));
        }

        [HttpGet("{articleId}/reviews/{reviewId}")]
        [Authorize]
        public async Task<IActionResult> GetArticleReviewByCompositeId([FromRoute] int articleId, int reviewId)
        {
            var articleReview = await articleReviewRepo.GetArticleReviewByCompositeId(articleId, reviewId);

            if (articleReview == null)
                return NotFound();
            else
                return Ok(articleReview.ToArticleReviewDto());
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

            try
            {
                var articleReview = await articleReviewRepo.CreateAsync(articleReviewModel);

                return CreatedAtAction(nameof(GetArticleReviewByCompositeId), new { articleId = articleReview.ArticleId, reviewId = articleReview.ScientificCommitteeId }, articleReview.ToArticleReviewDto());
            }
            catch (DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{articleId}/reviews/{scientificCommitteeId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int articleId, int scientificCommitteeId, [FromBody] UpdateArticleReviewRequestDto articleReviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await articleRepo.ArticleExists(articleId))
                return BadRequest("Article does not exist.");

            if (!await scientificCommitteeRepo.ScientificCommitteeExists(scientificCommitteeId))
                return BadRequest("Scientific committee does not exist");

            var articleReview = await articleReviewRepo.UpdateAsync(articleId, scientificCommitteeId, articleReviewDto.ToArticleReviewFromUpdateDto());

            if (articleReview == null)
                return NotFound();
            else
                return Ok(articleReview.ToArticleReviewDto());
        }

        [HttpDelete("{articleId}/reviews/{scientificCommitteeId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int articleId, int scientificCommitteeId)
        {
            if (!await articleRepo.ArticleExists(articleId))
                return BadRequest("Article does not exist.");

            if (!await scientificCommitteeRepo.ScientificCommitteeExists(scientificCommitteeId))
                return BadRequest("Scientific committee does not exist");

            var articleReview = await articleReviewRepo.DeleteAsync(articleId, scientificCommitteeId);

            if (articleReview == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
