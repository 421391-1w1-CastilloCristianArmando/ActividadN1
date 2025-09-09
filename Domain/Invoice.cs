using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Domain
{
    public class Invoice : Identifiable
    {
        public int InvoiceNumber { get; set; }        
        public DateTime Date { get; set; }
        public int IdPayment { get; set; }
        public string Customer { get; set; } = string.Empty;
        public List<DetailInvoice> Details { get; set; }

        public Invoice()
        {
            Details = new List<DetailInvoice>();
        }        

        public void AddItem(int articleId, string articleName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0) return;

            var existing = Details.FirstOrDefault(d => d.ArticleId == articleId);
            if (existing is null)
            {
                Details.Add(new DetailInvoice
                {
                    ArticleId = articleId,
                    ArticleName = articleName,
                    UnitPrice = unitPrice,
                    Quantity = quantity
                });
            }
            else
            {
                existing.Quantity += quantity;
            }
        }
        public void RemoveItem(int articleId)
        {
            var it = Details.FirstOrDefault(d => d.ArticleId == articleId);
            if (it != null) Details.Remove(it);
        }

        public decimal Total => Details.Sum(d => d.Subtotal);

    }
}
