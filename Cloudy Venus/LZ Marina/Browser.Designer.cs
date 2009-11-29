namespace LZ_Marina
{
    partial class Browser
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
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.reloadButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.webBrowser1 = new ExtendedWebBrowser.ExtendedWebBrowser();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.backButton.Location = new System.Drawing.Point(2, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(29, 24);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // forwardButton
            // 
            this.forwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.forwardButton.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.forwardButton.Location = new System.Drawing.Point(37, 12);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(29, 24);
            this.forwardButton.TabIndex = 1;
            this.forwardButton.Text = ">";
            this.forwardButton.UseVisualStyleBackColor = true;
            // 
            // reloadButton
            // 
            this.reloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reloadButton.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reloadButton.Location = new System.Drawing.Point(72, 12);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(29, 24);
            this.reloadButton.TabIndex = 2;
            this.reloadButton.Text = "R";
            this.reloadButton.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeButton.Location = new System.Drawing.Point(597, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(23, 24);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(512, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 24);
            this.button1.TabIndex = 6;
            this.button1.Text = "Favourite";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 42);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(620, 193);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // addressBox
            // 
            this.addressBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addressBox.Location = new System.Drawing.Point(107, 12);
            this.addressBox.Multiline = true;
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(399, 24);
            this.addressBox.TabIndex = 8;
            this.addressBox.WordWrap = false;
            // 
            // Browser
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(621, 235);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.backButton);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Browser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button button1;
        private ExtendedWebBrowser.ExtendedWebBrowser webBrowser1;
        private System.Windows.Forms.TextBox addressBox;
    }
        #endregion

}