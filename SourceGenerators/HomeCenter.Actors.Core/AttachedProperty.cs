using HomeCenter.Abstractions;
using Light.GuardClauses;

namespace HomeCenter.Actors.Core
{
    public class AttachedProperty : BaseObject
    {
        public AttachedProperty(string? service, string? attachedActor, string? attachedArea)
        {
            service = service.MustNotBeNullOrWhiteSpace();
            attachedActor = attachedActor.MustNotBeNullOrWhiteSpace();

            Service = service;
            AttachedActor = attachedActor;
            AttachedArea = attachedArea;
        }

        public string Service { get; }

        public string AttachedActor { get; }

        public string? AttachedArea { get; }
    }
}