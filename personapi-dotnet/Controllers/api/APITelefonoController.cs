using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.api
{
    [Route("api/telefonos")]
    [ApiController]
    public class APITelefonoController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public APITelefonoController(ITelefonoRepository telefonoRepository, IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var telefonos = await _telefonoRepository.GetAllAsync();
            return Ok(telefonos);
        }

        [HttpGet("{numero}")]
        public async Task<IActionResult> GetByNumber(string numero)
        {
            var telefono = await _telefonoRepository.GetByNumberAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }
            return Ok(telefono);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string numero, string operador, int duenioCedula)
        {
            var persona = await _personaRepository.GetByIdAsync(duenioCedula);
            if (persona == null)
            {
                return BadRequest("La persona con la cédula proporcionada no existe.");
            }

            var telefono = new Telefono
            {
                Num = numero,
                Oper = operador,
                Duenio = duenioCedula
            };

            persona.Telefonos.Add(telefono);
            await _personaRepository.UpdateAsync(persona);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedTelefono = JsonSerializer.Serialize(telefono, options);

            return CreatedAtAction(nameof(GetByNumber), new { numero = telefono.Num }, serializedTelefono);
        }

        [HttpPut("{numero}")]
        public async Task<IActionResult> Update(string numero, string oper)
        {
            var existingTelefono = await _telefonoRepository.GetByNumberAsync(numero);
            if (existingTelefono == null)
            {
                return NotFound();
            }

            existingTelefono.Oper = oper;
            await _telefonoRepository.UpdateAsync(existingTelefono);

            return NoContent();
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> Delete(string numero)
        {
            await _telefonoRepository.DeleteAsync(numero);
            return NoContent();
        }
    }
}
