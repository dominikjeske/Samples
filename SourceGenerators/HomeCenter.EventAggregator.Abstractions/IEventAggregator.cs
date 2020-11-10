using HomeCenter.EventAggregator.Handlers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCenter.EventAggregator
{
    public interface IEventAggregator
    {
        void ClearSubscriptions();

        bool IsSubscribed(Guid token);

        void UnSubscribe(Guid token);

        List<BaseCommandHandler> GetSubscriptors(object message, RoutingFilter filter = null);

        SubscriptionToken Subscribe<T>(Action<IMessageEnvelope<T>> action, RoutingFilter filter = null);

        SubscriptionToken Subscribe(Type messageType, Delegate action, RoutingFilter filter = null);

        SubscriptionToken Subscribe(Type messageType, Func<Delegate> actionFactory, RoutingFilter filter = null);

        SubscriptionToken SubscribeAsync<T>(Func<IMessageEnvelope<T>, Task> action, RoutingFilter filter = null);

        SubscriptionToken SubscribeForAsyncResult<T>(Func<IMessageEnvelope<T>, Task<object>> action, RoutingFilter filter = null);

        IObservable<IMessageEnvelope<T>> Observe<T>(RoutingFilter routingFilter = null);

        Func<BehaviorChain> DefaultBehavior { get; set; }

        Task<R> QueryAsync<T, R>
        (
            T message,
            RoutingFilter filter = null,
            CancellationToken cancellationToken = default,
            BehaviorChain behaviors = null
        );

        Task<R> QueryWithResultCheckAsync<T, R>
        (
            T message,
            R expectedResult,
            RoutingFilter filter = null,
            CancellationToken cancellationToken = default,
            BehaviorChain behaviors = null
        );

        IObservable<R> QueryWithResults<T, R>
        (
            T message,
            RoutingFilter filter = null,
            CancellationToken cancellationToken = default,
            BehaviorChain behaviors = null
        );

        Task QueryWithRepublishResult<T, R>
        (
            T message,
            RoutingFilter filter = null,
            CancellationToken cancellationToken = default,
            BehaviorChain behaviors = null
        );

        Task Publish<T>
        (
            T message,
            RoutingFilter filter = null,
            CancellationToken cancellationToken = default,
            BehaviorChain behaviors = null
        );
    }
}