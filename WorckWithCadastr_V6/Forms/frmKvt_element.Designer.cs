namespace WorckWithCadastr_V6
{
    partial class frmKvt_element
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
            this.txtPLH_BLK = new System.Windows.Forms.TextBox();
            this.lblPLH_BLK = new System.Windows.Forms.Label();
            this.txtNMR_KVT = new System.Windows.Forms.TextBox();
            this.lblNMR_KVT = new System.Windows.Forms.Label();
            this.txtKOD_TYP_KVT = new System.Windows.Forms.TextBox();
            this.lblKOD_TYP_KVT = new System.Windows.Forms.Label();
            this.txtKLK_DLK = new System.Windows.Forms.TextBox();
            this.lblKLK_DLK = new System.Windows.Forms.Label();
            this.txtID_MSB_OBJ = new System.Windows.Forms.TextBox();
            this.lblID_MSB_OBJ = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrymitka
            // 
            this.lblPrymitka.Location = new System.Drawing.Point(8, 220);
            this.lblPrymitka.Size = new System.Drawing.Size(142, 23);
            // 
            // txtPrymitka
            // 
            this.txtPrymitka.Location = new System.Drawing.Point(159, 201);
            this.txtPrymitka.Size = new System.Drawing.Size(451, 61);
            // 
            // lblN_Kad
            // 
            this.lblN_Kad.Location = new System.Drawing.Point(8, 31);
            this.lblN_Kad.Size = new System.Drawing.Size(142, 23);
            // 
            // txtN_Kad
            // 
            this.txtN_Kad.Location = new System.Drawing.Point(159, 33);
            this.txtN_Kad.Size = new System.Drawing.Size(147, 20);
            this.txtN_Kad.TabIndex = 2;
            // 
            // btnShowOnMap
            // 
            this.btnShowOnMap.Location = new System.Drawing.Point(248, 271);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(535, 271);
            this.btnOk.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(11, 271);
            this.btnCancel.TabIndex = 12;
            // 
            // cbRuleID
            // 
            this.cbRuleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRuleID.FormattingEnabled = true;
            this.cbRuleID.Location = new System.Drawing.Point(463, 89);
            this.cbRuleID.Name = "cbRuleID";
            this.cbRuleID.Size = new System.Drawing.Size(147, 21);
            this.cbRuleID.TabIndex = 5;
            this.cbRuleID.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblRuleID
            // 
            this.lblRuleID.Location = new System.Drawing.Point(316, 89);
            this.lblRuleID.Name = "lblRuleID";
            this.lblRuleID.Size = new System.Drawing.Size(142, 20);
            this.lblRuleID.TabIndex = 39;
            this.lblRuleID.Text = "Умовні знаки";
            // 
            // cbKOD_STS
            // 
            this.cbKOD_STS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_STS.FormattingEnabled = true;
            this.cbKOD_STS.Location = new System.Drawing.Point(159, 89);
            this.cbKOD_STS.Name = "cbKOD_STS";
            this.cbKOD_STS.Size = new System.Drawing.Size(147, 21);
            this.cbKOD_STS.TabIndex = 4;
            this.cbKOD_STS.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblKOD_STS
            // 
            this.lblKOD_STS.Location = new System.Drawing.Point(8, 89);
            this.lblKOD_STS.Name = "lblKOD_STS";
            this.lblKOD_STS.Size = new System.Drawing.Size(142, 20);
            this.lblKOD_STS.TabIndex = 37;
            this.lblKOD_STS.Text = "Статус об\'єкту";
            // 
            // cbKOD_KLS
            // 
            this.cbKOD_KLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKOD_KLS.FormattingEnabled = true;
            this.cbKOD_KLS.Location = new System.Drawing.Point(159, 60);
            this.cbKOD_KLS.Name = "cbKOD_KLS";
            this.cbKOD_KLS.Size = new System.Drawing.Size(147, 21);
            this.cbKOD_KLS.TabIndex = 3;
            this.cbKOD_KLS.SelectedIndexChanged += new System.EventHandler(this.main_SelectedIndexChanged);
            // 
            // lblKOD_KLS
            // 
            this.lblKOD_KLS.Location = new System.Drawing.Point(8, 60);
            this.lblKOD_KLS.Name = "lblKOD_KLS";
            this.lblKOD_KLS.Size = new System.Drawing.Size(142, 20);
            this.lblKOD_KLS.TabIndex = 35;
            this.lblKOD_KLS.Text = "Код за класифікатором";
            // 
            // txtPidcode
            // 
            this.txtPidcode.Location = new System.Drawing.Point(159, 172);
            this.txtPidcode.Name = "txtPidcode";
            this.txtPidcode.Size = new System.Drawing.Size(147, 20);
            this.txtPidcode.TabIndex = 9;
            this.txtPidcode.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtPidcode.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblPidcode
            // 
            this.lblPidcode.Location = new System.Drawing.Point(8, 169);
            this.lblPidcode.Name = "lblPidcode";
            this.lblPidcode.Size = new System.Drawing.Size(142, 20);
            this.lblPidcode.TabIndex = 176;
            this.lblPidcode.Text = "Підкод типу документа";
            // 
            // txtPLH_BLK
            // 
            this.txtPLH_BLK.Location = new System.Drawing.Point(159, 144);
            this.txtPLH_BLK.Name = "txtPLH_BLK";
            this.txtPLH_BLK.Size = new System.Drawing.Size(147, 20);
            this.txtPLH_BLK.TabIndex = 7;
            this.txtPLH_BLK.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtPLH_BLK.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblPLH_BLK
            // 
            this.lblPLH_BLK.Location = new System.Drawing.Point(8, 144);
            this.lblPLH_BLK.Name = "lblPLH_BLK";
            this.lblPLH_BLK.Size = new System.Drawing.Size(142, 20);
            this.lblPLH_BLK.TabIndex = 174;
            this.lblPLH_BLK.Text = "Площа ділянок";
            // 
            // txtNMR_KVT
            // 
            this.txtNMR_KVT.Location = new System.Drawing.Point(159, 118);
            this.txtNMR_KVT.Name = "txtNMR_KVT";
            this.txtNMR_KVT.Size = new System.Drawing.Size(147, 20);
            this.txtNMR_KVT.TabIndex = 6;
            this.txtNMR_KVT.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNMR_KVT.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNMR_KVT
            // 
            this.lblNMR_KVT.Location = new System.Drawing.Point(8, 115);
            this.lblNMR_KVT.Name = "lblNMR_KVT";
            this.lblNMR_KVT.Size = new System.Drawing.Size(142, 20);
            this.lblNMR_KVT.TabIndex = 172;
            this.lblNMR_KVT.Text = "Номер кварталу";
            // 
            // txtKOD_TYP_KVT
            // 
            this.txtKOD_TYP_KVT.Location = new System.Drawing.Point(463, 7);
            this.txtKOD_TYP_KVT.Name = "txtKOD_TYP_KVT";
            this.txtKOD_TYP_KVT.Size = new System.Drawing.Size(147, 20);
            this.txtKOD_TYP_KVT.TabIndex = 1;
            this.txtKOD_TYP_KVT.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtKOD_TYP_KVT.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKOD_TYP_KVT
            // 
            this.lblKOD_TYP_KVT.Location = new System.Drawing.Point(323, 7);
            this.lblKOD_TYP_KVT.Name = "lblKOD_TYP_KVT";
            this.lblKOD_TYP_KVT.Size = new System.Drawing.Size(142, 20);
            this.lblKOD_TYP_KVT.TabIndex = 170;
            this.lblKOD_TYP_KVT.Text = "Код типу Об’єкту";
            // 
            // txtKLK_DLK
            // 
            this.txtKLK_DLK.Location = new System.Drawing.Point(463, 144);
            this.txtKLK_DLK.Name = "txtKLK_DLK";
            this.txtKLK_DLK.Size = new System.Drawing.Size(147, 20);
            this.txtKLK_DLK.TabIndex = 8;
            this.txtKLK_DLK.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtKLK_DLK.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKLK_DLK
            // 
            this.lblKLK_DLK.Location = new System.Drawing.Point(316, 144);
            this.lblKLK_DLK.Name = "lblKLK_DLK";
            this.lblKLK_DLK.Size = new System.Drawing.Size(142, 20);
            this.lblKLK_DLK.TabIndex = 168;
            this.lblKLK_DLK.Text = "Кількість ділянок";
            // 
            // txtID_MSB_OBJ
            // 
            this.txtID_MSB_OBJ.Location = new System.Drawing.Point(159, 7);
            this.txtID_MSB_OBJ.Name = "txtID_MSB_OBJ";
            this.txtID_MSB_OBJ.Size = new System.Drawing.Size(147, 20);
            this.txtID_MSB_OBJ.TabIndex = 0;
            this.txtID_MSB_OBJ.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtID_MSB_OBJ.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblID_MSB_OBJ
            // 
            this.lblID_MSB_OBJ.Location = new System.Drawing.Point(8, 7);
            this.lblID_MSB_OBJ.Name = "lblID_MSB_OBJ";
            this.lblID_MSB_OBJ.Size = new System.Drawing.Size(142, 20);
            this.lblID_MSB_OBJ.TabIndex = 166;
            this.lblID_MSB_OBJ.Text = "Ідентифікатор об’єкту";
            // 
            // frmKvt_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 305);
            this.Controls.Add(this.txtPidcode);
            this.Controls.Add(this.lblPidcode);
            this.Controls.Add(this.txtPLH_BLK);
            this.Controls.Add(this.lblPLH_BLK);
            this.Controls.Add(this.txtNMR_KVT);
            this.Controls.Add(this.lblNMR_KVT);
            this.Controls.Add(this.txtKOD_TYP_KVT);
            this.Controls.Add(this.lblKOD_TYP_KVT);
            this.Controls.Add(this.txtKLK_DLK);
            this.Controls.Add(this.lblKLK_DLK);
            this.Controls.Add(this.txtID_MSB_OBJ);
            this.Controls.Add(this.lblID_MSB_OBJ);
            this.Controls.Add(this.cbRuleID);
            this.Controls.Add(this.lblRuleID);
            this.Controls.Add(this.cbKOD_STS);
            this.Controls.Add(this.lblKOD_STS);
            this.Controls.Add(this.cbKOD_KLS);
            this.Controls.Add(this.lblKOD_KLS);
            this.Name = "frmKvt_element";
            this.Text = "frmRej_Vul_element";
            this.Load += new System.EventHandler(this.frmReestrZek_element_Load);
            this.Controls.SetChildIndex(this.lblKOD_KLS, 0);
            this.Controls.SetChildIndex(this.cbKOD_KLS, 0);
            this.Controls.SetChildIndex(this.lblKOD_STS, 0);
            this.Controls.SetChildIndex(this.cbKOD_STS, 0);
            this.Controls.SetChildIndex(this.lblRuleID, 0);
            this.Controls.SetChildIndex(this.cbRuleID, 0);
            this.Controls.SetChildIndex(this.lblID_MSB_OBJ, 0);
            this.Controls.SetChildIndex(this.txtID_MSB_OBJ, 0);
            this.Controls.SetChildIndex(this.lblKLK_DLK, 0);
            this.Controls.SetChildIndex(this.txtKLK_DLK, 0);
            this.Controls.SetChildIndex(this.lblKOD_TYP_KVT, 0);
            this.Controls.SetChildIndex(this.txtKOD_TYP_KVT, 0);
            this.Controls.SetChildIndex(this.lblNMR_KVT, 0);
            this.Controls.SetChildIndex(this.txtNMR_KVT, 0);
            this.Controls.SetChildIndex(this.lblPLH_BLK, 0);
            this.Controls.SetChildIndex(this.txtPLH_BLK, 0);
            this.Controls.SetChildIndex(this.lblPidcode, 0);
            this.Controls.SetChildIndex(this.txtPidcode, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblPrymitka, 0);
            this.Controls.SetChildIndex(this.txtPrymitka, 0);
            this.Controls.SetChildIndex(this.lblN_Kad, 0);
            this.Controls.SetChildIndex(this.txtN_Kad, 0);
            this.Controls.SetChildIndex(this.btnShowOnMap, 0);
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
        private System.Windows.Forms.TextBox txtPLH_BLK;
        private System.Windows.Forms.Label lblPLH_BLK;
        private System.Windows.Forms.TextBox txtNMR_KVT;
        private System.Windows.Forms.Label lblNMR_KVT;
        private System.Windows.Forms.TextBox txtKOD_TYP_KVT;
        private System.Windows.Forms.Label lblKOD_TYP_KVT;
        private System.Windows.Forms.TextBox txtKLK_DLK;
        private System.Windows.Forms.Label lblKLK_DLK;
        private System.Windows.Forms.TextBox txtID_MSB_OBJ;
        private System.Windows.Forms.Label lblID_MSB_OBJ;
    }
}