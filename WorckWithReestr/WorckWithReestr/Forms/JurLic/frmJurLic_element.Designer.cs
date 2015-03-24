namespace WorckWithReestr
{
    partial class frmJurLic_element
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
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblFV = new System.Windows.Forms.Label();
            this.lblOKPO = new System.Windows.Forms.Label();
            this.cbFV = new System.Windows.Forms.ComboBox();
            this.txtOKPO = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(354, 105);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 105);
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(64, 6);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(76, 20);
            this.txtID.TabIndex = 22;
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(9, 9);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(49, 15);
            this.lblID.TabIndex = 21;
            this.lblID.Text = "ID";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(9, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(92, 20);
            this.lblName.TabIndex = 23;
            this.lblName.Text = "Наименование";
            // 
            // lblFV
            // 
            this.lblFV.Location = new System.Drawing.Point(9, 64);
            this.lblFV.Name = "lblFV";
            this.lblFV.Size = new System.Drawing.Size(131, 20);
            this.lblFV.TabIndex = 24;
            this.lblFV.Text = "Форма собственности";
            // 
            // lblOKPO
            // 
            this.lblOKPO.Location = new System.Drawing.Point(275, 64);
            this.lblOKPO.Name = "lblOKPO";
            this.lblOKPO.Size = new System.Drawing.Size(50, 16);
            this.lblOKPO.TabIndex = 25;
            this.lblOKPO.Text = "ОКПО";
            // 
            // cbFV
            // 
            this.cbFV.FormattingEnabled = true;
            this.cbFV.Location = new System.Drawing.Point(136, 61);
            this.cbFV.Name = "cbFV";
            this.cbFV.Size = new System.Drawing.Size(133, 21);
            this.cbFV.TabIndex = 26;
            // 
            // txtOKPO
            // 
            this.txtOKPO.Location = new System.Drawing.Point(329, 61);
            this.txtOKPO.Name = "txtOKPO";
            this.txtOKPO.Size = new System.Drawing.Size(100, 20);
            this.txtOKPO.TabIndex = 27;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(107, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(322, 20);
            this.txtName.TabIndex = 28;
            // 
            // frmJurLic_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 136);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtOKPO);
            this.Controls.Add(this.cbFV);
            this.Controls.Add(this.lblOKPO);
            this.Controls.Add(this.lblFV);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Name = "frmJurLic_element";
            this.Text = "frmJurLic_element";
            this.Load += new System.EventHandler(this.frmJurLic_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblID, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.lblFV, 0);
            this.Controls.SetChildIndex(this.lblOKPO, 0);
            this.Controls.SetChildIndex(this.cbFV, 0);
            this.Controls.SetChildIndex(this.txtOKPO, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblFV;
        private System.Windows.Forms.Label lblOKPO;
        private System.Windows.Forms.ComboBox cbFV;
        private System.Windows.Forms.TextBox txtOKPO;
        private System.Windows.Forms.TextBox txtName;
    }
}