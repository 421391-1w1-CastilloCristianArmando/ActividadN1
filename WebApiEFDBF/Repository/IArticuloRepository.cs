using WebApiEFDBF.Models;

namespace WebApiEFDBF.Repository
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo? GetById(int id);
        void Delete(int id);
        void Update(Articulo articulo);
        void Create(Articulo articulo);
    }
}
