using System;

namespace HomeCenter.EventAggregator
{
    public class SubscriptionToken : IDisposable
    {
        public static SubscriptionToken Empty = new SubscriptionToken();

        private readonly IEventAggregator _eventAggregator;

        private SubscriptionToken()
        {
        }

        public SubscriptionToken(Guid token, IEventAggregator eventAggregator)
        {
            this.Token = token;
            this._eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        public Guid Token { get; }

        public void Dispose()
        {
            if (this == Empty) return;

            _eventAggregator.UnSubscribe(Token);
        }
    }
}