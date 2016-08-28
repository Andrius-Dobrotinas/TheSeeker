using System;
using System.Collections.Generic;

namespace TheSeeker
{
    /// <summary>
    /// Interface for factories for creating search managers 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ISearchManagerFactory<TResult, TConsumer>
        where TConsumer : ISearchResultsConsumer<TResult>
    {
        /// <summary>
        /// Implementations must create a Search Manager using the supplied components
        /// </summary>
        ISearchManager Create(ISearchEngine<TResult> searchEngine, TConsumer searchResultsConsumer, IOperationTracker operationTracker);
    }
}
