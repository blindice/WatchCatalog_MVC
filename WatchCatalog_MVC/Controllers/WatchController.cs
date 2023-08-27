using MessagePack.Resolvers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using WatchCatalog_MVC.DTOs;
using WatchCatalog_MVC.Helpers;
using WatchCatalog_MVC.Interfaces;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.Controllers
{
    public class WatchController : Controller
    {
        readonly IHttpClientService _httpClientService;

        public WatchController(IHttpClientService httpClientService) => _httpClientService = httpClientService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getwatches")]
        public async Task<IActionResult> GetWatchesAsync([FromQuery] WatchPageParameters pageParams)
        {
            var paginatedWatches = await _httpClientService.GetPaginatedWatchesAsync(pageParams);

            return Ok(paginatedWatches);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var watch = await _httpClientService.GetWatchDetailsAsync((int)id!);

            return View(watch);
        }

        [HttpPost("togglewatch")]
        public async Task<IActionResult> Toggle([FromBody] ToggleWatchDTO watch)
        {
            var modifiedWatch = await _httpClientService.ToggleWatchAsync(watch);

            return Ok(modifiedWatch);
        }

        [HttpPut("updatewatch")]
        public async Task<IActionResult> Update([FromForm] UpdateWatchDTO watch)
        {
            var modifiedWatch = await _httpClientService.UpdateWatchAsync(watch);


            return Ok(modifiedWatch);
        }

        [HttpPost("addwatch")]
        public async Task<IActionResult> Add([FromForm] AddWatchDTO watch)
        {
            var modifiedWatch = await _httpClientService.AddWatchAsync(watch);

            return Ok(modifiedWatch);
        }

        [HttpDelete("deletewatch/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7093/api/Watch/deletewatch/{id}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return Ok();
        }

        [HttpPost]
        public IActionResult ModalBody([FromBody] int? id)
        {
            return ViewComponent("ModalBody", id);
        }
    }
}
