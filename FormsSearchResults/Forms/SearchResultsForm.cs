using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TheSeeker.Forms.Properties;

namespace TheSeeker.Forms
{
    /// <summary>
    /// A form that displays search results
    /// </summary>
    /// <typeparam name="TResult">Type of items returned by the search</typeparam>
    public abstract partial class SearchResultsForm<TResult> :
        Form, ISearchResultsConsumerControl<TResult>
    {
        private BindingList<TResult> resultListCached;

        protected BindingList<TResult> Results => resultListCached;

        public ListBox ResultList => list;

        protected ContextMenuStrip ListContextMenu => listContextMenu;
        
        public event EventHandler FormHide;

        public event EventHandler DataSourceChanged
        {
            add
            {
                ResultList.DataSourceChanged += value;
            }
            remove
            {
                ResultList.DataSourceChanged -= value;
            }
        }

        /// <summary>
        /// Creates a form and associates it with the supplied search resuls source
        /// </summary>
        /// <param name="resultsSource">Data source for the list that holds search results</param>
        public SearchResultsForm(IFormSettingsProvider formSettings)
        {
            InitializeComponent();

            // Set window properties from settings
            DesktopLocation = formSettings.DesktopLocation;
            Width = formSettings.Width;
            Height = formSettings.Height;

            // Init event handlers for saving window properties to settings
            LocationChanged += (sender, e) =>
            {
                formSettings.DesktopLocation = DesktopLocation;
            };
            ResizeEnd += (sender, e) =>
            {
                formSettings.Width = Width;
                formSettings.Height = Height;
            };

            // Cache data source (result list)
            ResultList.DataSourceChanged += (source, e) =>
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
                return (IList<TResult>)ResultList.DataSource;
            }

            set
            {
                ResultList.DataSource = value;
            }
        }

        /// <summary>
        /// Opens this form on a separate thread
        /// </summary>
        public virtual void ReInitialize()
        {
            if (!IsHandleCreated)
                FormsHelper.RunOnNewThread(this, true);
            else
                UpdateUi(() => Show());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!(e.CloseReason == CloseReason.ApplicationExitCall &&
                e.CloseReason == CloseReason.TaskManagerClosing &&
                e.CloseReason == CloseReason.WindowsShutDown))
            {
                FormHide?.Invoke(this, EventArgs.Empty);
                e.Cancel = true;

                Hide();
            }

            base.OnFormClosing(e);
        }

        protected virtual void ResultList_MouseDown(object sender, MouseEventArgs e)
        {
            // Select row on right mouse click
            if (e.Button == MouseButtons.Right)
            {
                var index = ResultList.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    ResultList.SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// Casts selected item to TResult and uses it when invoking the supplied action
        /// </summary>
        /// <param name="action">That uses selected item</param>
        protected void ActOnSingleSelectedItem(Action<TResult> action)
        {
            var file = (TResult)ResultList.Items[ResultList.SelectedIndex - 1];
            action(file);
        }

        /// <summary>
        /// Gets invoked on double-click event occurs on the main Result list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void ResultList_DoubleClick(object sender, EventArgs e);
    }
}