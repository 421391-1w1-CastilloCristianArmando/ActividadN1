using WebApiEFDBF.Models;
using WebApiEFDBF.Repository;

namespace WebApiEFDBF.Service
{
    public class FacturaService : IFacturaService
    {
        private IFacturaRepository _repository;

        public FacturaService(IFacturaRepository repository)
        {
            _repository = repository;
        }
        public void Create(Factura factura)
        {
            _repository.Create(factura);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Factura> GetAll()
        {
            return _repository.GetAll();
        }

        public Factura? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Factura factura)
        {
            _repository.Update(factura);
        }
    }
}
