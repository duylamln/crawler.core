using Crawler.API.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.API.Core.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;


        public IHttpService Create(string apiKey)
        {
            _httpClient = new HttpClient();
            string basicAuthHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{apiKey}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthHeader);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return this;
        }

        public async Task<HttpResponseMessage> Delete(string url) => await _httpClient.DeleteAsync(url);

        public async Task<HttpResponseMessage> Get(string url) => await _httpClient.GetAsync(url);

        public async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return default(T);
        }

        public async Task<string> GetString(string url) => await _httpClient.GetStringAsync(url);

        public async Task<TOut> Patch<TIn, TOut>(string url, TIn data)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, url)
            {
                Content = new ObjectContent<TIn>(data, new JsonMediaTypeFormatter())
            };

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(content);
            }
            return default(TOut);
        }

        public async Task<TOut> Post<TIn, TOut>(string url, TIn data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(content);
            }
            return default(TOut);
        }
    }
}