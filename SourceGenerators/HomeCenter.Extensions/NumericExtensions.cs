using System;
using System.Globalization;

namespace HomeCenter.Extensions
{
    public static class NumericExtensions
    {
        public static float? ToFloat(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException();

            return float.TryParse(text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out float result) ? (float?)result : null;
        }

        public static string ToFloatString(this double number)
        {
            return number.ToString("n1", CultureInfo.InvariantCulture);
        }
    }
}