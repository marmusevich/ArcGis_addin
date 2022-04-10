using System.Windows.Forms;
namespace WorckWithReestr
{
    partial class frmReestrZayav_doc
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
            this.txtSodergan = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.lblCane = new System.Windows.Forms.Label();
            this.lblFio_Z = new System.Windows.Forms.Label();
            this.lblFirstData = new System.Windows.Forms.Label();
            this.dtpData_Z = new System.Windows.Forms.DateTimePicker();
            this.lblSodergan = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTip_Doc = new System.Windows.Forms.Label();
            this.lblData_Z = new System.Windows.Forms.Label();
            this.lblKod_Z = new System.Windows.Forms.Label();
            this.txtN_Z = new System.Windows.Forms.TextBox();
            this.lblN_Z = new System.Windows.Forms.Label();
            this.txtKod_Z = new System.Windows.Forms.TextBox();
            this.btnKod_Z = new System.Windows.Forms.Button();
            this.txtTip_Doc = new System.Windows.Forms.TextBox();
            this.btnTip_Doc = new System.Windows.Forms.Button();
            this.txtCane = new System.Windows.Forms.TextBox();
            this.btnFio_Z = new System.Windows.Forms.Button();
            this.txtFio_Z = new System.Windows.Forms.TextBox();
            this.lblCane_Date = new System.Windows.Forms.Label();
            this.dtpCane_Date = new System.Windows.Forms.DateTimePicker();
            this.txtAdress_Text = new System.Windows.Forms.TextBox();
            this.lblAdress_Text = new System.Windows.Forms.Label();
            this.llblHaveReferense = new System.Windows.Forms.LinkLabel();
            this.cbQrKod = new System.Windows.Forms.CheckBox();
            this.lblPrim = new System.Windows.Forms.Label();
            this.txtPrim = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(468, 401);
            this.btnOk.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(11, 401);
            this.btnCancel.TabIndex = 25;
            // 
            // txtSodergan
            // 
            this.txtSodergan.AcceptsReturn = true;
            this.txtSodergan.Location = new System.Drawing.Point(143, 187);
            this.txtSodergan.Multiline = true;
            this.txtSodergan.Name = "txtSodergan";
            this.txtSodergan.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSodergan.Size = new System.Drawing.Size(400, 50);
            this.txtSodergan.TabIndex = 8;
            this.txtSodergan.Validating += new System.ComponentModel.CancelEventHandler(this.txtSodergan_Validating);
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(143, 103);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(151, 21);
            this.cbStatus.TabIndex = 2;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // lblCane
            // 
            this.lblCane.Location = new System.Drawing.Point(8, 53);
            this.lblCane.Name = "lblCane";
            this.lblCane.Size = new System.Drawing.Size(125, 20);
            this.lblCane.TabIndex = 18;
            this.lblCane.Text = "№ канцелярський";
            // 
            // lblFio_Z
            // 
            this.lblFio_Z.Location = new System.Drawing.Point(8, 247);
            this.lblFio_Z.Name = "lblFio_Z";
            this.lblFio_Z.Size = new System.Drawing.Size(391, 20);
            this.lblFio_Z.TabIndex = 15;
            this.lblFio_Z.Text = "ПІБ особи, що прийняла заяву / звернення";
            // 
            // lblFirstData
            // 
            this.lblFirstData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFirstData.Location = new System.Drawing.Point(12, 9);
            this.lblFirstData.Name = "lblFirstData";
            this.lblFirstData.Size = new System.Drawing.Size(188, 22);
            this.lblFirstData.TabIndex = 13;
            this.lblFirstData.Text = "Первинні вхідні данні";
            // 
            // dtpData_Z
            // 
            this.dtpData_Z.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Z.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Z.Location = new System.Drawing.Point(392, 30);
            this.dtpData_Z.Name = "dtpData_Z";
            this.dtpData_Z.Size = new System.Drawing.Size(151, 20);
            this.dtpData_Z.TabIndex = 1;
            this.dtpData_Z.Validating += new System.ComponentModel.CancelEventHandler(this.dtpData_Z_Validating);
            // 
            // lblSodergan
            // 
            this.lblSodergan.Location = new System.Drawing.Point(8, 202);
            this.lblSodergan.Name = "lblSodergan";
            this.lblSodergan.Size = new System.Drawing.Size(125, 20);
            this.lblSodergan.TabIndex = 10;
            this.lblSodergan.Text = "Стислий зміст";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(8, 103);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(125, 21);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Статус заявника";
            // 
            // lblTip_Doc
            // 
            this.lblTip_Doc.Location = new System.Drawing.Point(8, 159);
            this.lblTip_Doc.Name = "lblTip_Doc";
            this.lblTip_Doc.Size = new System.Drawing.Size(125, 21);
            this.lblTip_Doc.TabIndex = 6;
            this.lblTip_Doc.Text = "Тип документа";
            // 
            // lblData_Z
            // 
            this.lblData_Z.Location = new System.Drawing.Point(279, 30);
            this.lblData_Z.Name = "lblData_Z";
            this.lblData_Z.Size = new System.Drawing.Size(101, 20);
            this.lblData_Z.TabIndex = 3;
            this.lblData_Z.Text = "Дата звернення";
            // 
            // lblKod_Z
            // 
            this.lblKod_Z.Location = new System.Drawing.Point(8, 127);
            this.lblKod_Z.Name = "lblKod_Z";
            this.lblKod_Z.Size = new System.Drawing.Size(125, 21);
            this.lblKod_Z.TabIndex = 2;
            this.lblKod_Z.Text = "Замовник";
            // 
            // txtN_Z
            // 
            this.txtN_Z.Location = new System.Drawing.Point(143, 30);
            this.txtN_Z.Name = "txtN_Z";
            this.txtN_Z.Size = new System.Drawing.Size(130, 20);
            this.txtN_Z.TabIndex = 0;
            this.txtN_Z.TextChanged += new System.EventHandler(this.txtN_Z_TextChanged);
            this.txtN_Z.Validating += new System.ComponentModel.CancelEventHandler(this.txtN_Z_Validating);
            // 
            // lblN_Z
            // 
            this.lblN_Z.Location = new System.Drawing.Point(8, 30);
            this.lblN_Z.Name = "lblN_Z";
            this.lblN_Z.Size = new System.Drawing.Size(125, 20);
            this.lblN_Z.TabIndex = 0;
            this.lblN_Z.Text = "№ звернення / заяви";
            // 
            // txtKod_Z
            // 
            this.txtKod_Z.Location = new System.Drawing.Point(143, 127);
            this.txtKod_Z.Name = "txtKod_Z";
            this.txtKod_Z.Size = new System.Drawing.Size(372, 20);
            this.txtKod_Z.TabIndex = 3;
            this.txtKod_Z.Validating += new System.ComponentModel.CancelEventHandler(this.txtKod_Z_Validating);
            // 
            // btnKod_Z
            // 
            this.btnKod_Z.Location = new System.Drawing.Point(521, 126);
            this.btnKod_Z.Name = "btnKod_Z";
            this.btnKod_Z.Size = new System.Drawing.Size(22, 23);
            this.btnKod_Z.TabIndex = 34;
            this.btnKod_Z.Text = "...";
            this.btnKod_Z.UseVisualStyleBackColor = true;
            this.btnKod_Z.Click += new System.EventHandler(this.btnKod_Z_Click);
            // 
            // txtTip_Doc
            // 
            this.txtTip_Doc.Location = new System.Drawing.Point(143, 159);
            this.txtTip_Doc.Name = "txtTip_Doc";
            this.txtTip_Doc.Size = new System.Drawing.Size(372, 20);
            this.txtTip_Doc.TabIndex = 5;
            this.txtTip_Doc.Validating += new System.ComponentModel.CancelEventHandler(this.txtTip_Doc_Validating);
            // 
            // btnTip_Doc
            // 
            this.btnTip_Doc.Location = new System.Drawing.Point(521, 158);
            this.btnTip_Doc.Name = "btnTip_Doc";
            this.btnTip_Doc.Size = new System.Drawing.Size(22, 23);
            this.btnTip_Doc.TabIndex = 36;
            this.btnTip_Doc.Text = "...";
            this.btnTip_Doc.UseVisualStyleBackColor = true;
            this.btnTip_Doc.Click += new System.EventHandler(this.btnTip_Doc_Click);
            // 
            // txtCane
            // 
            this.txtCane.Location = new System.Drawing.Point(143, 53);
            this.txtCane.Name = "txtCane";
            this.txtCane.Size = new System.Drawing.Size(130, 20);
            this.txtCane.TabIndex = 12;
            this.txtCane.TextChanged += new System.EventHandler(this.txtCane_TextChanged);
            this.txtCane.Validating += new System.ComponentModel.CancelEventHandler(this.txtCane_Validating);
            // 
            // btnFio_Z
            // 
            this.btnFio_Z.Location = new System.Drawing.Point(521, 265);
            this.btnFio_Z.Name = "btnFio_Z";
            this.btnFio_Z.Size = new System.Drawing.Size(22, 23);
            this.btnFio_Z.TabIndex = 45;
            this.btnFio_Z.Text = "...";
            this.btnFio_Z.UseVisualStyleBackColor = true;
            this.btnFio_Z.Click += new System.EventHandler(this.btnFio_Z_Click);
            // 
            // txtFio_Z
            // 
            this.txtFio_Z.Location = new System.Drawing.Point(143, 266);
            this.txtFio_Z.Name = "txtFio_Z";
            this.txtFio_Z.Size = new System.Drawing.Size(372, 20);
            this.txtFio_Z.TabIndex = 11;
            this.txtFio_Z.Validating += new System.ComponentModel.CancelEventHandler(this.txtFio_Z_Validating);
            // 
            // lblCane_Date
            // 
            this.lblCane_Date.Location = new System.Drawing.Point(279, 53);
            this.lblCane_Date.Name = "lblCane_Date";
            this.lblCane_Date.Size = new System.Drawing.Size(101, 20);
            this.lblCane_Date.TabIndex = 50;
            this.lblCane_Date.Text = "Дата входящая";
            // 
            // dtpCane_Date
            // 
            this.dtpCane_Date.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpCane_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCane_Date.Location = new System.Drawing.Point(392, 53);
            this.dtpCane_Date.Name = "dtpCane_Date";
            this.dtpCane_Date.Size = new System.Drawing.Size(151, 20);
            this.dtpCane_Date.TabIndex = 49;
            this.dtpCane_Date.Validating += new System.ComponentModel.CancelEventHandler(this.dtpCane_Date_Validating);
            // 
            // txtAdress_Text
            // 
            this.txtAdress_Text.Location = new System.Drawing.Point(143, 78);
            this.txtAdress_Text.Name = "txtAdress_Text";
            this.txtAdress_Text.Size = new System.Drawing.Size(400, 20);
            this.txtAdress_Text.TabIndex = 51;
            this.txtAdress_Text.Validating += new System.ComponentModel.CancelEventHandler(this.txtAdress_Text_Validating);
            // 
            // lblAdress_Text
            // 
            this.lblAdress_Text.Location = new System.Drawing.Point(8, 78);
            this.lblAdress_Text.Name = "lblAdress_Text";
            this.lblAdress_Text.Size = new System.Drawing.Size(125, 20);
            this.lblAdress_Text.TabIndex = 52;
            this.lblAdress_Text.Text = "Описательный адресс";
            // 
            // llblHaveReferense
            // 
            this.llblHaveReferense.BackColor = System.Drawing.SystemColors.Control;
            this.llblHaveReferense.Location = new System.Drawing.Point(13, 291);
            this.llblHaveReferense.Name = "llblHaveReferense";
            this.llblHaveReferense.Size = new System.Drawing.Size(535, 31);
            this.llblHaveReferense.TabIndex = 56;
            this.llblHaveReferense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llblHaveReferense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblHaveReferense_LinkClicked);
            // 
            // cbQrKod
            // 
            this.cbQrKod.AutoSize = true;
            this.cbQrKod.Location = new System.Drawing.Point(11, 326);
            this.cbQrKod.Name = "cbQrKod";
            this.cbQrKod.Size = new System.Drawing.Size(101, 19);
            this.cbQrKod.TabIndex = 57;
            this.cbQrKod.Text = "Есть QR код";
            this.cbQrKod.UseVisualStyleBackColor = true;
            this.cbQrKod.CheckedChanged += new System.EventHandler(this.cbQrKod_CheckedChanged);
            // 
            // lblPrim
            // 
            this.lblPrim.AutoSize = true;
            this.lblPrim.Location = new System.Drawing.Point(11, 352);
            this.lblPrim.Name = "lblPrim";
            this.lblPrim.Size = new System.Drawing.Size(74, 15);
            this.lblPrim.TabIndex = 58;
            this.lblPrim.Text = "Имя файла";
            // 
            // txtPrim
            // 
            this.txtPrim.Location = new System.Drawing.Point(143, 352);
            this.txtPrim.Name = "txtPrim";
            this.txtPrim.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPrim.Size = new System.Drawing.Size(400, 20);
            this.txtPrim.TabIndex = 59;
            this.txtPrim.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrim_Validating);
            // 
            // frmReestrZayav_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 436);
            this.Controls.Add(this.txtPrim);
            this.Controls.Add(this.lblPrim);
            this.Controls.Add(this.cbQrKod);
            this.Controls.Add(this.llblHaveReferense);
            this.Controls.Add(this.txtAdress_Text);
            this.Controls.Add(this.lblAdress_Text);
            this.Controls.Add(this.lblCane_Date);
            this.Controls.Add(this.dtpCane_Date);
            this.Controls.Add(this.btnFio_Z);
            this.Controls.Add(this.txtFio_Z);
            this.Controls.Add(this.btnTip_Doc);
            this.Controls.Add(this.txtTip_Doc);
            this.Controls.Add(this.btnKod_Z);
            this.Controls.Add(this.txtKod_Z);
            this.Controls.Add(this.txtCane);
            this.Controls.Add(this.lblN_Z);
            this.Controls.Add(this.txtN_Z);
            this.Controls.Add(this.txtSodergan);
            this.Controls.Add(this.lblKod_Z);
            this.Controls.Add(this.lblData_Z);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.lblTip_Doc);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCane);
            this.Controls.Add(this.lblSodergan);
            this.Controls.Add(this.dtpData_Z);
            this.Controls.Add(this.lblFio_Z);
            this.Controls.Add(this.lblFirstData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReestrZayav_doc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Заява / звернення";
            this.Load += new System.EventHandler(this.frmReestrZayav_doc_Load);
            this.Controls.SetChildIndex(this.lblFirstData, 0);
            this.Controls.SetChildIndex(this.lblFio_Z, 0);
            this.Controls.SetChildIndex(this.dtpData_Z, 0);
            this.Controls.SetChildIndex(this.lblSodergan, 0);
            this.Controls.SetChildIndex(this.lblCane, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.lblTip_Doc, 0);
            this.Controls.SetChildIndex(this.cbStatus, 0);
            this.Controls.SetChildIndex(this.lblData_Z, 0);
            this.Controls.SetChildIndex(this.lblKod_Z, 0);
            this.Controls.SetChildIndex(this.txtSodergan, 0);
            this.Controls.SetChildIndex(this.txtN_Z, 0);
            this.Controls.SetChildIndex(this.lblN_Z, 0);
            this.Controls.SetChildIndex(this.txtCane, 0);
            this.Controls.SetChildIndex(this.txtKod_Z, 0);
            this.Controls.SetChildIndex(this.btnKod_Z, 0);
            this.Controls.SetChildIndex(this.txtTip_Doc, 0);
            this.Controls.SetChildIndex(this.btnTip_Doc, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtFio_Z, 0);
            this.Controls.SetChildIndex(this.btnFio_Z, 0);
            this.Controls.SetChildIndex(this.dtpCane_Date, 0);
            this.Controls.SetChildIndex(this.lblCane_Date, 0);
            this.Controls.SetChildIndex(this.lblAdress_Text, 0);
            this.Controls.SetChildIndex(this.txtAdress_Text, 0);
            this.Controls.SetChildIndex(this.llblHaveReferense, 0);
            this.Controls.SetChildIndex(this.cbQrKod, 0);
            this.Controls.SetChildIndex(this.lblPrim, 0);
            this.Controls.SetChildIndex(this.txtPrim, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtSodergan;
        private ComboBox cbStatus;
        private Label lblCane;
        private Label lblFio_Z;
        private Label lblFirstData;
        private DateTimePicker dtpData_Z;
        private Label lblSodergan;
        private Label lblStatus;
        private Label lblTip_Doc;
        private Label lblData_Z;
        private Label lblKod_Z;
        private TextBox txtN_Z;
        private Label lblN_Z;
        private TextBox txtKod_Z;
        private Button btnKod_Z;
        private TextBox txtTip_Doc;
        private Button btnTip_Doc;
        private TextBox txtCane;
        private Button btnFio_Z;
        private TextBox txtFio_Z;
        private Label lblCane_Date;
        private DateTimePicker dtpCane_Date;
        private TextBox txtAdress_Text;
        private Label lblAdress_Text;
        private LinkLabel llblHaveReferense;
        private CheckBox cbQrKod;
        private Label lblPrim;
        private TextBox txtPrim;
    }
}