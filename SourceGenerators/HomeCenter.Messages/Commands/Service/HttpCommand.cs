using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Service
{
    public class HttpCommand : Command
    {
        public HttpCommand()
        {
            RequestType = "POST";
        }

        public string Address
        {
            get => this.AsString(MessageProperties.Address);
            set => this.SetProperty(MessageProperties.Address, value);
        }

        public string Body
        {
            get => this.AsString(MessageProperties.Body);
            set => this.SetProperty(MessageProperties.Body, value);
        }

        public string RequestType
        {
            get => this.AsString(MessageProperties.RequestType);
            set => this.SetProperty(MessageProperties.RequestType, value);
        }

        public string ContentType
        {
            get => this.AsString(MessageProperties.ContentType);
            set => this.SetProperty(MessageProperties.ContentType, value);
        }
    }
}