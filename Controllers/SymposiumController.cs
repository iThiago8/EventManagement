using apis.Dtos.Symposium;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/symposium")]
    [ApiController]
    public class SymposiumController(ISymposiumRepository symposiumRepo, IAddressRepository addressRepo) : ControllerBase
    {
        private readonly ISymposiumRepository _symposiumRepo = symposiumRepo;
        private readonly IAddressRepository _addressRepo = addressRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _symposiumRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var symposiumModel = await _symposiumRepo.GetByIdAsync(id);

            if (symposiumModel == null)
                return NotFound();
            else
                return Ok(symposiumModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSymposiumRequestDto symposiumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _addressRepo.AddressExists(symposiumDto.LocationAddressId))
                return BadRequest("Address does not exist.");

            var symposiumModel = await _symposiumRepo.CreateAsync(symposiumDto.ToSymposiumFromCreateDto());

            return CreatedAtAction(nameof(GetById), new { id = symposiumModel.Id }, symposiumModel.ToSymposiumDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSymposiumRequestDto symposiumDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _addressRepo.AddressExists(symposiumDto.LocationAddressId))
                return BadRequest("Address does not exist.");

            Symposium? symposiumModel = await _symposiumRepo.UpdateAsync(id, symposiumDto);

            if (symposiumModel == null)
                return NotFound();
            else
                return Ok(symposiumModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var symposiumModel = await _symposiumRepo.DeleteAsync(id);

            if (symposiumModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
