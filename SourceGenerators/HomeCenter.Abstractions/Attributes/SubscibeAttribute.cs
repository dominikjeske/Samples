using System;

namespace HomeCenter.Abstractions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SubscribeAttribute : Attribute
    {
        /// <summary>
        /// Instruct proxy generator to register for message
        /// </summary>
        /// <param name="subscribeToParent">When set to True messages will be registered on parent of the actor (useful when actor is load balanced)</param>
        public SubscribeAttribute(bool subscribeToParent = false)
        {
            SubscribeToParent = subscribeToParent;
        }

        public bool SubscribeToParent { get; }
    }
}