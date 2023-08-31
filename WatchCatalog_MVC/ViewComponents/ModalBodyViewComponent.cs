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
            //var watch = new WatchDetailsViewModel();
            //if (id != null)
            //{
            //    var client = new HttpClient();
            //    var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7093/api/Watch/getwatchbyid/{id}");
            //    var response = await client.SendAsync(request);
            //    response.EnsureSuccessStatusCode();
            //    watch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

            var watch = await _httpClientService.GetWatchDetailsAsync((int)id!);

            return View<WatchDetailsViewModel>(watch);
        }
    }
}
