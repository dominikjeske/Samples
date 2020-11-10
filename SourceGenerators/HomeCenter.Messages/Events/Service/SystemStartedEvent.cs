using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Events.Service
{
    public class SystemStartedEvent : Event
    {
        public static SystemStartedEvent Default = new SystemStartedEvent();
    }
}