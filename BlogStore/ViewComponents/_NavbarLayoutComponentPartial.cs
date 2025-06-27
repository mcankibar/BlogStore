using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents;

public class _NavbarLayoutComponentPartial : ViewComponent
{
    
    public IViewComponentResult Invoke()
    {
        return View();
    }
    
}