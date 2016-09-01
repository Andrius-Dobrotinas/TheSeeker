using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker
{
    /// <summary>
    /// Builds a search manager that works with a Windows Forms-based Consumer
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class SearchManagerBase<TResult> : ISearchManager<TResult>
    {
        public abstract void Create(ISearchEngineWrapper<TResult> searchEngineWrapper, IOperationTracker operationTracker);

        public ISearchEngineWrapper SearchEngineWrapper { get; protected set; }
        public IOperationTracker OperationTracker { get; protected set; }
        public ISearchBox SearchBox { get; protected set; }

        #region IDisposable Support
        public bool IsDisposed { get; protected set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    SearchBox.Dispose();
                    (SearchEngineWrapper as IDisposable)?.Dispose();
                    (OperationTracker as IDisposable)?.Dispose();
                    OnDispose();
                }

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void OnDispose()
        {

        }
        #endregion
    }
}
