using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


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

        public async Task<Estudio> Add(Estudio estudio)
        {
            await _context.Estudios.AddAsync(estudio);
            await _context.SaveChangesAsync();
            return estudio;
        }

        public async Task<bool> Update(Estudio estudio)
        {
            _context.Entry(estudio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id_prof, int cc_per)
        {
            var estudio = _context.Estudios.Find(id_prof, cc_per);
            if (estudio == null)
            {
                return false;
            }
            _context.Set<Estudio>().Remove(estudio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
