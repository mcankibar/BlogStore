using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IArticleService :IGenericService<Article>
    {
        public List<Article> TGetArticlesWithCategory();

        public AppUser TGetAppUserByArticleId(int id);

        public List<Article> TGetTop3PopularArticles();

        public List<Article> TGetArticlesByAppUserId(string id);

        public Article TGetArticlesBySlug(string slug);
    }
}
