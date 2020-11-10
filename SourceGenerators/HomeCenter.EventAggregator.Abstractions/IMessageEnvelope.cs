using System;
using System.Threading;

namespace HomeCenter.EventAggregator
{
    public interface IMessageEnvelope<out T>
    {
        CancellationToken CancellationToken { get; }
        T Message { get; }
        Type ResponseType { get; }
    }
}