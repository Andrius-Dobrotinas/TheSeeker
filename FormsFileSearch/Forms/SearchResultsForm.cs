using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    /// <summary>
    /// A form that displays search results
    /// </summary>
    /// <typeparam name="TResult">Type of items returned by the search</typeparam>
    public abstract partial class SearchResultsForm<TResult> :
        Form, ISearchResultsConsumerControl<TResult>/*ListBox>
        where TSearchResultsDataSource : class, IBindingList, ICollection<TResult>*/
    {
        private BindingList<TResult> resultListCached;

        protected BindingList<TResult> Results => resultListCached;

        public ListBox ResultsOutput => list;

        public event EventHandler DataSourceChanged
        {
            add
            {
                ResultsOutput.DataSourceChanged += value;
            }
            remove
            {
                ResultsOutput.DataSourceChanged -= value;
            }
        }

        /// <summary>
        /// Creates a form and associates it with the supplied search resuls source
        /// </summary>
        /// <param name="resultsSource">Data source for the list that holds search results</param>
        public SearchResultsForm()
        {
            InitializeComponent();

            // Cache data source (result list)
            list.DataSourceChanged += (source, e) =>
            {
                resultListCached = list.DataSource as BindingList<TResult>;

                // Attach a ListChanged event handler to the new data source
                resultListCached.ListChanged += (source2, e2) =>
                {
                    UpdateUi(() => lblResultsCount.Text = (resultListCached as ICollection<TResult>).Count.ToString());
                };
            };
        }

        /// <summary>
        /// Updates the UI on the thread that owns this form's handle
        /// </summary>
        /// <param name="action"></param>
        protected void UpdateUi(Action action)
        {
            if (IsHandleCreated)
            {
                Invoke(action);
            }
        }
        
        /// <summary>
        /// Search status
        /// </summary>
        public virtual string Status
        {
            get
            {
                return lblStatus.Text;
            }
            set
            {
                UpdateUi(() => lblStatus.Text = value);
            }
        }

        public IList<TResult> DataSource
        {
            get
            {
                return (IList<TResult>)list.DataSource;
            }

            set
            {
                list.DataSource = value;
            }
        }

        /// <summary>
        /// Opens this form on a separate thread
        /// </summary>
        public virtual void ReInitialize()
        {
            if (!IsHandleCreated)
            {
                FormsHelper.RunOnNewThread(this, true);
            }
        }
        
        protected abstract void list_MouseDown(object sender, MouseEventArgs e);

        protected abstract void listContextMenu_Opening(object sender, CancelEventArgs e);

        protected abstract void listContextMenu_OpenLocation_Click(object sender, EventArgs e);

        protected abstract void list_DoubleClick(object sender, EventArgs e);
    }
}