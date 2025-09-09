using ActividadN1.Data;
using ActividadN1.Data.IRepository;
using ActividadN1.Data.Utils;
using ActividadN1.Domain;
using ActividadN1.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Service
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _repository;

        public InvoiceService()
        {
            _repository = new InvoiceRepository();
        }
        public bool Delete(int id)
        {
            if (id <= 0) 
                return false;

            using var uow = new UnitOfWork();
            var repo = new InvoiceRepository(uow.Transaction);

            var ok = repo.Delete(id);
            if (ok) 
                uow.SaveChanges();
            return ok;
        }

        public List<Invoice> GetAll()
        {
            return _repository.GetAll();
        }

        public Invoice GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Save(Invoice invoice)
        {
            if (invoice == null) 
                return false;

            using var uow = new UnitOfWork();
            var repo = new InvoiceRepository(uow.Transaction);
            var ok = repo.Save(invoice);
            if (ok) 
                uow.SaveChanges();
            return ok;
        }

        public bool Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
