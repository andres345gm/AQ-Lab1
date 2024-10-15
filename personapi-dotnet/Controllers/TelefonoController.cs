using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class TelefonoController : Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonoController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        // GET: Telefono
        public IActionResult Index()
        {
            var telefonos = _telefonoRepository.GetAll();
            return View(telefonos);
        }

        // GET: Telefono/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Telefono/Create
        [HttpPost]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                await _telefonoRepository.Add(telefono);
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: Telefono/Edit/{num}
        public IActionResult Edit(string num)
        {
            var telefono = _telefonoRepository.GetById(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: Telefono/Edit/{num}
        [HttpPost]
        public async Task<IActionResult> Edit(Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                await _telefonoRepository.Update(telefono);
                return RedirectToAction(nameof(Index));
            }
            return View(telefono);
        }

        // GET: Telefono/Delete/{num}
        public IActionResult Delete(string num)
        {
            var telefono = _telefonoRepository.GetById(num);
            if (telefono == null)
            {
                return NotFound();
            }
            return View(telefono);
        }

        // POST: Telefono/DeleteConfirmed/{num}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string num)
        {
            await _telefonoRepository.Delete(num);
            return RedirectToAction(nameof(Index));
        }
    }
}
