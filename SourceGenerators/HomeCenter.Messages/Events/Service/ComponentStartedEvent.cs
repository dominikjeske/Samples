using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Events.Service
{
    public class ComponentStartedEvent : Event
    {
        public static ComponentStartedEvent Create(string componentUid)
        {
            return new ComponentStartedEvent()
            {
                MessageSource = componentUid
            };
        }
    }
}