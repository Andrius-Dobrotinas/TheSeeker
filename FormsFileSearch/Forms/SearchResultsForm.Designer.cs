namespace TheSeeker.Forms
{
    abstract partial class SearchResultsForm<TResult>//, TSearchResultsDataSource>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Invoke(new System.Action(() =>
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }));
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentLocation = new System.Windows.Forms.Label();
            this.lblResultsCount = new System.Windows.Forms.Label();
            this.listContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yeahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listContextMenu_OpenLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.list = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.listContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.lblStatus);
            this.flowLayoutPanel1.Controls.Add(this.lblCurrentLocation);
            this.flowLayoutPanel1.Controls.Add(this.lblResultsCount);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(588, 22);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(103, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "label1 one two three";
            // 
            // lblCurrentLocation
            // 
            this.lblCurrentLocation.AutoSize = true;
            this.lblCurrentLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentLocation.Location = new System.Drawing.Point(112, 3);
            this.lblCurrentLocation.Name = "lblCurrentLocation";
            this.lblCurrentLocation.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentLocation.TabIndex = 1;
            this.lblCurrentLocation.Text = "label1";
            // 
            // lblResultsCount
            // 
            this.lblResultsCount.AutoSize = true;
            this.lblResultsCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblResultsCount.Location = new System.Drawing.Point(153, 3);
            this.lblResultsCount.Name = "lblResultsCount";
            this.lblResultsCount.Size = new System.Drawing.Size(35, 13);
            this.lblResultsCount.TabIndex = 2;
            this.lblResultsCount.Text = "label1";
            // 
            // listContextMenu
            // 
            this.listContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sadToolStripMenuItem,
            this.listContextMenu_OpenLocation});
            this.listContextMenu.Name = "contextMenuStrip1";
            this.listContextMenu.Size = new System.Drawing.Size(153, 48);
            this.listContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.listContextMenu_Opening);
            // 
            // sadToolStripMenuItem
            // 
            this.sadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yeahToolStripMenuItem});
            this.sadToolStripMenuItem.Name = "sadToolStripMenuItem";
            this.sadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sadToolStripMenuItem.Text = "sad";
            // 
            // yeahToolStripMenuItem
            // 
            this.yeahToolStripMenuItem.Name = "yeahToolStripMenuItem";
            this.yeahToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.yeahToolStripMenuItem.Text = "yeah";
            // 
            // listContextMenu_OpenLocation
            // 
            this.listContextMenu_OpenLocation.Name = "listContextMenu_OpenLocation";
            this.listContextMenu_OpenLocation.Size = new System.Drawing.Size(152, 22);
            this.listContextMenu_OpenLocation.Text = "Open Location";
            this.listContextMenu_OpenLocation.Click += new System.EventHandler(this.listContextMenu_OpenLocation_Click);
            // 
            // list
            // 
            this.list.ContextMenuStrip = this.listContextMenu;
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(0, 22);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(588, 352);
            this.list.TabIndex = 3;
            this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
            this.list.MouseDown += new System.Windows.Forms.MouseEventHandler(this.list_MouseDown);
            // 
            // SearchResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 374);
            this.Controls.Add(this.list);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SearchResultsForm";
            this.Text = "SearchResults";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.listContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ContextMenuStrip listContextMenu;
        private System.Windows.Forms.ToolStripMenuItem sadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yeahToolStripMenuItem;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.ToolStripMenuItem listContextMenu_OpenLocation;
        public System.Windows.Forms.Label lblCurrentLocation;
        public System.Windows.Forms.Label lblResultsCount;
    }
}