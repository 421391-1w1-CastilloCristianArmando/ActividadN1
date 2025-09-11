using ActividadN1.Data.IRepository;
using ActividadN1.Data.Utils;
using ActividadN1.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly SqlTransaction _transaction;

        public InvoiceRepository()
        {            
        }

        public InvoiceRepository(SqlTransaction transaction)
        {
            _transaction = transaction;
        }
        public bool Delete(int id)
        {            
            var param = new List<Parameters>
            {
                new Parameters("@id_factura", id)
            };
            DataHelper.GetInstance().ExecuteSPDML("SP_ELIMINAR_DETALLES_POR_FACTURA", param, _transaction);
            int row = DataHelper.GetInstance().ExecuteSPDML("SP_ELIMINAR_FACTURA", param, _transaction);
            return row > 0;
            
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> list = new List<Invoice>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURAS");
            foreach (DataRow row in dt.Rows)
            {
                Invoice invoice = new Invoice()
                {
                    Id = (int)row["id_factura"],
                    InvoiceNumber = (int)row["nro_factura"],
                    Date = (DateTime)row["fecha"],
                    IdPayment = (int)row["id_pago"],
                    Customer = (string)row["cliente"]
                };
                list.Add(invoice);
            }
            return list;
        }

        public Invoice GetById(int id)
        {
            var param = new List<Parameters>
            {
                new Parameters("@id", id)
            };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURA_POR_ID", param);
            
            if(dt == null || dt.Rows.Count == 0)
            {
                throw new InvalidOperationException($"No existe una factura con Id {id}");
            }
            var row = dt.Rows[0];
            Invoice invoice = new Invoice
            {
                Id = (int)row["id_factura"],
                InvoiceNumber = (int)row["nro_factura"],
                Date = (DateTime)row["fecha"],
                IdPayment = (int)row["id_pago"],
                Customer = (string)row["cliente"]
            };
            var pD = new List<Parameters> { new Parameters("@id_factura", id) };
            var dtD = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_DETALLES_POR_FACTURA", pD);
            foreach (DataRow rowD in dtD.Rows)
            {
                DetailInvoice detail = new DetailInvoice
                {
                    ArticleId = (int)rowD["id_articulo"],
                    ArticleName = (string)rowD["articulo"],
                    UnitPrice = (decimal)rowD["precio_unitario"],
                    Quantity = (int)rowD["cantidad"],                    
                };
                invoice.AddItem(detail.ArticleId, detail.ArticleName, detail.UnitPrice, detail.Quantity);                               
            }
            return invoice;

        }

        public bool Save(Invoice invoice)
        {            
            var param = new List<SqlParameter>
            {
                new SqlParameter("@nro_factura", invoice.InvoiceNumber),                
                new SqlParameter("@id_pago", invoice.IdPayment),
                new SqlParameter("@cliente", invoice.Customer),
                new SqlParameter("@id", SqlDbType.Int){ Direction = ParameterDirection.Output }
            };
            int rowM = DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_FACTURA", param, _transaction);            
            invoice.Id = (int)param[3].Value;

            bool allDetailsOk = true;
            foreach (var d in invoice.Details)
            {
                var pd = new List<Parameters>
                {
                    new Parameters("@id_factura", invoice.Id),
                    new Parameters("@id_articulo", d.ArticleId),
                    new Parameters("@precio_unitario", d.UnitPrice),
                    new Parameters("@cantidad", d.Quantity),
                };
                int rowD = DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_DETALLE_FACTURA", pd, _transaction);
                if (rowD <= 0) 
                    allDetailsOk = false;
            }
            return rowM > 0 && allDetailsOk;
            
        }

        public bool Update(Invoice invoice, UpdateMode mode)
        {
            if (invoice == null || invoice.Id <= 0)
                return false;
            var param = new List<Parameters>
            {
                new Parameters ("@id", invoice.Id),
                new Parameters ("@nro_factura", invoice.InvoiceNumber),
                new Parameters ("@id_pago", invoice.IdPayment),
                new Parameters ("@cliente", invoice.Customer),
            };
            int dt = DataHelper.GetInstance().ExecuteSPDML("SP_ACTUALIZAR_FACTURA", param, _transaction);
            if (dt <= 0)
                return false;

            if (mode == UpdateMode.HeaderOnly)
                return true;
            var paramDel = new List<Parameters> { new Parameters("@id_factura", invoice.Id) };
            DataHelper.GetInstance().ExecuteSPDML("SP_ELIMINAR_DETALLES_POR_FACTURA", paramDel, _transaction);
            if (invoice.Details == null || invoice.Details.Count == 0)
                return false;
            bool allOk = true;
            foreach (var detail in invoice.Details)
            {
                var paramDetails = new List<Parameters>
                {
                    new Parameters ("@id_factura", invoice.Id),
                    new Parameters ("@id_articulo", detail.ArticleId),
                    new Parameters ("@cantidad", detail.Quantity),
                    new Parameters ("@precio_unitario", detail.UnitPrice),
                };
                int rcD = DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_DETALLE_FACTURA", paramDetails, _transaction);
                allOk = allOk && (rcD > 0);
            }
            return allOk;  
        }
    }
}
