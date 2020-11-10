using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCenter.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Creates a task that will complete when all of the <seealso cref="Task"/> objects in an enumerable collection have completed.
        /// </summary>
        /// <param name="tasks">List of tasks to await</param>
        public static Task WhenAll(this IEnumerable<Task> tasks) => Task.WhenAll(tasks);

        /// <summary>
        /// Creates a task that will complete when all of the <seealso cref="Task"/> objects in an enumerable collection have completed or time out occurs
        /// </summary>
        /// <param name="tasks">List of tasks to await</param>
        public static async Task<Task> WhenAll(this IEnumerable<Task> tasks, TimeSpan timeout)
        {
            var timeoutTask = Task.Delay(timeout);
            var workingTask = Task.WhenAll(tasks);
            var result = await Task.WhenAny(timeoutTask, workingTask);

            if (result == timeoutTask) throw new TimeoutException();

            return result;
        }

        /// <summary>
        /// Creates a task that will complete when all of the <seealso cref="Task"/> objects in an enumerable collection have completed or token was canceled
        /// </summary>
        /// <param name="tasks">List of tasks to await</param>
        public static async Task<Task> WhenAll(this IEnumerable<Task> tasks, CancellationToken cancellationToken)
        {
            var cancellTask = cancellationToken.WhenCanceled();
            var workingTask = Task.WhenAll(tasks);
            var result = await Task.WhenAny(cancellTask, workingTask);

            if (result == cancellTask) throw new TaskCanceledException();

            return result;
        }

        /// <summary>
        /// Execute <paramref name="func"/> on each data from <paramref name="data"/>
        /// </summary>
        /// <typeparam name="T">Type of input data</typeparam>
        /// <typeparam name="R">Type of return value</typeparam>
        /// <param name="data">Data on which we are operate</param>
        /// <param name="func"><seealso cref="Func"/> that change data</param>
        /// <returns></returns>
        public static async Task<IEnumerable<(T Input, R Result)>> WhenAll<T, R>(this IEnumerable<T> data, Func<T, Task<R>> func)
        {
            var discoveryRequests = data.Select(d => new { ResultTask = func(d), Input = d }).ToArray();
            await Task.WhenAll(discoveryRequests.Select(c => c.ResultTask));
            return discoveryRequests.Select(d => (d.Input, d.ResultTask.Result));
        }

        /// <summary>
        /// Waits for first task to finish or time out
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="tasks"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<R> WhenAny<R>(this IEnumerable<Task> tasks, TimeSpan timeout) where R : class
        {
            var timeoutTask = Task.Delay(timeout);
            var result = await Task.WhenAny(tasks.ToList().AddChained(timeoutTask));

            if (result == timeoutTask) throw new TimeoutException();

            return (result as Task<R>)?.Result;
        }

        /// <summary>
        /// Waits for task to complete or timeout  / canceled
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="task"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<R> WhenDone<R>(this Task<R> task, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            var timeoutTask = Task.Delay(timeout, cancellationToken);
            var result = await Task.WhenAny(new Task[] { task, timeoutTask });

            if (result == timeoutTask)
            {
                if (cancellationToken.IsCancellationRequested && timeoutTask.Status == TaskStatus.Canceled)
                {
                    throw new OperationCanceledException();
                }
                if (timeoutTask.Status == TaskStatus.RanToCompletion && !cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException();
                }

                throw new InvalidOperationException("Not supported result in Timeout");
            }

            return (result as Task<R>).Result;
        }

        /// <summary>
        /// Change <seealso cref="CancellationToken"/> into <seealso cref="Task"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task WhenCanceled(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }

        /// <summary>
        /// Cast <paramref name="task"/> into <seealso cref="Task<typeparamref name="T"/>"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static Task<T> Cast<T>(this Task<object> task)
        {
            var tcs = new TaskCompletionSource<T>();
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult((T)t.Result);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        /// <summary>
        /// Cast <paramref name="task"/> into <seealso cref="Task<typeparamref name="T"/>"/>  or <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Task<T> Cast<T>(this Task task, T defaultValue)
        {
            var tcs = new TaskCompletionSource<T>();
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(defaultValue);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        /// <summary>
        /// Cast generic <paramref name="source"/> into <seealso cref="Task" of object/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Task<object> ToGenericTaskResult<TResult>(this Task<TResult> source) => source.ContinueWith(t => (object)t.Result);
    }
}