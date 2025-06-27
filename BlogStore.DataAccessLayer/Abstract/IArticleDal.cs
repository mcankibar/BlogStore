using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface IArticleDal : IGenericDal<Article>
    {
        List<Article> GetArticlesWithCategory();
        public AppUser GetAppUserByArticleId(int id);

        List<Article> GetTop3PopularArticles();

        List<Article> GetArticlesByAppUserId(string id);

        Article GetArticlesBySlug(string slug);
    }
}
