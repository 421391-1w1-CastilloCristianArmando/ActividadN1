using WebApiEFDBF.Models;

namespace WebApiEFDBF.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        private FacturacionDBContext _context;

        public FacturaRepository(FacturacionDBContext context)
        {
            _context = context;
        }
        public void Create(Factura factura)
        {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var del = GetById(id);
            if (del != null)
            {
                _context.Facturas.Remove(del);
                _context.SaveChanges();
            }
        }

        public List<Factura> GetAll()
        {
            return _context.Facturas.ToList();
        }

        public Factura? GetById(int id)
        {
            return _context.Facturas.Find(id);
        }

        public void Update(Factura factura)
        {
            if (factura != null)
            {
                _context.Facturas.Update(factura);
                _context.SaveChanges();
            }
        }
    }
}
