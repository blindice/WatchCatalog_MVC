using Microsoft.AspNetCore.Mvc;
using WatchCatalog_MVC.Interfaces;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.ViewComponents
{
    public class ModalBodyViewComponent: ViewComponent
    {
        readonly IHttpClientService _httpClientService;

        public ModalBodyViewComponent(IHttpClientService httpClientService) => _httpClientService = httpClientService;

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var watch = new WatchDetailsViewModel();
            if (id != null)
                watch = await _httpClientService.GetWatchDetailsAsync((int)id!);            

            return View<WatchDetailsViewModel>(watch);
        }
    }
}
