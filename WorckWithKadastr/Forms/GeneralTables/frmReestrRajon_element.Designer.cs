namespace WorckWithKadastr
{
    partial class frmReestrRajon_element
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
            this.lblKodKOATU = new System.Windows.Forms.Label();
            this.txtKodKOATU = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKodKOATU
            // 
            this.lblKodKOATU.Location = new System.Drawing.Point(17, 34);
            this.lblKodKOATU.Name = "lblKodKOATU";
            this.lblKodKOATU.Size = new System.Drawing.Size(100, 21);
            this.lblKodKOATU.TabIndex = 49;
            this.lblKodKOATU.Text = "Код КОАТУУ";
            // 
            // txtKodKOATU
            // 
            this.txtKodKOATU.Location = new System.Drawing.Point(121, 34);
            this.txtKodKOATU.Name = "txtKodKOATU";
            this.txtKodKOATU.Size = new System.Drawing.Size(212, 20);
            this.txtKodKOATU.TabIndex = 50;
            this.txtKodKOATU.Text = "KodKOATU";
            this.txtKodKOATU.TextChanged += new System.EventHandler(this.txtKodKOATU_TextChanged);
            this.txtKodKOATU.Validating += new System.ComponentModel.CancelEventHandler(this.txtKodKOATU_Validating);
            // 
            // frmReestrRajon_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 680);
            this.Controls.Add(this.txtKodKOATU);
            this.Controls.Add(this.lblKodKOATU);
            this.Name = "frmReestrRajon_element";
            this.Text = "ReestrRajon_element";
            this.Load += new System.EventHandler(this.frmReestrRajon_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblAdminRajo, 0);
            this.Controls.SetChildIndex(this.txtAdminRajon, 0);
            this.Controls.SetChildIndex(this.btnAdminRajo, 0);
            this.Controls.SetChildIndex(this.lblDocument, 0);
            this.Controls.SetChildIndex(this.txtDocument, 0);
            this.Controls.SetChildIndex(this.btnDocument, 0);
            this.Controls.SetChildIndex(this.lblKodKategorii, 0);
            this.Controls.SetChildIndex(this.txtKodKategorii, 0);
            this.Controls.SetChildIndex(this.btnKodKategorii, 0);
            this.Controls.SetChildIndex(this.lblStatusObject, 0);
            this.Controls.SetChildIndex(this.cbStatusObject, 0);
            this.Controls.SetChildIndex(this.lblLocalType, 0);
            this.Controls.SetChildIndex(this.cbLocalType, 0);
            this.Controls.SetChildIndex(this.lblDzhereloKoord, 0);
            this.Controls.SetChildIndex(this.cbDzhereloKoord, 0);
            this.Controls.SetChildIndex(this.lblKodObject, 0);
            this.Controls.SetChildIndex(this.txtKodObject, 0);
            this.Controls.SetChildIndex(this.lblNazvaKorotkaRus, 0);
            this.Controls.SetChildIndex(this.txtNazvaKorotkaRus, 0);
            this.Controls.SetChildIndex(this.lblNazvaPovnaRus, 0);
            this.Controls.SetChildIndex(this.txtNazvaPovnaRus, 0);
            this.Controls.SetChildIndex(this.lblNazvaKorotkaUkr, 0);
            this.Controls.SetChildIndex(this.txtNazvaKorotkaUkr, 0);
            this.Controls.SetChildIndex(this.lblNazvaPovnaUkr, 0);
            this.Controls.SetChildIndex(this.txtNazvaPovnaUkr, 0);
            this.Controls.SetChildIndex(this.lblNazvaPovnaLat, 0);
            this.Controls.SetChildIndex(this.txtNazvaPovnaLat, 0);
            this.Controls.SetChildIndex(this.lblNazvaKorotkaLat, 0);
            this.Controls.SetChildIndex(this.txtNazvaKorotkaLat, 0);
            this.Controls.SetChildIndex(this.lblNazvaDocument, 0);
            this.Controls.SetChildIndex(this.txtNazvaDocument, 0);
            this.Controls.SetChildIndex(this.lblNomerDocument, 0);
            this.Controls.SetChildIndex(this.txtNomerDocument, 0);
            this.Controls.SetChildIndex(this.lblLinkDocument, 0);
            this.Controls.SetChildIndex(this.txtLinkDocument, 0);
            this.Controls.SetChildIndex(this.lblOpys, 0);
            this.Controls.SetChildIndex(this.txtOpys, 0);
            this.Controls.SetChildIndex(this.lblDataDocument, 0);
            this.Controls.SetChildIndex(this.dtpDataDocument, 0);
            this.Controls.SetChildIndex(this.btnShowOnMap, 0);
            this.Controls.SetChildIndex(this.lblKodKOATU, 0);
            this.Controls.SetChildIndex(this.txtKodKOATU, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKodKOATU;
        private System.Windows.Forms.TextBox txtKodKOATU;
    }
}