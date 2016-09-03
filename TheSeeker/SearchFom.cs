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
            if (SearchBox.Search(SearchLocation.Text, SearchPattern.Text))
                Cancel.Enabled = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SearchBox.StopAndWait();
            base.OnFormClosing(e);
        }
    }
}
