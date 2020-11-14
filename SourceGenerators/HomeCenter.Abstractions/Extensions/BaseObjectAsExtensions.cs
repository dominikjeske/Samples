using HomeCenter.Abstractions;
using HomeCenter.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace HomeCenter
{
    public static class BaseObjectAsExtensions
    {
        public static bool AsBool(this BaseObject baseObject, string propertyName, bool? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is bool boolValue)
                return boolValue;
            if (bool.TryParse(baseObject[propertyName].ToString(), out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper bool value");
        }

        public static int AsInt(this BaseObject baseObject, string propertyName, int? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is int intValue)
                return intValue;
            if (int.TryParse(baseObject[propertyName].ToString(), out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper int value");
        }

        public static byte AsByte(this BaseObject baseObject, string propertyName, byte? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is byte byteValue)
                return byteValue;
            if (byte.TryParse(baseObject[propertyName].ToString(), out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper byte value");
        }

        public static DateTime AsDate(this BaseObject baseObject, string propertyName, DateTime? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is DateTime dateValue)
                return dateValue;
            if (DateTime.TryParse(baseObject[propertyName].ToString(), out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper date value");
        }

        public static TimeSpan AsTime(this BaseObject baseObject, string propertyName, TimeSpan? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is TimeSpan timeValue)
                return timeValue;
            if (TimeSpan.TryParse(baseObject[propertyName].ToString(), out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper time value");
        }

        public static TimeSpan AsIntTime(this BaseObject baseObject, string propertyName, int? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
            {
                if (defaultValue.HasValue) return TimeSpan.FromMilliseconds(defaultValue.Value);
                throw new ArgumentException(propertyName);
            }

            var rawValue = baseObject[propertyName];
            if (rawValue is TimeSpan timeValue)
                return timeValue;
            if (int.TryParse(baseObject[propertyName].ToString(), out var value))
                return TimeSpan.FromMilliseconds(value);

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper time value");
        }

        public static double AsDouble(this BaseObject baseObject, string propertyName,
            double defaultValue = double.MinValue)
        {
            if (!baseObject.ContainsProperty(propertyName))
            {
                if (defaultValue != double.MinValue) return defaultValue;
                throw new ArgumentException(propertyName);
            }

            return AsDoubleInner(baseObject, propertyName);
        }

        public static double? AsNullableDouble(this BaseObject baseObject, string propertyName)
        {
            if (!baseObject.ContainsProperty(propertyName)) return null;

            return AsDoubleInner(baseObject, propertyName);
        }

        private static double AsDoubleInner(BaseObject baseObject, string propertyName)
        {
            var rawValue = baseObject[propertyName];
            if (rawValue is double doubleValue)
                return doubleValue;
            if (baseObject[propertyName].ToString().ParseAsDouble(out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper double value");
        }

        public static uint AsUint(this BaseObject baseObject, string propertyName, uint? defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is uint uintValue)
                return uintValue;
            if (uint.TryParse(baseObject[propertyName].ToString(), out var value)) return value;

            throw new FormatException(
                $"Property {propertyName} value {baseObject[propertyName]} is not proper uint value");
        }

        public static string AsString(this BaseObject baseObject, string propertyName, string defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            return baseObject[propertyName].ToString();
        }

        public static IList<string> AsList(this BaseObject baseObject, string propertyName,
            IList<string> defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is IList<string> listValue) return listValue;

            return baseObject[propertyName].ToString().Split(',').Select(x => x.Trim()).ToList();
        }

        public static byte[] AsByteArray(this BaseObject baseObject, string propertyName, byte[] defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is byte[] bytesValue) return bytesValue;

            return rawValue.ToString().ToBytes();
        }

        public static T As<T>(this BaseObject baseObject, string propertyName, T defaultValue = default)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is T value) return value;

            throw new FormatException($"Property {propertyName} is not of type {typeof(T).Name}");
        }

        public static IDictionary<string, string> AsDictionary(this BaseObject baseObject, string propertyName,
            IDictionary<string, string> defaultValue = null)
        {
            if (!baseObject.ContainsProperty(propertyName))
                return defaultValue ?? throw new ArgumentException(propertyName);

            var rawValue = baseObject[propertyName];
            if (rawValue is IDictionary<string, string> dictionaryValue) return dictionaryValue;

            return JsonSerializer.Deserialize<IDictionary<string, string>>(baseObject[propertyName].ToString());
        }
    }
}