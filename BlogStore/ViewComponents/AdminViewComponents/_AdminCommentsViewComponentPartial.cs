using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.AdminViewComponents
{
    public class _AdminCommentsViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            
            return View();
        }
    }
}
