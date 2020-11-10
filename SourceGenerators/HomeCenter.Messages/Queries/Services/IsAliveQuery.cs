using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries.Services
{
    public class IsAliveQuery : Query
    {
        public static IsAliveQuery Default = new IsAliveQuery();
    }
}