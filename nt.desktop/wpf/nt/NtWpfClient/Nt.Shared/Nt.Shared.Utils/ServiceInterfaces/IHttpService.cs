using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Shared.Utils.ServiceInterfaces
{
    public interface IHttpService
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request) where TResponse : IBaseResponse, new();
        Task<TResponse> GetAsync<TRequest, TResponse>(string url, TRequest request);
    }
}
