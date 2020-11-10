using System.Collections.Generic;
using HomeCenter.Abstractions;

namespace HomeCenter.Actors.Core
{
    public abstract class Service : DeviceActor
    {
        public List<AttachedProperty> ComponentsAttachedProperties
        {
            get => this.As<List<AttachedProperty>>(MessageProperties.ComponentsAttachedProperties);
            set => this.SetProperty(MessageProperties.ComponentsAttachedProperties, value);
        }

        public List<AttachedProperty> AreasAttachedProperties
        {
            get => this.As<List<AttachedProperty>>(MessageProperties.AreasAttachedProperties);
            set => this.SetProperty(MessageProperties.AreasAttachedProperties, value);
        }
    }
}