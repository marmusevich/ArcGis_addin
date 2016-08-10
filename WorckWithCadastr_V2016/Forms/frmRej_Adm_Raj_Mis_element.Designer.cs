namespace WorckWithKadastr2016
{
    partial class frmRej_Adm_Raj_Mis_element
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
            this.cbKOD_KLS = new System.Windows.Forms.ComboBox();
            this.lblKOD_KLS = new System.Windows.Forms.Label();
            this.txtID_MSB_OBJ = new System.Windows.Forms.TextBox();
            this.lblID_MSB_OBJ = new System.Windows.Forms.Label();
            this.txtID_RAI = new System.Windows.Forms.TextBox();
            this.lblID_RAI = new System.Windows.Forms.Label();
            this.txtKOD_KOATUU_RAI = new System.Windows.Forms.TextBox();
            this.lblKOD_KOATUU_RAI = new System.Windows.Forms.Label();
            this.txtNAZVA_LAT = new System.Windows.Forms.TextBox();
            this.lblNAZVA_LAT = new System.Windows.Forms.Label();
            this.txtNAZVA_ROS = new System.Windows.Forms.TextBox();
            this.lblNAZVA_ROS = new System.Windows.Forms.Label();
            this.txtNAZVA_UKR = new System.Windows.Forms.TextBox();
            this.lblNAZVA_UKR = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrymitka
            // 
            this.lblPrymitka.Location = new System.Drawing.Point(15, 224);
            this.lblPrymitka.Size = new System.Drawing.Size(144, 23);
            // 
            // txtPrymitka
            // 
            this.txtPrymitka.Location = new System.Drawing.Point(181, 205);
            this.txtPrymitka.Size = new System.Drawing.Size(469, 61);
            this.txtPrymitka.TabIndex = 9;
            // 
            // lblN_Kad
            // 
            this.lblN_Kad.Location = new System.Drawing.Point(12, 64);
            this.lblN_Kad.Size = new System.Drawing.Size(147, 23);
            // 
            // txtN_Kad
            // 
            this.txtN_Kad.Location = new System.Drawing.Point(180, 65);
            this.txtN_Kad.Size = new System.Drawing.Size(150, 20);
            this.txtN_Kad.TabIndex = 3;
            // 
            // btnShowOnMap
            // 
            this.btnShowOnMap.Location = new System.Drawing.Point(298, 273);
            this.btnShowOnMap.TabIndex = 12;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(575, 274);
            this.btnOk.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(18, 274);
            this.btnCancel.TabIndex = 11;
            // 
            // cbRuleID
            // 
            this.cbRuleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRuleID.FormattingEnabled = true;
            this.cbRuleID.Location = new System.Drawing.Point(499, 93);
            this.cbRuleID.Name = "cbRuleID";
            this.cbRuleID.Size = new System.Drawing.Size(150, 21);
            this.cbRuleID.TabIndex = 5;
            this.cbRuleID.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblRuleID
            // 
            this.lblRuleID.Location = new System.Drawing.Point(347, 93);
            this.lblRuleID.Name = "lblRuleID";
            this.lblRuleID.Size = new System.Drawing.Size(147, 20);
            this.lblRuleID.TabIndex = 43;
            this.lblRuleID.Text = "Умовні знаки";
            // 
            // cbKOD_KLS
            // 
            this.cbKOD_KLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_KLS.FormattingEnabled = true;
            this.cbKOD_KLS.Location = new System.Drawing.Point(180, 93);
            this.cbKOD_KLS.Name = "cbKOD_KLS";
            this.cbKOD_KLS.Size = new System.Drawing.Size(150, 21);
            this.cbKOD_KLS.TabIndex = 4;
            this.cbKOD_KLS.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblKOD_KLS
            // 
            this.lblKOD_KLS.Location = new System.Drawing.Point(12, 93);
            this.lblKOD_KLS.Name = "lblKOD_KLS";
            this.lblKOD_KLS.Size = new System.Drawing.Size(147, 20);
            this.lblKOD_KLS.TabIndex = 41;
            this.lblKOD_KLS.Text = "Код за класифікатором";
            // 
            // txtID_MSB_OBJ
            // 
            this.txtID_MSB_OBJ.Location = new System.Drawing.Point(180, 9);
            this.txtID_MSB_OBJ.Name = "txtID_MSB_OBJ";
            this.txtID_MSB_OBJ.Size = new System.Drawing.Size(150, 20);
            this.txtID_MSB_OBJ.TabIndex = 0;
            this.txtID_MSB_OBJ.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtID_MSB_OBJ.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblID_MSB_OBJ
            // 
            this.lblID_MSB_OBJ.Location = new System.Drawing.Point(12, 9);
            this.lblID_MSB_OBJ.Name = "lblID_MSB_OBJ";
            this.lblID_MSB_OBJ.Size = new System.Drawing.Size(147, 20);
            this.lblID_MSB_OBJ.TabIndex = 168;
            this.lblID_MSB_OBJ.Text = "Ідентифікатор об’єкту";
            // 
            // txtID_RAI
            // 
            this.txtID_RAI.Location = new System.Drawing.Point(180, 37);
            this.txtID_RAI.Name = "txtID_RAI";
            this.txtID_RAI.Size = new System.Drawing.Size(150, 20);
            this.txtID_RAI.TabIndex = 2;
            this.txtID_RAI.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtID_RAI.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblID_RAI
            // 
            this.lblID_RAI.Location = new System.Drawing.Point(12, 37);
            this.lblID_RAI.Name = "lblID_RAI";
            this.lblID_RAI.Size = new System.Drawing.Size(147, 20);
            this.lblID_RAI.TabIndex = 176;
            this.lblID_RAI.Text = "Ідентифікатор району";
            // 
            // txtKOD_KOATUU_RAI
            // 
            this.txtKOD_KOATUU_RAI.Location = new System.Drawing.Point(499, 9);
            this.txtKOD_KOATUU_RAI.Name = "txtKOD_KOATUU_RAI";
            this.txtKOD_KOATUU_RAI.Size = new System.Drawing.Size(150, 20);
            this.txtKOD_KOATUU_RAI.TabIndex = 1;
            this.txtKOD_KOATUU_RAI.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtKOD_KOATUU_RAI.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKOD_KOATUU_RAI
            // 
            this.lblKOD_KOATUU_RAI.Location = new System.Drawing.Point(347, 9);
            this.lblKOD_KOATUU_RAI.Name = "lblKOD_KOATUU_RAI";
            this.lblKOD_KOATUU_RAI.Size = new System.Drawing.Size(147, 47);
            this.lblKOD_KOATUU_RAI.TabIndex = 174;
            this.lblKOD_KOATUU_RAI.Text = "Код КОАТУУ адміністративного району міста";
            // 
            // txtNAZVA_LAT
            // 
            this.txtNAZVA_LAT.Location = new System.Drawing.Point(180, 177);
            this.txtNAZVA_LAT.Name = "txtNAZVA_LAT";
            this.txtNAZVA_LAT.Size = new System.Drawing.Size(469, 20);
            this.txtNAZVA_LAT.TabIndex = 8;
            this.txtNAZVA_LAT.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNAZVA_LAT.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNAZVA_LAT
            // 
            this.lblNAZVA_LAT.Location = new System.Drawing.Point(15, 177);
            this.lblNAZVA_LAT.Name = "lblNAZVA_LAT";
            this.lblNAZVA_LAT.Size = new System.Drawing.Size(144, 20);
            this.lblNAZVA_LAT.TabIndex = 182;
            this.lblNAZVA_LAT.Text = "Назва району латиницею";
            // 
            // txtNAZVA_ROS
            // 
            this.txtNAZVA_ROS.Location = new System.Drawing.Point(180, 149);
            this.txtNAZVA_ROS.Name = "txtNAZVA_ROS";
            this.txtNAZVA_ROS.Size = new System.Drawing.Size(469, 20);
            this.txtNAZVA_ROS.TabIndex = 7;
            this.txtNAZVA_ROS.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNAZVA_ROS.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNAZVA_ROS
            // 
            this.lblNAZVA_ROS.Location = new System.Drawing.Point(12, 149);
            this.lblNAZVA_ROS.Name = "lblNAZVA_ROS";
            this.lblNAZVA_ROS.Size = new System.Drawing.Size(147, 20);
            this.lblNAZVA_ROS.TabIndex = 180;
            this.lblNAZVA_ROS.Text = "Назва району російською мовою";
            // 
            // txtNAZVA_UKR
            // 
            this.txtNAZVA_UKR.Location = new System.Drawing.Point(180, 121);
            this.txtNAZVA_UKR.Name = "txtNAZVA_UKR";
            this.txtNAZVA_UKR.Size = new System.Drawing.Size(469, 20);
            this.txtNAZVA_UKR.TabIndex = 6;
            this.txtNAZVA_UKR.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNAZVA_UKR.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNAZVA_UKR
            // 
            this.lblNAZVA_UKR.Location = new System.Drawing.Point(12, 121);
            this.lblNAZVA_UKR.Name = "lblNAZVA_UKR";
            this.lblNAZVA_UKR.Size = new System.Drawing.Size(147, 20);
            this.lblNAZVA_UKR.TabIndex = 178;
            this.lblNAZVA_UKR.Text = "Назва району українською мовою";
            // 
            // frmRej_Adm_Raj_Mis_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 304);
            this.Controls.Add(this.txtNAZVA_LAT);
            this.Controls.Add(this.lblNAZVA_LAT);
            this.Controls.Add(this.txtNAZVA_ROS);
            this.Controls.Add(this.lblNAZVA_ROS);
            this.Controls.Add(this.txtNAZVA_UKR);
            this.Controls.Add(this.lblNAZVA_UKR);
            this.Controls.Add(this.txtID_RAI);
            this.Controls.Add(this.lblID_RAI);
            this.Controls.Add(this.txtKOD_KOATUU_RAI);
            this.Controls.Add(this.lblKOD_KOATUU_RAI);
            this.Controls.Add(this.txtID_MSB_OBJ);
            this.Controls.Add(this.lblID_MSB_OBJ);
            this.Controls.Add(this.cbRuleID);
            this.Controls.Add(this.lblRuleID);
            this.Controls.Add(this.cbKOD_KLS);
            this.Controls.Add(this.lblKOD_KLS);
            this.Name = "frmRej_Adm_Raj_Mis_element";
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
            this.Controls.SetChildIndex(this.lblRuleID, 0);
            this.Controls.SetChildIndex(this.cbRuleID, 0);
            this.Controls.SetChildIndex(this.lblID_MSB_OBJ, 0);
            this.Controls.SetChildIndex(this.txtID_MSB_OBJ, 0);
            this.Controls.SetChildIndex(this.lblKOD_KOATUU_RAI, 0);
            this.Controls.SetChildIndex(this.txtKOD_KOATUU_RAI, 0);
            this.Controls.SetChildIndex(this.lblID_RAI, 0);
            this.Controls.SetChildIndex(this.txtID_RAI, 0);
            this.Controls.SetChildIndex(this.lblNAZVA_UKR, 0);
            this.Controls.SetChildIndex(this.txtNAZVA_UKR, 0);
            this.Controls.SetChildIndex(this.lblNAZVA_ROS, 0);
            this.Controls.SetChildIndex(this.txtNAZVA_ROS, 0);
            this.Controls.SetChildIndex(this.lblNAZVA_LAT, 0);
            this.Controls.SetChildIndex(this.txtNAZVA_LAT, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox cbRuleID;
        protected System.Windows.Forms.Label lblRuleID;
        protected System.Windows.Forms.ComboBox cbKOD_KLS;
        protected System.Windows.Forms.Label lblKOD_KLS;
        private System.Windows.Forms.TextBox txtID_MSB_OBJ;
        private System.Windows.Forms.Label lblID_MSB_OBJ;
        private System.Windows.Forms.TextBox txtID_RAI;
        private System.Windows.Forms.Label lblID_RAI;
        private System.Windows.Forms.TextBox txtKOD_KOATUU_RAI;
        private System.Windows.Forms.Label lblKOD_KOATUU_RAI;
        private System.Windows.Forms.TextBox txtNAZVA_LAT;
        private System.Windows.Forms.Label lblNAZVA_LAT;
        private System.Windows.Forms.TextBox txtNAZVA_ROS;
        private System.Windows.Forms.Label lblNAZVA_ROS;
        private System.Windows.Forms.TextBox txtNAZVA_UKR;
        private System.Windows.Forms.Label lblNAZVA_UKR;
    }
}