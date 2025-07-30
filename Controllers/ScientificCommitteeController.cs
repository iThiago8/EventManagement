using apis.Dtos.ScientificCommittee;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/scientificcommittee")]
    [ApiController]
    public class ScientificCommitteeController(IScientificCommitteeRepository scientificCommitteeRepo, ISubjectRepository subjectRepo) : ControllerBase
    {
        private readonly IScientificCommitteeRepository _scientificCommitteeRepo = scientificCommitteeRepo;
        private readonly ISubjectRepository _subjectRepo = subjectRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _scientificCommitteeRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ScientificCommittee? scientificCommitteeModel = await _scientificCommitteeRepo.GetByIdAsync(id);

            if (scientificCommitteeModel == null)
                return NotFound();
            else
                return Ok(scientificCommitteeModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScientificCommitteeRequestDto scientificCommitteeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool subjectExists = await _subjectRepo.SubjectExists(scientificCommitteeDto.SubjectId);

            if (!subjectExists)
                return NotFound();

            ScientificCommittee scientificCommitteeModel = await _scientificCommitteeRepo.CreateAsync(scientificCommitteeDto.ToScientificCommitteFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = scientificCommitteeModel.Id }, scientificCommitteeModel.ToScientificCommitteeDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateScientificCommitteeResquestDto scientificCommitteeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool subjectExists = await _subjectRepo.SubjectExists(scientificCommitteeDto.SubjectId);

            if (!subjectExists)
                return NotFound();

            ScientificCommittee? scientificCommitteeModel = await _scientificCommitteeRepo.UpdateAsync(id, scientificCommitteeDto);

            if (scientificCommitteeModel == null)
                return NotFound();
            else
                return Ok(scientificCommitteeModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            ScientificCommittee? scientificCommitteeModel = await _scientificCommitteeRepo.DeleteAsync(id);

            if (scientificCommitteeModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
