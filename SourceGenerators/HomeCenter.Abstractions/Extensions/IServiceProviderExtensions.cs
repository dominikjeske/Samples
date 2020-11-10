using System;

namespace HomeCenter
{
    public static class IServiceProviderExtensions
    {
        public static T Get<T>(this IServiceProvider serviceProvider) where T : class
        {
            //if (serviceProvider.GetService(typeof(T)) is not T service) throw new InvalidOperationException($"Cannot create service of type {typeof(T).Name}");

            return serviceProvider.GetService(typeof(T)) as T;
        }
    }
}