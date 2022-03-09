using CurrencyConverter.Domain.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace CurrencyConverter.Domain.Services
{
    public class HttpClientRequester<T, K> : IHttpClientRequester<T, K>
                where T : IRequestModel
                where K : IResponseModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpClientRequester<T, K>> _logger;
        private readonly HttpClient _client;

        public HttpClientRequester(IHttpClientFactory httpClientFactory,
            ILogger<HttpClientRequester<T, K>> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
            this._client = _httpClientFactory.CreateClient();
        }

        public async Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod)
        {
            return await this.SendHttpClientRequest<TRequest, TResponse>(T, Url, HttpMethod, null);
        }

        public async Task<TResponse> SendHttpClientRequest<TRequest, TResponse>(TRequest T, string Url, HttpMethod HttpMethod, params KeyValuePair<string, string>[] AdditionalHeaders)
        {
            HttpRequestMessage httpRequest = this.CreateHttpRequestMessage(T, Url, HttpMethod, AdditionalHeaders);

            HttpResponseMessage httpResponse = await _client.SendAsync(httpRequest);
            _logger.LogInformation("HttpClient Api Request. Request: {@HttpRequestMessage}, Response: {@HttpResponseMessage}",
                 httpRequest, httpResponse);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseBody = await httpResponse.Content.ReadFromJsonAsync<TResponse>();
                return responseBody;
            }
            else
            {
                _logger.LogInformation("Error while making Api Request. Request: {@HttpRequestMessage}, Response: {@HttpResponseMessage}",
                     httpRequest, httpResponse);
                var responseBody = await httpResponse.Content.ReadFromJsonAsync<TResponse>();
                return responseBody;
            }
        }

        private HttpRequestMessage CreateHttpRequestMessage<TRequest>(TRequest T, string Url, HttpMethod HttpMethod, KeyValuePair<string, string>[] AdditionalHeaders)
        {
            string modeljson = JsonConvert.SerializeObject(T);

            var httpRequest = new HttpRequestMessage(HttpMethod, Url);
            if (AdditionalHeaders != null && AdditionalHeaders.Count() > 0)
            {
                foreach (var header in AdditionalHeaders)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }
            }

            httpRequest.Content = new StringContent(modeljson, Encoding.UTF8, "application/json");
            return httpRequest;
        }
    }
}
