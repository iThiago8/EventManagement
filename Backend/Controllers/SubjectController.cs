using Core.Dtos.Subject;
using Backend.Interfaces;
using Backend.Mappers;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await subjectRepo.GetAllAsync();

            return Ok(subjects.Select(s => s.ToSubjectDto()));
        }
        
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subject = await subjectRepo.GetByIdAsync(id);

            if (subject == null)
                return NotFound();

            return Ok(subject.ToSubjectDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequestDto subjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subjectModel = subjectDto.ToSubjectFromCreateDto();
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

            var subjectModel = await subjectRepo.UpdateAsync(id, updateDto.ToSubjectFromUpdateDto());

            if (subjectModel == null)
                return NotFound();

            return Ok(subjectModel.ToSubjectDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subjectModel = await subjectRepo.DeleteAsync(id);

            if (subjectModel == null)
                return NotFound();

            return NoContent();
        }

    }
}
