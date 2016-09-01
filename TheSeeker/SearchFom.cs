using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheSeeker.Forms.Properties;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Delegate type that is intended for initiating a new search. Returns a value indicating whether a new search has been started
    /// </summary>
    /// <param name="searchLocation"></param>
    /// <param name="searchPattern"></param>
    /// <returns></returns>
    public delegate bool InitiateSearchDelegate(string searchLocation, string searchPattern);

    public partial class SearchForm : Form
    {
        private InitiateSearchDelegate initiateSearchDelegate;

        /// <summary>
        /// Occurs when the form cancels current search
        /// </summary>
        public event EventHandler CancelSearch;

        /// <summary>
        /// Occurs when the form cancels current search and wants to block until search stops
        /// </summary>
        public event EventHandler CancelSearchAndBlock;

        public SearchForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search form
        /// </summary>
        /// <param name="initSearchDelegate">Delegate that will be run each time search is initialized from within this form</param>
        /// <param name="settings">Application settings for this window</param>
        public SearchForm(InitiateSearchDelegate initSearchDelegate) : this()
        {
            initiateSearchDelegate = initSearchDelegate;
            DesktopLocation = Settings.Default.WindowLocation;
            LocationChanged += (sender, e) =>
            {
                // Save window position
                Settings.Default.WindowLocation = DesktopLocation;
                Settings.Default.Save();
            };
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            CancelSearch?.Invoke(sender, EventArgs.Empty);
            Cancel.Enabled = false;
        }

        private void SearchPattern_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CallSearch();
            }
        }

        private void CallSearch()
        {
            Cancel.Enabled = initiateSearchDelegate?.Invoke(SearchLocation.Text, SearchPattern.Text) == true;
        }

        /// <summary>
        /// Method must be run when search is stops/finishes
        /// </summary>
        public void SearchStopped()
        {
            Cancel.Enabled = false;
        }
    }
}
