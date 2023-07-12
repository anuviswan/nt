using Newtonsoft.Json;
using Nt.Data.Contracts.Dto.Base;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using static Nt.Utils.Helper.HttpUtils;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace Nt.Utils.Services;

public class HttpService : IHttpService
{
    private readonly RestClient _restClient;
    private readonly ICurrentUserService _currentUserService;
    public HttpService(ICurrentUserService currentUserService)
    {
        _restClient = new RestClient(ServerUrl);
        _currentUserService = currentUserService;
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
        if (_currentUserService.IsAuthenticated)
        {
            restRequest.AddHeader("authorization", "Bearer " + _currentUserService.AuthToken);
        }
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
            HttpStatusCode.BadRequest => ParseErrorResponse<TResponse>(response.Content),
            HttpStatusCode.NoContent => new TResponse(),
            _ => new TResponse { Errors = new[] { string.Empty } },
        };
    }

    // This methos/approach has to be improved later
    private TResponse ParseErrorResponse<TResponse>(string content) where TResponse : IBaseResponse, new()
    {
        try
        {
            var defaultSchema = JSchema.Parse(GetDefaultSchema());
            var jsonString = JObject.Parse(content);

            if (jsonString.IsValid(defaultSchema))
            {
                return JsonConvert.DeserializeObject<TResponse>(content);
            }
            else
            {
                return new TResponse
                {
                    Errors = new[] { JsonConvert.DeserializeObject<string>(content) }
                };
            }
        }
        catch
        {
            return new TResponse
            {
                Errors = new[] { JsonConvert.DeserializeObject<string>(content) }
            };
        }
    }

    private string GetDefaultSchema()
    {
        return @"{
  '$schema': 'http://json-schema.org/draft-04/schema#',
  'type': 'object',
  'properties': {
    'type': {
      'type': 'string'
    },
    'title': {
      'type': 'string'
    },
    'status': {
      'type': 'integer'
    },
    'traceId': {
      'type': 'string'
    },
    'errors': {
      'type': 'object',
      'properties': {
        'PassKey': {
          'type': 'array',
          'items': [
            {
              'type': 'string'
            }
          ]
        }
      },
      'required': [
        'PassKey'
      ]
    }
  },
  'required': [
    'type',
    'title',
    'status',
    'traceId',
    'errors'
  ]
}";
    }
}
