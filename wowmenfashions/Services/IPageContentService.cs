using wowmenfashions.Models;

namespace wowmenfashions.Services
{
    public interface IPageContentService
    {
        Task<PageContent?> GetContentByNameAsync(string pageName);
        Task UpdateContentAsync(string pageName, string contentHtml);
    }
}
