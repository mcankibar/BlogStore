using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.Controllers
{
    [Route("[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        private readonly UserManager<AppUser> _userManager;

        private readonly IToxicityDetectionService _toxicityDetectionService;

        private readonly ITranslationService _translationService;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager, IToxicityDetectionService toxicityDetectionService, ITranslationService translationService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _toxicityDetectionService = toxicityDetectionService;
            _translationService = translationService;
        }
        public IActionResult CommentList()
        {
            var values = _commentService.TGetAll();

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            try
            {
                if (comment == null || string.IsNullOrWhiteSpace(comment.CommentDetail))
                    return Json(new { status = "error", message = "Yorum verisi eksik veya geçersiz." });

                var translatedCommentDetail = await _translationService.TranslateToEnglishAsync(comment.CommentDetail);
                
                var detectionResult = await _toxicityDetectionService.DetectToxicityAsync(translatedCommentDetail);
                comment.IsToxic = detectionResult.IsToxic;
                comment.ToxicityScore = (float)detectionResult.Score;
                comment.AppUserId = _userManager.GetUserId(User);
                comment.UserNameSurname = _userManager.GetUserName(User);
                comment.CommentDate = DateTime.Now;
                
                _commentService.TInsert(comment);
                if (detectionResult.IsToxic)
                    return Json(new { status = "toxic", message = "Yorumunuz toksik içerik barındırıyor." });
                return Json(new { status = "success", message = "Yorumunuz başarıyla eklendi." });

            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult UpdateComment(int id)
        {
            var value = _commentService.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateComment(Comment comment)
        {
            // DEBUG: Gelen CommentId'yi kontrol et
            System.Diagnostics.Debug.WriteLine("Gelen CommentId: " + comment.Id);

            comment.CommentDate = DateTime.Now;
            comment.IsToxic = false;
            _commentService.TUpdate(comment);
            return RedirectToAction(nameof(CommentList));
        }

    }
}
