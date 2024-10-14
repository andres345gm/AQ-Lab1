using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface ITelefonoRepository
    {
        IEnumerable<Telefono> GetAll();
        Telefono GetById(string num);
        Task<Telefono> Add(Telefono telefono);
        Task<bool> Update(Telefono telefono);
        Task<bool> Delete(string num);
    }
}
