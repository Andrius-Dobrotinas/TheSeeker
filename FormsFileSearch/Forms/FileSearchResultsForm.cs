using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheSeeker.Forms
{
    /// <summary>
    /// Works with FileInfo and DirectoryInfo types. Not a good way, but I cannot override FileInfo and make
    /// it use an interface and I don't really want to write my own FileInfo
    /// </summary>
    /// <typeparam name="TSearchResultsDataSource"></typeparam>
    public class FileSearchResultsForm<TResult> : SearchResultsForm<TResult> where TResult : FileSystemInfo
    {
        public FileSearchResultsForm()
        {

        }

        protected override void list_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var index = ResultsOutput.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    ResultsOutput.SelectedIndex = index;
                }
            }
        }

        protected override void listContextMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        protected override void listContextMenu_OpenLocation_Click(object sender, EventArgs e)
        {
            // Open containing folder
            var file = ResultsOutput.Items[ResultsOutput.SelectedIndex - 1] as FileInfo;
            Process.Start(file.DirectoryName);
        }

        protected override void list_DoubleClick(object sender, EventArgs e)
        {
            // Run / open file
            if (ResultsOutput.Items.Count > 0 && ResultsOutput.SelectedIndex != ListBox.NoMatches)
            {
                var file = ResultsOutput.Items[ResultsOutput.SelectedIndex - 1] as FileInfo;
                Process.Start(file.FullName);
            }
        }
    }
}
