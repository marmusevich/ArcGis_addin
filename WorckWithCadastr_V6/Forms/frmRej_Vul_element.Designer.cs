namespace WorckWithCadastr_V6
{
    partial class frmRej_Vul_element
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
            this.cbKOD_KLS = new System.Windows.Forms.ComboBox();
            this.lblKOD_KLS = new System.Windows.Forms.Label();
            this.cbKOD_STAN_VUL = new System.Windows.Forms.ComboBox();
            this.lblKOD_STAN_VUL = new System.Windows.Forms.Label();
            this.cbKOD_KAT = new System.Windows.Forms.ComboBox();
            this.lblKOD_KAT = new System.Windows.Forms.Label();
            this.cbRuleID = new System.Windows.Forms.ComboBox();
            this.lblRuleID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrymitka
            // 
            this.lblPrymitka.Location = new System.Drawing.Point(5, 140);
            this.lblPrymitka.Size = new System.Drawing.Size(104, 18);
            // 
            // txtPrymitka
            // 
            this.txtPrymitka.Location = new System.Drawing.Point(149, 140);
            this.txtPrymitka.Size = new System.Drawing.Size(447, 35);
            // 
            // lblN_Kad
            // 
            this.lblN_Kad.Location = new System.Drawing.Point(5, 114);
            this.lblN_Kad.Size = new System.Drawing.Size(104, 18);
            // 
            // txtN_Kad
            // 
            this.txtN_Kad.Location = new System.Drawing.Point(149, 114);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(569, 471);
            // 
            // cbKOD_KLS
            // 
            this.cbKOD_KLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_KLS.FormattingEnabled = true;
            this.cbKOD_KLS.Location = new System.Drawing.Point(149, 6);
            this.cbKOD_KLS.Name = "cbKOD_KLS";
            this.cbKOD_KLS.Size = new System.Drawing.Size(150, 21);
            this.cbKOD_KLS.TabIndex = 20;
            this.cbKOD_KLS.SelectedIndexChanged += new System.EventHandler(this.main__SelectedIndexChanged);
            // 
            // lblKOD_KLS
            // 
            this.lblKOD_KLS.Location = new System.Drawing.Point(5, 9);
            this.lblKOD_KLS.Name = "lblKOD_KLS";
            this.lblKOD_KLS.Size = new System.Drawing.Size(138, 18);
            this.lblKOD_KLS.TabIndex = 21;
            this.lblKOD_KLS.Text = "Код за класифікатором";
            // 
            // cbKOD_STAN_VUL
            // 
            this.cbKOD_STAN_VUL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_STAN_VUL.FormattingEnabled = true;
            this.cbKOD_STAN_VUL.Location = new System.Drawing.Point(149, 33);
            this.cbKOD_STAN_VUL.Name = "cbKOD_STAN_VUL";
            this.cbKOD_STAN_VUL.Size = new System.Drawing.Size(150, 21);
            this.cbKOD_STAN_VUL.TabIndex = 22;
            this.cbKOD_STAN_VUL.SelectedIndexChanged += new System.EventHandler(this.main__SelectedIndexChanged);
            // 
            // lblKOD_STAN_VUL
            // 
            this.lblKOD_STAN_VUL.Location = new System.Drawing.Point(5, 33);
            this.lblKOD_STAN_VUL.Name = "lblKOD_STAN_VUL";
            this.lblKOD_STAN_VUL.Size = new System.Drawing.Size(138, 18);
            this.lblKOD_STAN_VUL.TabIndex = 23;
            this.lblKOD_STAN_VUL.Text = "Стан вулиці";
            // 
            // cbKOD_KAT
            // 
            this.cbKOD_KAT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_KAT.FormattingEnabled = true;
            this.cbKOD_KAT.Location = new System.Drawing.Point(149, 60);
            this.cbKOD_KAT.Name = "cbKOD_KAT";
            this.cbKOD_KAT.Size = new System.Drawing.Size(150, 21);
            this.cbKOD_KAT.TabIndex = 24;
            this.cbKOD_KAT.SelectedIndexChanged += new System.EventHandler(this.main__SelectedIndexChanged);
            // 
            // lblKOD_KAT
            // 
            this.lblKOD_KAT.Location = new System.Drawing.Point(5, 60);
            this.lblKOD_KAT.Name = "lblKOD_KAT";
            this.lblKOD_KAT.Size = new System.Drawing.Size(138, 18);
            this.lblKOD_KAT.TabIndex = 25;
            this.lblKOD_KAT.Text = "Код категорії вулиці";
            // 
            // cbRuleID
            // 
            this.cbRuleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRuleID.FormattingEnabled = true;
            this.cbRuleID.Location = new System.Drawing.Point(149, 87);
            this.cbRuleID.Name = "cbRuleID";
            this.cbRuleID.Size = new System.Drawing.Size(150, 21);
            this.cbRuleID.TabIndex = 26;
            this.cbRuleID.SelectedIndexChanged += new System.EventHandler(this.main__SelectedIndexChanged);
            // 
            // lblRuleID
            // 
            this.lblRuleID.Location = new System.Drawing.Point(5, 87);
            this.lblRuleID.Name = "lblRuleID";
            this.lblRuleID.Size = new System.Drawing.Size(138, 18);
            this.lblRuleID.TabIndex = 27;
            this.lblRuleID.Text = "Умовні знаки";
            // 
            // frmRej_Vul_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 506);
            this.Controls.Add(this.cbRuleID);
            this.Controls.Add(this.lblRuleID);
            this.Controls.Add(this.cbKOD_KAT);
            this.Controls.Add(this.lblKOD_KAT);
            this.Controls.Add(this.cbKOD_STAN_VUL);
            this.Controls.Add(this.lblKOD_STAN_VUL);
            this.Controls.Add(this.cbKOD_KLS);
            this.Controls.Add(this.lblKOD_KLS);
            this.Name = "frmRej_Vul_element";
            this.Text = "frmRej_Vul_element";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblPrymitka, 0);
            this.Controls.SetChildIndex(this.txtPrymitka, 0);
            this.Controls.SetChildIndex(this.lblN_Kad, 0);
            this.Controls.SetChildIndex(this.txtN_Kad, 0);
            this.Controls.SetChildIndex(this.lblKOD_KLS, 0);
            this.Controls.SetChildIndex(this.cbKOD_KLS, 0);
            this.Controls.SetChildIndex(this.lblKOD_STAN_VUL, 0);
            this.Controls.SetChildIndex(this.cbKOD_STAN_VUL, 0);
            this.Controls.SetChildIndex(this.lblKOD_KAT, 0);
            this.Controls.SetChildIndex(this.cbKOD_KAT, 0);
            this.Controls.SetChildIndex(this.lblRuleID, 0);
            this.Controls.SetChildIndex(this.cbRuleID, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox cbKOD_KLS;
        protected System.Windows.Forms.Label lblKOD_KLS;
        protected System.Windows.Forms.ComboBox cbKOD_STAN_VUL;
        protected System.Windows.Forms.Label lblKOD_STAN_VUL;
        protected System.Windows.Forms.ComboBox cbKOD_KAT;
        protected System.Windows.Forms.Label lblKOD_KAT;
        protected System.Windows.Forms.ComboBox cbRuleID;
        protected System.Windows.Forms.Label lblRuleID;
    }
}