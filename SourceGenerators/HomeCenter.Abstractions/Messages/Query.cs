using System.Text.Json.Serialization;
using System.Threading;

namespace HomeCenter.Abstractions
{
    public class Query : ActorMessage
    {
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

        public Query(string commandType, CancellationToken cancellationToken)
        {
            Type = commandType;
            CancellationToken = cancellationToken;
        }

        [JsonIgnore] public CancellationToken CancellationToken { get; }

        public static implicit operator Query(string value)
        {
            return new Query(value);
        }
    }
}