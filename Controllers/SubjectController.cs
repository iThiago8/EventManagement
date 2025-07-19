using apis.Data;
using apis.Dtos.Person;
using apis.Dtos.Subject;
using apis.Mappers;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace apis.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {
            var subjects = _context.Subject.ToList().Select(s => s.ToSubjectDto());

            return Ok(subjects);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var subject = _context.Subject.Find(id);

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSubjectRequestDto subjectDto)
        {
            var subjectModel = subjectDto.ToSubjectFromCreateDto();
            _context.Add(subjectModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = subjectModel.Id }, subjectModel.ToSubjectDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateSubjectRequestDto updateDto)
        {
            var subjectModel = _context.Subject.FirstOrDefault(s => s.Id == id);

            if (subjectModel == null)
                return NotFound();

            subjectModel.Name = updateDto.Name;

            _context.SaveChanges();

            return Ok(subjectModel.ToSubjectDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var subjectModel = _context.Subject.FirstOrDefault(s => s.Id == id);

            if (subjectModel == null)
                return NotFound();

            _context.Subject.Remove(subjectModel);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
