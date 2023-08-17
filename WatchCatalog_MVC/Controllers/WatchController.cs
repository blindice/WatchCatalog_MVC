using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WatchCatalog_MVC.DTOs;
using WatchCatalog_MVC.Helpers;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.Controllers
{
    public class WatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getwatches")]
        public async Task<IActionResult> GetWatchesAsync([FromQuery] WatchPageParameters pageParams)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get
                ,$"https://localhost:7093/api/Watch/getwatches?PageNumber={pageParams.PageNumber}&PageSize={pageParams.PageSize}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var watches = await response.Content.ReadFromJsonAsync<List<WatchDTO>>();

            return Ok(watches);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7093/api/Watch/getwatchbyid/{id}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var watch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

            return View(watch);
        }

        [HttpPost("togglewatch")]
        public async Task<IActionResult> Toggle([FromBody]ToggleWatchDTO watch)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7093/api/Watch/togglewatch");
            var content = JsonConvert.SerializeObject(watch);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            request.Content = data;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var modifiedWatch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

            return Ok(modifiedWatch);
        }

        [HttpPut("updatewatch")]
        public async Task<IActionResult> Update([FromForm]UpdateWatchDTO watch)
        {
            return NoContent();
        }
    }
}
