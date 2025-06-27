using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface ISlugService
    {
        Task<string> GenerateUniqueSlugAsync(string title, int? excludeArticleId = null);
    }
}
