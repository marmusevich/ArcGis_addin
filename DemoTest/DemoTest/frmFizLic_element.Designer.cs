namespace DemoTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFizLic_element));
            this.lblID = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblFIO = new System.Windows.Forms.Label();
            this.lblCategor = new System.Windows.Forms.Label();
            this.lblINN = new System.Windows.Forms.Label();
            this.cbIsWorker = new System.Windows.Forms.CheckBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCategor = new System.Windows.Forms.TextBox();
            this.txtINN = new System.Windows.Forms.TextBox();
            this.txtFIO = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(4, 10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(49, 15);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "ID";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(7, 134);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(345, 134);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblFIO
            // 
            this.lblFIO.Location = new System.Drawing.Point(4, 33);
            this.lblFIO.Name = "lblFIO";
            this.lblFIO.Size = new System.Drawing.Size(49, 15);
            this.lblFIO.TabIndex = 30;
            this.lblFIO.Text = "Ф.И.О.";
            // 
            // lblCategor
            // 
            this.lblCategor.Location = new System.Drawing.Point(210, 56);
            this.lblCategor.Name = "lblCategor";
            this.lblCategor.Size = new System.Drawing.Size(65, 15);
            this.lblCategor.TabIndex = 50;
            this.lblCategor.Text = "Категория";
            // 
            // lblINN
            // 
            this.lblINN.Location = new System.Drawing.Point(4, 56);
            this.lblINN.Name = "lblINN";
            this.lblINN.Size = new System.Drawing.Size(49, 15);
            this.lblINN.TabIndex = 40;
            this.lblINN.Text = "ИНН";
            // 
            // cbIsWorker
            // 
            this.cbIsWorker.AutoSize = true;
            this.cbIsWorker.Location = new System.Drawing.Point(60, 93);
            this.cbIsWorker.Name = "cbIsWorker";
            this.cbIsWorker.Size = new System.Drawing.Size(99, 17);
            this.cbIsWorker.TabIndex = 3;
            this.cbIsWorker.Text = "Это сотрудник";
            this.cbIsWorker.UseVisualStyleBackColor = true;
            this.cbIsWorker.CheckedChanged += new System.EventHandler(this.cbIsWorker_CheckedChanged);
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(59, 7);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(151, 20);
            this.txtID.TabIndex = 20;
            // 
            // txtCategor
            // 
            this.txtCategor.Location = new System.Drawing.Point(278, 53);
            this.txtCategor.Name = "txtCategor";
            this.txtCategor.Size = new System.Drawing.Size(145, 20);
            this.txtCategor.TabIndex = 2;
            this.txtCategor.TextChanged += new System.EventHandler(this.txtCategor_TextChanged);
            // 
            // txtINN
            // 
            this.txtINN.Location = new System.Drawing.Point(59, 53);
            this.txtINN.Name = "txtINN";
            this.txtINN.Size = new System.Drawing.Size(100, 20);
            this.txtINN.TabIndex = 1;
            this.txtINN.TextChanged += new System.EventHandler(this.txtINN_TextChanged);
            // 
            // txtFIO
            // 
            this.txtFIO.Location = new System.Drawing.Point(59, 30);
            this.txtFIO.Name = "txtFIO";
            this.txtFIO.Size = new System.Drawing.Size(364, 20);
            this.txtFIO.TabIndex = 0;
            this.txtFIO.TextChanged += new System.EventHandler(this.txtFIO_TextChanged);
            // 
            // frmFizLic_element
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(432, 165);
            this.Controls.Add(this.txtFIO);
            this.Controls.Add(this.txtINN);
            this.Controls.Add(this.txtCategor);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.cbIsWorker);
            this.Controls.Add(this.lblCategor);
            this.Controls.Add(this.lblINN);
            this.Controls.Add(this.lblFIO);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFizLic_element";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmFizLic_element";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFizLic_element_FormClosing);
            this.Load += new System.EventHandler(this.frmFizLic_element_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblFIO;
        private System.Windows.Forms.Label lblCategor;
        private System.Windows.Forms.Label lblINN;
        private System.Windows.Forms.CheckBox cbIsWorker;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtCategor;
        private System.Windows.Forms.TextBox txtINN;
        private System.Windows.Forms.TextBox txtFIO;
    }
}