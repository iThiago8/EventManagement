using apis.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    public class ArticleReviewController(IArticleRepository articleRepo, IScientificCommitteeRepository scientificCommitteeRepo) : ControllerBase
    {
        [Route("api/articlereview")]
        [ApiController]

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetArticleReviews()
        {
            var article = 
        }
    }
}
