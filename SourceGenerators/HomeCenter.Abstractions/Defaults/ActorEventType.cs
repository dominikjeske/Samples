using Microsoft.Extensions.Logging;

namespace HomeCenter.Abstractions.Defaults
{
    public static class ActorEventType
    {
        public const int MessageBase = 0;

        public static EventId Behavior = new EventId(MessageBase, nameof(Behavior));
        public static EventId Messagee = new EventId(MessageBase + 1, nameof(Messagee));
        public static EventId ActorState = new EventId(MessageBase + 2, nameof(ActorState));
        public static EventId ActorStart = new EventId(MessageBase + 3, nameof(ActorStart));
        public static EventId EventPublished = new EventId(MessageBase + 4, nameof(EventPublished));
    }
}