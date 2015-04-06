using System.Windows.Forms;
namespace WorckWithReestr
{
    partial class frmReestrVedomostey_doc
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
            this.txtName_GD = new System.Windows.Forms.TextBox();
            this.txtN_Doc_GD = new System.Windows.Forms.TextBox();
            this.txtN_GD = new System.Windows.Forms.TextBox();
            this.lblN_GD = new System.Windows.Forms.Label();
            this.lblN_Doc_GD = new System.Windows.Forms.Label();
            this.lblName_GD = new System.Windows.Forms.Label();
            this.lblRozmischennyaKadastr = new System.Windows.Forms.Label();
            this.txtKol_Str_GD = new System.Windows.Forms.TextBox();
            this.txtPrim = new System.Windows.Forms.TextBox();
            this.txtN_Kad = new System.Windows.Forms.TextBox();
            this.txtEl_Format_GD = new System.Windows.Forms.TextBox();
            this.dtpData_Otp = new System.Windows.Forms.DateTimePicker();
            this.lblN_Vh = new System.Windows.Forms.Label();
            this.dtpData_Kad = new System.Windows.Forms.DateTimePicker();
            this.txtN_Vh = new System.Windows.Forms.TextBox();
            this.lblFIO_Kad = new System.Windows.Forms.Label();
            this.lblPrim = new System.Windows.Forms.Label();
            this.lblN_Kad = new System.Windows.Forms.Label();
            this.lblData_Kad = new System.Windows.Forms.Label();
            this.lblData_Vh = new System.Windows.Forms.Label();
            this.txtN_Sop_List = new System.Windows.Forms.TextBox();
            this.lblData_Otp = new System.Windows.Forms.Label();
            this.lblIst_Ved = new System.Windows.Forms.Label();
            this.lblEl_Format_GD = new System.Windows.Forms.Label();
            this.txtIst_Ved = new System.Windows.Forms.TextBox();
            this.lblN_Sop_List = new System.Windows.Forms.Label();
            this.lblKol_Str_GD = new System.Windows.Forms.Label();
            this.dtpData_Vh = new System.Windows.Forms.DateTimePicker();
            this.lblVhidni = new System.Windows.Forms.Label();
            this.lblDocMD = new System.Windows.Forms.Label();
            this.btnFIO_Kad = new System.Windows.Forms.Button();
            this.txtFIO_Kad = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(529, 418);
            this.btnOk.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(19, 418);
            this.btnCancel.TabIndex = 15;
            // 
            // txtName_GD
            // 
            this.txtName_GD.Location = new System.Drawing.Point(195, 164);
            this.txtName_GD.Name = "txtName_GD";
            this.txtName_GD.Size = new System.Drawing.Size(409, 20);
            this.txtName_GD.TabIndex = 7;
            this.txtName_GD.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_GD_Validating);
            // 
            // txtN_Doc_GD
            // 
            this.txtN_Doc_GD.Location = new System.Drawing.Point(463, 138);
            this.txtN_Doc_GD.Name = "txtN_Doc_GD";
            this.txtN_Doc_GD.Size = new System.Drawing.Size(141, 20);
            this.txtN_Doc_GD.TabIndex = 6;
            this.txtN_Doc_GD.Validating += new System.ComponentModel.CancelEventHandler(this.txtN_Doc_GD_Validating);
            // 
            // txtN_GD
            // 
            this.txtN_GD.Location = new System.Drawing.Point(195, 138);
            this.txtN_GD.Name = "txtN_GD";
            this.txtN_GD.Size = new System.Drawing.Size(98, 20);
            this.txtN_GD.TabIndex = 5;
            this.txtN_GD.Validating += new System.ComponentModel.CancelEventHandler(this.txtN_GD_Validating);
            // 
            // lblN_GD
            // 
            this.lblN_GD.Location = new System.Drawing.Point(16, 138);
            this.lblN_GD.Name = "lblN_GD";
            this.lblN_GD.Size = new System.Drawing.Size(171, 20);
            this.lblN_GD.TabIndex = 162;
            this.lblN_GD.Text = "№ п/п";
            // 
            // lblN_Doc_GD
            // 
            this.lblN_Doc_GD.Location = new System.Drawing.Point(302, 138);
            this.lblN_Doc_GD.Name = "lblN_Doc_GD";
            this.lblN_Doc_GD.Size = new System.Drawing.Size(155, 20);
            this.lblN_Doc_GD.TabIndex = 163;
            this.lblN_Doc_GD.Text = "Номер документу МД";
            // 
            // lblName_GD
            // 
            this.lblName_GD.Location = new System.Drawing.Point(16, 164);
            this.lblName_GD.Name = "lblName_GD";
            this.lblName_GD.Size = new System.Drawing.Size(171, 20);
            this.lblName_GD.TabIndex = 164;
            this.lblName_GD.Text = "Назва документу МД";
            // 
            // lblRozmischennyaKadastr
            // 
            this.lblRozmischennyaKadastr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRozmischennyaKadastr.Location = new System.Drawing.Point(16, 249);
            this.lblRozmischennyaKadastr.Name = "lblRozmischennyaKadastr";
            this.lblRozmischennyaKadastr.Size = new System.Drawing.Size(188, 20);
            this.lblRozmischennyaKadastr.TabIndex = 161;
            this.lblRozmischennyaKadastr.Text = "Розміщення в кадастрі";
            // 
            // txtKol_Str_GD
            // 
            this.txtKol_Str_GD.Location = new System.Drawing.Point(195, 190);
            this.txtKol_Str_GD.Name = "txtKol_Str_GD";
            this.txtKol_Str_GD.Size = new System.Drawing.Size(98, 20);
            this.txtKol_Str_GD.TabIndex = 8;
            this.txtKol_Str_GD.TextChanged += new System.EventHandler(this.txtKol_Str_GD_TextChanged);
            this.txtKol_Str_GD.Validating += new System.ComponentModel.CancelEventHandler(this.txtKol_Str_GD_Validating);
            // 
            // txtPrim
            // 
            this.txtPrim.Location = new System.Drawing.Point(195, 298);
            this.txtPrim.Multiline = true;
            this.txtPrim.Name = "txtPrim";
            this.txtPrim.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPrim.Size = new System.Drawing.Size(409, 61);
            this.txtPrim.TabIndex = 12;
            this.txtPrim.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrim_Validating);
            // 
            // txtN_Kad
            // 
            this.txtN_Kad.Location = new System.Drawing.Point(195, 272);
            this.txtN_Kad.Name = "txtN_Kad";
            this.txtN_Kad.Size = new System.Drawing.Size(99, 20);
            this.txtN_Kad.TabIndex = 10;
            this.txtN_Kad.VisibleChanged += new System.EventHandler(this.txtN_Kad_VisibleChanged);
            // 
            // txtEl_Format_GD
            // 
            this.txtEl_Format_GD.Location = new System.Drawing.Point(195, 216);
            this.txtEl_Format_GD.Name = "txtEl_Format_GD";
            this.txtEl_Format_GD.Size = new System.Drawing.Size(410, 20);
            this.txtEl_Format_GD.TabIndex = 9;
            this.txtEl_Format_GD.Validating += new System.ComponentModel.CancelEventHandler(this.txtEl_Format_GD_Validating);
            // 
            // dtpData_Otp
            // 
            this.dtpData_Otp.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Otp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Otp.Location = new System.Drawing.Point(463, 84);
            this.dtpData_Otp.Name = "dtpData_Otp";
            this.dtpData_Otp.Size = new System.Drawing.Size(141, 20);
            this.dtpData_Otp.TabIndex = 4;
            this.dtpData_Otp.Validating += new System.ComponentModel.CancelEventHandler(this.dtpData_Otp_Validating);
            // 
            // lblN_Vh
            // 
            this.lblN_Vh.Location = new System.Drawing.Point(16, 32);
            this.lblN_Vh.Name = "lblN_Vh";
            this.lblN_Vh.Size = new System.Drawing.Size(171, 20);
            this.lblN_Vh.TabIndex = 137;
            this.lblN_Vh.Text = "Вхідний №";
            // 
            // dtpData_Kad
            // 
            this.dtpData_Kad.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Kad.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Kad.Location = new System.Drawing.Point(463, 272);
            this.dtpData_Kad.Name = "dtpData_Kad";
            this.dtpData_Kad.Size = new System.Drawing.Size(141, 20);
            this.dtpData_Kad.TabIndex = 11;
            this.dtpData_Kad.Validating += new System.ComponentModel.CancelEventHandler(this.dtpData_Kad_Validating);
            // 
            // txtN_Vh
            // 
            this.txtN_Vh.Location = new System.Drawing.Point(195, 32);
            this.txtN_Vh.Name = "txtN_Vh";
            this.txtN_Vh.Size = new System.Drawing.Size(99, 20);
            this.txtN_Vh.TabIndex = 0;
            this.txtN_Vh.TextChanged += new System.EventHandler(this.txtN_Vh_TextChanged);
            this.txtN_Vh.Validating += new System.ComponentModel.CancelEventHandler(this.txtN_Vh_Validating);
            // 
            // lblFIO_Kad
            // 
            this.lblFIO_Kad.Location = new System.Drawing.Point(16, 365);
            this.lblFIO_Kad.Name = "lblFIO_Kad";
            this.lblFIO_Kad.Size = new System.Drawing.Size(171, 20);
            this.lblFIO_Kad.TabIndex = 151;
            this.lblFIO_Kad.Text = "ПІБ що розмістила";
            // 
            // lblPrim
            // 
            this.lblPrim.Location = new System.Drawing.Point(16, 318);
            this.lblPrim.Name = "lblPrim";
            this.lblPrim.Size = new System.Drawing.Size(171, 20);
            this.lblPrim.TabIndex = 149;
            this.lblPrim.Text = "Примітки";
            // 
            // lblN_Kad
            // 
            this.lblN_Kad.Location = new System.Drawing.Point(16, 272);
            this.lblN_Kad.Name = "lblN_Kad";
            this.lblN_Kad.Size = new System.Drawing.Size(171, 20);
            this.lblN_Kad.TabIndex = 148;
            this.lblN_Kad.Text = "Реєстраційний № в кадастрі";
            // 
            // lblData_Kad
            // 
            this.lblData_Kad.Location = new System.Drawing.Point(303, 272);
            this.lblData_Kad.Name = "lblData_Kad";
            this.lblData_Kad.Size = new System.Drawing.Size(155, 20);
            this.lblData_Kad.TabIndex = 147;
            this.lblData_Kad.Text = "Дата розміщення в кадастрі";
            // 
            // lblData_Vh
            // 
            this.lblData_Vh.Location = new System.Drawing.Point(302, 32);
            this.lblData_Vh.Name = "lblData_Vh";
            this.lblData_Vh.Size = new System.Drawing.Size(155, 20);
            this.lblData_Vh.TabIndex = 139;
            this.lblData_Vh.Text = "Дата внесення документа";
            // 
            // txtN_Sop_List
            // 
            this.txtN_Sop_List.Location = new System.Drawing.Point(195, 84);
            this.txtN_Sop_List.Name = "txtN_Sop_List";
            this.txtN_Sop_List.Size = new System.Drawing.Size(99, 20);
            this.txtN_Sop_List.TabIndex = 3;
            this.txtN_Sop_List.Validating += new System.ComponentModel.CancelEventHandler(this.txtN_Sop_List_Validating);
            // 
            // lblData_Otp
            // 
            this.lblData_Otp.Location = new System.Drawing.Point(302, 84);
            this.lblData_Otp.Name = "lblData_Otp";
            this.lblData_Otp.Size = new System.Drawing.Size(155, 20);
            this.lblData_Otp.TabIndex = 145;
            this.lblData_Otp.Text = "Дата відправлення";
            // 
            // lblIst_Ved
            // 
            this.lblIst_Ved.Location = new System.Drawing.Point(16, 58);
            this.lblIst_Ved.Name = "lblIst_Ved";
            this.lblIst_Ved.Size = new System.Drawing.Size(171, 20);
            this.lblIst_Ved.TabIndex = 140;
            this.lblIst_Ved.Text = "Джерело відомостей";
            // 
            // lblEl_Format_GD
            // 
            this.lblEl_Format_GD.Location = new System.Drawing.Point(16, 216);
            this.lblEl_Format_GD.Name = "lblEl_Format_GD";
            this.lblEl_Format_GD.Size = new System.Drawing.Size(171, 20);
            this.lblEl_Format_GD.TabIndex = 142;
            this.lblEl_Format_GD.Text = "Електронна форма подання";
            // 
            // txtIst_Ved
            // 
            this.txtIst_Ved.Location = new System.Drawing.Point(195, 58);
            this.txtIst_Ved.Name = "txtIst_Ved";
            this.txtIst_Ved.Size = new System.Drawing.Size(409, 20);
            this.txtIst_Ved.TabIndex = 2;
            this.txtIst_Ved.Validating += new System.ComponentModel.CancelEventHandler(this.txtIst_Ved_Validating);
            // 
            // lblN_Sop_List
            // 
            this.lblN_Sop_List.Location = new System.Drawing.Point(16, 84);
            this.lblN_Sop_List.Name = "lblN_Sop_List";
            this.lblN_Sop_List.Size = new System.Drawing.Size(171, 20);
            this.lblN_Sop_List.TabIndex = 141;
            this.lblN_Sop_List.Text = "Вихідий № супровідного листа";
            // 
            // lblKol_Str_GD
            // 
            this.lblKol_Str_GD.Location = new System.Drawing.Point(16, 190);
            this.lblKol_Str_GD.Name = "lblKol_Str_GD";
            this.lblKol_Str_GD.Size = new System.Drawing.Size(171, 20);
            this.lblKol_Str_GD.TabIndex = 150;
            this.lblKol_Str_GD.Text = "Кількість аркушів";
            // 
            // dtpData_Vh
            // 
            this.dtpData_Vh.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Vh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Vh.Location = new System.Drawing.Point(463, 32);
            this.dtpData_Vh.Name = "dtpData_Vh";
            this.dtpData_Vh.Size = new System.Drawing.Size(141, 20);
            this.dtpData_Vh.TabIndex = 1;
            this.dtpData_Vh.Validating += new System.ComponentModel.CancelEventHandler(this.dtpData_Vh_Validating);
            // 
            // lblVhidni
            // 
            this.lblVhidni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVhidni.Location = new System.Drawing.Point(16, 9);
            this.lblVhidni.Name = "lblVhidni";
            this.lblVhidni.Size = new System.Drawing.Size(188, 20);
            this.lblVhidni.TabIndex = 144;
            this.lblVhidni.Text = "Вхідні дані";
            // 
            // lblDocMD
            // 
            this.lblDocMD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDocMD.Location = new System.Drawing.Point(16, 116);
            this.lblDocMD.Name = "lblDocMD";
            this.lblDocMD.Size = new System.Drawing.Size(188, 20);
            this.lblDocMD.TabIndex = 146;
            this.lblDocMD.Text = "Документи МД";
            // 
            // btnFIO_Kad
            // 
            this.btnFIO_Kad.Location = new System.Drawing.Point(582, 364);
            this.btnFIO_Kad.Name = "btnFIO_Kad";
            this.btnFIO_Kad.Size = new System.Drawing.Size(22, 23);
            this.btnFIO_Kad.TabIndex = 166;
            this.btnFIO_Kad.Text = "...";
            this.btnFIO_Kad.UseVisualStyleBackColor = true;
            this.btnFIO_Kad.Click += new System.EventHandler(this.btnFIO_Kad_Click);
            // 
            // txtFIO_Kad
            // 
            this.txtFIO_Kad.Location = new System.Drawing.Point(195, 365);
            this.txtFIO_Kad.Name = "txtFIO_Kad";
            this.txtFIO_Kad.Size = new System.Drawing.Size(380, 20);
            this.txtFIO_Kad.TabIndex = 13;
            this.txtFIO_Kad.Validating += new System.ComponentModel.CancelEventHandler(this.txtFIO_Kad_Validating);
            // 
            // frmReestrVedomostey_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 451);
            this.Controls.Add(this.btnFIO_Kad);
            this.Controls.Add(this.txtFIO_Kad);
            this.Controls.Add(this.txtName_GD);
            this.Controls.Add(this.txtN_Doc_GD);
            this.Controls.Add(this.txtN_GD);
            this.Controls.Add(this.lblN_GD);
            this.Controls.Add(this.lblN_Doc_GD);
            this.Controls.Add(this.lblName_GD);
            this.Controls.Add(this.lblRozmischennyaKadastr);
            this.Controls.Add(this.txtKol_Str_GD);
            this.Controls.Add(this.txtPrim);
            this.Controls.Add(this.txtN_Kad);
            this.Controls.Add(this.txtEl_Format_GD);
            this.Controls.Add(this.dtpData_Otp);
            this.Controls.Add(this.lblN_Vh);
            this.Controls.Add(this.dtpData_Kad);
            this.Controls.Add(this.txtN_Vh);
            this.Controls.Add(this.lblFIO_Kad);
            this.Controls.Add(this.lblPrim);
            this.Controls.Add(this.lblN_Kad);
            this.Controls.Add(this.lblData_Kad);
            this.Controls.Add(this.lblData_Vh);
            this.Controls.Add(this.txtN_Sop_List);
            this.Controls.Add(this.lblData_Otp);
            this.Controls.Add(this.lblIst_Ved);
            this.Controls.Add(this.lblEl_Format_GD);
            this.Controls.Add(this.txtIst_Ved);
            this.Controls.Add(this.lblN_Sop_List);
            this.Controls.Add(this.lblKol_Str_GD);
            this.Controls.Add(this.dtpData_Vh);
            this.Controls.Add(this.lblVhidni);
            this.Controls.Add(this.lblDocMD);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReestrVedomostey_doc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmReestrVedomostey_doc";
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblDocMD, 0);
            this.Controls.SetChildIndex(this.lblVhidni, 0);
            this.Controls.SetChildIndex(this.dtpData_Vh, 0);
            this.Controls.SetChildIndex(this.lblKol_Str_GD, 0);
            this.Controls.SetChildIndex(this.lblN_Sop_List, 0);
            this.Controls.SetChildIndex(this.txtIst_Ved, 0);
            this.Controls.SetChildIndex(this.lblEl_Format_GD, 0);
            this.Controls.SetChildIndex(this.lblIst_Ved, 0);
            this.Controls.SetChildIndex(this.lblData_Otp, 0);
            this.Controls.SetChildIndex(this.txtN_Sop_List, 0);
            this.Controls.SetChildIndex(this.lblData_Vh, 0);
            this.Controls.SetChildIndex(this.lblData_Kad, 0);
            this.Controls.SetChildIndex(this.lblN_Kad, 0);
            this.Controls.SetChildIndex(this.lblPrim, 0);
            this.Controls.SetChildIndex(this.lblFIO_Kad, 0);
            this.Controls.SetChildIndex(this.txtN_Vh, 0);
            this.Controls.SetChildIndex(this.dtpData_Kad, 0);
            this.Controls.SetChildIndex(this.lblN_Vh, 0);
            this.Controls.SetChildIndex(this.dtpData_Otp, 0);
            this.Controls.SetChildIndex(this.txtEl_Format_GD, 0);
            this.Controls.SetChildIndex(this.txtN_Kad, 0);
            this.Controls.SetChildIndex(this.txtPrim, 0);
            this.Controls.SetChildIndex(this.txtKol_Str_GD, 0);
            this.Controls.SetChildIndex(this.lblRozmischennyaKadastr, 0);
            this.Controls.SetChildIndex(this.lblName_GD, 0);
            this.Controls.SetChildIndex(this.lblN_Doc_GD, 0);
            this.Controls.SetChildIndex(this.lblN_GD, 0);
            this.Controls.SetChildIndex(this.txtN_GD, 0);
            this.Controls.SetChildIndex(this.txtN_Doc_GD, 0);
            this.Controls.SetChildIndex(this.txtName_GD, 0);
            this.Controls.SetChildIndex(this.txtFIO_Kad, 0);
            this.Controls.SetChildIndex(this.btnFIO_Kad, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtName_GD;
        private TextBox txtN_Doc_GD;
        private TextBox txtN_GD;
        private Label lblN_GD;
        private Label lblN_Doc_GD;
        private Label lblName_GD;
        private Label lblRozmischennyaKadastr;
        private TextBox txtKol_Str_GD;
        private TextBox txtPrim;
        private TextBox txtN_Kad;
        private TextBox txtEl_Format_GD;
        private DateTimePicker dtpData_Otp;
        private Label lblN_Vh;
        private DateTimePicker dtpData_Kad;
        private TextBox txtN_Vh;
        private Label lblFIO_Kad;
        private Label lblPrim;
        private Label lblN_Kad;
        private Label lblData_Kad;
        private Label lblData_Vh;
        private TextBox txtN_Sop_List;
        private Label lblData_Otp;
        private Label lblIst_Ved;
        private Label lblEl_Format_GD;
        private TextBox txtIst_Ved;
        private Label lblN_Sop_List;
        private Label lblKol_Str_GD;
        private DateTimePicker dtpData_Vh;
        private Label lblVhidni;
        private Label lblDocMD;
        private Button btnFIO_Kad;
        private TextBox txtFIO_Kad;

    }
}