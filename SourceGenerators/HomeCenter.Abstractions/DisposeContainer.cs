using System;
using System.Collections.Generic;
using System.Threading;

namespace HomeCenter.Abstractions
{
    public class DisposeContainer : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private readonly object _disposeLock = new object();
        private volatile bool _disposed;
        private readonly CancellationTokenSource _cancelationTokenSource = new CancellationTokenSource();

        public CancellationToken Token => _cancelationTokenSource.Token;

        public DisposeContainer(params IDisposable[] disposables)
        {
            _disposables.AddRange(disposables);
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Add(params IDisposable[] disposables)
        {
            _disposables.AddRange(disposables);
        }

        public void Dispose()
        {
            if (_disposed) return;
            lock (_disposeLock)
            {
                _cancelationTokenSource.Cancel();

                if (_disposed) return;
                foreach (var d in _disposables)
                {
                    d.Dispose();
                }
                _disposables.Clear();
                _disposed = true;
            }
        }
    }
}