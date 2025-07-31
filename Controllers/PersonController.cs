using apis.Dtos.Person;
using apis.Helpers.QueryObjects;
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
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PersonQueryObject query)
        {
            return Ok(await personRepo.GetAllAsync(query));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Person? person = await personRepo.GetByIdAsync(id);

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

            await personRepo.CreateAsync(personModel);

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            Person? personModel = await personRepo.UpdateAsync(id, updateDto);

            if (personModel == null)
                return NotFound();
            else
                return Ok(personModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Person? personModel = await personRepo.DeleteAsync(id);

            if (personModel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
