using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries.Service
{
    public class RegisterPinChangedCommand : Command
    {
        public string PinNumber
        {
            get => this.AsString(MessageProperties.PinNumber);
            set => this.SetProperty(MessageProperties.PinNumber, value);
        }

        public string PinMode
        {
            get => this.AsString(MessageProperties.PinMode);
            set => this.SetProperty(MessageProperties.PinMode, value);
        }
    }
}