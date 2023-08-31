using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchCatalog_MVC.DTOs;
using WatchCatalog_MVC.Helpers;
using WatchCatalog_MVC.Interfaces;
using WatchCatalog_MVC.ViewModels;
using System.Net;
using System.Text;
using System.Net.Http.Headers;

namespace WatchCatalog_MVC.Services
{
    public class HttpClientService: IHttpClientService
    {
        readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService( IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<WatchesPaginationViewModel> GetPaginatedWatchesAsync(WatchPageParameters pageParams, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");

            var uri = $"getwatches?PageNumber={pageParams.PageNumber}&PageSize={pageParams.PageSize}";

            if (!string.IsNullOrEmpty(pageParams.SearchString))
                uri += $"&SearchString={pageParams.SearchString}";

            using (var response = await httpClient.GetAsync(uri, cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<WatchesPaginationViewModel>(response.Headers.GetValues("X-Pagination").First());
                result!.WatchDTOs = await response.Content.ReadFromJsonAsync<List<WatchDTO>>();

                return result;
            }
        }

        public async Task<WatchDetailsViewModel> GetWatchDetailsAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");

            using (var response = await httpClient.GetAsync($"getwatchbyid/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);

                var watch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

                return watch!;
            }
        }

        public async Task<WatchDetailsViewModel> ToggleWatchAsync(ToggleWatchDTO watch)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");
            var content = JsonConvert.SerializeObject(watch);
            var requestContent = new StringContent(content, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync("togglewatch", requestContent))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);

                var modifiedWatch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

                return modifiedWatch!;
            }
        }

        public async Task<WatchDetailsViewModel> UpdateWatchAsync(UpdateWatchDTO watch)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");
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

            using (var response = await httpClient.PutAsync("updatewatch", content))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);

                var modifiedWatch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

                return modifiedWatch!;
            }
        }

        public async Task<WatchDetailsViewModel> AddWatchAsync(AddWatchDTO watch)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");
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

            using (var response = await httpClient.PostAsync("createwatch", content))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);

                var modifiedWatch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

                return modifiedWatch!;
            }
        }

        public async Task<WatchDetailsViewModel> DeleteWatchAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");

            using (var response = await httpClient.DeleteAsync($"deletewatch/{id}"))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);

                var watch = await response.Content.ReadFromJsonAsync<WatchDetailsViewModel>();

                return watch!;
            }
        }
    }
}
