using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Builds a search manager that works with a Windows Forms-based Consumer
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TConsumer"></typeparam>
    public class SearchManagerFactory<TResult, TConsumer> : SearchManagerFactoryBase<TResult, TConsumer>
        where TConsumer : ISearchResultsConsumerControl<TResult>
    {
        protected override SearchComponents<TResult> SetupComponents(ISearchEngine<TResult> searchEngine, TConsumer searchResultsConsumer, IOperationTracker operationTracker)
        {
            var resultsList = new BindingList2<TResult>(operationTracker, searchResultsConsumer);
            ISearchResults<TResult> searchResults = new SearchResults<TResult>(resultsList);

            ISearchEngineWrapper<TResult> searchEngineWrapper = new SearchEngineWrapper<TResult>(searchEngine);

            searchEngine.ItemFoundHandler += (file) => searchResults.Add(file);

            searchEngineWrapper.SearchStarted += () => {
                searchResultsConsumer.ReInitialize();
                //operationTracker.Start() //TODO?
                searchResultsConsumer.Status = "Searching...";
            };

            searchEngineWrapper.SearchFinished += (source, timeElapsed) => {
                operationTracker.Stop();
                searchResultsConsumer.Status = $"Search finished ({timeElapsed})";
            };

            return new SearchComponents<TResult>
            {
                SearchEngineWrapper = searchEngineWrapper,
                SearchResults = searchResults
            };
        }
    }
}