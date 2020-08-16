using System.Threading.Tasks;

namespace Nt.Utils.ServiceInterfaces
{
    public interface IHttpService
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string url,TRequest request);
        Task<TResponse> GetAsync<TRequest, TResponse>(string url, TRequest request);
    }
}
