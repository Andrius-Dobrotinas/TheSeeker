using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheSeeker
{
    /// <summary>
    /// Manages searches and handles the results using the supplied search results handler
    /// </summary>
    public class SearchBox<TResult> : ISearchBox
    {
        private ISearchEngineWrapper<TResult> searchEngine;
        private ISearchResultsConsumer<TResult> results;

        private Task searchTask;
        private CancellationTokenSource searchCancellation;

        /// <summary>
        /// Indicates whether a search is currently running
        /// </summary>
        public bool IsSearching => searchTask?.Status == TaskStatus.Running;

        /// <summary>
        /// Occurs when search finishes
        /// </summary>
        public event EventHandler<TimeSpan> SearchFinished
        {
            add
            {
                searchEngine.SearchFinished += value;
            }
            remove
            {
                searchEngine.SearchFinished -= value;
            }
        }

        /// <summary>
        /// Occurs immediately after a request to stop current search when calling non-waiting Stop method
        /// </summary>
        public event EventHandler<TimeSpan> SearchStopped;

        /// <summary>
        /// Initializes Search Manager with the supplied search engine and search results handler
        /// Disposes of these objects on Dispose
        /// </summary>
        /// <param name="searchResultsHandler"></param>
        public SearchBox(ISearchEngineWrapper<TResult> searchEngine, ISearchResultsConsumer<TResult> searchResults)
        {
            if (searchEngine == null)
                throw new ArgumentNullException(nameof(searchEngine));
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            this.searchEngine = searchEngine;
            results = searchResults;
            searchEngine.ItemFoundHandler += (item) => results.Add(item);
        }

        /// <summary>
        /// Starts searching on a separate thread if no search is currently being performed
        /// </summary>
        /// <param name="searchLocation"></param>
        /// <param name="searchPattern"></param>
        /// <returns>true if new search started, false if there was an active search running</returns>
        public bool Search(string searchLocation, string searchPattern)
        {
            if (!IsSearching)
            {
                searchCancellation = new CancellationTokenSource();
                results.ReInitialize();

                // Start it in a new thread so that the UI did not get blocked
                searchTask = Task.Factory.StartNew(() =>
                {
                    searchEngine.Search(searchLocation, searchPattern, searchCancellation.Token);
                }, searchCancellation.Token);

                // TODO: handle search exceptions (maybe invoke an OnExecption event handler from ContinuationTask or something like that?

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Cancels current search
        /// </summary>
        public void Stop()
        {
            Stop(false);
        }

        /// <summary>
        /// Cancels current search and blocks until it actually stops
        /// </summary>
        public void StopAndWait()
        {
            Stop(true);
        }

        protected void Stop(bool wait)
        {
            searchCancellation?.Cancel();
            if (wait)
                searchTask?.Wait();
            else
                SearchStopped?.Invoke(this, searchEngine.TimeElapsed);
        }

        #region IDisposable Support
        public bool IsDisposed { get; protected set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Stop(true);
                    searchCancellation?.Dispose();
                }

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
