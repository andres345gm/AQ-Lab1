using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace personapi_dotnet.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Estudio> GetAll()
        {
            return _context.Estudios.ToList();
        }

        public Estudio GetById(int id_prof, int cc_per)
        {
            return _context.Estudios.Find(id_prof, cc_per);
        }

        public void Add(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            _context.SaveChanges();
        }

        public void Update(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            _context.SaveChanges();
        }

        public void Delete(int id_prof, int cc_per)
        {
            var estudio = _context.Estudios.Find(id_prof, cc_per);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                _context.SaveChanges();
            }
        }
    }
}
