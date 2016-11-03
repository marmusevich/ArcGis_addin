namespace GvS.Controls {
    partial class HtmlTextbox {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBold = new System.Windows.Forms.ToolStripButton();
            this.tsbUnderline = new System.Windows.Forms.ToolStripButton();
            this.tsbItalic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbJustifyLeft = new System.Windows.Forms.ToolStripButton();
            this.tsbJustifyCenter = new System.Windows.Forms.ToolStripButton();
            this.tsbJustifyRight = new System.Windows.Forms.ToolStripButton();
            this.tsbJustifyFull = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOrderedList = new System.Windows.Forms.ToolStripButton();
            this.tsbBulletList = new System.Windows.Forms.ToolStripButton();
            this.tsbUnIndent = new System.Windows.Forms.ToolStripButton();
            this.tsbIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFont = new System.Windows.Forms.ToolStripComboBox();
            this.tsbFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbViewSource = new System.Windows.Forms.ToolStripButton();
            this.editSplit = new System.Windows.Forms.SplitContainer();
            this.theBrowser = new System.Windows.Forms.WebBrowser();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.tmrSourceSync = new System.Windows.Forms.Timer(this.components);
            this.toolBar.SuspendLayout();
            this.editSplit.Panel1.SuspendLayout();
            this.editSplit.Panel2.SuspendLayout();
            this.editSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator5,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator1,
            this.tsbBold,
            this.tsbUnderline,
            this.tsbItalic,
            this.toolStripSeparator6,
            this.tsbJustifyLeft,
            this.tsbJustifyCenter,
            this.tsbJustifyRight,
            this.tsbJustifyFull,
            this.toolStripSeparator2,
            this.tsbOrderedList,
            this.tsbBulletList,
            this.tsbUnIndent,
            this.tsbIndent,
            this.toolStripSeparator3,
            this.tsbFont,
            this.tsbFontSize,
            this.toolStripSeparator4,
            this.tsbViewSource});
            this.toolBar.Location = new System.Drawing.Point(1, 1);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(497, 27);
            this.toolBar.TabIndex = 1;
            this.toolBar.Text = "toolStrip1";
            this.toolBar.Visible = false;
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = global::GvS.Controls.Properties.Resources.CutHS;
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 24);
            this.tsbCut.Text = "Cut";
            this.tsbCut.Click += new System.EventHandler(this.tsbCut_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = global::GvS.Controls.Properties.Resources.CopyHS;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(23, 24);
            this.tsbCopy.Text = "Copy";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = global::GvS.Controls.Properties.Resources.PasteHS;
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(23, 24);
            this.tsbPaste.Text = "Paste";
            this.tsbPaste.Click += new System.EventHandler(this.tsbPaste_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = global::GvS.Controls.Properties.Resources.UndoHS;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 24);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new System.EventHandler(this.tsbUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = global::GvS.Controls.Properties.Resources.RedoHS;
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 24);
            this.tsbRedo.Text = "Redo";
            this.tsbRedo.Click += new System.EventHandler(this.tsbRedo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbBold
            // 
            this.tsbBold.CheckOnClick = true;
            this.tsbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBold.Image = global::GvS.Controls.Properties.Resources.boldhs;
            this.tsbBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBold.Name = "tsbBold";
            this.tsbBold.Size = new System.Drawing.Size(23, 24);
            this.tsbBold.Text = "Bold";
            this.tsbBold.Click += new System.EventHandler(this.tsbBold_Click);
            // 
            // tsbUnderline
            // 
            this.tsbUnderline.CheckOnClick = true;
            this.tsbUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUnderline.Image = global::GvS.Controls.Properties.Resources.UnderlineHS;
            this.tsbUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUnderline.Name = "tsbUnderline";
            this.tsbUnderline.Size = new System.Drawing.Size(23, 24);
            this.tsbUnderline.Text = "Underline";
            this.tsbUnderline.Click += new System.EventHandler(this.tsbUnderline_Click);
            // 
            // tsbItalic
            // 
            this.tsbItalic.CheckOnClick = true;
            this.tsbItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbItalic.Image = global::GvS.Controls.Properties.Resources.ItalicHS;
            this.tsbItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbItalic.Name = "tsbItalic";
            this.tsbItalic.Size = new System.Drawing.Size(23, 24);
            this.tsbItalic.Text = "Italic";
            this.tsbItalic.Click += new System.EventHandler(this.tsbItalic_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbJustifyLeft
            // 
            this.tsbJustifyLeft.Checked = true;
            this.tsbJustifyLeft.CheckOnClick = true;
            this.tsbJustifyLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbJustifyLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJustifyLeft.Image = global::GvS.Controls.Properties.Resources.justifyLeft;
            this.tsbJustifyLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJustifyLeft.Name = "tsbJustifyLeft";
            this.tsbJustifyLeft.Size = new System.Drawing.Size(23, 24);
            this.tsbJustifyLeft.Text = "Justify left";
            this.tsbJustifyLeft.Click += new System.EventHandler(this.tsbJustifyLeft_Click);
            // 
            // tsbJustifyCenter
            // 
            this.tsbJustifyCenter.CheckOnClick = true;
            this.tsbJustifyCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJustifyCenter.Image = global::GvS.Controls.Properties.Resources.justifyCenter;
            this.tsbJustifyCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJustifyCenter.Name = "tsbJustifyCenter";
            this.tsbJustifyCenter.Size = new System.Drawing.Size(23, 24);
            this.tsbJustifyCenter.Text = "Justify center";
            this.tsbJustifyCenter.Click += new System.EventHandler(this.tsbJustifyCenter_Click);
            // 
            // tsbJustifyRight
            // 
            this.tsbJustifyRight.CheckOnClick = true;
            this.tsbJustifyRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJustifyRight.Image = global::GvS.Controls.Properties.Resources.justifyRight;
            this.tsbJustifyRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJustifyRight.Name = "tsbJustifyRight";
            this.tsbJustifyRight.Size = new System.Drawing.Size(23, 24);
            this.tsbJustifyRight.Text = "Justify right";
            this.tsbJustifyRight.Click += new System.EventHandler(this.tsbJustifyRight_Click);
            // 
            // tsbJustifyFull
            // 
            this.tsbJustifyFull.CheckOnClick = true;
            this.tsbJustifyFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJustifyFull.Image = global::GvS.Controls.Properties.Resources.justifyFull;
            this.tsbJustifyFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJustifyFull.Name = "tsbJustifyFull";
            this.tsbJustifyFull.Size = new System.Drawing.Size(23, 24);
            this.tsbJustifyFull.Text = "Justify full";
            this.tsbJustifyFull.Click += new System.EventHandler(this.tsbJustifyFull_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbOrderedList
            // 
            this.tsbOrderedList.CheckOnClick = true;
            this.tsbOrderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOrderedList.Image = global::GvS.Controls.Properties.Resources.List_NumberedHS;
            this.tsbOrderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOrderedList.Name = "tsbOrderedList";
            this.tsbOrderedList.Size = new System.Drawing.Size(23, 24);
            this.tsbOrderedList.Text = "Ordered list";
            this.tsbOrderedList.Click += new System.EventHandler(this.tsbOrderedList_Click);
            // 
            // tsbBulletList
            // 
            this.tsbBulletList.CheckOnClick = true;
            this.tsbBulletList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBulletList.Image = global::GvS.Controls.Properties.Resources.List_BulletsHS;
            this.tsbBulletList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBulletList.Name = "tsbBulletList";
            this.tsbBulletList.Size = new System.Drawing.Size(23, 24);
            this.tsbBulletList.Text = "Bullet List";
            this.tsbBulletList.Click += new System.EventHandler(this.tsbBulletList_Click);
            // 
            // tsbUnIndent
            // 
            this.tsbUnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUnIndent.Image = global::GvS.Controls.Properties.Resources.OutdentHS;
            this.tsbUnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUnIndent.Name = "tsbUnIndent";
            this.tsbUnIndent.Size = new System.Drawing.Size(23, 24);
            this.tsbUnIndent.Text = "Unindent";
            this.tsbUnIndent.Click += new System.EventHandler(this.tsbUnIndent_Click);
            // 
            // tsbIndent
            // 
            this.tsbIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbIndent.Image = global::GvS.Controls.Properties.Resources.IndentHS;
            this.tsbIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbIndent.Name = "tsbIndent";
            this.tsbIndent.Size = new System.Drawing.Size(23, 24);
            this.tsbIndent.Text = "Indent";
            this.tsbIndent.Click += new System.EventHandler(this.tsbIndent_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbFont
            // 
            this.tsbFont.Items.AddRange(new object[] {
            "Corbel",
            "Corbel, Verdana, Arial, Helvetica, sans-serif",
            "Georgia, Times New Roman, Times, serif",
            "Consolas, Courier New, Courier, monospace"});
            this.tsbFont.Name = "tsbFont";
            this.tsbFont.Size = new System.Drawing.Size(121, 23);
            this.tsbFont.SelectedIndexChanged += new System.EventHandler(this.tsbFont_SelectedIndexChanged);
            this.tsbFont.Leave += new System.EventHandler(this.tsbFont_Leave);
            this.tsbFont.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsbFont_KeyDown);
            // 
            // tsbFontSize
            // 
            this.tsbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbFontSize.DropDownWidth = 75;
            this.tsbFontSize.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.tsbFontSize.Name = "tsbFontSize";
            this.tsbFontSize.Size = new System.Drawing.Size(75, 23);
            this.tsbFontSize.SelectedIndexChanged += new System.EventHandler(this.tsbFontSize_SelectedIndexChanged);
            this.tsbFontSize.Leave += new System.EventHandler(this.tsbFontSize_Leave);
            this.tsbFontSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsbFontSize_KeyDown);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbViewSource
            // 
            this.tsbViewSource.CheckOnClick = true;
            this.tsbViewSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbViewSource.Image = global::GvS.Controls.Properties.Resources.EditCodeHS;
            this.tsbViewSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewSource.Name = "tsbViewSource";
            this.tsbViewSource.Size = new System.Drawing.Size(23, 20);
            this.tsbViewSource.Text = "View/Edit Html code";
            this.tsbViewSource.CheckedChanged += new System.EventHandler(this.tsbViewSource_CheckedChanged);
            // 
            // editSplit
            // 
            this.editSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editSplit.Location = new System.Drawing.Point(1, 28);
            this.editSplit.Name = "editSplit";
            this.editSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // editSplit.Panel1
            // 
            this.editSplit.Panel1.Controls.Add(this.theBrowser);
            // 
            // editSplit.Panel2
            // 
            this.editSplit.Panel2.Controls.Add(this.txtSource);
            this.editSplit.Panel2Collapsed = true;
            this.editSplit.Size = new System.Drawing.Size(497, 332);
            this.editSplit.SplitterDistance = 191;
            this.editSplit.TabIndex = 0;
            // 
            // theBrowser
            // 
            this.theBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theBrowser.Location = new System.Drawing.Point(0, 0);
            this.theBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.theBrowser.Name = "theBrowser";
            this.theBrowser.Size = new System.Drawing.Size(497, 332);
            this.theBrowser.TabIndex = 0;
            this.theBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.theBrowser_DocumentCompleted);
            // 
            // txtSource
            // 
            this.txtSource.AcceptsReturn = true;
            this.txtSource.AcceptsTab = true;
            this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSource.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSource.Location = new System.Drawing.Point(0, 0);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(150, 46);
            this.txtSource.TabIndex = 1;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            this.txtSource.Enter += new System.EventHandler(this.txtSource_Enter);
            // 
            // tmrSourceSync
            // 
            this.tmrSourceSync.Enabled = true;
            this.tmrSourceSync.Interval = 1000;
            this.tmrSourceSync.Tick += new System.EventHandler(this.tmrSourceSync_Tick);
            // 
            // HtmlTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editSplit);
            this.Controls.Add(this.toolBar);
            this.Name = "HtmlTextbox";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(499, 361);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.editSplit.Panel1.ResumeLayout(false);
            this.editSplit.Panel2.ResumeLayout(false);
            this.editSplit.Panel2.PerformLayout();
            this.editSplit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.SplitContainer editSplit;
        private System.Windows.Forms.Timer tmrSourceSync;
        private System.Windows.Forms.ToolStripButton tsbViewSource;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbBold;
        private System.Windows.Forms.ToolStripButton tsbItalic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbOrderedList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbBulletList;
        private System.Windows.Forms.ToolStripButton tsbUnIndent;
        private System.Windows.Forms.ToolStripButton tsbIndent;
        private System.Windows.Forms.ToolStripComboBox tsbFontSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.WebBrowser theBrowser;
        internal System.Windows.Forms.TextBox txtSource;
        internal System.Windows.Forms.ToolStripComboBox tsbFont;
        private System.Windows.Forms.ToolStripButton tsbUnderline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbJustifyLeft;
        private System.Windows.Forms.ToolStripButton tsbJustifyCenter;
        private System.Windows.Forms.ToolStripButton tsbJustifyRight;
        private System.Windows.Forms.ToolStripButton tsbJustifyFull;
    }
}
