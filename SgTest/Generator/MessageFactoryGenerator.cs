using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Text;

namespace Generator
{
    [Generator]
    public class MessageFactoryGenerator : ISourceGenerator
    {
        private const string attributeText = @"
using System;
namespace AutoNotify
{
    sealed class AutoNotifyTest
    {
        public AutoNotifyTest()
        {
        }
        public string PropertyName { get; set; }
    }
}
";


        public void Initialize(GeneratorInitializationContext context)
        {

        }

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("AutoNotifyAttribute", SourceText.From(attributeText, Encoding.UTF8));
        }

    }
}
