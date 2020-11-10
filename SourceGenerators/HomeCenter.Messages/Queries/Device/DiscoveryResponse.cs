using HomeCenter.Abstractions;
using System.Collections.Generic;

namespace HomeCenter.Messages.Queries.Device
{
    public class DiscoveryResponse : BaseObject
    {
        public DiscoveryResponse(IList<string> requierdProperties, params StateBase[] supportedStates)
        {
            SupportedStates = supportedStates;
            RequierdProperties = requierdProperties;
        }

        public DiscoveryResponse(params StateBase[] supportedStates)
        {
            SupportedStates = supportedStates;
            RequierdProperties = new List<string>();
        }

        public StateBase[] SupportedStates { get; }
        public IList<string> RequierdProperties { get; }
    }
}