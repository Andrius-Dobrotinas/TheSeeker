using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheSeeker.Forms.Properties;

namespace TheSeeker.Forms
{
    public partial class SearchForm : Form, ISearchForm
    {
        public ISearchBox SearchBox { get; private set; }

        /// <summary>
        /// Creates new Search Form with the supplied Search box and adds items to the supplied Context menu
        /// </summary>
        public SearchForm(ISearchBox searchBox)
        {
            if (searchBox == null)
                throw new ArgumentNullException(nameof(searchBox));
            SearchBox = searchBox;

            InitializeComponent();

            // Window position load/save
            DesktopLocation = Settings.Default.WindowLocation;
            LocationChanged += (sender, e) =>
            {
                // Save window position
                Settings.Default.WindowLocation = DesktopLocation;
                Settings.Default.Save();
            };
        }
        /// <summary>
        /// Creates new Search Form with the supplied Search box and adds items to the supplied Context menu
        /// </summary>
        public SearchForm(ISearchBox searchBox, ContextMenuStrip contextMenu) : this(searchBox)
        {
            // Create Cancel/Search menu item
            var actMenuItem = new ToolStripMenuItem();
            actMenuItem.Click += (sender, e) =>
            {
                if (SearchBox.IsSearching)
                    Cancel_Click(this, EventArgs.Empty);
                else
                    CallSearch();
            };
            contextMenu.Opening += (sender, e) => actMenuItem.Text = SearchBox.IsSearching ? "Cancel" : "Search";
            contextMenu.Items.Add(actMenuItem);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Cancel.Enabled = false;
            SearchBox.Stop();
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
            if (!string.IsNullOrWhiteSpace(SearchLocation.Text) && string.IsNullOrWhiteSpace(SearchPattern.Text))
            {
                if (SearchBox.Search(SearchLocation.Text, SearchPattern.Text))
                    Cancel.Enabled = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SearchBox.StopAndWait();
            base.OnFormClosing(e);
        }
    }
}
