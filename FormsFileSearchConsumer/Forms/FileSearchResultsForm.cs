using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TheSeeker.Forms;

namespace TheSeeker.FileSystem.Forms
{
    public class FileSearchResultsForm<TResult> : SearchResultsForm<TResult> where TResult : FileSystemInfo
    {
        public FileSearchResultsForm(IFormSettingsProvider formSettings) : base(formSettings)
        {
            // Add menu items to the Context Menu

            // Open file
            ListContextMenu.Items.Add(new ToolStripMenuItem("Open file", null, ResultList_DoubleClick, Keys.Control | Keys.Enter));

            // Open containing folder
            ListContextMenu.Items.Add(new ToolStripMenuItem("Open Location", null, (sender, e) =>
            {
                ActOnSingleSelectedItem((file) =>
                    Process.Start("explorer.exe", $"/select, \"{file.FullName}\""));         
            },
            Keys.Control | Keys.O));

            // Open properties window
            ListContextMenu.Items.Add(new ToolStripMenuItem("Properties", null, (sender, e) =>
            {
                ActOnSingleSelectedItem((file) =>
                    ShellHelper.ShowFileProperties(file.FullName));
            },
            Keys.Alt | Keys.Enter));
        }       

        protected override void ResultList_DoubleClick(object sender, EventArgs e)
        {
            // Run / open file
            if (ResultList.Items.Count > 0 && ResultList.SelectedIndex != ListBox.NoMatches)
            {
                ActOnSingleSelectedItem((file) =>
                    Process.Start(file.FullName));
            }
        }
    }
}
