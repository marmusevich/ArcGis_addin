namespace CadastralReference
{
    partial class frmSelectLayers
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lbAllLayers = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.lblAllLayers = new System.Windows.Forms.Label();
            this.lblSelectedLayers = new System.Windows.Forms.Label();
            this.lbSelectedLayers = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(427, 359);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lbAllLayers
            // 
            this.lbAllLayers.FormattingEnabled = true;
            this.lbAllLayers.Location = new System.Drawing.Point(5, 21);
            this.lbAllLayers.Name = "lbAllLayers";
            this.lbAllLayers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAllLayers.Size = new System.Drawing.Size(228, 329);
            this.lbAllLayers.TabIndex = 1;
            this.lbAllLayers.DoubleClick += new System.EventHandler(this.lbAllLayers_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(240, 97);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(239, 163);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(30, 23);
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "<<";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // lblAllLayers
            // 
            this.lblAllLayers.AutoSize = true;
            this.lblAllLayers.Location = new System.Drawing.Point(54, 3);
            this.lblAllLayers.Name = "lblAllLayers";
            this.lblAllLayers.Size = new System.Drawing.Size(97, 13);
            this.lblAllLayers.TabIndex = 5;
            this.lblAllLayers.Text = "Имеющиеся слои";
            // 
            // lblSelectedLayers
            // 
            this.lblSelectedLayers.AutoSize = true;
            this.lblSelectedLayers.Location = new System.Drawing.Point(344, 3);
            this.lblSelectedLayers.Name = "lblSelectedLayers";
            this.lblSelectedLayers.Size = new System.Drawing.Size(93, 13);
            this.lblSelectedLayers.TabIndex = 6;
            this.lblSelectedLayers.Text = "Выбранные слои";
            // 
            // lbSelectedLayers
            // 
            this.lbSelectedLayers.FormattingEnabled = true;
            this.lbSelectedLayers.Location = new System.Drawing.Point(277, 21);
            this.lbSelectedLayers.Name = "lbSelectedLayers";
            this.lbSelectedLayers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelectedLayers.Size = new System.Drawing.Size(228, 329);
            this.lbSelectedLayers.TabIndex = 7;
            this.lbSelectedLayers.DoubleClick += new System.EventHandler(this.lbSelectedLayers_DoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(5, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmSelectLayers
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(511, 388);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbSelectedLayers);
            this.Controls.Add(this.lblSelectedLayers);
            this.Controls.Add(this.lblAllLayers);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbAllLayers);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSelectLayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор слоёв для листа";
            this.Load += new System.EventHandler(this.frmSelectLayers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListBox lbAllLayers;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label lblAllLayers;
        private System.Windows.Forms.Label lblSelectedLayers;
        private System.Windows.Forms.ListBox lbSelectedLayers;
        private System.Windows.Forms.Button btnCancel;
    }
}