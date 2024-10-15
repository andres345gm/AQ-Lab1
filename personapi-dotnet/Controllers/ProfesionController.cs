using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class ProfesionController : Controller
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        // GET: Profesion
        public IActionResult Index()
        {
            var profesiones = _profesionRepository.GetAll();
            return View(profesiones);
        }

        // GET: Profesion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesion/Create
        [HttpPost]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.Add(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: Profesion/Edit/{id}
        public IActionResult Edit(int id)
        {
            var profesion = _profesionRepository.GetById(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesion/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.Update(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: Profesion/Delete/{id}
        public IActionResult Delete(int id)
        {
            var profesion = _profesionRepository.GetById(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesion/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _profesionRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
