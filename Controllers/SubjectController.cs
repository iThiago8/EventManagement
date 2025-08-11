using apis.Dtos.Subject;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await subjectRepo.GetAllAsync());
        }
        
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Subject? subject = await subjectRepo.GetByIdAsync(id);

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequestDto subjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Subject subjectModel = subjectDto.ToSubjectFromCreateDto();
            await subjectRepo.CreateAsync(subjectModel);

            return CreatedAtAction(nameof(GetById), new { id = subjectModel.Id }, subjectModel.ToSubjectDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubjectRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Subject? subjectModel = await subjectRepo.UpdateAsync(id, updateDto);

            if (subjectModel == null)
                return NotFound();

            return Ok(subjectModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Subject? subjectModel = await subjectRepo.DeleteAsync(id);

            if (subjectModel == null)
                return NotFound();

            return NoContent();
        }

    }
}
