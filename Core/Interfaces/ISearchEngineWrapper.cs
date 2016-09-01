using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Search engine wrapper that allows to perform extra operations directly related to searching
    /// </summary>
    /// <typeparam name="TResult">Type of items returned by the search</typeparam>
    public interface ISearchEngineWrapper : ISearchAsync, ISearchFinishedHandler
    {
        event Action SearchStarted;

        /// <summary>
        /// The amount of time it took for the last search to finish
        /// </summary>
        TimeSpan TimeElapsed { get; }
    }

    /// <summary>
    /// Search engine wrapper that allows to perform extra operations directly related to searching and exposes the Search Engine it wraps
    /// </summary>
    /// <typeparam name="TResult">Type of items returned by the search</typeparam>
    public interface ISearchEngineWrapper<TResult> : ISearchEngineWrapper, ISearchEngine<TResult>
    {
        ISearchEngine<TResult> SearchEngine { get; }
    }
}
