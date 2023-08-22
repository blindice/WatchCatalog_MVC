using Microsoft.AspNetCore.Mvc;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.ViewComponents
{
    public class ModalBodyViewComponent: ViewComponent
    {
        public ModalBodyViewComponent()
        {
            
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7093/api/Watch/getwatchbyid/{id}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var watch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

            return View<WatchDetailsViewModel>(watch);
        }
    }
}
