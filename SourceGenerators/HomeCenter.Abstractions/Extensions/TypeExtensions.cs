using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using HomeCenter.Abstractions;

namespace HomeCenter
{
    public static class TypeExtensions
    {
        public static void MustDeriveFrom(this Type type, Type baseType)
        {
            if (type.IsAssignableFrom(baseType))
            {
                throw new ArgumentException($"Type '{type.Name}' is not '{baseType.Name}'");
            }
        }

        public static void MustDeriveFrom<T>(this Type type) => type.MustDeriveFrom(typeof(T));

        public static IEnumerable<MethodInfo> GetMethodsBySignature(this Type type, params Type[] parameterTypes)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where((m) =>
            {
                var parameters = m.GetParameters();
                if ((parameterTypes == null || parameterTypes.Length == 0))
                    return parameters.Length == 0;
                if (parameters.Length != parameterTypes.Length)
                    return false;
                for (int i = 0; i < parameterTypes.Length; i++)
                {
                    if (parameters[i].ParameterType != parameterTypes[i])
                        return false;
                }
                return true;
            });
        }

        public static Func<Query, Task<object>> WrapTaskToGenericTask(this MethodInfo handler, object objectInstance)
        {
            if
            (
                   handler.ReturnType.BaseType != typeof(Task)
                || handler.ReturnType.GetGenericArguments()?.Length != 1
                || handler.GetParameters().FirstOrDefault()?.ParameterType != typeof(Query)
            )
            {
                throw new ArgumentException($"Input method for {nameof(WrapTaskToGenericTask)} should have following syntax: Task<ReturnType> Method(Command command)");
            }

            var commandParameter = Expression.Parameter(typeof(Query), "commandParameter");
            var task = Expression.Call(Expression.Constant(objectInstance), handler, commandParameter);
            if (task.Type != typeof(Task<object>))
            {
                task = Expression.Call(typeof(TaskExtensions), "ToGenericTaskResult", task.Type.GetGenericArguments(), task);
            }
            return Expression.Lambda<Func<Query, Task<object>>>(task, commandParameter).Compile();
        }

        public static Func<Query, Task<object>> WrapSimpleTypeToGenericTask(this MethodInfo handler, object objectInstance)
        {
            if
            (
                  handler.ReturnType.BaseType == typeof(Task)
               || handler.ReturnType == typeof(Task)
               || handler.ReturnType == typeof(void)
               || handler.GetParameters().FirstOrDefault()?.ParameterType != typeof(Query)
            )
            {
                throw new ArgumentException($"Input method for {nameof(WrapSimpleTypeToGenericTask)} should have following syntax: ReturnType Method(Command command)");
            }

            var commandParameter = Expression.Parameter(typeof(Query), "commandParameter");
            var result = Expression.Call(Expression.Constant(objectInstance), handler, commandParameter);
            result = Expression.Call(typeof(Task), "FromResult", new Type[] { typeof(object) }, Expression.Convert(result, typeof(object)));
            return Expression.Lambda<Func<Query, Task<object>>>(result, commandParameter).Compile();
        }

        public static Func<Command, Task<object>> WrapReturnTypeToGenericTask(this MethodInfo handler, object objectInstance)
        {
            var commandParameter = Expression.Parameter(typeof(Command), "commandParameter");
            var result = Expression.Call(Expression.Constant(objectInstance), handler, commandParameter);
            result = Expression.Call(typeof(Task), "FromResult", new Type[] { typeof(object) }, Expression.Convert(result, typeof(object)));
            return Expression.Lambda<Func<Command, Task<object>>>(result, commandParameter).Compile();
        }

        public static T CreateInstance<T>(this Type type) where T : class
        {
            return type.GetConstructors().FirstOrDefault()?.Invoke(null) as T;
        }
    }
}