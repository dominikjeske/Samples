using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace HomeCenter.SourceGenerators.Tests
{
    internal static class GeneratorRunner
    {
        private static Compilation CreateCompilation(string source)
        {
            var syntaxTrees = new[] 
            { 
                CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview)) 
            };

            var references = new List<PortableExecutableReference>();
            foreach (var library in DependencyContext.Default.RuntimeLibraries.Where(lib => lib.Name.IndexOf("HomeCenter.") > -1))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                references.Add(MetadataReference.CreateFromFile(assembly.Location));
            }
            references.Add(MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location));

            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create(nameof(GeneratorRunner), syntaxTrees, references, options);

            return compilation;
        }
        public static GeneratorResult Run(string sourceCode, ISourceGenerator generators)
        {
            Compilation compilation = CreateCompilation(sourceCode);

            var driver = CSharpGeneratorDriver.Create(ImmutableArray.Create(generators),
                                                      ImmutableArray<AdditionalText>.Empty,
                                                      (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
                                                      null);
            driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);


            var generatedCode = GetGeneratedCode(generators, outputCompilation);

            return new GeneratorResult(outputCompilation, diagnostics, generatedCode);
        }

        private static string GetGeneratedCode(ISourceGenerator generators, Compilation outputCompilation)
        {
            return outputCompilation.SyntaxTrees.FirstOrDefault(file => file.FilePath.IndexOf(generators.GetType().Name) > -1)?.ToString();
        }
    }
}