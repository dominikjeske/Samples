using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Events.Device
{
    public class DipswitchEvent : Event
    {
        public static DipswitchEvent Create(string mesageSource, string unit, string system, string command)
        {
            return new DipswitchEvent()
            {
                MessageSource = mesageSource,
                Unit = unit,
                System = system,
                CommandCode = command
            };
        }

        public string Unit
        {
            get => this.AsString(MessageProperties.Unit);
            set => this.SetProperty(MessageProperties.Unit, value);
        }

        public string System
        {
            get => this.AsString(MessageProperties.System);
            set => this.SetProperty(MessageProperties.System, value);
        }

        public string CommandCode
        {
            get => this.AsString(MessageProperties.CommandCode);
            set => this.SetProperty(MessageProperties.CommandCode, value);
        }
    }
}