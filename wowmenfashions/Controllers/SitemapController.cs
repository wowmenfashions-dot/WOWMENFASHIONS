using Microsoft.AspNetCore.Mvc;
using System.Text;
using wowmenfashions.Services;

namespace wowmenfashions.Controllers;

[Route("sitemap.xml")]
public class SitemapController : Controller
{
    private readonly IProductService _productService;

    public SitemapController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        
        var xml = new StringBuilder();
        xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        xml.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        // Core pages
        xml.AppendLine($"  <url><loc>{baseUrl}/</loc><changefreq>daily</changefreq><priority>1.0</priority></url>");
        xml.AppendLine($"  <url><loc>{baseUrl}/shop</loc><changefreq>daily</changefreq><priority>0.9</priority></url>");
        xml.AppendLine($"  <url><loc>{baseUrl}/cart</loc><changefreq>weekly</changefreq><priority>0.5</priority></url>");

        // Categories
        var categories = await _productService.GetCategoriesAsync();
        foreach (var category in categories)
        {
            var slug = category.ToLower().Replace(" ", "-");
            xml.AppendLine($"  <url><loc>{baseUrl}/category/{slug}</loc><changefreq>weekly</changefreq><priority>0.8</priority></url>");
        }

        // Products
        var result = await _productService.GetProductsAsync(pageSize: 1000);
        foreach (var product in result.Products)
        {
            xml.AppendLine($"  <url><loc>{baseUrl}/product/{product.Id}</loc><changefreq>weekly</changefreq><priority>0.7</priority></url>");
        }

        xml.AppendLine("</urlset>");

        return Content(xml.ToString(), "application/xml", Encoding.UTF8);
    }
}
