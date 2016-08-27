using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker
{
    /// <summary>
    /// Base class for factories for creating Search Managers
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TConsumer"></typeparam>
    public abstract class SearchManagerFactoryBase<TResult, TConsumer> : ISearchManagerFactory<TResult, TConsumer>
        where TConsumer : ISearchResultsConsumer<TResult>
    {
        /// <summary>
        /// Creates a Search Manager using the supplied components
        /// </summary>
        public ISearchManager Create(ISearchEngine<TResult> searchEngine, TConsumer searchResultsConsumer, IOperationTracker operationTracker)
        {
            var searchComponents = SetupComponents(searchEngine, searchResultsConsumer, operationTracker);

            return new SearchManager(searchComponents.SearchEngineWrapper, searchComponents.SearchResults);
        }

        /// <summary>
        /// Search components for Search Manager must be produced and set up here
        /// </summary>
        protected abstract SearchComponents<TResult> SetupComponents(ISearchEngine<TResult> searchEngine, TConsumer searchResultsConsumer, IOperationTracker operationTracker);
    }
}
