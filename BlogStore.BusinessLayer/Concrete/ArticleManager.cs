using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.EntityFramework;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public void TInsert(Article entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Article cannot be null.");
            }

           

            if (entity.Description.Length >= 3  && entity.Description.Length < 10000)
            {
                _articleDal.Insert(entity);
            }

            else
            {
                throw new ArgumentException("Article is not valid.");
            }
        }


        public void TUpdate(Article entity)
        {
            _articleDal.Update(entity);
        }

        public void TDelete(int id)
        {
            _articleDal.Delete(id);
        }

        public List<Article> TGetAll()
        {
            return _articleDal.GetAll();
        }

        public Article TGetById(int id)
        {
            var article = _articleDal.GetById(id);
           

            return article;
        }

        public List<Article> TGetArticlesWithCategory()
        {
            return _articleDal.GetArticlesWithCategory();
        }

        public AppUser TGetAppUserByArticleId(int id)
        {
            
            
            return _articleDal.GetAppUserByArticleId(id);
        }

        public List<Article> TGetTop3PopularArticles()
        {
            return _articleDal.GetTop3PopularArticles();

        }

        public List<Article> TGetArticlesByAppUserId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new List<Article>();
            }
            else
            {
                return _articleDal.GetArticlesByAppUserId(id);
            }
        }

        public Article TGetArticlesBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }
            else
            {
                return _articleDal.GetArticlesBySlug(slug);
            }
        }
    }
}
