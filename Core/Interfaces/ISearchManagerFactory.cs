using System;
using System.Collections.Generic;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Factory for creating search managers 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ISearchManagerFactory<TResult, TConsumer> where TConsumer : ISearchResultsConsumer<TResult>
    {
        ISearchManager Create(ISearchEngine<TResult> searchEngine, TConsumer searchResultsOutput, IOperationTracker operationTracker);
    }
}
