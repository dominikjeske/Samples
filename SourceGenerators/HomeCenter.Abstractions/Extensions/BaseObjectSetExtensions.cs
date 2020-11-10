using HomeCenter.Abstractions;
using HomeCenter.Extensions;
using Light.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace HomeCenter
{
    public static class BaseObjectSetExtensions
    {
        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, string value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, DateTimeOffset value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, TimeSpan value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, Guid value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, int value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, double value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, bool value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, byte[] value)
        {
            baseObject[propertyName] = value.ToHex();
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, IDictionary<string, string> value)
        {
            baseObject[propertyName] = JsonSerializer.Serialize(value);
            return baseObject;
        }

        public static BaseObject SetPropertyList(this BaseObject baseObject, string propertyName, params string[] values)
        {
            baseObject[propertyName] = string.Join(", ", values);
            return baseObject;
        }

        public static BaseObject SetProperty(this BaseObject baseObject, string propertyName, object value)
        {
            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperty<T>(this BaseObject baseObject, string propertyName, T value) where T : class
        {
            value = value.MustNotBeNull(nameof(value));

            baseObject[propertyName] = value;
            return baseObject;
        }

        public static BaseObject SetProperties(this BaseObject baseObject, BaseObject source)
        {
            foreach (var property in source.GetProperties())
            {
                baseObject.SetProperty(property.Key, property.Value);
            }
            return baseObject;
        }
    }
}