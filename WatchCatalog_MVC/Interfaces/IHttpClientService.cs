using WatchCatalog_MVC.Helpers;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.Interfaces
{
    public interface IHttpClientService
    {
        Task<WatchesPaginationViewModel> GetPaginatedWatchesAsync(WatchPageParameters pageParams);

        Task<WatchDetailsViewModel> GetWatchDetailsAsync(int id);
    }
}
