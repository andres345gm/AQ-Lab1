using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.api
{
    [Produces("application/json")]
    [Route("api/personas")]
    [ApiController]
    public class APIPersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IEstudioRepository _estudioRepository;

        public APIPersonaController(IPersonaRepository personaRepository, ITelefonoRepository telefonoRepository, IEstudioRepository estudioRepository)
        {
            _personaRepository = personaRepository;
            _telefonoRepository = telefonoRepository;
            _estudioRepository = estudioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _personaRepository.GetAllAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int cc, string nombre, string apellido, string genero, int edad)
        {
            var persona = new Persona
            {
                Cc = cc,
                Nombre = nombre,
                Apellido = apellido,
                Genero = genero,
                Edad = edad,
            };
            await _personaRepository.AddAsync(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string nombre = null, string apellido = null, string genero = null, int edad = 0)
        {
            var persona = await _personaRepository.GetByIdAsync(id);

            if (persona == null || id != persona.Cc)
            {
                return BadRequest();
            }

            if (nombre != null)
            {
                persona.Nombre = nombre;
            }

            if (apellido != null)
            {
                persona.Apellido = apellido;
            }

            if (genero != null)
            {
                persona.Genero = genero;
            }

            if (edad != 0)
            {
                persona.Edad = edad;
            }

            await _personaRepository.UpdateAsync(persona);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var telefonos = await _telefonoRepository.GetByDuenioAsync(id);
                if (telefonos.Any())
                {
                    List<string> phoneNumbersToDelete = telefonos.Select(t => t.Num).ToList();
                    foreach (var phoneNumber in phoneNumbersToDelete)
                    {
                        await _telefonoRepository.DeleteAsync(phoneNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            try
            {
                var estudios = await _estudioRepository.GetAllByCcPerAsync(id);
                if (estudios.Any())
                {
                    foreach (var estudio in estudios)
                    {
                        await _estudioRepository.DeleteAsync(estudio.CcPer, estudio.IdProf);
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exception
            }

            await _personaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
