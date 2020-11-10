using System.Threading.Tasks;

namespace HomeCenter.EventAggregator
{
    public interface IAsyncCommandHandler
    {
        Task<R> HandleAsync<T, R>(IMessageEnvelope<T> message);
    }
}