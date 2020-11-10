using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Events.Device
{
    public class PinValueChangedEvent : Event
    {
        public static PinValueChangedEvent Create(string deviceUID, int pinNumber, bool isRising)
        {
            return new PinValueChangedEvent()
            {
                MessageSource = deviceUID,
                PinNumber = pinNumber,
                IsRising = isRising,
                LogLevel = nameof(Microsoft.Extensions.Logging.LogLevel.Trace)
            };
        }

        public int PinNumber
        {
            get => this.AsInt(MessageProperties.PinNumber);
            set => this.SetProperty(MessageProperties.PinNumber, value);
        }

        public bool IsRising
        {
            get => this.AsBool(MessageProperties.IsRising);
            set => this.SetProperty(MessageProperties.IsRising, value);
        }
    }
}