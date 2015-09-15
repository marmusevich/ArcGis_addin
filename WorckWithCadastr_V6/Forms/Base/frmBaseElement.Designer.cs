namespace WorckWithCadastr_V6
{
    partial class frmBaseElement
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
            this.lblPrymitka = new System.Windows.Forms.Label();
            this.txtPrymitka = new System.Windows.Forms.TextBox();
            this.lblN_Kad = new System.Windows.Forms.Label();
            this.txtN_Kad = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(344, 471);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 471);
            // 
            // lblPrymitka
            // 
            this.lblPrymitka.Location = new System.Drawing.Point(17, 385);
            this.lblPrymitka.Name = "lblPrymitka";
            this.lblPrymitka.Size = new System.Drawing.Size(100, 23);
            this.lblPrymitka.TabIndex = 9;
            this.lblPrymitka.Text = "Примітка";
            // 
            // txtPrymitka
            // 
            this.txtPrymitka.Location = new System.Drawing.Point(141, 383);
            this.txtPrymitka.Multiline = true;
            this.txtPrymitka.Name = "txtPrymitka";
            this.txtPrymitka.Size = new System.Drawing.Size(447, 61);
            this.txtPrymitka.TabIndex = 10;
            this.txtPrymitka.TextChanged += new System.EventHandler(this.txtPrymitka_TextChanged);
            this.txtPrymitka.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrymitka_Validating);
            // 
            // lblN_Kad
            // 
            this.lblN_Kad.Location = new System.Drawing.Point(20, 334);
            this.lblN_Kad.Name = "lblN_Kad";
            this.lblN_Kad.Size = new System.Drawing.Size(100, 23);
            this.lblN_Kad.TabIndex = 11;
            this.lblN_Kad.Text = "Кадастровий № ДПТ";
            // 
            // txtN_Kad
            // 
            this.txtN_Kad.Location = new System.Drawing.Point(141, 326);
            this.txtN_Kad.Name = "txtN_Kad";
            this.txtN_Kad.Size = new System.Drawing.Size(100, 20);
            this.txtN_Kad.TabIndex = 12;
            this.txtN_Kad.TextChanged += new System.EventHandler(this.txtN_Kad_TextChanged);
            this.txtN_Kad.Validating += new System.ComponentModel.CancelEventHandler(this.txtN_Kad_Validating);
            // 
            // frmBaseElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 506);
            this.Controls.Add(this.txtN_Kad);
            this.Controls.Add(this.lblN_Kad);
            this.Controls.Add(this.txtPrymitka);
            this.Controls.Add(this.lblPrymitka);
            this.Name = "frmBaseElement";
            this.Text = "frmBaseElement";
            this.Load += new System.EventHandler(this.frmBaseElement_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblPrymitka, 0);
            this.Controls.SetChildIndex(this.txtPrymitka, 0);
            this.Controls.SetChildIndex(this.lblN_Kad, 0);
            this.Controls.SetChildIndex(this.txtN_Kad, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblPrymitka;
        protected System.Windows.Forms.TextBox txtPrymitka;
        protected System.Windows.Forms.Label lblN_Kad;
        protected System.Windows.Forms.TextBox txtN_Kad;
    }
}