using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
