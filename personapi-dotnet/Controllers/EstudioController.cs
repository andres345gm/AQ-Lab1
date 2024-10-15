using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class EstudioController : Controller
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository; // Para obtener información de persona
        private readonly IProfesionRepository _profesionRepository; // Para obtener información de profesion

        public EstudioController(IEstudioRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var estudios = _estudioRepository.GetAll();
            return View(estudios);
        }

        public IActionResult Create()
        {
            ViewBag.Personas = _personaRepository.GetAll(); // Lista de personas
            ViewBag.Profesiones = _profesionRepository.GetAll(); // Lista de profesiones
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.Add(estudio);
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, vuelve a cargar las listas
            ViewBag.Personas = _personaRepository.GetAll();
            ViewBag.Profesiones = _profesionRepository.GetAll();
            return View(estudio);
        }

        public IActionResult Edit(int id_prof, int cc_per)
        {
            var estudio = _estudioRepository.GetById(id_prof, cc_per);
            if (estudio == null)
            {
                return NotFound();
            }

            ViewBag.Personas = _personaRepository.GetAll(); // Lista de personas
            ViewBag.Profesiones = _profesionRepository.GetAll(); // Lista de profesiones
            return View(estudio);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.Update(estudio);
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, vuelve a cargar las listas
            ViewBag.Personas = _personaRepository.GetAll();
            ViewBag.Profesiones = _profesionRepository.GetAll();
            return View(estudio);
        }

        public async Task<IActionResult> Delete(int id_prof, int cc_per)
        {
            var estudio = _estudioRepository.GetById(id_prof, cc_per);
            if (estudio == null)
            {
                return NotFound();
            }
            return View(estudio);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id_prof, int cc_per)
        {
            var success = await _estudioRepository.Delete(id_prof, cc_per);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
