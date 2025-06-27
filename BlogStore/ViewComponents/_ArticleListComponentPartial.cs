using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents;

public class _ArticleListComponentPartial : ViewComponent
{
    private readonly IArticleService _articleService;
    
public _ArticleListComponentPartial(IArticleService articleService)
    {
        _articleService = articleService;
    }
    public IViewComponentResult Invoke()
    {
        var articles = _articleService.TGetArticlesWithCategory();
        return View(articles);
    }
    
}