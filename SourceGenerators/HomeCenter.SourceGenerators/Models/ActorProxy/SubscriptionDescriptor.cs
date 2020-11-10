using System.Threading.Tasks;

namespace HomeCenter.SourceGenerators
{
    internal class SubscriptionDescriptor
    {
        public string MessageType { get; set; }

        public string ReturnType { get; set; }

        public string ReturnTypeGenericArgument { get; set; }

        public bool SubscribeOnParent { get; set; }

        public bool IsReturnTask => string.CompareOrdinal(ReturnType, nameof(Task)) == 0;

        public bool HasReturnType => !string.IsNullOrWhiteSpace(ReturnType);
    }
}