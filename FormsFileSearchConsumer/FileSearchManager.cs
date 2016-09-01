using System;
using System.Collections.Generic;
using System.Linq;
using TheSeeker.Forms;

namespace TheSeeker.FileSystem.Forms
{
    /// <summary>
    /// Builds a file search manager that works with a Windows Forms-based Consumer
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class FileSearchManager<TResult> : SearchManagerBase<TResult>
        where TResult : System.IO.FileSystemInfo
    {
        public override void Create(ISearchEngineWrapper<TResult> searchEngineWrapper, IOperationTracker operationTracker)
        {
            SearchEngineWrapper = searchEngineWrapper;
            OperationTracker = operationTracker;
            form = new FileSearchResultsForm<TResult>();
            BindingList2<TResult> resultsList = new BindingList2<TResult>(operationTracker, form);

            var consumer = new SearchResultsConsumerWithForm<TResult>(resultsList, form);

            searchResultsConsumer = consumer;
            SearchBox = new SearchBox<TResult>(searchEngineWrapper, consumer);

            form.FormHide += (sender, e) =>
            {
                if (!SearchBox.IsDisposed)
                    SearchBox.Stop();
            };

            SearchEngineWrapper.SearchStarted += () =>
            {
                //operationTracker.Start() //TODO? Should I?
                form.Status = "Searching...";
            };

            SearchEngineWrapper.SearchFinished += (source, timeElapsed) =>
            {
                operationTracker.Stop();
                form.Status = "Stopped";
            };
        }

        private SearchResultsForm<TResult> form;
        private ISearchResultsConsumer searchResultsConsumer;

        #region IDisposable Support
        protected override void OnDispose()
        {
            (searchResultsConsumer as IDisposable)?.Dispose();
            form.Dispose();
        }
        #endregion
    }
}