using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailAboutAuthorComponentPartial : ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleDetailAboutAuthorComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }


        public IViewComponentResult Invoke(int id)
        {
            AppUser value = _articleService.TGetAppUserByArticleId(id);
            return View(value);
        }
    }
}
