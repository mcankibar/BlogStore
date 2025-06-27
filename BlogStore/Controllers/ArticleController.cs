using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogStore.Controllers;

public class ArticleController : Controller
{
    private readonly BlogContext _context; 

    private readonly IArticleService _articleService;

    

    public ArticleController(BlogContext context, IArticleService articleService)
    {
        _context = context;
        _articleService = articleService;
        
    }
    // GET
    public IActionResult ArticleDetail(string slug)
    {
        if (string.IsNullOrEmpty(slug))
        {
            return NotFound(); 
        }

        var article = _articleService.TGetArticlesBySlug(slug);

        if (article == null)
        {
            return NotFound();
        }

        

        

        return View(article.ArticleId); 
    }

}