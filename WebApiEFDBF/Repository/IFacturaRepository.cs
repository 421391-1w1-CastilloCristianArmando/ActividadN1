using WebApiEFDBF.Models;

namespace WebApiEFDBF.Repository
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();
        Factura? GetById(int id);
        void Create(Factura factura);
        void Update(Factura factura);
        void Delete(int id);

    }
}
