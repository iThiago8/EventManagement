using apis.Dtos.Workshop;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/workshop")]
    [ApiController]
    public class WorkshopController(IWorkshopRepository workshopRepo, ISubjectRepository subjectRepo) : ControllerBase
    {
        private readonly IWorkshopRepository _workshopRepo = workshopRepo;
        private readonly ISubjectRepository _subjectRepo = subjectRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _workshopRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Workshop? workshopModel = await _workshopRepo.GetByIdAsync(id);

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkshopRequestDto workshopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _subjectRepo.SubjectExists(workshopDto.SubjectId))
                return BadRequest("Subject does not exist.");

            Workshop workshopModel = await _workshopRepo.CreateAsync(workshopDto.ToWorkshopFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = workshopModel.Id }, workshopModel.ToWorkshopDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkshopRequestDto workshopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _subjectRepo.SubjectExists(workshopDto.SubjectId))
                return BadRequest("Subject does not exist.");

            Workshop? workshopModel = await _workshopRepo.UpdateAsync(id, workshopDto);

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Workshop? workshopModel = await _workshopRepo.DeleteAsync(id);

            if (workshopModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
