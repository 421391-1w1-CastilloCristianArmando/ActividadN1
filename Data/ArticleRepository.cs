using ActividadN1.Data.IRepository;
using ActividadN1.Data.Utils;
using ActividadN1.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Data
{
    public class ArticleRepository : IArticleRepository
    {
        public bool Detele(int id)
        {
            var param = new List<Parameters>
            { 
                new Parameters("id", id) 
            };
            int rowAffected = DataHelper.GetInstance().ExecuteSPDML("SP_ELIMINAR_PRODUCTO", param);
            return rowAffected > 0;
            
        }

        public List<Article> GetAll()
        {
            List<Article> list = new List<Article>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTOS");
            foreach (DataRow row in dt.Rows)
            {
                Article article = new Article()
                {
                    Id = (int)row["id"],
                    Name = (string)row["nombre"],
                    Price = (decimal)row["precio"]
                };
                list.Add(article);
            }
            return list;
        }

        public Article GetById(int id)
        {
            var param = new List<Parameters>
            {
                new Parameters("id", id)
            };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTO_POR_CODIGO", param);
            if (dt != null && dt.Rows.Count > 0)
            { 
                var row = dt.Rows[0];
                Article article = new Article()
                {
                    Id = (int)row["id"],
                    Name = (string)row["nombre"],
                    Price = (decimal)row["precio"]
                };
                return article;
            }
            throw new InvalidOperationException($"No existe un articulo con Id {id}");
        }

        public bool Save(Article article)
        {
            var param = new List<Parameters>
            {
                new Parameters("id", article.Id),
                new Parameters("nombre", article.Name),
                new Parameters("precio", article.Price)
            };
            try
            {
                int row = DataHelper.GetInstance().ExecuteSPDML("SP_GUARDAR_PRODUCTO", param);
                return row > 0;
                
            }
            catch (SqlException)
            {
                return false;
            }
           
        }

        public bool Update(Article article)
        {
            var param = new List<Parameters>
            {
                new Parameters("id", article.Id),
                new Parameters("nombre", article.Name),
                new Parameters("precio", article.Price)
            };
            try
            {
                int row = DataHelper.GetInstance().ExecuteSPDML("SP_ACTUALIZAR_PRODUCTO", param);
                return row > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
