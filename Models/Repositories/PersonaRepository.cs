using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace personapi_dotnet.Models.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Persona> GetAll()
        {
            return _context.Personas.ToList();
        }

        public Persona GetById(int cc)
        {
            return _context.Personas.Find(cc);
        }

        public void Add(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
        }

        public void Update(Persona persona)
        {
            _context.Personas.Update(persona);
            _context.SaveChanges();
        }

        public void Delete(int cc)
        {
            var persona = _context.Personas.Find(cc);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                _context.SaveChanges();
            }
        }
    }
}
