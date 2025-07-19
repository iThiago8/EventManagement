using apis.Data;
using apis.Dtos.Person;
using apis.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/article")]
    [ApiController]    
    public class ArticleController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        /*[HttpGet]
        public IActionResult GetAll()
        {
            var article = _context.Article.ToList().Select(a => a.ToArticleDto());

            return Ok(article);
        }*/
    }
}
