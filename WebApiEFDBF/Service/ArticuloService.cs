using WebApiEFDBF.Models;
using WebApiEFDBF.Repository;

namespace WebApiEFDBF.Service
{
    public class ArticuloService : IArticuloService
    {
        private IArticuloRepository _repository;

        public ArticuloService(IArticuloRepository repository)
        {
            _repository = repository;
        }
        public void Create(Articulo articulo)
        {
            _repository.Create(articulo);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Articulo> GetAll()
        {
            return _repository.GetAll();
        }

        public Articulo? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Articulo articulo)
        {
            _repository.Update(articulo);
        }
    }
}
