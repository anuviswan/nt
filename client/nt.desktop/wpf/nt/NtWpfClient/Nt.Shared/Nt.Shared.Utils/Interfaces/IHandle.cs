using System.Threading.Tasks;

namespace Nt.Shared.Utils.Interfaces
{
    public interface IHandle<T> where T:IEventMessage
    {
        Task HandleAsync(T message);
    }
}