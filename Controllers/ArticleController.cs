using apis.Data;
using apis.Dtos.Article;
using apis.Dtos.Person;
using apis.Dtos.Workshop;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apis.Controllers
{
    [Route("api/article")]
    [ApiController]    
    public class ArticleController(IArticleRepository articleRepo, ISubjectRepository subjectRepo) : ControllerBase
    {
        private readonly IArticleRepository _articleRepo = articleRepo;
        private readonly ISubjectRepository _subjectRepo = subjectRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _articleRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _articleRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArticleRequestDto articleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _subjectRepo.SubjectExists(articleDto.SubjectId))
                return BadRequest("Subject does not exist.");

            Article articleModel = await _articleRepo.CreateAsync(articleDto.ToArticleFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = articleModel.Id }, articleModel.ToArticleDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArticleRequestDto articleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _subjectRepo.SubjectExists(articleDto.SubjectId))
                return BadRequest("Subject does not exist.");

            Article? articleModel = await _articleRepo.UpdateAsync(id, articleDto);

            if (articleModel == null)
                return NotFound();
            else
                return Ok(articleModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Article? articleModel = await _articleRepo.DeleteAsync(id);

            if (articleModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
