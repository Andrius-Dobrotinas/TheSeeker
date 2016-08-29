namespace TheSeeker.Forms
{
    partial class SearchForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SearchLocation = new System.Windows.Forms.TextBox();
            this.SearchPattern = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SearchLocation
            // 
            this.SearchLocation.Location = new System.Drawing.Point(12, 12);
            this.SearchLocation.Name = "SearchLocation";
            this.SearchLocation.Size = new System.Drawing.Size(100, 20);
            this.SearchLocation.TabIndex = 1;
            this.SearchLocation.Text = "d:\\aac";
            this.SearchLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchPattern_KeyPress);
            // 
            // SearchPattern
            // 
            this.SearchPattern.Location = new System.Drawing.Point(12, 39);
            this.SearchPattern.Name = "SearchPattern";
            this.SearchPattern.Size = new System.Drawing.Size(100, 20);
            this.SearchPattern.TabIndex = 2;
            this.SearchPattern.Text = "*.*";
            this.SearchPattern.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchPattern_KeyPress);
            // 
            // Cancel
            // 
            this.Cancel.Enabled = false;
            this.Cancel.Location = new System.Drawing.Point(118, 12);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 71);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.SearchPattern);
            this.Controls.Add(this.SearchLocation);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SearchForm";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox SearchLocation;
        private System.Windows.Forms.TextBox SearchPattern;
        private System.Windows.Forms.Button Cancel;
    }
}

