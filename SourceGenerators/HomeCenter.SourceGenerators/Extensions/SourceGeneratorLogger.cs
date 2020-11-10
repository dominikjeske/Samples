using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HomeCenter.SourceGenerators
{
    internal class SourceGeneratorLogger<T> : IDisposable where T : ISourceGenerator
    {
        private const int LineSurfixLenght = 20;
        private const int LineLenght = 100;

        private readonly Stopwatch _loggerStopwatch;
        private readonly GeneratorExecutionContext _executionContext;
        private readonly SourceGeneratorOptions<T> _options;

        public SourceGeneratorLogger(GeneratorExecutionContext generatorExecutionContext, SourceGeneratorOptions<T> options)
        {
            _executionContext = generatorExecutionContext;
            _options = options;

            if (!options.EnableLogging) return;

            _loggerStopwatch = new Stopwatch();
            _loggerStopwatch.Start();

            WriteHeader();

            if(_options.DetailedLogging)
            {
                WriteLog(OptionsInternalReader.ReadOptions(generatorExecutionContext));
            }
        }

        public void TryLogSourceCode(ClassDeclarationSyntax classDeclaration, string generatedSource)
        {
            if (!_options.EnableLogging) return;

            var sb = new StringBuilder();
            sb.AppendLine($"-> Generated class for '{classDeclaration.Identifier.Text}':{Environment.NewLine}");
            sb.AppendLine(generatedSource);
            sb.AppendLine("");

            WriteLog(sb.ToString());
        }

        public void TryLogException(ClassDeclarationSyntax classDeclaration, Exception exception)
        {
            if (!_options.EnableLogging) return;

            var sb = new StringBuilder();
            sb.AppendLine($"-> Exception for '{classDeclaration.Identifier.Text}':{Environment.NewLine}");
            sb.AppendLine(exception.ToString());
            sb.AppendLine("");

            WriteLog(sb.ToString());
        }

        public void LogMessage(string message)
        {
            if (!_options.EnableLogging) return;

            WriteLog(message);
        }

        public void Dispose()
        {
            if (!_options.EnableLogging) return;

            _loggerStopwatch.Stop();

            var summary = GetTextWithLine($"END [{typeof(T).Name} | {_loggerStopwatch.Elapsed:g}] ");

            WriteLog(summary);
        }

        
        private void WriteHeader()
        {
            var sb = new StringBuilder();
            var header = GetTextWithLine($" [{typeof(T).Name} | {DateTime.Now:g}] ");

            sb.AppendLine(header);
            sb.AppendLine();

            sb.AppendLine($"-> Language: {_executionContext.ParseOptions.Language}");
            sb.AppendLine($"-> Kind: {_executionContext.ParseOptions.Kind}");
            sb.AppendLine("-> Additional files:");
            
            foreach (var additionalFile in _executionContext.AdditionalFiles)
            {
              sb.AppendLine(additionalFile.Path);
            }
            
            WriteLog(sb.ToString());
        }

        
        private void WriteLog(string logtext)
        {
            File.AppendAllText(_options.LogPath, $"{logtext}{Environment.NewLine}");
        }

        private string GetTextWithLine(string context)
        {
            return new string('-', LineSurfixLenght) + context + new string('-', LineLenght - LineSurfixLenght - context.Length);
        }
    }
}