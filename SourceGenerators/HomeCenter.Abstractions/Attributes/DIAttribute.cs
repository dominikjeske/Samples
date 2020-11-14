using System;

namespace HomeCenter.Abstractions
{
    /// <summary>
    ///     Property injected in proxy generated class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DIAttribute : Attribute
    {
    }
}