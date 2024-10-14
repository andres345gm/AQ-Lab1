using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _repository;

        public PersonaController(IPersonaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var personas = _repository.GetAll();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var persona = _repository.GetById(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Persona persona)
        {
            _repository.Add(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Persona persona)
        {
            if (id != persona.Cc) return BadRequest();
            _repository.Update(persona);
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
