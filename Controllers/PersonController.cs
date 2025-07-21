using apis.Data;
using apis.Dtos.Person;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apis.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController(IPersonRepository personRepo) : ControllerBase
    {
        private readonly IPersonRepository _personRepo = personRepo;

        private static bool IsPersonModelInvalid(Person person)
        {
            return string.IsNullOrWhiteSpace(person.Cpf) ||
                    string.IsNullOrWhiteSpace(person.Name) ||
                    string.IsNullOrWhiteSpace(person.Email) ||
                    string.IsNullOrWhiteSpace(person.PhoneNumber);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var people = await _personRepo.GetAllAsync();

            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var person = await _personRepo.GetByIdAsync(id);

            if (person != null)
                return Ok(person);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonRequestDto personDto)
        {
            var personModel = personDto.ToPersonFromCreateDto();

            if (IsPersonModelInvalid(personModel))
                return BadRequest();

            await _personRepo.CreateAsync(personModel);

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePersonRequestDto updateDto)
        {
            var personModel = await _personRepo.UpdateAsync(id, updateDto);

            if (personModel == null)
                return NotFound();
                        
            return Ok(personModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var personModel = await _personRepo.DeleteAsync(id);

            if (personModel == null)
                return NotFound();

            return NoContent();
        }
    }
}
