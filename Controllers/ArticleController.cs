using apis.Dtos.Article;
using apis.Interfaces;
using apis.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/article")]
    [ApiController]    
    public class ArticleController(IArticleRepository articleRepo, ISubjectRepository subjectRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await articleRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await articleRepo.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateArticleRequestDto articleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await subjectRepo.SubjectExists(articleDto.SubjectId))
                return BadRequest("Subject does not exist.");

            var articleModel = await articleRepo.CreateAsync(articleDto.ToArticleFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = articleModel.Id }, articleModel);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArticleRequestDto articleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await subjectRepo.SubjectExists(articleDto.SubjectId))
                return BadRequest("Subject does not exist.");

            var articleModel = await articleRepo.UpdateAsync(id, articleDto.ToArticleFromUpdateDto());

            if (articleModel == null)
                return NotFound();
            else
                return Ok(articleModel);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var articleModel = await articleRepo.DeleteAsync(id);

            if (articleModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
