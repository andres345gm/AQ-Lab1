using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // GET: Persona
        public IActionResult Index()
        {
            var personas = _personaRepository.GetAll();
            return View(personas);
        }

        // GET: Persona/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _personaRepository.Add(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Persona/Edit/{cc}
        public IActionResult Edit(int cc)
        {
            var persona = _personaRepository.GetById(cc);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Persona/Edit/{cc}
        [HttpPost]
        public async Task<IActionResult> Edit(Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _personaRepository.Update(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Persona/Delete/{cc}
        public IActionResult Delete(int cc)
        {
            var persona = _personaRepository.GetById(cc);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Persona/DeleteConfirmed/{cc}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int cc)
        {
            await _personaRepository.Delete(cc);
            return RedirectToAction(nameof(Index));
        }
    }
}
