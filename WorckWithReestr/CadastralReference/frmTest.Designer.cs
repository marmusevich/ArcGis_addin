namespace CadastralReference
{
    partial class frmTest
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
            this.txt = new System.Windows.Forms.TextBox();
            this.lblAllLayers = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tvLayers = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt.Location = new System.Drawing.Point(341, 30);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt.Size = new System.Drawing.Size(301, 411);
            this.txt.TabIndex = 0;
            // 
            // lblAllLayers
            // 
            this.lblAllLayers.AutoSize = true;
            this.lblAllLayers.Location = new System.Drawing.Point(130, 9);
            this.lblAllLayers.Name = "lblAllLayers";
            this.lblAllLayers.Size = new System.Drawing.Size(97, 13);
            this.lblAllLayers.TabIndex = 9;
            this.lblAllLayers.Text = "Имеющиеся слои";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(13, 447);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(556, 447);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tvLayers
            // 
            this.tvLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvLayers.CheckBoxes = true;
            this.tvLayers.FullRowSelect = true;
            this.tvLayers.HotTracking = true;
            this.tvLayers.Location = new System.Drawing.Point(13, 30);
            this.tvLayers.Name = "tvLayers";
            this.tvLayers.ShowNodeToolTips = true;
            this.tvLayers.Size = new System.Drawing.Size(321, 411);
            this.tvLayers.TabIndex = 13;
            this.tvLayers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.tvLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.tvLayers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLayers_NodeMouseClick);
            // 
            // frmTest
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(654, 475);
            this.Controls.Add(this.tvLayers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblAllLayers);
            this.Controls.Add(this.txt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор слоёв для листа";
            this.Load += new System.EventHandler(this.frmTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Label lblAllLayers;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TreeView tvLayers;
    }
}