using MessagePack.Resolvers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
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
                , $"https://localhost:7093/api/Watch/getwatches?PageNumber={pageParams.PageNumber}&PageSize={pageParams.PageSize}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<WatchesPaginationViewModel>(response.Headers.GetValues("X-Pagination").First());
            result.WatchDTOs = await response.Content.ReadFromJsonAsync<List<WatchDTO>>();

            return Ok(result);
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
        public async Task<IActionResult> Toggle([FromBody] ToggleWatchDTO watch)
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
        public async Task<IActionResult> Update([FromForm] UpdateWatchDTO watch)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7093/api/watch/updatewatch");
            var content = new MultipartFormDataContent();

            if (watch.Image != null)
            {
                var fileContent = new StreamContent(watch.Image.OpenReadStream());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(watch.Image.ContentType);
                content.Add(fileContent, "image", watch.Image.FileName);
            }

            content.Add(new StringContent(watch.WatchName), "watchname");
            content.Add(new StringContent(watch.Case), "case");
            content.Add(new StringContent(watch.Jewel), "jewel");
            content.Add(new StringContent(watch.Strap), "strap");
            content.Add(new StringContent(watch.Caliber), "caliber");
            content.Add(new StringContent(watch.Full_Description), "full_description");
            content.Add(new StringContent(watch.Short_description), "short_description");
            content.Add(new StringContent(watch.WatchId.ToString()), "watchId");
            content.Add(new StringContent(watch.Chronograph), "Chronograph");
            content.Add(new StringContent(watch.Price.ToString()), "price");
            content.Add(new StringContent(watch.Height.ToString()), "height");
            content.Add(new StringContent(watch.Weight.ToString()), "weight");
            content.Add(new StringContent(watch.Diameter.ToString()), "diameter");
            content.Add(new StringContent(watch.IsActive.ToString()), "isActive");
            content.Add(new StringContent(watch.Thickness.ToString()), "thickness");
            request.Content = content;
            var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();


            return Ok();
        }

        [HttpPost("addwatch")]
        public async Task<IActionResult> Add([FromForm] AddWatchDTO watch)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7093/api/Watch/createwatch");
            var content = new MultipartFormDataContent();

            if (watch.Image != null)
            {
                var fileContent = new StreamContent(watch.Image.OpenReadStream());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(watch.Image.ContentType);
                content.Add(fileContent, "image", watch.Image.FileName);
            }

            content.Add(new StringContent(watch.WatchName), "watchname");
            content.Add(new StringContent(watch.Case), "case");
            content.Add(new StringContent(watch.Jewel), "jewel");
            content.Add(new StringContent(watch.Strap), "strap");
            content.Add(new StringContent(watch.Caliber), "caliber");
            content.Add(new StringContent(watch.Full_Description), "full_description");
            content.Add(new StringContent(watch.Short_description), "short_description");
            content.Add(new StringContent(watch.Chronograph), "Chronograph");
            content.Add(new StringContent(watch.Price.ToString()), "price");
            content.Add(new StringContent(watch.Height.ToString()), "height");
            content.Add(new StringContent(watch.Weight.ToString()), "weight");
            content.Add(new StringContent(watch.Diameter.ToString()), "diameter");
            content.Add(new StringContent(watch.Thickness.ToString()), "thickness");
            request.Content = content;
            var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();


            return Ok();
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
