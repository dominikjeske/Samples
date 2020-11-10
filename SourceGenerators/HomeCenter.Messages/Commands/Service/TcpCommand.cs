using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Service
{
    public class TcpCommand : Command
    {
        public string Address
        {
            get => this.AsString(MessageProperties.Address);
            set => this.SetProperty(MessageProperties.Address, value);
        }

        public byte[] Body
        {
            get => this.AsByteArray(MessageProperties.Body);
            set => this.SetProperty(MessageProperties.Body, value);
        }
    }
}