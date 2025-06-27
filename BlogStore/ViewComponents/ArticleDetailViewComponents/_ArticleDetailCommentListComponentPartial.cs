using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailCommentListComponentPartial : ViewComponent
        
    {
        private readonly ICommentService _commentService;
        public _ArticleDetailCommentListComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        public IViewComponentResult Invoke(int id)
        {
            var values = _commentService.TGetCommentsByArticle(id).Where(x=>x.IsToxic==false).ToList();
            ViewBag.ArticleId = id;
            ViewBag.AppUser = User.Identity;
            
            // userId ile işlemler...
            return View(values);
        }
    }
}
