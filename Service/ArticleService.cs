using ActividadN1.Data;
using ActividadN1.Data.IRepository;
using ActividadN1.Domain;
using ActividadN1.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Service
{
    public class ArticleService : IArticleService
    {
        private IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }
        public bool Detele(int id)
        {
            return _repository.Detele(id);
        }

        public List<Article> GetAll()
        {
            return _repository.GetAll();
        }

        public Article GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Save(Article article)
        {
            return _repository.Save(article);
        }

        public bool Update(Article article)
        {
            return _repository.Update(article);
        }
    }
}
