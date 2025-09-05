using Backend.Dtos.Workshop;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/workshop")]
    [ApiController]
    public class WorkshopController(IWorkshopRepository workshopRepo, ISubjectRepository subjectRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var workshops = await workshopRepo.GetAllAsync();

            return Ok(workshops.Select(w => w.ToWorkshopDto()));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Workshop? workshopModel = await workshopRepo.GetByIdAsync(id);

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel.ToWorkshopDto());
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWorkshopRequestDto workshopDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await subjectRepo.SubjectExists(workshopDto.SubjectId))
                return BadRequest("Subject does not exist.");

            var workshopModel = await workshopRepo.UpdateAsync(id, workshopDto.ToWorkshopFromUpdateDto());

            if (workshopModel == null)
                return NotFound();
            else
                return Ok(workshopModel.ToWorkshopDto());
        }

        [Authorize]
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
