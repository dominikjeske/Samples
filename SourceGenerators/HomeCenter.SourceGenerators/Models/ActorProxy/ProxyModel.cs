using System.Collections.Generic;
using System.Linq;

namespace HomeCenter.SourceGenerators
{
    internal class ProxyModel
    {
        public string ClassName { get; set; }

        public string Namespace { get; set; }

        public string ClassBase { get; set; }

        public string ClassModifier { get; set; }

        public List<string> Usings { get; set; } = new List<string>();

        public List<MethodDescription> Commands { get; set; } = new List<MethodDescription>();

        public List<MethodDescription> Queries { get; set; } = new List<MethodDescription>();

        public List<MethodDescription> Events { get; set; } = new List<MethodDescription>();

        public List<ParameterDescriptor> ConstructorParameters { get; set; } = new List<ParameterDescriptor>();

        public List<PropertyAssignDescriptor> InjectedProperties { get; set; } = new List<PropertyAssignDescriptor>();

        public List<SubscriptionDescriptor> Subscriptions { get; set; } = new List<SubscriptionDescriptor>();

        public IEnumerable<ParameterDescriptor> Constructor => ConstructorParameters.Concat(InjectedProperties.Select(x => x.ToCamelCase()));

        public IEnumerable<string> BaseConstructor => ConstructorParameters.Select(x => x.Name);

        public bool HasSubscriptions => Subscriptions.Count > 0;
    };
}