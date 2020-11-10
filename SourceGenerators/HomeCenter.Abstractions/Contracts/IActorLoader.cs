using Proto;
using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    public interface IActorLoader
    {
        IActor GetProxyType<T>(T actorConfig) where T : IBaseObject;

        Task LoadTypes();
    }
}