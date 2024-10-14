using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace personapi_dotnet.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Telefono> GetAll()
        {
            return _context.Telefonos.ToList();
        }

        public Telefono GetById(string num)
        {
            return _context.Telefonos.Find(num);
        }

        public async Task<Telefono> Add(Telefono telefono)
        {
            await _context.Telefonos.AddAsync(telefono);
            await _context.SaveChangesAsync();
            return telefono;
        }

        public async Task<bool> Update(Telefono telefono)
        {
            _context.Entry(telefono).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(string num)
        {
            var telefono = _context.Telefonos.Find(num);
            if (telefono == null) {
                return false;
            }
            _context.Set<Telefono>().Remove(telefono);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
