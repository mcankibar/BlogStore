using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>,IArticleDal
    {
        private readonly BlogContext _context;
        public EfArticleDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Article> GetArticlesWithCategory()
        {
            return _context.Articles.Include(x => x.Category).ToList();
        }

        public AppUser GetAppUserByArticleId(int id)
        {
            var value = _context.Articles.Where(x => x.ArticleId == id).Select(y => y.AppUserId).FirstOrDefault();
            var userValue = _context.Users.FirstOrDefault(x => x.Id == value);
            return userValue;
        }

        public List<Article> GetTop3PopularArticles()
        {
            var values = _context.Articles.OrderByDescending(x => x.ArticleId).Take(3).ToList();
            return values;
        }

        public List<Article> GetArticlesByAppUserId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new List<Article>();
            }
            else
            {
                return _context.Articles.Where(x => x.AppUserId == id).ToList();
            }
        }

        public Article GetArticlesBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }
            else
            {
                return _context.Articles.FirstOrDefault(x => x.Slug == slug);

            }
        }
    }


}
