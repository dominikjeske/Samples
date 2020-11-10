using System;

namespace HomeCenter.Abstractions
{
    public interface IDayOffProvider
    {
        bool IsDayOff(DateTime date);

        string Name { get; }
    }
}