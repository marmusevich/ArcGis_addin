namespace SharedClasses
{
    partial class frmBaseAdrKategor_element
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
            this.txtKodKategorii = new System.Windows.Forms.TextBox();
            this.lblKodKategorii = new System.Windows.Forms.Label();
            this.lblNazvaTypu = new System.Windows.Forms.Label();
            this.txtNazvaTypu = new System.Windows.Forms.TextBox();
            this.lblKorotkaNazvaTypu = new System.Windows.Forms.Label();
            this.txtKorotkaNazvaTypu = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(341, 115);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(15, 115);
            // 
            // txtKodKategorii
            // 
            this.txtKodKategorii.Location = new System.Drawing.Point(135, 11);
            this.txtKodKategorii.Name = "txtKodKategorii";
            this.txtKodKategorii.Size = new System.Drawing.Size(100, 20);
            this.txtKodKategorii.TabIndex = 9;
            this.txtKodKategorii.TextChanged += new System.EventHandler(this.txtKodKategorii_TextChanged);
            this.txtKodKategorii.Validating += new System.ComponentModel.CancelEventHandler(this.txtKodKategorii_Validating);
            // 
            // lblKodKategorii
            // 
            this.lblKodKategorii.Location = new System.Drawing.Point(12, 12);
            this.lblKodKategorii.Name = "lblKodKategorii";
            this.lblKodKategorii.Size = new System.Drawing.Size(117, 15);
            this.lblKodKategorii.TabIndex = 10;
            this.lblKodKategorii.Text = "Код";
            // 
            // lblNazvaTypu
            // 
            this.lblNazvaTypu.Location = new System.Drawing.Point(12, 36);
            this.lblNazvaTypu.Name = "lblNazvaTypu";
            this.lblNazvaTypu.Size = new System.Drawing.Size(117, 15);
            this.lblNazvaTypu.TabIndex = 12;
            this.lblNazvaTypu.Text = "Найменування типу";
            // 
            // txtNazvaTypu
            // 
            this.txtNazvaTypu.Location = new System.Drawing.Point(135, 35);
            this.txtNazvaTypu.Name = "txtNazvaTypu";
            this.txtNazvaTypu.Size = new System.Drawing.Size(281, 20);
            this.txtNazvaTypu.TabIndex = 11;
            this.txtNazvaTypu.TextChanged += new System.EventHandler(this.txtNazvaTypu_TextChanged);
            this.txtNazvaTypu.Validating += new System.ComponentModel.CancelEventHandler(this.txtNazvaTypu_Validating);
            // 
            // lblKorotkaNazvaTypu
            // 
            this.lblKorotkaNazvaTypu.Location = new System.Drawing.Point(12, 65);
            this.lblKorotkaNazvaTypu.Name = "lblKorotkaNazvaTypu";
            this.lblKorotkaNazvaTypu.Size = new System.Drawing.Size(117, 33);
            this.lblKorotkaNazvaTypu.TabIndex = 14;
            this.lblKorotkaNazvaTypu.Text = "Скорочення найменування типу";
            // 
            // txtKorotkaNazvaTypu
            // 
            this.txtKorotkaNazvaTypu.Location = new System.Drawing.Point(135, 73);
            this.txtKorotkaNazvaTypu.Name = "txtKorotkaNazvaTypu";
            this.txtKorotkaNazvaTypu.Size = new System.Drawing.Size(281, 20);
            this.txtKorotkaNazvaTypu.TabIndex = 13;
            this.txtKorotkaNazvaTypu.TextChanged += new System.EventHandler(this.txtKorotkaNazvaTypu_TextChanged);
            // 
            // frmBaseAdrKategor_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 146);
            this.Controls.Add(this.lblKorotkaNazvaTypu);
            this.Controls.Add(this.txtKorotkaNazvaTypu);
            this.Controls.Add(this.lblNazvaTypu);
            this.Controls.Add(this.txtNazvaTypu);
            this.Controls.Add(this.lblKodKategorii);
            this.Controls.Add(this.txtKodKategorii);
            this.Name = "frmBaseAdrKategor_element";
            this.Text = "frmBaseAdrKategor_element";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtKodKategorii, 0);
            this.Controls.SetChildIndex(this.lblKodKategorii, 0);
            this.Controls.SetChildIndex(this.txtNazvaTypu, 0);
            this.Controls.SetChildIndex(this.lblNazvaTypu, 0);
            this.Controls.SetChildIndex(this.txtKorotkaNazvaTypu, 0);
            this.Controls.SetChildIndex(this.lblKorotkaNazvaTypu, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKodKategorii;
        private System.Windows.Forms.Label lblKodKategorii;
        private System.Windows.Forms.Label lblNazvaTypu;
        private System.Windows.Forms.TextBox txtNazvaTypu;
        private System.Windows.Forms.Label lblKorotkaNazvaTypu;
        private System.Windows.Forms.TextBox txtKorotkaNazvaTypu;
    }
}