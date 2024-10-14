using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IProfesionRepository
    {
        IEnumerable<Profesion> GetAll();
        Profesion GetById(int id);
        Task<Profesion> Add(Profesion profesion);
        Task<bool> Update(Profesion profesion);
        Task<bool> Delete(int id);
    }
}
