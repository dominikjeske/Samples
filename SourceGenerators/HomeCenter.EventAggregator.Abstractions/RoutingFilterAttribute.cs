using System;

namespace HomeCenter.EventAggregator
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RoutingFilterAttribute : Attribute
    {
        public RoutingFilterAttribute(string simpleFilter)
        {
            SimpleFilter = simpleFilter;
        }

        public string SimpleFilter { get; }

        public RoutingFilter ToMessageFilter()
        {
            return new RoutingFilter(SimpleFilter);
        }
    }
}