using Microsoft.CodeAnalysis;

namespace HomeCenter.SourceGenerators
{
    internal class AdditionalFilesOptions
    {
        public string Type { get; set; }

        public AdditionalText AdditionalText { get; set; }
    }
}