using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioController : ControllerBase
    {
        private readonly IEstudioRepository _repository;

        public EstudioController(IEstudioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var estudios = _repository.GetAll();
            return Ok(estudios);
        }

        [HttpGet("{idProf}/{ccPer}")]
        public IActionResult GetById(int idProf, int ccPer)
        {
            var estudio = _repository.GetById(idProf, ccPer);
            if (estudio == null) return NotFound();
            return Ok(estudio);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Estudio estudio)
        {
            _repository.Add(estudio);
            return CreatedAtAction(nameof(GetById), new { idProf = estudio.IdProf, ccPer = estudio.CcPer }, estudio);
        }

        [HttpPut("{idProf}/{ccPer}")]
        public IActionResult Update(int idProf, int ccPer, [FromBody] Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer) return BadRequest();
            _repository.Update(estudio);
            return NoContent();
        }

        [HttpDelete("{idProf}/{ccPer}")]
        public IActionResult Delete(int idProf, int ccPer)
        {
            _repository.Delete(idProf, ccPer);
            return NoContent();
        }
    }
}
