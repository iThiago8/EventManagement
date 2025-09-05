using Backend.Dtos.Address;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController(IAddressRepository addressRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var addressModels = await addressRepo.GetAllAsync();

            return Ok(addressModels.Select(a => a.ToAddressDto()));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addressModel = await addressRepo.GetByIdAsync(id);

            if (addressModel == null)
                return NotFound();
            else
                return Ok(addressModel.ToAddressDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAddressRequestDto addressDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var addressModel = addressDto.ToAddressFromCreateDto();

            await addressRepo.CreateAsync(addressModel);

            return CreatedAtAction(nameof(GetById), new { id = addressModel.Id }, addressModel.ToAddressDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateAddressRequestDto addressDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingAddress = await addressRepo.UpdateAsync(id, addressDto.ToAddressFromUpdateDto());


            if (existingAddress == null)
                return NotFound();
            else
                return Ok(existingAddress.ToAddressDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var addressModel = await addressRepo.DeleteAsync(id);

            if (addressModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
