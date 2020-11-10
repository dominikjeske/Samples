using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace HomeCenter.SourceGenerators
{
    internal class OptionsInternalReader
    {
        /// <summary>
        /// Method is using internal structure via Reflections and it could break in future versions
        /// </summary>
        public static string ReadOptions(GeneratorExecutionContext executionContext)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                var values = ReadOptions(executionContext.AnalyzerConfigOptions.GlobalOptions);

                sb.AppendLine("-> Global options:");
                foreach (var value in values)
                {
                    sb.AppendLine($"\t{value.Key}:{value.Value}");
                }

                var info2 = executionContext.AnalyzerConfigOptions
                                            .GetType()
                                            .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                            .FirstOrDefault(g => g.Name == "_treeDict");

                if (info2.GetValue(executionContext.AnalyzerConfigOptions) is ImmutableDictionary<object, AnalyzerConfigOptions> options)
                {
                    sb.AppendLine("-> Options:");

                    foreach (var optionKey in options.Keys)
                    {
                        string keyContext = "";
                        if (optionKey is AdditionalText text)
                        {
                            keyContext = text.Path;
                        }
                        else if (optionKey is SyntaxTree syntaxTree)
                        {
                            keyContext = syntaxTree.FilePath;
                        }

                        sb.AppendLine($"- {optionKey.GetType().Name}[{keyContext}]:");

                        var optionsList = ReadOptions(options[optionKey]);
                        foreach (var value in optionsList)
                        {
                            sb.AppendLine($"\t{value.Key}:{value.Value}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                sb.Append("-> Options: Error when try use reflection to load");
            }

            return sb.ToString();
        }

        private static ImmutableDictionary<string, string> ReadOptions(AnalyzerConfigOptions analyzerConfigOptions)
        {
            var backing = analyzerConfigOptions.GetType()
                                               .GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                               .FirstOrDefault(g => g.Name == "_backing");

            return backing.GetValue(analyzerConfigOptions) as ImmutableDictionary<string, string>;
        }

    }
}