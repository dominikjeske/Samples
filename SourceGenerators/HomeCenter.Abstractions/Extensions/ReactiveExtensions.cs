using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCenter.Model.Extensions
{
    public static class ReactiveExtensions
    {
        public static IObservable<TSource> DistinctForTime<TSource>(this IObservable<TSource> source, TimeSpan expirationTime)
        {
            return DistinctForTime(source, expirationTime, Scheduler.Default);
        }

        public static IObservable<TSource> DistinctForTime<TSource>(this IObservable<TSource> source, TimeSpan expirationTime, IScheduler scheduler)
        {
            return DistinctForTime<TSource>(source, expirationTime, EqualityComparer<TSource>.Default, scheduler);
        }

        public static IObservable<TSource> DistinctForTime<TSource>(this IObservable<TSource> source, TimeSpan expirationTime, IEqualityComparer<TSource> comparer)
        {
            return DistinctForTime(source, expirationTime, comparer, Scheduler.Default);
        }

        public static IObservable<TSource> DistinctForTime<TSource>(this IObservable<TSource> source, TimeSpan expirationTime,
                                                                    IEqualityComparer<TSource> comparer, IScheduler scheduler)
        {
            return source
                .Timestamp(scheduler)
                .Scan(new
                {
                    Acumulator = new Dictionary<TSource, DateTimeOffset>(comparer),
                    Next = Observable.Empty<TSource>()
                }, (state, item) => new
                {
                    Acumulator = state.Acumulator
                                      .Where(acumulator => item.Timestamp - acumulator.Value < expirationTime)
                                      .Concat(CheckForValueOrTimeout(expirationTime, state.Acumulator, item) ? Enumerable.Repeat(new KeyValuePair<TSource, DateTimeOffset>(item.Value, item.Timestamp), 1) : Enumerable.Empty<KeyValuePair<TSource, DateTimeOffset>>())
                                      .ToDictionary(acumulator => acumulator.Key, acumulator => acumulator.Value, comparer),
                    Next = CheckForValueOrTimeout(expirationTime, state.Acumulator, item) ? Observable.Return(item.Value) : Observable.Empty(item.Value)
                }
                )
                .SelectMany(t => t.Next);
        }

        private static bool CheckForValueOrTimeout<TSource>(TimeSpan expirationTime, Dictionary<TSource, DateTimeOffset> state, Timestamped<TSource> item)
        {
            return !state.ContainsKey(item.Value) || item.Timestamp - state[item.Value] >= expirationTime;
        }

        public static long MilisecondsToTicks(this int ms)
        {
            return TimeSpan.FromMilliseconds(ms).Ticks;
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
           source.Select(message => Observable.FromAsync(() => onNextAsync(message)))
                 .Concat()
                 .Subscribe();

        public static void Subscribe<T>(this IObservable<T> source, Func<T, Task> onNextAsync, Action<Exception> onError, CancellationToken cancellationToken, IScheduler observeOn) =>
            source.Select(x => Observable.FromAsync(() => onNextAsync(x)))
                  .Concat()
                  .ObserveOn(observeOn)
                  .Subscribe(_ => { }, onError, cancellationToken);
    }
}