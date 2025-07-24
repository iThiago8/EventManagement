using apis.Dtos.Subject;
using apis.Mappers;
using apis.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepo) : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepo = subjectRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _subjectRepo.GetAllAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var subject = await _subjectRepo.GetByIdAsync(id);

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubjectRequestDto subjectDto)
        {
            var subjectModel = subjectDto.ToSubjectFromCreateDto();
            await _subjectRepo.CreateAsync(subjectModel);

            return CreatedAtAction(nameof(GetById), new { id = subjectModel.Id }, subjectModel.ToSubjectDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubjectRequestDto updateDto)
        {
            var subjectModel = await _subjectRepo.UpdateAsync(id, updateDto);

            if (subjectModel == null)
                return NotFound();

            return Ok(subjectModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subjectModel = await _subjectRepo.DeleteAsync(id);

            if (subjectModel == null)
                return NotFound();

            return NoContent();
        }

    }
}
