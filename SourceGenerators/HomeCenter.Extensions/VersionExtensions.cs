using System;

namespace HomeCenter.Extensions
{
    public static class VersionExtensions
    {
        public static bool Differ(this Version current, Version other)
        {
            return !(current.Major == other.Major && current.Minor == other.Minor && current.Build == other.Build);
        }
    }
}