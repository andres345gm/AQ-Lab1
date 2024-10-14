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

        public void Add(Profesion profesion)
        {
            _context.Profesions.Add(profesion);
            _context.SaveChanges();
        }

        public void Update(Profesion profesion)
        {
            _context.Profesions.Update(profesion);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var profesion = _context.Profesions.Find(id);
            if (profesion != null)
            {
                _context.Profesions.Remove(profesion);
                _context.SaveChanges();
            }
        }
    }
}
