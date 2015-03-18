namespace DemoTestMap
{
    partial class frmFindAndSelect_MapObject
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSelTabl = new System.Windows.Forms.Label();
            this.lblSelFild = new System.Windows.Forms.Label();
            this.lblSelVal = new System.Windows.Forms.Label();
            this.cbSelTab = new System.Windows.Forms.ComboBox();
            this.cblSelFild = new System.Windows.Forms.ComboBox();
            this.cbSelVal = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(297, 175);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(123, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Показать на карте";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(7, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSelTabl
            // 
            this.lblSelTabl.Location = new System.Drawing.Point(14, 18);
            this.lblSelTabl.Name = "lblSelTabl";
            this.lblSelTabl.Size = new System.Drawing.Size(100, 23);
            this.lblSelTabl.TabIndex = 9;
            this.lblSelTabl.Text = "Слой (таблица):";
            // 
            // lblSelFild
            // 
            this.lblSelFild.Location = new System.Drawing.Point(48, 47);
            this.lblSelFild.Name = "lblSelFild";
            this.lblSelFild.Size = new System.Drawing.Size(100, 23);
            this.lblSelFild.TabIndex = 10;
            this.lblSelFild.Text = "Признак (поле):";
            // 
            // lblSelVal
            // 
            this.lblSelVal.Location = new System.Drawing.Point(73, 76);
            this.lblSelVal.Name = "lblSelVal";
            this.lblSelVal.Size = new System.Drawing.Size(100, 23);
            this.lblSelVal.TabIndex = 11;
            this.lblSelVal.Text = "Значение";
            // 
            // cbSelTab
            // 
            this.cbSelTab.FormattingEnabled = true;
            this.cbSelTab.Location = new System.Drawing.Point(121, 18);
            this.cbSelTab.Name = "cbSelTab";
            this.cbSelTab.Size = new System.Drawing.Size(301, 21);
            this.cbSelTab.TabIndex = 12;
            this.cbSelTab.SelectedValueChanged += new System.EventHandler(this.cbSelTab_SelectedValueChanged);
            // 
            // cblSelFild
            // 
            this.cblSelFild.FormattingEnabled = true;
            this.cblSelFild.Location = new System.Drawing.Point(155, 46);
            this.cblSelFild.Name = "cblSelFild";
            this.cblSelFild.Size = new System.Drawing.Size(185, 21);
            this.cblSelFild.TabIndex = 13;
            this.cblSelFild.SelectedValueChanged += new System.EventHandler(this.cblSelFild_SelectedValueChanged);
            // 
            // cbSelVal
            // 
            this.cbSelVal.FormattingEnabled = true;
            this.cbSelVal.Location = new System.Drawing.Point(180, 74);
            this.cbSelVal.Name = "cbSelVal";
            this.cbSelVal.Size = new System.Drawing.Size(121, 21);
            this.cbSelVal.TabIndex = 14;
            this.cbSelVal.SelectedValueChanged += new System.EventHandler(this.cbSelVal_SelectedValueChanged);
            // 
            // frmFindAndSelect_MapObject
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(441, 211);
            this.Controls.Add(this.cbSelVal);
            this.Controls.Add(this.cblSelFild);
            this.Controls.Add(this.cbSelTab);
            this.Controls.Add(this.lblSelVal);
            this.Controls.Add(this.lblSelFild);
            this.Controls.Add(this.lblSelTabl);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFindAndSelect_MapObject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Найти объект на карте";
            this.Load += new System.EventHandler(this.frmFindAndSelect_MapObject_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSelTabl;
        private System.Windows.Forms.Label lblSelFild;
        private System.Windows.Forms.Label lblSelVal;
        private System.Windows.Forms.ComboBox cbSelTab;
        private System.Windows.Forms.ComboBox cblSelFild;
        private System.Windows.Forms.ComboBox cbSelVal;
    }
}