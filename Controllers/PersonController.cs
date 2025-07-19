using apis.Data;
using apis.Dtos.Person;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        private bool IsPersonModelInvalid(Person person)
        {
            return string.IsNullOrWhiteSpace(person.Cpf) ||
                    string.IsNullOrWhiteSpace(person.Name) ||
                    string.IsNullOrWhiteSpace(person.Email) ||
                    string.IsNullOrWhiteSpace(person.PhoneNumber);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var people = _context.Person.ToList().Select(p => p.ToPersonDto());

            return Ok(people);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var person = _context.Person.Find(id);

            if (person != null)
                return Ok(person);
            else
                return Ok("Pessoa não encontrada");
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonRequestDto personDto)
        {
            var personModel = personDto.ToPersonFromCreateDto();
            _context.Person.Add(personModel);
            _context.SaveChanges();

            if (IsPersonModelInvalid(personModel))
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdatePersonRequestDto updateDto)
        {
            var personModel = _context.Person.FirstOrDefault(p => p.Id == id);

            if (personModel == null)
                return NotFound();

            if (IsPersonModelInvalid(personModel))
                return BadRequest();

            personModel.Cpf = updateDto.Cpf;
            personModel.Name = updateDto.Name;
            personModel.Email = updateDto.Email;
            personModel.PhoneNumber = updateDto.PhoneNumber;
            personModel.BirthDate = updateDto.BirthDate;

            _context.SaveChanges();

            return Ok(personModel.ToPersonDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var personModel = _context.Person.FirstOrDefault(p => p.Id == id);

            if (personModel == null)
                return NotFound();

            _context.Person.Remove(personModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
