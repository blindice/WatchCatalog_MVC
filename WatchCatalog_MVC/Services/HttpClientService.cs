using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchCatalog_MVC.DTOs;
using WatchCatalog_MVC.Helpers;
using WatchCatalog_MVC.Interfaces;
using WatchCatalog_MVC.ViewModels;
using System.Net;

namespace WatchCatalog_MVC.Services
{
    public class HttpClientService: IHttpClientService
    {
        readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService( IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<WatchesPaginationViewModel> GetPaginatedWatchesAsync(WatchPageParameters pageParams)
        {
            var httpClient = _httpClientFactory.CreateClient("WatchClient");

            using (var response = await httpClient.GetAsync($"getwatches?PageNumber={pageParams.PageNumber}&PageSize={pageParams.PageSize}"))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<WatchesPaginationViewModel>(response.Headers.GetValues("X-Pagination").First());
                result.WatchDTOs = await response.Content.ReadFromJsonAsync<List<WatchDTO>>();

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

                return watch;
            }
        }
    }
}
