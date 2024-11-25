using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Library.Adapters
{
    public class AdapterBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _client;

        public AdapterBase(IHttpClientFactory httpClientFactory, string client)
        {
            _httpClientFactory = httpClientFactory;
            _client = client;
        }

        public async Task<HttpResponseMessage> MakeCommandRequest<T>(HttpMethod methodType, string url, T body, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient(_client);
            var httpRequest = new HttpRequestMessage(methodType, url);
            if(body != null)
            {
                httpRequest.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            }
            return await client.SendAsync(httpRequest, cancellationToken);
        }

        public async Task<HttpResponseMessage> MakeQueryRequest(string url, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient(_client);
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            return await client.SendAsync(httpRequest, cancellationToken);
        }

        public async Task<CommandResponseStatus> GetCommandResponse(HttpResponseMessage httpResponse, CancellationToken cancellationToken)
        {
            var response = new CommandResponseStatus()
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Messages = new()
            };

            try
            {
                var details = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                if(httpResponse.IsSuccessStatusCode && String.IsNullOrWhiteSpace(details))
                {
                    JsonSerializer.Deserialize<int>(details);
                }
                else if(httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest &&
                    String.IsNullOrWhiteSpace(details))
                {                
                    response.Messages.Add(details);
                }
                else if(String.IsNullOrWhiteSpace(details))
                {                
                    var problem = JsonSerializer.Deserialize<ProblemDetails>(details);
                    response.Messages.AddRange(problem.Detail.Split('.'));
                }
            }
            catch (Exception ex) { }

            return response;
        }
        
        public async Task<T> GetQueryResponse<T>(HttpResponseMessage httpResponse, CancellationToken cancellationToken) where T : new()
        {
            try
            {
                var details = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                return JsonSerializer.Deserialize<T>(details);
            }
            catch (Exception ex) 
            {
                return new T();
            }
        }
    }
}
