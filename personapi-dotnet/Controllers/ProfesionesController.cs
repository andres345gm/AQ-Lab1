﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class ProfesionesController : Controller
    {
        private readonly PersonaDbContext _context;

        public ProfesionesController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: Profesiones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesions.ToListAsync());
        }

        // GET: Profesiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: Profesiones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesiones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] Profesion profesion)
        {
            // Verificar si el teléfono ya existe
            bool profesionExiste = await _context.Profesions
                .AnyAsync(t => t.Id == profesion.Id);

            // Si el teléfono ya existe, se muestra en la pantalla
            if (profesionExiste)
            {
                ModelState.AddModelError("Id", "El id de profesión ya existe.");
            }
            else
            {
                ModelState.Clear();

                if (ModelState.IsValid)
                {
                    _context.Add(profesion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            
            return View(profesion);
        }

        // GET: Profesiones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesiones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesionExists(profesion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: Profesiones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // POST: Profesiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Encontrar la profesión por su ID
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion != null)
            {
                // Buscar todos los estudios asociados con esta profesión
                var estudiosAsociados = await _context.Estudios
                    .Where(e => e.IdProf == id)
                    .ToListAsync();

                // Eliminar todos los estudios asociados
                if (estudiosAsociados.Any())
                {
                    _context.Estudios.RemoveRange(estudiosAsociados);
                }

                // Luego eliminar la profesión
                _context.Profesions.Remove(profesion);

                // Guardar cambios
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ProfesionExists(int id)
        {
            return _context.Profesions.Any(e => e.Id == id);
        }
    }
}
