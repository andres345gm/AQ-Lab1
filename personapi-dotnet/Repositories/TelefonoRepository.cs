using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Linq;

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

        public void Add(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            _context.SaveChanges();
        }

        public void Update(Telefono telefono)
        {
            _context.Telefonos.Update(telefono);
            _context.SaveChanges();
        }

        public void Delete(string num)
        {
            var telefono = _context.Telefonos.Find(num);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                _context.SaveChanges();
            }
        }
    }
}
