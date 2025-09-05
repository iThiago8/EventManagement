using Backend.Dtos.Symposium;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/symposium")]
    [ApiController]
    public class SymposiumController(ISymposiumRepository symposiumRepo, IAddressRepository addressRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var symposiums = await symposiumRepo.GetAllAsync();

            return Ok(symposiums.Select(s => s.ToSymposiumDto()));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var symposiumModel = await symposiumRepo.GetByIdAsync(id);

            if (symposiumModel == null)
                return NotFound();
            else
                return Ok(symposiumModel.ToSymposiumDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSymposiumRequestDto symposiumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await addressRepo.AddressExists(symposiumDto.LocationAddressId))
                return BadRequest("Address does not exist.");

            var symposiumModel = await symposiumRepo.CreateAsync(symposiumDto.ToSymposiumFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = symposiumModel.Id }, symposiumModel.ToSymposiumDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSymposiumRequestDto symposiumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await addressRepo.AddressExists(symposiumDto.LocationAddressId))
                return BadRequest("Address does not exist.");

            var symposiumModel = await symposiumRepo.UpdateAsync(id, symposiumDto.ToSymposiumFromUpdateDto());

            if (symposiumModel == null)
                return NotFound();
            else
                return Ok(symposiumModel.ToSymposiumDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var symposiumModel = await symposiumRepo.DeleteAsync(id);

            if (symposiumModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
