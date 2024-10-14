using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using personapi_dotnet.Interfaces;

namespace personapi_dotnet.Repositories
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly PersonaDbContext _context;

        public ProfesionRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Profesion> GetAll()
        {
            // Utiliza la propiedad DbSet de Profesion
            return _context.Profesions.ToList(); // Asegúrate de usar "Profesions"
        }

        public Profesion GetById(int id)
        {
            return _context.Profesions.Find(id);
        }

        public async Task<Profesion> Add(Profesion profesion)
        {
            await _context.AddAsync(profesion);
            await _context.SaveChangesAsync();
            return profesion;
        }

        public async Task<bool> Update(Profesion profesion)
        {
            _context.Entry(profesion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var profesion = _context.Profesions.Find(id);
            if (profesion == null)
            {
                return false;
            }
            _context.Profesions.Remove(profesion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
