using System;
using System.Threading.Tasks;

namespace HomeCenter.EventAggregator.Handlers
{
    public sealed class CommandHandler : BaseCommandHandler, IAsyncCommandHandler
    {
        public CommandHandler(Type type, Guid token, object handler, RoutingFilter filter) : base(type, token, handler, filter)
        {
        }

        public Task<R> HandleAsync<T, R>(IMessageEnvelope<T> message)
        {
            var handler = Handler as Action<IMessageEnvelope<T>>;
            if (handler == null && Handler is Func<Delegate> factory)
            {
                handler = factory.Invoke() as Action<IMessageEnvelope<T>>;
            }

            if (handler == null) throw new InvalidCastException($"Invalid cast from {Handler.GetType()} to Action<IMessageEnvelope<{typeof(T).Name}>>");
            handler(message);

            return Task.FromResult(default(R));
        }
    }
}