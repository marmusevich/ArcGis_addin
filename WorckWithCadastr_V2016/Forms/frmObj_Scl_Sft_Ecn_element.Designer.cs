namespace WorckWithKadastr2016
{
    partial class frmObj_Scl_Sft_Ecn_element
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
            this.cbRuleID = new System.Windows.Forms.ComboBox();
            this.lblRuleID = new System.Windows.Forms.Label();
            this.cbKOD_STS = new System.Windows.Forms.ComboBox();
            this.lblKOD_STS = new System.Windows.Forms.Label();
            this.cbKOD_KLS = new System.Windows.Forms.ComboBox();
            this.lblKOD_KLS = new System.Windows.Forms.Label();
            this.txtPidcode = new System.Windows.Forms.TextBox();
            this.lblPidcode = new System.Windows.Forms.Label();
            this.txtKOD_TYP_OBJ = new System.Windows.Forms.TextBox();
            this.lblKOD_TYP_OBJ = new System.Windows.Forms.Label();
            this.txtID_MSB_OBJ = new System.Windows.Forms.TextBox();
            this.lblID_MSB_OBJ = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrymitka
            // 
            this.lblPrymitka.Location = new System.Drawing.Point(10, 149);
            this.lblPrymitka.Size = new System.Drawing.Size(134, 23);
            // 
            // txtPrymitka
            // 
            this.txtPrymitka.Location = new System.Drawing.Point(167, 150);
            this.txtPrymitka.Size = new System.Drawing.Size(443, 61);
            this.txtPrymitka.TabIndex = 7;
            // 
            // lblN_Kad
            // 
            this.lblN_Kad.Location = new System.Drawing.Point(10, 38);
            this.lblN_Kad.Size = new System.Drawing.Size(134, 23);
            // 
            // txtN_Kad
            // 
            this.txtN_Kad.Location = new System.Drawing.Point(167, 38);
            this.txtN_Kad.Size = new System.Drawing.Size(154, 20);
            this.txtN_Kad.TabIndex = 2;
            // 
            // btnShowOnMap
            // 
            this.btnShowOnMap.Location = new System.Drawing.Point(253, 220);
            this.btnShowOnMap.TabIndex = 10;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(535, 220);
            this.btnOk.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(13, 220);
            this.btnCancel.TabIndex = 9;
            // 
            // cbRuleID
            // 
            this.cbRuleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRuleID.FormattingEnabled = true;
            this.cbRuleID.Location = new System.Drawing.Point(456, 94);
            this.cbRuleID.Name = "cbRuleID";
            this.cbRuleID.Size = new System.Drawing.Size(154, 21);
            this.cbRuleID.TabIndex = 5;
            this.cbRuleID.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblRuleID
            // 
            this.lblRuleID.Location = new System.Drawing.Point(329, 94);
            this.lblRuleID.Name = "lblRuleID";
            this.lblRuleID.Size = new System.Drawing.Size(110, 20);
            this.lblRuleID.TabIndex = 45;
            this.lblRuleID.Text = "Умовні знаки";
            // 
            // cbKOD_STS
            // 
            this.cbKOD_STS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_STS.FormattingEnabled = true;
            this.cbKOD_STS.Location = new System.Drawing.Point(167, 94);
            this.cbKOD_STS.Name = "cbKOD_STS";
            this.cbKOD_STS.Size = new System.Drawing.Size(154, 21);
            this.cbKOD_STS.TabIndex = 4;
            this.cbKOD_STS.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblKOD_STS
            // 
            this.lblKOD_STS.Location = new System.Drawing.Point(10, 94);
            this.lblKOD_STS.Name = "lblKOD_STS";
            this.lblKOD_STS.Size = new System.Drawing.Size(134, 20);
            this.lblKOD_STS.TabIndex = 43;
            this.lblKOD_STS.Text = "Статус об\'єкту";
            // 
            // cbKOD_KLS
            // 
            this.cbKOD_KLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_KLS.FormattingEnabled = true;
            this.cbKOD_KLS.Location = new System.Drawing.Point(167, 65);
            this.cbKOD_KLS.Name = "cbKOD_KLS";
            this.cbKOD_KLS.Size = new System.Drawing.Size(154, 21);
            this.cbKOD_KLS.TabIndex = 3;
            this.cbKOD_KLS.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblKOD_KLS
            // 
            this.lblKOD_KLS.Location = new System.Drawing.Point(10, 65);
            this.lblKOD_KLS.Name = "lblKOD_KLS";
            this.lblKOD_KLS.Size = new System.Drawing.Size(134, 20);
            this.lblKOD_KLS.TabIndex = 41;
            this.lblKOD_KLS.Text = "Код за класифікатором";
            // 
            // txtPidcode
            // 
            this.txtPidcode.Location = new System.Drawing.Point(167, 123);
            this.txtPidcode.Name = "txtPidcode";
            this.txtPidcode.Size = new System.Drawing.Size(154, 20);
            this.txtPidcode.TabIndex = 6;
            this.txtPidcode.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtPidcode.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblPidcode
            // 
            this.lblPidcode.Location = new System.Drawing.Point(10, 120);
            this.lblPidcode.Name = "lblPidcode";
            this.lblPidcode.Size = new System.Drawing.Size(134, 20);
            this.lblPidcode.TabIndex = 182;
            this.lblPidcode.Text = "Підкод типу документа";
            // 
            // txtKOD_TYP_OBJ
            // 
            this.txtKOD_TYP_OBJ.Location = new System.Drawing.Point(456, 12);
            this.txtKOD_TYP_OBJ.Name = "txtKOD_TYP_OBJ";
            this.txtKOD_TYP_OBJ.Size = new System.Drawing.Size(154, 20);
            this.txtKOD_TYP_OBJ.TabIndex = 1;
            this.txtKOD_TYP_OBJ.Text = "0";
            this.txtKOD_TYP_OBJ.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtKOD_TYP_OBJ.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKOD_TYP_OBJ
            // 
            this.lblKOD_TYP_OBJ.Location = new System.Drawing.Point(329, 12);
            this.lblKOD_TYP_OBJ.Name = "lblKOD_TYP_OBJ";
            this.lblKOD_TYP_OBJ.Size = new System.Drawing.Size(110, 20);
            this.lblKOD_TYP_OBJ.TabIndex = 180;
            this.lblKOD_TYP_OBJ.Text = "Код типу Об’єкту";
            // 
            // txtID_MSB_OBJ
            // 
            this.txtID_MSB_OBJ.Location = new System.Drawing.Point(167, 12);
            this.txtID_MSB_OBJ.Name = "txtID_MSB_OBJ";
            this.txtID_MSB_OBJ.Size = new System.Drawing.Size(154, 20);
            this.txtID_MSB_OBJ.TabIndex = 0;
            this.txtID_MSB_OBJ.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtID_MSB_OBJ.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblID_MSB_OBJ
            // 
            this.lblID_MSB_OBJ.Location = new System.Drawing.Point(10, 12);
            this.lblID_MSB_OBJ.Name = "lblID_MSB_OBJ";
            this.lblID_MSB_OBJ.Size = new System.Drawing.Size(134, 20);
            this.lblID_MSB_OBJ.TabIndex = 178;
            this.lblID_MSB_OBJ.Text = "Ідентифікатор об’єкту";
            // 
            // frmObj_Scl_Sft_Ecn_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 250);
            this.Controls.Add(this.txtPidcode);
            this.Controls.Add(this.lblPidcode);
            this.Controls.Add(this.txtKOD_TYP_OBJ);
            this.Controls.Add(this.lblKOD_TYP_OBJ);
            this.Controls.Add(this.txtID_MSB_OBJ);
            this.Controls.Add(this.lblID_MSB_OBJ);
            this.Controls.Add(this.cbRuleID);
            this.Controls.Add(this.lblRuleID);
            this.Controls.Add(this.cbKOD_STS);
            this.Controls.Add(this.lblKOD_STS);
            this.Controls.Add(this.cbKOD_KLS);
            this.Controls.Add(this.lblKOD_KLS);
            this.Name = "frmObj_Scl_Sft_Ecn_element";
            this.Text = "frmRej_Vul_element";
            this.Load += new System.EventHandler(this.frmReestrZek_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblPrymitka, 0);
            this.Controls.SetChildIndex(this.txtPrymitka, 0);
            this.Controls.SetChildIndex(this.lblN_Kad, 0);
            this.Controls.SetChildIndex(this.txtN_Kad, 0);
            this.Controls.SetChildIndex(this.btnShowOnMap, 0);
            this.Controls.SetChildIndex(this.lblKOD_KLS, 0);
            this.Controls.SetChildIndex(this.cbKOD_KLS, 0);
            this.Controls.SetChildIndex(this.lblKOD_STS, 0);
            this.Controls.SetChildIndex(this.cbKOD_STS, 0);
            this.Controls.SetChildIndex(this.lblRuleID, 0);
            this.Controls.SetChildIndex(this.cbRuleID, 0);
            this.Controls.SetChildIndex(this.lblID_MSB_OBJ, 0);
            this.Controls.SetChildIndex(this.txtID_MSB_OBJ, 0);
            this.Controls.SetChildIndex(this.lblKOD_TYP_OBJ, 0);
            this.Controls.SetChildIndex(this.txtKOD_TYP_OBJ, 0);
            this.Controls.SetChildIndex(this.lblPidcode, 0);
            this.Controls.SetChildIndex(this.txtPidcode, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox cbRuleID;
        protected System.Windows.Forms.Label lblRuleID;
        protected System.Windows.Forms.ComboBox cbKOD_STS;
        protected System.Windows.Forms.Label lblKOD_STS;
        protected System.Windows.Forms.ComboBox cbKOD_KLS;
        protected System.Windows.Forms.Label lblKOD_KLS;
        private System.Windows.Forms.TextBox txtPidcode;
        private System.Windows.Forms.Label lblPidcode;
        private System.Windows.Forms.TextBox txtKOD_TYP_OBJ;
        private System.Windows.Forms.Label lblKOD_TYP_OBJ;
        private System.Windows.Forms.TextBox txtID_MSB_OBJ;
        private System.Windows.Forms.Label lblID_MSB_OBJ;
    }
}