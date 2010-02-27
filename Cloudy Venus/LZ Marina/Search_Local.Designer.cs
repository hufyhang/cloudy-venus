namespace LZ_Marina
{
    partial class Search_Local
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search_Local));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.checkBoxX1);
            this.panel1.Controls.Add(this.buttonX3);
            this.panel1.Controls.Add(this.textBoxX3);
            this.panel1.Controls.Add(this.buttonX2);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Controls.Add(this.textBoxX2);
            this.panel1.Controls.Add(this.textBoxX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 39);
            this.panel1.TabIndex = 0;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackColor = System.Drawing.Color.Transparent;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX3.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX3.Location = new System.Drawing.Point(3, 8);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.buttonX3.Size = new System.Drawing.Size(24, 24);
            this.buttonX3.TabIndex = 5;
            this.buttonX3.Text = "X";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // textBoxX3
            // 
            this.textBoxX3.AcceptsReturn = true;
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxX3.Location = new System.Drawing.Point(245, 8);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.Size = new System.Drawing.Size(122, 24);
            this.textBoxX3.TabIndex = 1;
            this.textBoxX3.WatermarkText = "Extension...";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX2.BackColor = System.Drawing.Color.Transparent;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX2.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX2.Location = new System.Drawing.Point(777, 8);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.buttonX2.Size = new System.Drawing.Size(54, 24);
            this.buttonX2.TabIndex = 4;
            this.buttonX2.Text = "Search";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.BackColor = System.Drawing.Color.Transparent;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX1.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX1.Location = new System.Drawing.Point(662, 8);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.buttonX1.Size = new System.Drawing.Size(24, 24);
            this.buttonX1.TabIndex = 3;
            this.buttonX1.Text = "...";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // textBoxX2
            // 
            this.textBoxX2.AcceptsReturn = true;
            this.textBoxX2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxX2.Location = new System.Drawing.Point(373, 8);
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.Size = new System.Drawing.Size(283, 24);
            this.textBoxX2.TabIndex = 2;
            this.textBoxX2.WatermarkText = "Arrangement...";
            // 
            // textBoxX1
            // 
            this.textBoxX1.AcceptsReturn = true;
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxX1.Location = new System.Drawing.Point(33, 8);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(206, 24);
            this.textBoxX1.TabIndex = 0;
            this.textBoxX1.WatermarkText = "Search for...";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 37);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(859, 442);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Directory";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Extension";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Last Modified Date & Time";
            this.columnHeader5.Width = 160;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runAsDefaultToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 26);
            // 
            // runAsDefaultToolStripMenuItem
            // 
            this.runAsDefaultToolStripMenuItem.Name = "runAsDefaultToolStripMenuItem";
            this.runAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.runAsDefaultToolStripMenuItem.Text = "Run as default";
            this.runAsDefaultToolStripMenuItem.Click += new System.EventHandler(this.runAsDefaultToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 441);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(843, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // checkBoxX1
            // 
            this.checkBoxX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxX1.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxX1.Checked = true;
            this.checkBoxX1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxX1.CheckValue = "Y";
            this.checkBoxX1.Location = new System.Drawing.Point(692, 9);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(79, 23);
            this.checkBoxX1.TabIndex = 6;
            this.checkBoxX1.Text = "Sub-path";
            // 
            // Search_Local
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(843, 463);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Search_Local";
            this.Text = "Search_Local";
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem runAsDefaultToolStripMenuItem;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
    }
}