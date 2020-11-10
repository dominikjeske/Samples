using System.Reactive.Concurrency;

namespace HomeCenter.Abstractions
{
    /// <summary>
    /// Provides provider for IScheduler - it helps to "mock time" in unit tests
    /// </summary>
    public interface IConcurrencyProvider
    {
        IScheduler Scheduler { get; }
        IScheduler Task { get; }
        IScheduler Thread { get; }
    }
}