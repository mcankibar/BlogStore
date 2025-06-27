using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class SlugManager : ISlugService
    {
        private readonly BlogContext _context;

        public SlugManager(BlogContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateUniqueSlugAsync(string title, int? excludeArticleId = null)
        {
            string baseSlug = GenerateSlugInternal(title);
            string uniqueSlug = baseSlug;
            int counter = 1;

            
            while (await _context.Articles.AnyAsync(a => a.Slug == uniqueSlug && a.ArticleId != excludeArticleId))
            {
                uniqueSlug = $"{baseSlug}-{counter}";
                counter++;
            }
            return uniqueSlug;
        }

        private static string GenerateSlugInternal(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "";

            string slug = title.ToLowerInvariant();
            slug = slug.Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");
            slug = slug.Trim('-');

            return slug;
        }
    }
}
