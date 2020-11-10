using System;

namespace HomeCenter.Abstractions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ProxyAttribute : Attribute
    {
        public static string Name = nameof(ProxyAttribute).Replace("Attribute", string.Empty);
    }
}