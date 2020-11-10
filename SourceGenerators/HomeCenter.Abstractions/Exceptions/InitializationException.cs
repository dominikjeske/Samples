using System;

namespace HomeCenter.Abstractions
{
    public class InitializationException : Exception
    {
        public InitializationException() : base()
        {
        }

        protected InitializationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
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