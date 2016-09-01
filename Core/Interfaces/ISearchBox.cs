using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Search managers are responsible for the disposal of objects supplied to them via constructor
    /// </summary>
    public interface ISearchBox : ISearchFinishedHandler, IDisposableOnce
    {
        /// <summary>
        /// Indicates whether a search is currently running
        /// </summary>
        bool IsSearching { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchLocation"></param>
        /// <param name="searchPattern"></param>
        /// <returns>true if new search started, false if there was an active search running</returns>
        bool Search(string searchLocation, string searchPattern);

        /// <summary>
        /// Sends request to stop current search
        /// </summary>
        void Stop();

        /// <summary>
        /// Sends request to stop current search and blocks until seach actually stops
        /// </summary>
        void StopAndWait();

        /// <summary>
        /// Occurs immediately after a request to stop current search when calling non-waiting Stop method
        /// </summary>
        event EventHandler SearchStopped;
    }
}
