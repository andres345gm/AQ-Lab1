using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IPersonaRepository
    {
        IEnumerable<Persona> GetAll();
        Persona GetById(int cc);
        Task<Persona> Add(Persona persona);
        Task<bool> Update(Persona persona);
        Task<bool> Delete(int cc);
    }
}
