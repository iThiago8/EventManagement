using apis.Dtos.Subject;
using apis.Mappers;
using apis.Interfaces;
using Microsoft.AspNetCore.Mvc;
using apis.Models;

namespace apis.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await subjectRepo.GetAllAsync());
        }
        
        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Subject? subjectModel = await subjectRepo.DeleteAsync(id);

            if (subjectModel == null)
                return NotFound();

            return NoContent();
        }

    }
}
