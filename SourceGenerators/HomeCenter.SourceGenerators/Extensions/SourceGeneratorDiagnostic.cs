using Microsoft.CodeAnalysis;
using System;

namespace HomeCenter.SourceGenerators
{
    internal class SourceGeneratorDiagnostic<T> where T : ISourceGenerator
    {
        private readonly GeneratorExecutionContext _generatorExecutionContext;
        private readonly SourceGeneratorOptions<T> _options;

        private readonly DiagnosticDescriptor _errorRuleWithLog = new DiagnosticDescriptor("SG0001", "SG0001: Error in source generator", "Error in source generator<{0}>: '{1}'. Log file details: '{2}'", "SourceGenerator", DiagnosticSeverity.Error, isEnabledByDefault: true);
        private readonly DiagnosticDescriptor _errorRule = new DiagnosticDescriptor("SG0001", "SG0001: Error in source generator", "Error in source generator<{0}>: '{1}'.", "SourceGenerator", DiagnosticSeverity.Error, isEnabledByDefault: true);
        private readonly DiagnosticDescriptor _infoRule = new DiagnosticDescriptor("SG0002", "SG0002: Source code generated", "Source code generated<{0}>", "SourceGenerator", DiagnosticSeverity.Info, isEnabledByDefault: true);

        public SourceGeneratorDiagnostic(GeneratorExecutionContext generatorExecutionContext, SourceGeneratorOptions<T> Options)
        {
            _generatorExecutionContext = generatorExecutionContext;
            _options = Options;
        }

        public void ReportInformation(Location location = null)
        {
            if (location == null)
            {
                location = Location.None;
            }

            _generatorExecutionContext.ReportDiagnostic(Diagnostic.Create(_infoRule, location, typeof(T).Name));
        }

        public void ReportError(Exception e, Location location = null)
        {
            if(location == null)
            {
                location = Location.None;
            }

            if(_options.EnableLogging)
            {
                _generatorExecutionContext.ReportDiagnostic(Diagnostic.Create(_errorRuleWithLog, location, typeof(T).Name, e.Message, _options.LogPath));
            }
            else
            {
                _generatorExecutionContext.ReportDiagnostic(Diagnostic.Create(_errorRule, location, typeof(T).Name, e.Message));
            }
        }

    }
}