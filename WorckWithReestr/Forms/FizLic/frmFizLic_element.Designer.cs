using System.Windows.Forms;
namespace WorckWithReestr
{
    partial class frmFizLic_element
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
            this.lblFIO = new Label();
            this.lblCategor = new Label();
            this.lblINN = new Label();
            this.cbIsWorker = new CheckBox();
            this.txtCategor = new TextBox();
            this.txtINN = new TextBox();
            this.txtFIO = new TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(348, 109);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(7, 109);
            // 
            // lblFIO
            // 
            this.lblFIO.Location = new System.Drawing.Point(4, 12);
            this.lblFIO.Name = "lblFIO";
            this.lblFIO.Size = new System.Drawing.Size(49, 15);
            this.lblFIO.TabIndex = 30;
            this.lblFIO.Text = "Ф.И.О.";
            // 
            // lblCategor
            // 
            this.lblCategor.Location = new System.Drawing.Point(210, 35);
            this.lblCategor.Name = "lblCategor";
            this.lblCategor.Size = new System.Drawing.Size(65, 15);
            this.lblCategor.TabIndex = 50;
            this.lblCategor.Text = "Категория";
            // 
            // lblINN
            // 
            this.lblINN.Location = new System.Drawing.Point(4, 35);
            this.lblINN.Name = "lblINN";
            this.lblINN.Size = new System.Drawing.Size(49, 15);
            this.lblINN.TabIndex = 40;
            this.lblINN.Text = "ИНН";
            // 
            // cbIsWorker
            // 
            this.cbIsWorker.AutoSize = true;
            this.cbIsWorker.Location = new System.Drawing.Point(60, 72);
            this.cbIsWorker.Name = "cbIsWorker";
            this.cbIsWorker.Size = new System.Drawing.Size(99, 17);
            this.cbIsWorker.TabIndex = 3;
            this.cbIsWorker.Text = "Это сотрудник";
            this.cbIsWorker.UseVisualStyleBackColor = true;
            this.cbIsWorker.CheckedChanged += new System.EventHandler(this.cbIsWorker_CheckedChanged);
            // 
            // txtCategor
            // 
            this.txtCategor.Location = new System.Drawing.Point(278, 32);
            this.txtCategor.Name = "txtCategor";
            this.txtCategor.Size = new System.Drawing.Size(145, 20);
            this.txtCategor.TabIndex = 2;
            this.txtCategor.TextChanged += new System.EventHandler(this.txtCategor_TextChanged);
            // 
            // txtINN
            // 
            this.txtINN.Location = new System.Drawing.Point(59, 32);
            this.txtINN.Name = "txtINN";
            this.txtINN.Size = new System.Drawing.Size(100, 20);
            this.txtINN.TabIndex = 1;
            this.txtINN.TextChanged += new System.EventHandler(this.txtINN_TextChanged);
            // 
            // txtFIO
            // 
            this.txtFIO.Location = new System.Drawing.Point(59, 9);
            this.txtFIO.Name = "txtFIO";
            this.txtFIO.Size = new System.Drawing.Size(364, 20);
            this.txtFIO.TabIndex = 0;
            this.txtFIO.TextChanged += new System.EventHandler(this.txtFIO_TextChanged);
            this.txtFIO.Validating += new System.ComponentModel.CancelEventHandler(this.txtFIO_Validating);
            // 
            // frmFizLic_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 136);
            this.Controls.Add(this.txtFIO);
            this.Controls.Add(this.txtINN);
            this.Controls.Add(this.txtCategor);
            this.Controls.Add(this.cbIsWorker);
            this.Controls.Add(this.lblCategor);
            this.Controls.Add(this.lblINN);
            this.Controls.Add(this.lblFIO);
            this.Name = "frmFizLic_element";
            this.Text = "frmFizLic_element";
            this.Load += new System.EventHandler(this.frmFizLic_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblFIO, 0);
            this.Controls.SetChildIndex(this.lblINN, 0);
            this.Controls.SetChildIndex(this.lblCategor, 0);
            this.Controls.SetChildIndex(this.cbIsWorker, 0);
            this.Controls.SetChildIndex(this.txtCategor, 0);
            this.Controls.SetChildIndex(this.txtINN, 0);
            this.Controls.SetChildIndex(this.txtFIO, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblFIO;
        private Label lblCategor;
        private Label lblINN;
        private CheckBox cbIsWorker;
        private TextBox txtCategor;
        private TextBox txtINN;
        private TextBox txtFIO;
    }
}