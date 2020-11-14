using System;
using System.Threading;

namespace HomeCenter.Extensions
{
    public static class StringExtensions
    {
        public static int Compare(this string orginalText, string comparedText)
        {
            return string.Compare(orginalText, comparedText, StringComparison.OrdinalIgnoreCase);
        }

        public static bool InvariantEquals(this string stringA, string stringB)
        {
            return string.Equals(stringA, stringB, StringComparison.InvariantCultureIgnoreCase);
        }

        public static byte[] ToBytes(this string byteString)
        {
            // Get the separator character.
            if (byteString.Length > 2)
            {
                var separator = byteString[2];

                // Split at the separators.
                var pairs = byteString.Split(separator);
                var bytes = new byte[pairs.Length];
                for (var i = 0; i < pairs.Length; i++) bytes[i] = Convert.ToByte(pairs[i], 16);
                return bytes;
            }

            return new[] {Convert.ToByte(byteString, 16)};
        }

        public static bool ParseAsDouble(this string input, out double value)
        {
            var separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (separator == ",")
                input = input.Replace(".", ",");
            else
                input = input.Replace(",", ".");

            if (double.TryParse(input, out var number))
            {
                value = number;
                return true;
            }

            value = 0;
            return false;
        }
    }
}