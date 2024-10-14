using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IEstudioRepository
    {
        IEnumerable<Estudio> GetAll();
        Estudio GetById(int id_prof, int cc_per);
        void Add(Estudio estudio);
        void Update(Estudio estudio);
        void Delete(int id_prof, int cc_per);
    }
}
