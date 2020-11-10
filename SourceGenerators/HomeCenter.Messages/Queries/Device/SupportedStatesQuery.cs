using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries.Device
{
    public class SupportedStatesQuery : Query
    {
        public static SupportedStatesQuery Default = new SupportedStatesQuery();
    }
}