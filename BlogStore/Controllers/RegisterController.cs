using BlogStore.EntityLayer.Entities;
using BlogStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserRegisterViewModel userRegisterViewModel)
        {
            if (userRegisterViewModel == null)
            {
                // Log veya hata mesajı
                throw new Exception("Model null geldi!");
            }
            AppUser appUser = new AppUser()
            {
                Name = userRegisterViewModel.Name,
                Surname = userRegisterViewModel.Surname,
                UserName = userRegisterViewModel.Username,
                Email = userRegisterViewModel.Email,
                
                ImageUrl = "test",
                Description = "test description"
            };
            await _userManager.CreateAsync(appUser, userRegisterViewModel.Password);
            return RedirectToAction("UserLogin","Login");
        }
    }
}
