namespace SharedClasses
{
    partial class frmSelectObjAndLayerForToolShowObjInfo
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
            this.lsbSelectedLayers = new System.Windows.Forms.ListBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lsbSelectedLayers
            // 
            this.lsbSelectedLayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsbSelectedLayers.FormattingEnabled = true;
            this.lsbSelectedLayers.HorizontalScrollbar = true;
            this.lsbSelectedLayers.Location = new System.Drawing.Point(12, 49);
            this.lsbSelectedLayers.Name = "lsbSelectedLayers";
            this.lsbSelectedLayers.ScrollAlwaysVisible = true;
            this.lsbSelectedLayers.Size = new System.Drawing.Size(260, 234);
            this.lsbSelectedLayers.TabIndex = 0;
            this.lsbSelectedLayers.SelectedIndexChanged += new System.EventHandler(this.lsbSelectedLayers_SelectedIndexChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCaption.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCaption.Location = new System.Drawing.Point(12, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(260, 28);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "Выберете слой и объект по которому хотите получить информацию";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmSelectObjAndLayerForToolShowObjInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 299);
            this.ControlBox = false;
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lsbSelectedLayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectObjAndLayerForToolShowObjInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSelectObjAndLayerForToolShowObjInfo";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox lsbSelectedLayers;
        private System.Windows.Forms.Label lblCaption;
    }
}