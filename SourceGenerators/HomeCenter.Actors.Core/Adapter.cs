using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCenter.Abstractions;
using HomeCenter.Messages.Commands.Device;
using HomeCenter.Messages.Events.Device;

namespace HomeCenter.Actors.Core
{
    public abstract class Adapter : DeviceActor
    {
        protected readonly List<string> _requierdProperties = new List<string>();

        protected IList<string> RequierdProperties() => _requierdProperties;

        protected async Task<T> UpdateState<T>(string stateName, T oldValue, T newValue, IDictionary<string, string> additionalProperties = null)
        {
            if (newValue == null || EqualityComparer<T>.Default.Equals(oldValue, newValue)) return oldValue;

            if (_requierdProperties.Count > 0)
            {
                if (additionalProperties == null || additionalProperties.Count != _requierdProperties.Count || !_requierdProperties.SequenceEqual(additionalProperties.Keys))
                {
                    throw new ArgumentException($"Update state on component {Uid} should be invoked with required properties: {string.Join(",", _requierdProperties)}");
                }
            }

            await MessageBroker.Publish(PropertyChangedEvent.Create(Uid, stateName, oldValue?.ToString(), newValue.ToString(), additionalProperties), Uid);
            return newValue;
        }

        protected Task ScheduleDeviceRefresh(TimeSpan interval) => MessageBroker.SendWithSimpleRepeat(ActorMessageContext.Create(Self, RefreshCommand.Default), interval, _disposables.Token);

        protected Task ScheduleDeviceLightRefresh(TimeSpan interval) => MessageBroker.SendWithSimpleRepeat(ActorMessageContext.Create(Self, RefreshLightCommand.Default), interval, _disposables.Token);

        protected Task DelayDeviceRefresh(TimeSpan interval) => MessageBroker.SendAfterDelay(ActorMessageContext.Create(Self, RefreshLightCommand.Default), interval, false, _disposables.Token);
    }
}