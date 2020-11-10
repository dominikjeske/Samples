using System.Text.Json.Serialization;
using System.Threading;

namespace HomeCenter.Abstractions
{
    public class Query : ActorMessage
    {
        [JsonIgnore]
        public CancellationToken CancellationToken { get; }

        public Query()
        {
        }

        public Query(string commandType)
        {
            Type = commandType;
        }

        public Query(string commandType, string uid)
        {
            Type = commandType;
            Uid = uid;
        }

        public Query(string commandType, CancellationToken cancellationToken) : base()
        {
            Type = commandType;
            CancellationToken = cancellationToken;
        }

        public static implicit operator Query(string value) => new Query(value);
    }
}