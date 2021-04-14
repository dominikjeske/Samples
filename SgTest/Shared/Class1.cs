using Proto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ProxyAttribute : Attribute
    {
        public static string Name = nameof(ProxyAttribute).Replace("Attribute", string.Empty);
    }

    public interface IValueConverter
    {
        string Convert(string old);

        string ConvertBack(string old);
    }

    public class CapabilitiesQuery : Query
    {
        public static CapabilitiesQuery Default = new CapabilitiesQuery();
    }

    public class Query 
    {
        public Query()
        {
        }

       
    }

    public class Command 
    {
    }

    public class Event
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DIAttribute : Attribute
    {
    }

    public abstract class Adapter 
    {
        protected virtual Task ReceiveAsyncInternal(IContext context)
        {
            return Task.CompletedTask;
        }

        protected virtual Task<bool> HandleSystemMessages(IContext context)
        {
            

            return Task.FromResult(false);
        }

        protected object FormatMessage(object rawMessage)
        {

            return rawMessage;
        }

        protected virtual Task UnhandledMessage(object message)
        {
            return Task.CompletedTask;
        }
    }

    public abstract class ActorMessage 
    {
        public IContext Context { get; set; }
    }

    /// <summary>
    ///     Added to all generated classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class GeneratedCodeAttribute : Attribute
    {
    }

}