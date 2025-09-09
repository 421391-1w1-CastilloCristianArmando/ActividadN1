using ActividadN1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Data.IRepository
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        Article GetById(int id);
        bool Detele(int id);
        bool Save(Article article);
        bool Update (Article article);
    }
}
