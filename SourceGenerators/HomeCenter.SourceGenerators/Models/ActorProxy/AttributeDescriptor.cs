using System.Collections.Generic;

namespace HomeCenter.SourceGenerators
{
    internal class AttributeDescriptor
    {
        public string Name { get; set; }

        public List<string> Values { get; set; }
    }
}