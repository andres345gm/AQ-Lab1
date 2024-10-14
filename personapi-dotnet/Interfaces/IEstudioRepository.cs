using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IEstudioRepository
    {
        IEnumerable<Estudio> GetAll();
        Estudio GetById(int id_prof, int cc_per);
        Task<Estudio> Add(Estudio estudio);
        Task<bool> Update(Estudio estudio);
        Task<bool> Delete(int id_prof, int cc_per);
    }
}
