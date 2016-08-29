namespace TheSeeker.Forms
{
    abstract partial class SearchResultsForm<TResult>
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
            if (IsHandleCreated == true) { 
                Invoke(new System.Action(() =>
                {
                    if (disposing && (components != null))
                    {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }));
            }
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
            this.list = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Stopped";
            // 
            // lblCurrentLocation
            // 
            this.lblCurrentLocation.AutoSize = true;
            this.lblCurrentLocation.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCurrentLocation.Location = new System.Drawing.Point(56, 3);
            this.lblCurrentLocation.Name = "lblCurrentLocation";
            this.lblCurrentLocation.Size = new System.Drawing.Size(21, 13);
            this.lblCurrentLocation.TabIndex = 1;
            this.lblCurrentLocation.Text = "c:\\";
            // 
            // lblResultsCount
            // 
            this.lblResultsCount.AutoSize = true;
            this.lblResultsCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblResultsCount.Location = new System.Drawing.Point(83, 3);
            this.lblResultsCount.Name = "lblResultsCount";
            this.lblResultsCount.Size = new System.Drawing.Size(13, 13);
            this.lblResultsCount.TabIndex = 2;
            this.lblResultsCount.Text = "0";
            // 
            // listContextMenu
            // 
            this.listContextMenu.Name = "contextMenuStrip1";
            this.listContextMenu.Size = new System.Drawing.Size(61, 4);
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
            this.list.DoubleClick += new System.EventHandler(this.ResultList_DoubleClick);
            this.list.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ResultList_MouseDown);
            // 
            // SearchResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 374);
            this.Controls.Add(this.list);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SearchResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Search Results";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ContextMenuStrip listContextMenu;
        private System.Windows.Forms.ListBox list; 
        public System.Windows.Forms.Label lblCurrentLocation;
        public System.Windows.Forms.Label lblResultsCount;
    }
}