using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries.Device
{
    public class StatusQuery : Query
    {
        public static StatusQuery Default = new StatusQuery();
    }
}