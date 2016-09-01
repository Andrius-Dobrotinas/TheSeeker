using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace TheSeeker
{
    /// <summary>
    /// Base SearchEngine implementation with methods for calling event handlers.
    /// Thread unsafe in terms of event un/subscription in for better performance
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class SearchEngineBase<TResult> : ISearchEngine<TResult>
    {
        public virtual event EventHandler<Exception> SearchExceptionHandler;
        public virtual event ResultHandler<TResult> ItemFoundHandler;

        protected virtual void OnItemFound(TResult file)
        {
            ItemFoundHandler?.Invoke(file);
        }

        protected virtual void OnSearchException(Exception exception)
        {
            SearchExceptionHandler?.Invoke(this, exception);
        }

        public abstract void Search(string location, string searchPattern, CancellationToken cancellationToken);
    }
}