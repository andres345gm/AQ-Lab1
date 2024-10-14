using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoController : ControllerBase
    {
        private readonly ITelefonoRepository _repository;

        public TelefonoController(ITelefonoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var telefonos = _repository.GetAll();
            return Ok(telefonos);
        }

        [HttpGet("{num}")]
        public IActionResult GetById(string num)
        {
            var telefono = _repository.GetById(num);
            if (telefono == null) return NotFound();
            return Ok(telefono);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Telefono telefono)
        {
            _repository.Add(telefono);
            return CreatedAtAction(nameof(GetById), new { num = telefono.Num }, telefono);
        }

        [HttpPut("{num}")]
        public IActionResult Update(string num, [FromBody] Telefono telefono)
        {
            if (num != telefono.Num) return BadRequest();
            _repository.Update(telefono);
            return NoContent();
        }

        [HttpDelete("{num}")]
        public IActionResult Delete(string num)
        {
            _repository.Delete(num);
            return NoContent();
        }
    }
}
