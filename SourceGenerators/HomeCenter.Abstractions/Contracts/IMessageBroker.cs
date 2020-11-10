using HomeCenter.EventAggregator;
using Proto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    public interface IMessageBroker
    {
        Task Publish<T>(T message, RoutingFilter routingFilter = null) where T : ActorMessage;

        Task SendToService<T>(T command, RoutingFilter filter = null) where T : Command;

        Task<R> QueryService<T, R>(T query, RoutingFilter filter = null)
            where T : Query;

        Task<R> QueryJsonService<T, R>(T query, RoutingFilter filter = null)
            where T : Query;

        Task<bool> QueryServiceWithVerify<T, Q, R>(T query, R expectedResult, RoutingFilter filter = null)
            where T : Query, IMessageResult<Q, R>
            where Q : class;

        Task<R> Request<T, R>(T message, PID actor) where T : ActorMessage;

        Task<R> Request<T, R>(T message, string uid) where T : ActorMessage;

        void Send(object message, PID destination);

        void Send(object message, string uid, string address = null);

        SubscriptionToken SubscribeForMessage<T>(PID subscriber, bool subscribeOnParent, RoutingFilter filter = null) where T : ActorMessage;

        SubscriptionToken SubscribeForQuery<T, R>(PID subscriber, bool subscribeOnParent, RoutingFilter filter = null) where T : Query;

        IObservable<IMessageEnvelope<T>> Observe<T>(RoutingFilter routingFilter = null) where T : Event;

        SubscriptionToken SubscribeForEvent<T>(Action<IMessageEnvelope<T>> action, RoutingFilter filter = null) where T : Event;

        Task<Event> PublishWithTranslate(ActorMessage source, ActorMessage destination, RoutingFilter filter = null);

        void SendWithTranslate(ActorMessage source, ActorMessage destination, string address);

        Task SendWithSimpleRepeat(ActorMessageContext message, TimeSpan interval, CancellationToken token = default);

        Task SendAfterDelay(ActorMessageContext message, TimeSpan delay, bool cancelExisting = true, CancellationToken token = default);

        Task SendAtTime(ActorMessageContext message, DateTimeOffset time, CancellationToken token = default);

        Task SendWithCronRepeat(ActorMessageContext message, string cronExpression, CancellationToken token = default, string calendar = default);

        Task SendDailyAt(ActorMessageContext message, TimeSpan time, CancellationToken token = default, string calendar = default);

        PID GetPID(string uid, string address = null);
    }
}