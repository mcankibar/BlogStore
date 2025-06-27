using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailMainCoverImageComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;
        public _ArticleDetailMainCoverImageComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public IViewComponentResult Invoke(int id)
        {
            Article article = _articleService.TGetById(id);
            
            AppUser value = _articleService.TGetAppUserByArticleId(id);
            ViewBag.AuthorName = value.Name + " " + value.Surname;
            ViewBag.AuthorImage = value.ImageUrl;
            

            return View(article);
        }
    }
}
