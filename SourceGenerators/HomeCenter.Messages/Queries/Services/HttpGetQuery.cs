using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries.Services
{
    public abstract class HttpGetQuery : Query
    {
        public abstract object Parse(string rawHttpResult);

        protected HttpGetQuery()
        {
            RequestType = "GET";
        }

        public string Address
        {
            get => this.AsString(MessageProperties.Address);
            set => this.SetProperty(MessageProperties.Address, value);
        }

        public string RequestType
        {
            get => this.AsString(MessageProperties.RequestType);
            set => this.SetProperty(MessageProperties.RequestType, value);
        }
    }
}