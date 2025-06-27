using Microsoft.AspNetCore.Mvc;

namespace BlogStore.ViewComponents.AdminViewComponents
{
    public class _AdminArticleStatisticsViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Burada istatistik verilerini alabilir ve View'a gönderebilirsiniz.
            // Örnek olarak, basit bir model oluşturup gönderebiliriz.
            var statistics = new
            {
                TotalArticles = 100, // Örnek veri
                TotalComments = 250, // Örnek veri
                TotalUsers = 50      // Örnek veri
            };
            return View(statistics);
        }
    }
}
