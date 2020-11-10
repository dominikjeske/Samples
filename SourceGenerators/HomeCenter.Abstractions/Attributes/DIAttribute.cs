using System;

namespace HomeCenter.Abstractions
{
    /// <summary>
    /// Property injected in proxy generated class
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DIAttribute : Attribute
    {
    }
}