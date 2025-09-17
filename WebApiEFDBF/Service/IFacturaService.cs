using WebApiEFDBF.Models;

namespace WebApiEFDBF.Service
{
    public interface IFacturaService
    {
        List<Factura> GetAll();
        Factura? GetById(int id);
        void Create (Factura factura);
        void Update (Factura factura);
        void Delete (int id);
    }
}
