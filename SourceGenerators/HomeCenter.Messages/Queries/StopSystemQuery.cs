using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries
{
    public class StopSystemQuery : Query
    {
        public static StopSystemQuery Default => new StopSystemQuery();
    }
}