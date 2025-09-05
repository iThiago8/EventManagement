using Backend.Dtos.Person;
using Backend.Helpers.QueryObjects;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController(IPersonRepository personRepo) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] PersonQueryObject query)
        {
            var people = await personRepo.GetAllAsync(query);

            return Ok(
                people.Select(
                    p => p.ToPersonDto()
                )
            );
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Person? person = await personRepo.GetByIdAsync(id);

            if (person == null)
                return NotFound();
            else
                return Ok(person.ToPersonDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequestDto personDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Person personModel = personDto.ToPersonFromCreateDto();

            await personRepo.CreateAsync(personModel);

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 
            
            Person? personModel = await personRepo.UpdateAsync(id, updateDto.ToPersonFromUpdateDto());

            if (personModel == null)
                return NotFound();
            else
                return Ok(personModel);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
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
