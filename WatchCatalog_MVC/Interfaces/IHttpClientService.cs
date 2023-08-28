using WatchCatalog_MVC.DTOs;
using WatchCatalog_MVC.Helpers;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.Interfaces
{
    public interface IHttpClientService
    {
        Task<WatchesPaginationViewModel> GetPaginatedWatchesAsync(WatchPageParameters pageParams);

        Task<WatchDetailsViewModel> GetWatchDetailsAsync(int id);

        Task<WatchDetailsViewModel> ToggleWatchAsync(ToggleWatchDTO watch);

        Task<WatchDetailsViewModel> UpdateWatchAsync(UpdateWatchDTO watch);

        Task<WatchDetailsViewModel> AddWatchAsync(AddWatchDTO watch);

        Task<WatchDetailsViewModel> DeleteWatchAsync(int id);
    }
}
