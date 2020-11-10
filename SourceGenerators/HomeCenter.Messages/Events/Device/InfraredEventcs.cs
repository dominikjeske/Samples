using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Events.Device
{
    public class InfraredEvent : Event
    {
        public static InfraredEvent Create(string deviceUID, int system, uint commandCode)
        {
            return new InfraredEvent()
            {
                MessageSource = deviceUID,
                System = system,
                CommandCode = commandCode
            };
        }

        public int System
        {
            get => this.AsInt(MessageProperties.System);
            set => this.SetProperty(MessageProperties.System, value);
        }

        public uint CommandCode
        {
            get => this.AsUint(MessageProperties.CommandCode);
            set => this.SetProperty(MessageProperties.CommandCode, value);
        }
    }
}