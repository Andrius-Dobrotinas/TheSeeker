using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker.Forms
{
    public class SearchManagerFactory<TResult, TConsumer> : ISearchManagerFactory<TResult, TConsumer> where TConsumer : ISearchResultsConsumerControl<TResult>
    {        
        public ISearchManager Create(ISearchEngine<TResult> searchEngine, TConsumer searchResultsConsumer, IOperationTracker operationTracker)
        {
            var resultsList = new BindingList2<TResult>(operationTracker, searchResultsConsumer);
            ISearchResults<TResult> searchResults = new SearchResults<TResult>(resultsList);

            ISearchEngineWrapper<TResult> searchEngineWrapper = new SearchEngineWrapper<TResult>(searchEngine);

            searchEngine.ItemFoundHandler += (file) => searchResults.Add(file);

            // Move this out??
            searchEngineWrapper.SearchStarted += () => {
                searchResultsConsumer.ReInitialize();
                //operationTracker.Start()?
                searchResultsConsumer.Status = "Searching...";
            };

            searchEngineWrapper.SearchFinished += (source, timeElapsed) => {
                operationTracker.Stop();
                searchResultsConsumer.Status = $"Search finished ({timeElapsed})";
            };

            return new SearchManager(searchEngineWrapper, searchResults);
        }
    }
}
