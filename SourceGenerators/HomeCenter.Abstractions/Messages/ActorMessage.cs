using HomeCenter.Extensions;
using System;
using System.Text.Json.Serialization;

namespace HomeCenter.Abstractions
{
    public abstract class ActorMessage : BaseObject, IEquatable<ActorMessage>
    {
        [JsonIgnore]
        public Proto.IContext Context { get; set; }

        public string LogLevel
        {
            get => this.AsString(MessageProperties.LogLevel, nameof(Microsoft.Extensions.Logging.LogLevel.Information));
            set => this.SetProperty(MessageProperties.LogLevel, value);
        }

        public bool Equals(ActorMessage other)
        {
            return other != null && GetProperties().LeftEqual(other.GetProperties());
        }
    }
}