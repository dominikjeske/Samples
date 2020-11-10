using HomeCenter.Abstractions;
using System.Collections.Generic;
using System.Net;

namespace HomeCenter.Messages.Queries.Services
{
    public abstract class HttpPostQuery : Query
    {
        protected HttpPostQuery()
        {
            RequestType = "POST";
        }

        public virtual object Parse(string rawHttpResult) => rawHttpResult;

        public CookieContainer Cookies { get; protected set; }

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

        public bool IgnoreReturnStatus
        {
            get => this.AsBool(MessageProperties.IgnoreReturnStatus);
            set => this.SetProperty(MessageProperties.IgnoreReturnStatus, value);
        }

        public IDictionary<string, string> AuthorisationHeader
        {
            get => this.AsDictionary(MessageProperties.AuthorisationHeader);
            set => this.SetProperty(MessageProperties.AuthorisationHeader, value);
        }

        public IDictionary<string, string> Headers
        {
            get => this.AsDictionary(MessageProperties.Headers);
            set => this.SetProperty(MessageProperties.Headers, value);
        }

        public IDictionary<string, string> Creditionals
        {
            get => this.AsDictionary(MessageProperties.Creditionals);
            set => this.SetProperty(MessageProperties.Creditionals, value);
        }
    }
}