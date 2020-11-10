namespace HomeCenter.SourceGenerators
{
    internal class PropertyAssignDescriptor
    {
        public string Destination { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public  ParameterDescriptor ToCamelCase() => new ParameterDescriptor
        {
            Name = Source.ToCamelCase(),
            Type = Type
        };
    }
}