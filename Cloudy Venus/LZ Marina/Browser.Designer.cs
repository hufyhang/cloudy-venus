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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browser));
            this.backButton = new DevComponents.DotNetBar.ButtonX();
            this.forwardButton = new DevComponents.DotNetBar.ButtonX();
            this.reloadButton = new DevComponents.DotNetBar.ButtonX();
            this.closeButton = new DevComponents.DotNetBar.ButtonX();
            this.button1 = new DevComponents.DotNetBar.ButtonX();
            this.button2 = new DevComponents.DotNetBar.ButtonX();
            this.button3 = new DevComponents.DotNetBar.ButtonX();
            this.button4 = new DevComponents.DotNetBar.ButtonX();
            this.webBrowser1 = new ExtendedWebBrowser.ExtendedWebBrowser();
            this.addressBox = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.backButton.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.backButton.Location = new System.Drawing.Point(38, 9);
            this.backButton.Name = "backButton";
            this.backButton.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.backButton.Size = new System.Drawing.Size(29, 24);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<";
            // 
            // forwardButton
            // 
            this.forwardButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.forwardButton.BackColor = System.Drawing.Color.Transparent;
            this.forwardButton.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.forwardButton.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.forwardButton.Location = new System.Drawing.Point(73, 9);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.forwardButton.Size = new System.Drawing.Size(29, 24);
            this.forwardButton.TabIndex = 1;
            this.forwardButton.Text = ">";
            // 
            // reloadButton
            // 
            this.reloadButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.reloadButton.BackColor = System.Drawing.Color.Transparent;
            this.reloadButton.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.reloadButton.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reloadButton.Location = new System.Drawing.Point(108, 9);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.reloadButton.Size = new System.Drawing.Size(29, 24);
            this.reloadButton.TabIndex = 2;
            this.reloadButton.Text = "R";
            // 
            // closeButton
            // 
            this.closeButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.closeButton.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.closeButton.Location = new System.Drawing.Point(686, 9);
            this.closeButton.Name = "closeButton";
            this.closeButton.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.closeButton.Size = new System.Drawing.Size(23, 24);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "X";
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.button1.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(601, 9);
            this.button1.Name = "button1";
            this.button1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.button1.Size = new System.Drawing.Size(79, 24);
            this.button1.TabIndex = 6;
            this.button1.Text = "Favourite";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.button2.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(3, 8);
            this.button2.Name = "button2";
            this.button2.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.button2.Size = new System.Drawing.Size(29, 24);
            this.button2.TabIndex = 9;
            this.button2.Text = "▲";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.button3.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(566, 9);
            this.button3.Name = "button3";
            this.button3.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.button3.Size = new System.Drawing.Size(29, 24);
            this.button3.TabIndex = 11;
            this.button3.Text = "□";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.button4.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(491, 9);
            this.button4.Name = "button4";
            this.button4.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.button4.Size = new System.Drawing.Size(69, 24);
            this.button4.TabIndex = 12;
            this.button4.Text = "Print...";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 38);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(714, 193);
            this.webBrowser1.TabIndex = 7;
            // 
            // addressBox
            // 
            this.addressBox.AcceptsReturn = true;
            this.addressBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addressBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.addressBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            // 
            // 
            // 
            this.addressBox.Border.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.addressBox.Border.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.addressBox.Border.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.addressBox.Border.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.addressBox.Border.Class = "TextBoxBorder";
            this.addressBox.Font = new System.Drawing.Font("Arial Unicode MS", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addressBox.Location = new System.Drawing.Point(143, 8);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(342, 26);
            this.addressBox.TabIndex = 8;
            this.addressBox.WatermarkText = "Go to visit this address...";
            this.addressBox.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.addressBox);
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Controls.Add(this.forwardButton);
            this.panel1.Controls.Add(this.reloadButton);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 40);
            this.panel1.TabIndex = 13;
            // 
            // Browser
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(714, 231);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Browser";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
/*
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button button1;
 */
        private DevComponents.DotNetBar.ButtonX backButton;
        private DevComponents.DotNetBar.ButtonX forwardButton;
        private DevComponents.DotNetBar.ButtonX reloadButton;
        private DevComponents.DotNetBar.ButtonX closeButton;
        private DevComponents.DotNetBar.ButtonX button1;
        private ExtendedWebBrowser.ExtendedWebBrowser webBrowser1;
//        private System.Windows.Forms.TextBox addressBox;
        private DevComponents.DotNetBar.Controls.TextBoxX addressBox;
        private DevComponents.DotNetBar.ButtonX button2;
        private DevComponents.DotNetBar.ButtonX button3;
        private DevComponents.DotNetBar.ButtonX button4;
/*        
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
*/
        private System.Windows.Forms.Panel panel1;
    }
        #endregion

}