using apis.Dtos.ScientificCommittee;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/scientific-committee")]
    [ApiController]
    public class ScientificCommitteeController(IScientificCommitteeRepository scientificCommitteeRepo, ISubjectRepository subjectRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var scientificCommittees = await scientificCommitteeRepo.GetAllAsync();

            return Ok(scientificCommittees.Select(sc => sc.ToScientificCommitteeDto()));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var scientificCommitteeModel = await scientificCommitteeRepo.GetByIdAsync(id);

            if (scientificCommitteeModel == null)
                return NotFound();
            else
                return Ok(scientificCommitteeModel.ToScientificCommitteeDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateScientificCommitteeRequestDto scientificCommitteeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool subjectExists = await subjectRepo.SubjectExists(scientificCommitteeDto.SubjectId);

            if (!subjectExists)
                return NotFound();

            var scientificCommitteeModel = await scientificCommitteeRepo.CreateAsync(scientificCommitteeDto.ToScientificCommitteFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = scientificCommitteeModel.Id }, scientificCommitteeModel.ToScientificCommitteeDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateScientificCommitteeRequestDto scientificCommitteeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool subjectExists = await subjectRepo.SubjectExists(scientificCommitteeDto.SubjectId);

            if (!subjectExists)
                return NotFound();

            ScientificCommittee? scientificCommitteeModel = await scientificCommitteeRepo.UpdateAsync(id, scientificCommitteeDto.ToScientificCommitteFromUpdateDto());

            if (scientificCommitteeModel == null)
                return NotFound();
            else
                return Ok(scientificCommitteeModel.ToScientificCommitteeDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            ScientificCommittee? scientificCommitteeModel = await scientificCommitteeRepo.DeleteAsync(id);

            if (scientificCommitteeModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
