using apis.Data;
using apis.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var people = _context.Pessoa.ToList().Select(p => p.ToPersonDto());

            return Ok(people);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var person = _context.Pessoa.Find(id);

            if (person != null)
                return Ok(person);
            else
                return Ok("Pessoa não encontrada");
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonRequestDto personDto)
        {
            var personModel = personDto.ToPersonFromCreateDTO();
            _context.Pessoa.Add(personModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = personModel.Id }, personModel.ToPersonDto());
        }
    }
}
