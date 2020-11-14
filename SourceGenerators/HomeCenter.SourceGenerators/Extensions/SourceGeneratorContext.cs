using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HomeCenter.SourceGenerators
{
    internal class SourceGeneratorContext<T> : IDisposable where T : ISourceGenerator
    {
        private SourceGeneratorContext(GeneratorExecutionContext generatorExecutionContext)
        {
            Options = new SourceGeneratorOptions<T>(generatorExecutionContext);
            Logger = new SourceGeneratorLogger<T>(generatorExecutionContext, Options);
            Diagnostic = new SourceGeneratorDiagnostic<T>(generatorExecutionContext, Options);
            GeneratorExecutionContext = generatorExecutionContext;
        }

        public SourceGeneratorOptions<T> Options { get; }
        private SourceGeneratorLogger<T> Logger { get; }
        private SourceGeneratorDiagnostic<T> Diagnostic { get; }
        public GeneratorExecutionContext GeneratorExecutionContext { get; }

        public void Dispose()
        {
            Logger.Dispose();
        }

        public static SourceGeneratorContext<T> Create(GeneratorExecutionContext context)
        {
            var sourceGenContext = new SourceGeneratorContext<T>(context);

            if (sourceGenContext.Options.EnableDebug)
                if (!Debugger.IsAttached)
                    Debugger.Launch();

            return sourceGenContext;
        }

        public void ApplyDesignTimeFix(GeneratorExecutionContext context, string content, string hintName)
        {
            var includeFix = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.SourceGenerator_IntellisenseFix", out var raw) &&
                bool.TryParse(raw, out var value) &&
                value;

            if (includeFix)
            {
                if (!context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.IntermediateOutputPath", out var intermediate))
                    throw new NotSupportedException();

                //var path = Path.Combine(intermediate, hintName + ".generated.cs");
                var path = hintName + ".generated.cs";
                Directory.CreateDirectory(intermediate);
                File.WriteAllText(path, content, Encoding.UTF8);
            }
        }

        public GeneratedSource GenerateErrorSourceCode(Exception exception, ClassDeclarationSyntax classDeclaration)
        {
            var context = $"[{typeof(T).Name} - {classDeclaration.Identifier.Text}]";

            var templateString = ResourceReader.GetResource("ErrorModel.cstemplate");
            templateString = templateString.Replace("//Error",
                $"#error {context} {exception.Message} | Logfile: {Options.LogPath}");

            return new GeneratedSource(templateString, classDeclaration.Identifier.Text);
        }

        /// <summary>
        ///     Log source code when logging is enabled
        /// </summary>
        public void TryLogSourceCode(ClassDeclarationSyntax classDeclaration, string generatedSource)
        {
            Diagnostic.ReportInformation(classDeclaration.GetLocation());
            Logger.TryLogSourceCode(classDeclaration, generatedSource);
        }

        /// <summary>
        ///     Log exception when logging is enabled
        /// </summary>
        public void TryLogException(ClassDeclarationSyntax classDeclaration, Exception exception)
        {
            Diagnostic.ReportError(exception, classDeclaration.GetLocation());
            Logger.TryLogException(classDeclaration, exception);
        }
    }
}