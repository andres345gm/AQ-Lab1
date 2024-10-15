using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.api
{
    [Route("api/profesiones")]
    public class APIProfesionesController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;
        private readonly IEstudioRepository _estudioRepository;

        public APIProfesionesController(IProfesionRepository profesionRepository, IEstudioRepository estudioRepository)
        {
            _profesionRepository = profesionRepository;
            _estudioRepository = estudioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profesiones = await _profesionRepository.GetAllAsync();
            return Ok(profesiones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return Ok(profesion);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            await _profesionRepository.AddAsync(profesion);
            return CreatedAtAction(nameof(GetById), new { id = profesion.Id }, profesion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string nombre, string descripcion)
        {
            var profesion = await _profesionRepository.GetByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            if (nombre != null)
            {
                profesion.Nom = nombre;
            }

            if (descripcion != null)
            {
                profesion.Des = descripcion;
            }

            await _profesionRepository.UpdateAsync(profesion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var profesion = await _profesionRepository.GetByIdAsync(id);
                if (profesion == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
            }

            try
            {
                var estudios = await _estudioRepository.GetAllByIdProfAsync(id);
                if (estudios.Any())
                {
                    foreach (var estudio in estudios)
                    {
                        await _estudioRepository.DeleteAsync(estudio.CcPer, estudio.IdProf);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
            }

            await _profesionRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
