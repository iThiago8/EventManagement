using apis.Data;
using apis.Dtos.Person;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController(IPersonRepository personRepo) : ControllerBase
    {
        private readonly IPersonRepository _personRepo = personRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _personRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Person? person = await _personRepo.GetByIdAsync(id);

            if (person == null)
                return NotFound();
            else
                return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequestDto personDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Person personModel = personDto.ToPersonFromCreateDto();

            await _personRepo.CreateAsync(personModel);

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            Person? personModel = await _personRepo.UpdateAsync(id, updateDto);

            if (personModel == null)
                return NotFound();
            else
                return Ok(personModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Person? personModel = await _personRepo.DeleteAsync(id);

            if (personModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
