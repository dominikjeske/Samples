using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace HomeCenter.SourceGenerators.Tests
{
    internal class GeneratorResult
    {
        public GeneratorResult(Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics, string generatedCode)
        {
            this.Compilation = Compilation;
            this.Diagnostics = Diagnostics;
            GeneratedCode = generatedCode;
        }

        public Compilation Compilation { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public string GeneratedCode { get; }
    }
}