using System;
using System.Runtime.Serialization;

namespace HomeCenter.Abstractions
{
    public class InitializationException : Exception
    {
        public InitializationException()
        {
        }

        protected InitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InitializationException(string message) : base(message)
        {
        }

        public InitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}