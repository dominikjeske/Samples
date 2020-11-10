using System;

namespace HomeCenter.Abstractions
{
    /// <summary>
    /// Exception throwed when configuration is not properly formated
    /// </summary>
    public class ConfigurationException : Exception
    {
        public ConfigurationException() : base()
        {
        }

        protected ConfigurationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public ConfigurationException(string message) : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}