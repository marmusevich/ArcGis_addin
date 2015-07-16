using System.Windows.Forms;
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
            this.lblName = new Label();
            this.lblFV = new Label();
            this.lblOKPO = new Label();
            this.cbFV = new ComboBox();
            this.txtOKPO = new TextBox();
            this.txtName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(354, 82);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 82);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(9, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(92, 20);
            this.lblName.TabIndex = 23;
            this.lblName.Text = "Наименование";
            // 
            // lblFV
            // 
            this.lblFV.Location = new System.Drawing.Point(9, 41);
            this.lblFV.Name = "lblFV";
            this.lblFV.Size = new System.Drawing.Size(131, 20);
            this.lblFV.TabIndex = 24;
            this.lblFV.Text = "Форма собственности";
            // 
            // lblOKPO
            // 
            this.lblOKPO.Location = new System.Drawing.Point(275, 41);
            this.lblOKPO.Name = "lblOKPO";
            this.lblOKPO.Size = new System.Drawing.Size(50, 16);
            this.lblOKPO.TabIndex = 25;
            this.lblOKPO.Text = "ОКПО";
            // 
            // cbFV
            // 
            this.cbFV.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbFV.FormattingEnabled = true;
            this.cbFV.Location = new System.Drawing.Point(136, 38);
            this.cbFV.Name = "cbFV";
            this.cbFV.Size = new System.Drawing.Size(133, 21);
            this.cbFV.TabIndex = 26;
            // 
            // txtOKPO
            // 
            this.txtOKPO.Location = new System.Drawing.Point(329, 38);
            this.txtOKPO.Name = "txtOKPO";
            this.txtOKPO.Size = new System.Drawing.Size(100, 20);
            this.txtOKPO.TabIndex = 27;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(107, 9);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(322, 20);
            this.txtName.TabIndex = 28;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // frmJurLic_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 111);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtOKPO);
            this.Controls.Add(this.cbFV);
            this.Controls.Add(this.lblOKPO);
            this.Controls.Add(this.lblFV);
            this.Controls.Add(this.lblName);
            this.Name = "frmJurLic_element";
            this.Text = "frmJurLic_element";
            this.Load += new System.EventHandler(this.frmJurLic_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.lblFV, 0);
            this.Controls.SetChildIndex(this.lblOKPO, 0);
            this.Controls.SetChildIndex(this.cbFV, 0);
            this.Controls.SetChildIndex(this.txtOKPO, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblName;
        private Label lblFV;
        private Label lblOKPO;
        private ComboBox cbFV;
        private TextBox txtOKPO;
        private TextBox txtName;
    }
}