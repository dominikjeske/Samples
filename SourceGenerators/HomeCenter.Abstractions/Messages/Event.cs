using System;

namespace HomeCenter.Abstractions
{
    //TODO - Why it has to be property based
    public class Event : ActorMessage
    {
        public Event()
        {
            Uid = Guid.NewGuid().ToString();
            EventTime = SystemTime.Now;
        }

        public DateTimeOffset EventTime
        {
            get => this.AsDate(MessageProperties.EventTime);
            set => this.SetProperty(MessageProperties.EventTime, value);
        }

        public string MessageSource
        {
            get => this.AsString(MessageProperties.MessageSource);
            set => this.SetProperty(MessageProperties.MessageSource, value);
        }
    }
}