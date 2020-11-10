using System.Collections.Generic;

namespace HomeCenter.EventAggregator
{
    public interface IPropertySource
    {
        object this[string propertyName] { get; set; }

        bool ContainsProperty(string propertyName);

        IReadOnlyDictionary<string, object> GetProperties();
    }
}