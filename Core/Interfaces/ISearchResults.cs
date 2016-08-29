using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Interface for typed search results classes that can add found items to the underlying results collection
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ISearchResults<TResult> : ISearchResults
    {
        /// <summary>
        /// Adds an item to the underlying results collection
        /// </summary>
        /// <param name="item"></param>
        void Add(TResult item);
    }

    /// <summary>
    /// Common interface for all search results classes with methods that initialize them before search
    /// Implementing classes must use ReInitialize to get ready for handling search results so that new objects of this type don't need to be created for each search
    /// </summary>
    public interface ISearchResults : IAmReInitializable
    {
        /// <summary>
        /// Occurs right before actual ReInitialization of this object occurs
        /// </summary>
        event Action BeforeReInitialization;
    }
}
