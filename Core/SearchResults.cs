using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker
{
    /// <summary>
    /// A wrapper class that gives search manager access to ICollection classes
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class SearchResults<TResult> : ISearchResults<TResult>
    {
        private ICollection<TResult> resultsCollection;

        public SearchResults(ICollection<TResult> resultsCollection)
        {
            this.resultsCollection = resultsCollection;
        }

        public event Action BeforeReInitialization;

        /// <summary>
        /// Updates the list with a new item on the thread that owns the form's handle
        /// </summary>
        /// <param name="item"></param>
        public void Add(TResult item)
        {
            resultsCollection.Add(item);
        }

        /// <summary>
        /// Shows results window
        /// </summary>
        public void ReInitialize()
        {
            BeforeReInitialization?.Invoke();
            resultsCollection.Clear();
        }
    }
}
