using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker
{
    /// <summary>
    /// A wrapper class that gives search manager access to ICollection classes
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class SearchResultsConsumer<TResult> : ISearchResultsConsumer<TResult>
    {
        public ICollection<TResult> ResultsCollection { get; private set; }

        public event Action BeforeReInitialization;

        public SearchResultsConsumer(ICollection<TResult> resultsCollection)
        {
            if (resultsCollection == null)
                throw new ArgumentNullException(nameof(resultsCollection));

            ResultsCollection = resultsCollection;
        }

        /// <summary>
        /// Updates the list with a new item on the thread that owns the form's handle
        /// </summary>
        /// <param name="item"></param>
        public void Add(TResult item)
        {
            ResultsCollection.Add(item);
        }

        /// <summary>
        /// Initializes the object for a new search
        /// </summary>
        public void ReInitialize()
        {
            BeforeReInitialization?.Invoke();
            ResultsCollection.Clear();
            OnReInitialization();
        }

        protected virtual void OnReInitialization()
        {
            
        }
    }
}
