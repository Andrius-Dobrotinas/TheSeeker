using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Delegate for handling result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    public delegate void ResultHandler<TResult>(TResult item);

    /// <summary>
    /// Implementations must perform search and return search results one by one via ItemFound event
    /// </summary>
    /// <typeparam name="TResult">Type of items returned by the search</typeparam>
    public interface ISearchEngine<TResult> : ISearchAsync
    {
        //event EventHandler<TResult> FoundItemHandler;

        /// <summary>
        /// Occurs when an item is found
        /// </summary>
        event ResultHandler<TResult> ItemFoundHandler;

        /// <summary>
        /// Occurs when an exception is encountered during search
        /// </summary>
        event EventHandler<Exception> SearchExceptionHandler;
    }
}
