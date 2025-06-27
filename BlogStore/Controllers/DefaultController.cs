using Microsoft.AspNetCore.Mvc;

namespace BlogStore.Controllers;

public class DefaultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}