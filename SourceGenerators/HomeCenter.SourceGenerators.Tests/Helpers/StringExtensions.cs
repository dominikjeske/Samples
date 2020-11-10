using System.Text.RegularExpressions;

namespace HomeCenter.SourceGenerators.Tests
{
    public static class StringExtensions
    {
        public static string TrimWhiteSpaces(this string text)
        {
            if (text == null)
            {
                return text;
            }

            return Regex.Replace(text, @"\s+", string.Empty);
        }
    }
}