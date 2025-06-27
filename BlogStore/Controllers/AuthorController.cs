using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogStore.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IArticleService _articleService;

        private readonly ICategoryService _categoryService;

        private readonly ISlugService _slugService;

        private readonly UserManager<AppUser> _userManager;

        public AuthorController(IArticleService articleService, ICategoryService categoryService,ISlugService slugService,
            UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
            _slugService = slugService;
        }

        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.name = user.Name;
            ViewBag.surname = user.Surname;
            ViewBag.id = user.Id;
            return View();
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            List<SelectListItem> values = (from x in _categoryService.TGetAll()
                select new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                }).ToList();
            ViewBag.v = values;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            article.AppUserId = user.Id;
            article.CreatedDate = DateTime.Now;
            article.Slug = await _slugService.GenerateUniqueSlugAsync(article.Title);

            _articleService.TInsert(article);
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        public async Task<IActionResult> MyArticleList()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _articleService.TGetArticlesByAppUserId(user.Id);
            return View(values);
        }
    }
}
