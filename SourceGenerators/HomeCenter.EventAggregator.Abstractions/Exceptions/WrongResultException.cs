using System;

namespace HomeCenter.EventAggregator.Exceptions
{
    public class WrongResultException : Exception
    {
        public WrongResultException(object actual, object excepted) : base($"Result value is {actual} but excepted value is {excepted}")
        {
        }
    }
}