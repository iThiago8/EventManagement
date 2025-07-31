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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await workshopRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Workshop? workshopModel = await workshopRepo.GetByIdAsync(id);

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

            if (!await subjectRepo.SubjectExists(workshopDto.SubjectId))
                return BadRequest("Subject does not exist.");

            Workshop workshopModel = await workshopRepo.CreateAsync(workshopDto.ToWorkshopFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = workshopModel.Id }, workshopModel.ToWorkshopDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkshopRequestDto workshopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await subjectRepo.SubjectExists(workshopDto.SubjectId))
                return BadRequest("Subject does not exist.");

            Workshop? workshopModel = await workshopRepo.UpdateAsync(id, workshopDto);

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Workshop? workshopModel = await workshopRepo.DeleteAsync(id);

            if (workshopModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
