using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace HomeCenter.SourceGenerators.Tests
{
    public class ActorProxySourceGeneratorTests
    {
        [Fact]
        public async Task ProxyGeneratorTest()
        {
            var userSource = await File.ReadAllTextAsync(@"..\..\..\TestInputs\TestAdapter.cs");
            var expectedResult = await File.ReadAllTextAsync(@"..\..\..\TestOutputs\TestAdapterOutput.cs");

            var result = GeneratorRunner.Run(userSource, new ActorProxySourceGenerator());

            expectedResult.AssertSourceCodesEquals(result.GeneratedCode);
        }


        //[Fact]
        //public async Task MessageFactoryTest()
        //{
        //    var userSource = await File.ReadAllTextAsync(@"..\..\..\TestInputs\TestMessageFactory.cs");
        //    var expectedResult = await File.ReadAllTextAsync(@"..\..\..\TestOutputs\TestMessageFactoryOutput.cs");

        //    var result = GeneratorRunner.Run(userSource, new MessageFactoryGenerator());

        //    expectedResult.AssertSourceCodesEquals(result.GeneratedCode);
        //}
    }

}