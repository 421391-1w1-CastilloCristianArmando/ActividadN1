using WebApiEFDBF.Models;

namespace WebApiEFDBF.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private FacturacionDBContext _context;

        public ArticuloRepository(FacturacionDBContext context)
        {
            _context = context;
        }
        public void Create(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var art = GetById(id);
            if (art != null)
            {
                _context.Articulos.Remove(art);
                _context.SaveChanges();
            }
        }

        public List<Articulo> GetAll()
        {
            return _context.Articulos.ToList();
        }

        public Articulo? GetById(int id)
        {
            return _context.Articulos.Find(id);
        }

        public void Update(Articulo articulo)
        {
            if (articulo != null)
            {
                _context.Articulos.Update(articulo);
                _context.SaveChanges();
            }
        }
    }
}
