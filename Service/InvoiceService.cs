using ActividadN1.Data;
using ActividadN1.Data.IRepository;
using ActividadN1.Data.Utils;
using ActividadN1.Domain;
using ActividadN1.Service.IService;
using Microsoft.Data.SqlClient;
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
        private readonly Func<SqlTransaction, IInvoiceRepository> _repoFactory;

        public InvoiceService(IInvoiceRepository repository, Func<SqlTransaction, IInvoiceRepository> repoFactory)
        {
            _repository = repository;
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

        public bool Update(Invoice invoice, UpdateMode mode)
        {
            if (invoice == null || invoice.Id <= 0)
                return false;
            using var uow = new UnitOfWork();
            var repo = new InvoiceRepository(uow.Transaction);

            var ok = repo.Update(invoice, mode);
            if (ok)
                uow.SaveChanges();
            return ok;

        }
    }
}
