using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IArticleService _articleService;

        private readonly ICategoryService _categoryService;

        private readonly ICommentService _commentService;

        private readonly UserManager<AppUser> _userManager;

        private readonly ISlugService _slugService;




        public AdminController(IArticleService articleService, ICategoryService categoryService, ICommentService commentService, UserManager<AppUser> userManager, ISlugService slugService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
            _slugService = slugService;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var articles = _articleService.TGetArticlesByAppUserId(userId).ToList();

            ViewBag.articleCount = articles.Count;
            ViewBag.categoryCount = articles.Select(a => a.CategoryId).Distinct().Count();

            var articleIds = articles.Select(a => a.ArticleId).ToList();
            var allComments = _commentService.TGetAll();
            var userArticleComments = allComments.Where(c => articleIds.Contains(c.ArticleId)).ToList();

            ViewBag.userTotalCommentCount = userArticleComments.Count;

            
            var mostCommentedArticle = articles
                .Select(a => new
                {
                    Article = a,
                    CommentCount = userArticleComments.Count(c => c.ArticleId == a.ArticleId)
                })
                .OrderByDescending(x => x.CommentCount)
                .FirstOrDefault();

            ViewBag.mostCommentedArticleTitle = mostCommentedArticle?.Article.Title ?? "-";
            ViewBag.mostCommentedArticleCount = mostCommentedArticle?.CommentCount ?? 0;

            
            var newestArticle = articles.OrderByDescending(a => a.CreatedDate).FirstOrDefault();
            ViewBag.newestArticleTitle = newestArticle?.Title ?? "-";
            ViewBag.newestArticleDate = newestArticle?.CreatedDate.ToString("dd.MM.yyyy") ?? "-";

            
            ViewBag.avgArticleLength = articles.Any()
                ? (int)articles.Average(a => (a.Description ?? "").Length)
                : 0;

            
            int positiveCount = userArticleComments.Count(c => !c.IsToxic);
            ViewBag.positiveCommentRate = userArticleComments.Count > 0
                ? (int)(100.0 * positiveCount / userArticleComments.Count)
                : 0;



           

            var allUsers = _userManager.Users.ToList();

            var lastComments = userArticleComments
                .OrderByDescending(c => c.CommentDate)
                .Take(5)
                .Select(c => {
                    var user = allUsers.FirstOrDefault(u => u.Id == c.AppUserId);
                    return new
                    {
                        UserName = c.AppUser.Name+" "+c.AppUser.Surname,
                        ArticleTitle = articles.FirstOrDefault(a => a.ArticleId == c.ArticleId)?.Title ?? "-",
                        Score = c.ToxicityScore,
                        Date = c.CommentDate.ToString("dd.MM.yyyy HH:mm"),
                        Detail = c.CommentDetail,
                        UserImageUrl = user?.ImageUrl // Kullanıcıdan doğrudan alınıyor
                    };
                })
                .ToList();

           

            ViewBag.lastComments = lastComments;


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
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult ArticleList()
        {
            var userId = _userManager.GetUserId(User);
            var articles = _articleService.TGetArticlesByAppUserId(userId).ToList();
            var allComments = _commentService.TGetAll();

            var model = articles.Select(a =>
            {
                var comments = allComments.Where(c => c.ArticleId == a.ArticleId).ToList();
                int commentCount = comments.Count;
                int positiveCount = comments.Count(c => !c.IsToxic);
                int positiveRate = commentCount > 0 ? (int)(100.0 * positiveCount / commentCount) : 0;

                return new MyArticleViewModel
                {
                    Title = a.Title,
                    CommentCount = commentCount,
                    PositiveCommentRate = positiveRate,
                    Slug = a.Slug 
                };
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.username = user.UserName;
            ViewBag.name = user.Name;
            ViewBag.surname = user.Surname;
            ViewBag.id = user.Id;
            ViewBag.imageUrl = user.ImageUrl; // Kullanıcı profil resmini de ekleyelim
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["PasswordMessage"] = "Lütfen tüm alanları doldurun.";
                return RedirectToAction("Profile");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                TempData["PasswordMessage"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Profile");
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["PasswordMessage"] = "Yeni şifreler eşleşmiyor.";
                return RedirectToAction("Profile");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["PasswordMessage"] = "Şifreniz başarıyla değiştirildi.";
            }
            else
            {
                TempData["PasswordMessage"] = string.Join(" ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction("Profile");
        }
    }



    public class MyArticleViewModel
    {
        public string Title { get; set; }
        public int CommentCount { get; set; }
        public int PositiveCommentRate { get; set; } 

        public string Slug { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
