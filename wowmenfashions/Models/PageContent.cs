namespace wowmenfashions.Models
{
    public class PageContent
    {
        public int Id { get; set; }
        public string PageName { get; set; } = string.Empty;
        public string ContentHtml { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}
