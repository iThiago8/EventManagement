using apis.Dtos.Address;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace apis.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController(IAddressRepository addressRepo) : ControllerBase
    {
        private readonly IAddressRepository _addressRepo = addressRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _addressRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Address? addressModel = await _addressRepo.GetByIdAsync(id);

            if (addressModel == null)
                return NotFound();
            else
                return Ok(addressModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressRequestDto addressDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            Address addressModel = addressDto.ToAddressFromCreateDto();

            await _addressRepo.CreateAsync(addressModel);

            return CreatedAtAction(nameof(GetById), new { id = addressModel.Id }, addressModel.ToAddressDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateAddressRequestDto addressDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Address? addressModel = await _addressRepo.UpdateAsync(id, addressDto);

            if (addressModel == null)
                return NotFound();
            else
                return Ok(addressModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Address? addressModel = await _addressRepo.DeleteAsync(id);

            if (addressModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
