using System;
using System.Reflection;

namespace HomeCenter.EventAggregator.Handlers
{
    public abstract class BaseCommandHandler
    {
        internal Guid Token { get; }
        internal TypeInfo MessageType { get; }
        internal RoutingFilter SubscriptionFilter { get; }
        internal object Handler { get; }

        protected BaseCommandHandler(Type type, Guid token, object handler, RoutingFilter filter)
        {
            MessageType = type.GetTypeInfo();
            Token = token;
            Handler = handler;
            SubscriptionFilter = filter;
        }

        public bool IsFilterMatch(object message, RoutingFilter messageFilter)
        {
            if (SubscriptionFilter == null)
            {
                if (messageFilter == null || messageFilter?.RoutingKey == "*")
                {
                    return true;
                }
                return false;
            }

            return SubscriptionFilter?.EvaluateFilter(message, messageFilter) ?? true;
        }
    }
}