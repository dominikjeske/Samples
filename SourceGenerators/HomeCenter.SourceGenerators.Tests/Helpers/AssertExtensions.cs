using Xunit;

namespace HomeCenter.SourceGenerators.Tests
{
    public static class AssertExtensions
    {
        public static void AssertSourceCodesEquals(this string expected, string actual)
        {
            Assert.Equal(expected.TrimWhiteSpaces(), actual.TrimWhiteSpaces());
        }
    }

}