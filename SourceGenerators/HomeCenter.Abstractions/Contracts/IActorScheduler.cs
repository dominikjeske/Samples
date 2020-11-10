using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    public interface IActorScheduler
    {
        Task Start(CancellationToken cancellationToken);

        Task ShutDown();

        Task SendWithSimpleRepeat(ActorMessageContext message, TimeSpan interval, CancellationToken token = default);

        Task SendWithCronRepeat(ActorMessageContext message, string cronExpression, CancellationToken token = default, string calendar = null);

        Task SendAtTime(ActorMessageContext message, DateTimeOffset time, CancellationToken token = default);

        Task SendDailyAt(ActorMessageContext message, TimeSpan time, CancellationToken token = default, string calendar = null);

        Task SendAfterDelay(ActorMessageContext message, TimeSpan delay, bool cancelExisting = true, CancellationToken token = default);
    }
}