using System.Windows.Forms;
namespace WorckWithReestr
{
    partial class frmTipDoc_element
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
            this.txtTip_Doc = new TextBox();
            this.txtKod_Doc = new TextBox();
            this.lblTip_Doc = new Label();
            this.lblKod_Doc = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(347, 87);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 87);
            // 
            // txtTip_Doc
            // 
            this.txtTip_Doc.Location = new System.Drawing.Point(115, 6);
            this.txtTip_Doc.Name = "txtTip_Doc";
            this.txtTip_Doc.Size = new System.Drawing.Size(307, 20);
            this.txtTip_Doc.TabIndex = 9;
            this.txtTip_Doc.TextChanged += new System.EventHandler(this.txtTip_Doc_TextChanged);
            this.txtTip_Doc.Validating += new System.ComponentModel.CancelEventHandler(this.txtTip_Doc_Validating);
            // 
            // txtKod_Doc
            // 
            this.txtKod_Doc.Location = new System.Drawing.Point(115, 35);
            this.txtKod_Doc.Name = "txtKod_Doc";
            this.txtKod_Doc.Size = new System.Drawing.Size(100, 20);
            this.txtKod_Doc.TabIndex = 10;
            this.txtKod_Doc.TextChanged += new System.EventHandler(this.txtKod_Doc_TextChanged);
            this.txtKod_Doc.Validating += new System.ComponentModel.CancelEventHandler(this.txtKod_Doc_Validating);
            // 
            // lblTip_Doc
            // 
            this.lblTip_Doc.Location = new System.Drawing.Point(9, 9);
            this.lblTip_Doc.Name = "lblTip_Doc";
            this.lblTip_Doc.Size = new System.Drawing.Size(100, 23);
            this.lblTip_Doc.TabIndex = 11;
            this.lblTip_Doc.Text = "Тіп документа";
            // 
            // lblKod_Doc
            // 
            this.lblKod_Doc.Location = new System.Drawing.Point(9, 38);
            this.lblKod_Doc.Name = "lblKod_Doc";
            this.lblKod_Doc.Size = new System.Drawing.Size(100, 23);
            this.lblKod_Doc.TabIndex = 12;
            this.lblKod_Doc.Text = "Код документа";
            // 
            // frmTipDoc_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 120);
            this.Controls.Add(this.lblKod_Doc);
            this.Controls.Add(this.lblTip_Doc);
            this.Controls.Add(this.txtKod_Doc);
            this.Controls.Add(this.txtTip_Doc);
            this.Name = "frmTipDoc_element";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmTipDoc_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtTip_Doc, 0);
            this.Controls.SetChildIndex(this.txtKod_Doc, 0);
            this.Controls.SetChildIndex(this.lblTip_Doc, 0);
            this.Controls.SetChildIndex(this.lblKod_Doc, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtTip_Doc;
        private TextBox txtKod_Doc;
        private Label lblTip_Doc;
        private Label lblKod_Doc;
    }
}