using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace personapi_dotnet.Repositories
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

        public async Task<Persona> Add(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
            await _context.SaveChangesAsync();
            return persona;
        }

        public async Task<bool> Update(Persona persona)
        {
            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int cc)
        {
            var persona = _context.Personas.Find(cc);
            if (persona == null)
            {
                return false;
            }
            _context.Set<Persona>().Remove(persona);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
