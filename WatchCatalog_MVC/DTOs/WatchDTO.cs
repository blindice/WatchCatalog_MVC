namespace WatchCatalog_MVC.DTOs
{
    public class WatchDTO
    {
        public int WatchId { get; set; }

        public string WatchName { get; set; } = string.Empty;

        public string Image { get; set; } = null!;

        public string Short_description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool? IsActive { get; set; }
    }
}
