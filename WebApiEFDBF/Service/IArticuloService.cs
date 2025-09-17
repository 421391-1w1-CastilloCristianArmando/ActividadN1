using WebApiEFDBF.Models;

namespace WebApiEFDBF.Service
{
    public interface IArticuloService
    {
        List<Articulo> GetAll();
        Articulo? GetById(int id);
        void Update(Articulo articulo);
        void Delete(int id);
        void Create (Articulo articulo);
    }
}
