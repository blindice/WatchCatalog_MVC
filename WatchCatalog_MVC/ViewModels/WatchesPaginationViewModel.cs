using WatchCatalog_MVC.DTOs;

namespace WatchCatalog_MVC.ViewModels
{
    public class WatchesPaginationViewModel
    {
        public List<WatchDTO>? WatchDTOs { get; set; } = new List<WatchDTO>();
        
        public int TotalCount { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public bool HasNext { get; set; }

        public bool HasPrevious { get; set; }
    }
}
