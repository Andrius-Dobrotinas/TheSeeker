using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    public partial class SearchForm : Form
    {
        private ISearchManager searchManager;

        public SearchForm(ISearchManager searchManager)
        {
            InitializeComponent();

            this.searchManager = searchManager;

            this.searchManager.SearchFinished += (source, timeElapsed) => Invoke(new Action(() => Cancel.Enabled = false));
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            searchManager.Stop();
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
            if (!searchManager.IsSearching)
            {
                searchManager.Search(SearchLocation.Text, SearchPattern.Text);
                Cancel.Enabled = true;
            }
        }
    }
}
