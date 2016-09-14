using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker.Forms.SearchResults
{
    public class SearchResultsConsumerWithForm<TResult> : SearchResultsConsumer<TResult>
    {
        public ISearchResultsConsumerControl<TResult> SearchResultsForm { get; private set; }

        public SearchResultsConsumerWithForm(ICollection<TResult> resultsCollection, ISearchResultsConsumerControl<TResult> searchResultsForm) :
            base(resultsCollection)
        {
            if (searchResultsForm == null)
                throw new ArgumentNullException(nameof(searchResultsForm));

            SearchResultsForm = searchResultsForm;
        }

        protected override void OnReInitialization()
        {
            SearchResultsForm.ReInitialize();
        }
    }
}
