using System;

namespace HomeCenter.EventAggregator
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RoutingFilterAttribute : Attribute
    {
        public RoutingFilterAttribute(string simpleFilter)
        {
            SimpleFilter = simpleFilter;
        }

        public string SimpleFilter { get; }

        public RoutingFilter ToMessageFilter() => new RoutingFilter(SimpleFilter);
    }
}