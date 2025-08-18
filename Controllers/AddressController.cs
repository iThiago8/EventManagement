using apis.Dtos.Address;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController(IAddressRepository addressRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await addressRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Address? addressModel = await addressRepo.GetByIdAsync(id);

            if (addressModel == null)
                return NotFound();
            else
                return Ok(addressModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAddressRequestDto addressDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            Address addressModel = addressDto.ToAddressFromCreateDto();

            await addressRepo.CreateAsync(addressModel);

            return CreatedAtAction(nameof(GetById), new { id = addressModel.Id }, addressModel.ToAddressDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateAddressRequestDto addressDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Address? addressModel = await addressRepo.UpdateAsync(id, addressDto.ToAddressFromUpdateDto());

            if (addressModel == null)
                return NotFound();
            else
                return Ok(addressModel);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Address? addressModel = await addressRepo.DeleteAsync(id);

            if (addressModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
