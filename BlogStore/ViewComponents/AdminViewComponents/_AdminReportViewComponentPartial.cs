using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.AdminViewComponents
{
    public class _AdminReportViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            
            return View();
        }



    }
}
