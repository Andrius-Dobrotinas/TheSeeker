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
    public class SearchManager : ISearchManager
    {
        private ISearchEngineWrapper searchEngine;
        private ISearchResults results;

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
        /// Initializes SearchManager with the supplied search engine and search results handler and adding all found items to it
        /// </summary>
        /// <param name="searchResultsHandler"></param>
        public SearchManager(ISearchEngineWrapper searchEngine, ISearchResults searchResults)
        {
            this.searchEngine = searchEngine;
            results = searchResults;
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

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Stops the current search
        /// </summary>
        public void Stop()
        {
            searchCancellation.Cancel();
        }
        
    }
}
