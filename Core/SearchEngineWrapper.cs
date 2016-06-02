using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace TheSeeker
{
    /// <summary>
    /// Measures search duration and invokes SearchFinished event handlers when search finishes
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class SearchEngineWrapper<TResult> : ISearchEngineWrapper<TResult>
    {
        private Stopwatch watch = new Stopwatch();

        public ISearchEngine<TResult> SearchEngine { get; }
        public virtual event EventHandler<TimeSpan> SearchFinished;
        public virtual event Action SearchStarted;
        public TimeSpan TimeElapsed => watch.Elapsed;

        public SearchEngineWrapper()
        {
            
        }

        public SearchEngineWrapper(ISearchEngine<TResult> searchEngine)
        {
            SearchEngine = searchEngine;
        }

        public virtual void Search(string location, string searchPattern, CancellationToken cancellationToken)
        {
            SearchStarted?.Invoke();

            watch.Restart();
            SearchEngine.Search(location, searchPattern, cancellationToken);
            watch.Stop();

            SearchFinished?.Invoke(this, TimeElapsed);
        }
    }
}