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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Workshop? workshopModel = await _workshopRepo.GetByIdAsync(id);

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkshopRequestDto workshopDto)
        {
            bool subjectExists = await _subjectRepo.SubjectExists(workshopDto.SubjectId);

            if (!subjectExists)
                return NotFound();

            Workshop workshopModel = await _workshopRepo.CreateAsync(workshopDto.ToWorkshopFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = workshopModel.Id }, workshopModel.ToWorkshopDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkshopRequestDto workshopDto)
        {
            bool subjectExists = await _subjectRepo.SubjectExists(workshopDto.SubjectId);

            if (!subjectExists)
                return NotFound();

            Workshop? workshopModel = await _workshopRepo.UpdateAsync(id, workshopDto);

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel);
        }

        [HttpDelete("{id}")]
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
