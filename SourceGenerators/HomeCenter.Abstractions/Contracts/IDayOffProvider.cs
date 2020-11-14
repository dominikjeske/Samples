using System;

namespace HomeCenter.Abstractions
{
    public interface IDayOffProvider
    {
        string Name { get; }
        bool IsDayOff(DateTime date);
    }
}