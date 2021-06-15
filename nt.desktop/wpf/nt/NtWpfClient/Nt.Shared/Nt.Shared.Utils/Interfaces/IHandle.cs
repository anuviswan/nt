using System.Threading.Tasks;

namespace Nt.Shared.Utils.Interfaces
{
    public interface IHandle<T>
    {
        Task HandleAsync(T message);
    }
}