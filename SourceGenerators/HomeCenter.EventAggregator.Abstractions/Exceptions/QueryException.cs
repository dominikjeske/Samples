using System;
using System.Runtime.Serialization;

namespace HomeCenter.EventAggregator.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException()
        {
        }

        protected QueryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public QueryException(string message) : base(message)
        {
        }

        public QueryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}