using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

public class ProfesionRepository : IProfesionRepository
{
    private readonly PersonaDbContext _context;

    public ProfesionRepository(PersonaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Profesion>> GetAllAsync()
    {
        return await _context.Profesions.ToListAsync();
    }

    public async Task<Profesion?> GetByIdAsync(int id)
    {
        return await _context.Profesions.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Profesion profesion)
    {
        await _context.Profesions.AddAsync(profesion);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Profesion profesion)
    {
        _context.Profesions.Update(profesion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var profesion = await _context.Profesions.FirstOrDefaultAsync(p => p.Id == id);
        if (profesion != null)
        {
            _context.Profesions.Remove(profesion);
            await _context.SaveChangesAsync();
        }
    }
}
