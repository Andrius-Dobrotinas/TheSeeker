using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace TheSeeker.Forms
{
    public class SearchManagerFactory : ISearchManagerFactory
    {
        private int listRefreshInterval;
        private string searchEngineClassString;

        public SearchManagerFactory()
        {
            ReadConfiguration();
        }

        public ISearchManager CreateSearchManager()
        {
            ISearchEngine<FileSystemInfo> searchEngine = GetSearchEngine<FileSystemInfo>();

            ISearchResultsConsumerControl<FileSystemInfo> searchResultsOutput = new FileSearchResultsForm();
            IOperationTracker operationTracker = new OperationIntervalClock(listRefreshInterval);
            ISearchResults<FileSystemInfo> searchResults = GetSearchResults<FileSystemInfo>(searchResultsOutput, operationTracker);

            ISearchEngineWrapper<FileSystemInfo> searchEngineWrapper = new SearchEngineWrapper<FileSystemInfo>(searchEngine);

            searchEngine.ItemFoundHandler += (file) => searchResults.Add(file);

            searchEngineWrapper.SearchStarted += () => {
                searchResultsOutput.ReInitialize();
                searchResultsOutput.Status = "Searching...";
            };

            searchEngineWrapper.SearchFinished += (source, timeElapsed) => {
                operationTracker.Stop();
                searchResultsOutput.Status = $"Search finished ({timeElapsed})";
            };

            return new SearchManager(searchEngineWrapper, searchResults);
        }

        protected ISearchResults<TResult> GetSearchResults<TResult>(IDataConsumerControl<TResult> searchResultsOutput, IOperationTracker operationTracker)
        {
            var resultsList = new BindingList2<TResult>(operationTracker, searchResultsOutput);
            return new SearchResults<TResult>(resultsList);
        }

        protected ISearchEngine<TResult> GetSearchEngine<TResult>()
        {
            
            Type type = Type.GetType(searchEngineClassString, true);

            try
            {
                return Activator.CreateInstance(type) as ISearchEngine<TResult>;
            }
            catch (Exception e)
            {
                throw new TypeInitializationException(searchEngineClassString, e);
            }
        }

        protected void ReadConfiguration()
        {
            string assemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;

            var config = ConfigurationManager.GetSection(assemblyName) as ConfigSection;

            listRefreshInterval = config.ListRefreshInterval;
            searchEngineClassString = config.SearchEngine;
        }
    }
}
