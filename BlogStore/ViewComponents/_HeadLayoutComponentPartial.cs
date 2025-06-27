using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents;

public class _HeadLayoutComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}