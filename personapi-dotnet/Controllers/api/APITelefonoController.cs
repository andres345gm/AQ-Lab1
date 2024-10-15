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
        public async Task<IActionResult> Create([FromBody] Telefono telefono)
        {
            var persona = await _personaRepository.GetByIdAsync(telefono.Duenio);
            if (persona == null)
            {
                return BadRequest("La persona con la cédula proporcionada no existe.");
            }

            // Verificar si el teléfono ya existe
            if (await _telefonoRepository.TelefonoExistsAsync(telefono.Num))
            {
                return Conflict("El número de teléfono ya existe.");
            }

            // Asignar el dueño al teléfono
            telefono.DuenioNavigation = persona;
            await _telefonoRepository.AddAsync(telefono);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedTelefono = JsonSerializer.Serialize(telefono, options);

            return CreatedAtAction(nameof(GetByNumber), new { numero = telefono.Num }, serializedTelefono);
        }

        [HttpPut("{numero}")]
        public async Task<IActionResult> Update(string numero, [FromBody] Telefono telefono)
        {
            var existingTelefono = await _telefonoRepository.GetByNumberAsync(numero);
            if (existingTelefono == null)
            {
                return NotFound();
            }

            existingTelefono.Oper = telefono.Oper;
            existingTelefono.Duenio = telefono.Duenio;
            existingTelefono.DuenioNavigation = await _personaRepository.GetByIdAsync(telefono.Duenio);

            await _telefonoRepository.UpdateAsync(existingTelefono);

            return NoContent();
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> Delete(string numero)
        {
            var telefono = await _telefonoRepository.GetByNumberAsync(numero);
            if (telefono == null)
            {
                return NotFound();
            }

            await _telefonoRepository.DeleteAsync(numero);
            return NoContent();
        }
    }
}
