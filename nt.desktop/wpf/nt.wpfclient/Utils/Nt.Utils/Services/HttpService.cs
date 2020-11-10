using Newtonsoft.Json;
using Nt.Data.Contracts.Dto.Base;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using static Nt.Utils.Helper.HttpUtils;

namespace Nt.Utils.Services
{
    public class HttpService : IHttpService
    {
        private RestClient _restClient;
        public HttpService()
        {
            _restClient = new RestClient(ServerUrl);
        }
        public async Task<TResponse> GetAsync<TRequest, TResponse>(string url, TRequest request)
        {
            var restRequest = new RestRequest(url, Method.GET);
            return await _restClient.GetAsync<TResponse>(restRequest);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request) where TResponse:IBaseResponse,new()
        {
            var restRequest = new RestRequest(url, Method.POST);
            restRequest.JsonSerializer = new RestSharpJsonNetSerializer();
            restRequest.AddJsonBody(request);
            try
            {
                var response = await _restClient.ExecuteAsync(restRequest);
                return ProcesssResponse<TResponse>(response);
            }
            catch(Exception e)
            {
                return new TResponse();
            }
            
        }

        private TResponse ProcesssResponse<TResponse>(IRestResponse response) where TResponse:IBaseResponse,new()
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => JsonConvert.DeserializeObject<TResponse>(response.Content),
                HttpStatusCode.BadRequest => new TResponse { Errors = new[] { JsonConvert.DeserializeObject<string>(response.Content)} },
                _ => new TResponse { Errors = new[] { string.Empty } },
            };
        }

    }
}
