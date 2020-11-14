using HomeCenter.EventAggregator;
using HomeCenter.Extensions;
using System.Collections.Generic;

namespace HomeCenter.Abstractions
{
    public abstract class BaseObject : IPropertySource, IBaseObject
    {
        public BaseObject()
        {
            Type = GetType().Name;
        }

        private Dictionary<string, object> _properties { get; } = new Dictionary<string, object>();

        public string Uid
        {
            get => this.AsString(MessageProperties.Uid, GetType().Name);
            set => this.SetProperty(MessageProperties.Uid, value);
        }

        public string Type
        {
            get => this.AsString(MessageProperties.Type);
            set => this.SetProperty(MessageProperties.Type, value);
        }

        public object this[string propertyName]
        {
            get
            {
                if (!ContainsProperty(propertyName))
                    throw new KeyNotFoundException($"Property {propertyName} not found on component {Uid}");
                return _properties[propertyName];
            }
            set => _properties[propertyName] = value;
        }

        public bool ContainsProperty(string propertyName)
        {
            return _properties.ContainsKey(propertyName);
        }

        public IReadOnlyDictionary<string, object> GetProperties()
        {
            return _properties.AsReadOnly();
        }

        public override string ToString()
        {
            return GetProperties()?.ToFormatedString() ?? string.Empty;
        }
    }
}