using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents;

public class _ScriptsLayoutComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}