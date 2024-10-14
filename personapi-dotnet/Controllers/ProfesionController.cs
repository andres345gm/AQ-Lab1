using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionController : ControllerBase
    {
        private readonly IProfesionRepository _repository;

        public ProfesionController(IProfesionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var profesions = _repository.GetAll();
            return Ok(profesions);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var profesion = _repository.GetById(id);
            if (profesion == null) return NotFound();
            return Ok(profesion);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Profesion profesion)
        {
            _repository.Add(profesion);
            return CreatedAtAction(nameof(GetById), new { id = profesion.Id }, profesion);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Profesion profesion)
        {
            if (id != profesion.Id) return BadRequest();
            _repository.Update(profesion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }
    }
}
