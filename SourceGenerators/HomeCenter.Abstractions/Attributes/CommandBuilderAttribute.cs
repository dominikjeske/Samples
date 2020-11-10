using System;

namespace HomeCenter.Abstractions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class CommandBuilderAttribute : Attribute
    {
        public static string Name = nameof(CommandBuilderAttribute).Replace("Attribute", string.Empty);
    }
}