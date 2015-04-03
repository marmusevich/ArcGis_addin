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
            this.txtTel_Z = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.dtpData_Ish = new System.Windows.Forms.DateTimePicker();
            this.txtN_Ish_Z = new System.Windows.Forms.TextBox();
            this.lblCane = new System.Windows.Forms.Label();
            this.lblPr_Otkaz = new System.Windows.Forms.Label();
            this.lblOtkaz = new System.Windows.Forms.Label();
            this.lblFio_Z = new System.Windows.Forms.Label();
            this.lblDoppData = new System.Windows.Forms.Label();
            this.lblFirstData = new System.Windows.Forms.Label();
            this.dtpData_Z = new System.Windows.Forms.DateTimePicker();
            this.lblSodergan = new System.Windows.Forms.Label();
            this.lblTel_Z = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTip_Doc = new System.Windows.Forms.Label();
            this.lblData_Ish = new System.Windows.Forms.Label();
            this.lblN_Ish_Z = new System.Windows.Forms.Label();
            this.lblData_Z = new System.Windows.Forms.Label();
            this.lblKod_Z = new System.Windows.Forms.Label();
            this.txtN_Z = new System.Windows.Forms.TextBox();
            this.lblN_Z = new System.Windows.Forms.Label();
            this.txtForma_Ved = new System.Windows.Forms.TextBox();
            this.dtpData_Ved = new System.Windows.Forms.DateTimePicker();
            this.dtpData_Oplata = new System.Windows.Forms.DateTimePicker();
            this.lblFio_Ved_Prin = new System.Windows.Forms.Label();
            this.lblFio_Ved_Vid = new System.Windows.Forms.Label();
            this.lblForma_Ved = new System.Windows.Forms.Label();
            this.lblOpisan_Ved = new System.Windows.Forms.Label();
            this.lblData_Oplata = new System.Windows.Forms.Label();
            this.lblDoc_Oplata = new System.Windows.Forms.Label();
            this.lblData_Ved = new System.Windows.Forms.Label();
            this.lblOplata = new System.Windows.Forms.Label();
            this.lblTip_Doc_code = new System.Windows.Forms.Label();
            this.lblServicesProvided = new System.Windows.Forms.Label();
            this.txtKod_Z = new System.Windows.Forms.TextBox();
            this.btnKod_Z = new System.Windows.Forms.Button();
            this.txtTip_Doc = new System.Windows.Forms.TextBox();
            this.btnTip_Doc = new System.Windows.Forms.Button();
            this.txtPrim = new System.Windows.Forms.TextBox();
            this.lblPrim = new System.Windows.Forms.Label();
            this.btnFio_Ved_Vid = new System.Windows.Forms.Button();
            this.txtFio_Ved_Vid = new System.Windows.Forms.TextBox();
            this.btnFio_Ved_Prin = new System.Windows.Forms.Button();
            this.txtFio_Ved_Prin = new System.Windows.Forms.TextBox();
            this.txtOpisan_Ved = new System.Windows.Forms.TextBox();
            this.txtDoc_Oplata = new System.Windows.Forms.TextBox();
            this.cbOplata = new System.Windows.Forms.ComboBox();
            this.txtTip_Doc_code = new System.Windows.Forms.TextBox();
            this.cbOtkaz = new System.Windows.Forms.ComboBox();
            this.txtCane = new System.Windows.Forms.TextBox();
            this.txtPr_Otkaz = new System.Windows.Forms.TextBox();
            this.btnFio_Z = new System.Windows.Forms.Button();
            this.txtFio_Z = new System.Windows.Forms.TextBox();
            this.txtKod_Z_code = new System.Windows.Forms.TextBox();
            this.lblKod_Z_code = new System.Windows.Forms.Label();
            this.txtDodatok = new System.Windows.Forms.TextBox();
            this.lblDodatok = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(1030, 443);
            this.btnOk.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(15, 443);
            this.btnCancel.TabIndex = 25;
            // 
            // txtSodergan
            // 
            this.txtSodergan.Location = new System.Drawing.Point(143, 215);
            this.txtSodergan.Multiline = true;
            this.txtSodergan.Name = "txtSodergan";
            this.txtSodergan.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSodergan.Size = new System.Drawing.Size(400, 50);
            this.txtSodergan.TabIndex = 8;
            // 
            // txtTel_Z
            // 
            this.txtTel_Z.Location = new System.Drawing.Point(143, 123);
            this.txtTel_Z.Name = "txtTel_Z";
            this.txtTel_Z.Size = new System.Drawing.Size(400, 20);
            this.txtTel_Z.TabIndex = 4;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(143, 53);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(151, 21);
            this.cbStatus.TabIndex = 2;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // dtpData_Ish
            // 
            this.dtpData_Ish.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Ish.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Ish.Location = new System.Drawing.Point(392, 192);
            this.dtpData_Ish.Name = "dtpData_Ish";
            this.dtpData_Ish.Size = new System.Drawing.Size(151, 20);
            this.dtpData_Ish.TabIndex = 7;
            // 
            // txtN_Ish_Z
            // 
            this.txtN_Ish_Z.Location = new System.Drawing.Point(143, 192);
            this.txtN_Ish_Z.Name = "txtN_Ish_Z";
            this.txtN_Ish_Z.Size = new System.Drawing.Size(130, 20);
            this.txtN_Ish_Z.TabIndex = 6;
            // 
            // lblCane
            // 
            this.lblCane.Location = new System.Drawing.Point(564, 30);
            this.lblCane.Name = "lblCane";
            this.lblCane.Size = new System.Drawing.Size(125, 20);
            this.lblCane.TabIndex = 18;
            this.lblCane.Text = "№ канцелярський";
            // 
            // lblPr_Otkaz
            // 
            this.lblPr_Otkaz.Location = new System.Drawing.Point(564, 192);
            this.lblPr_Otkaz.Name = "lblPr_Otkaz";
            this.lblPr_Otkaz.Size = new System.Drawing.Size(103, 20);
            this.lblPr_Otkaz.TabIndex = 17;
            this.lblPr_Otkaz.Text = "Причина відмови";
            // 
            // lblOtkaz
            // 
            this.lblOtkaz.Location = new System.Drawing.Point(564, 169);
            this.lblOtkaz.Name = "lblOtkaz";
            this.lblOtkaz.Size = new System.Drawing.Size(125, 20);
            this.lblOtkaz.TabIndex = 16;
            this.lblOtkaz.Text = "Відмітка про відмову";
            // 
            // lblFio_Z
            // 
            this.lblFio_Z.Location = new System.Drawing.Point(8, 375);
            this.lblFio_Z.Name = "lblFio_Z";
            this.lblFio_Z.Size = new System.Drawing.Size(391, 20);
            this.lblFio_Z.TabIndex = 15;
            this.lblFio_Z.Text = "ПІБ особи, що прийняла заяву / звернення";
            // 
            // lblDoppData
            // 
            this.lblDoppData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDoppData.Location = new System.Drawing.Point(564, 9);
            this.lblDoppData.Name = "lblDoppData";
            this.lblDoppData.Size = new System.Drawing.Size(188, 19);
            this.lblDoppData.TabIndex = 14;
            this.lblDoppData.Text = "Додаткові вхідні данні";
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
            // 
            // lblSodergan
            // 
            this.lblSodergan.Location = new System.Drawing.Point(8, 230);
            this.lblSodergan.Name = "lblSodergan";
            this.lblSodergan.Size = new System.Drawing.Size(125, 20);
            this.lblSodergan.TabIndex = 10;
            this.lblSodergan.Text = "Стислий зміст";
            // 
            // lblTel_Z
            // 
            this.lblTel_Z.Location = new System.Drawing.Point(8, 123);
            this.lblTel_Z.Name = "lblTel_Z";
            this.lblTel_Z.Size = new System.Drawing.Size(125, 20);
            this.lblTel_Z.TabIndex = 9;
            this.lblTel_Z.Text = "Тел. / e-mail заявника";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(8, 53);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(125, 21);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Статус заявника";
            // 
            // lblTip_Doc
            // 
            this.lblTip_Doc.Location = new System.Drawing.Point(8, 146);
            this.lblTip_Doc.Name = "lblTip_Doc";
            this.lblTip_Doc.Size = new System.Drawing.Size(125, 21);
            this.lblTip_Doc.TabIndex = 6;
            this.lblTip_Doc.Text = "Тип документа";
            // 
            // lblData_Ish
            // 
            this.lblData_Ish.Location = new System.Drawing.Point(288, 192);
            this.lblData_Ish.Name = "lblData_Ish";
            this.lblData_Ish.Size = new System.Drawing.Size(101, 20);
            this.lblData_Ish.TabIndex = 5;
            this.lblData_Ish.Text = "Дата листа";
            // 
            // lblN_Ish_Z
            // 
            this.lblN_Ish_Z.Location = new System.Drawing.Point(8, 192);
            this.lblN_Ish_Z.Name = "lblN_Ish_Z";
            this.lblN_Ish_Z.Size = new System.Drawing.Size(125, 20);
            this.lblN_Ish_Z.TabIndex = 4;
            this.lblN_Ish_Z.Text = "Вихідний номер листа";
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
            this.lblKod_Z.Location = new System.Drawing.Point(8, 77);
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
            // txtForma_Ved
            // 
            this.txtForma_Ved.Location = new System.Drawing.Point(705, 291);
            this.txtForma_Ved.Multiline = true;
            this.txtForma_Ved.Name = "txtForma_Ved";
            this.txtForma_Ved.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtForma_Ved.Size = new System.Drawing.Size(400, 50);
            this.txtForma_Ved.TabIndex = 20;
            // 
            // dtpData_Ved
            // 
            this.dtpData_Ved.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Ved.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Ved.Location = new System.Drawing.Point(705, 215);
            this.dtpData_Ved.Name = "dtpData_Ved";
            this.dtpData_Ved.Size = new System.Drawing.Size(151, 20);
            this.dtpData_Ved.TabIndex = 18;
            // 
            // dtpData_Oplata
            // 
            this.dtpData_Oplata.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpData_Oplata.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpData_Oplata.Location = new System.Drawing.Point(705, 77);
            this.dtpData_Oplata.Name = "dtpData_Oplata";
            this.dtpData_Oplata.Size = new System.Drawing.Size(151, 20);
            this.dtpData_Oplata.TabIndex = 14;
            // 
            // lblFio_Ved_Prin
            // 
            this.lblFio_Ved_Prin.Location = new System.Drawing.Point(564, 372);
            this.lblFio_Ved_Prin.Name = "lblFio_Ved_Prin";
            this.lblFio_Ved_Prin.Size = new System.Drawing.Size(140, 14);
            this.lblFio_Ved_Prin.TabIndex = 20;
            this.lblFio_Ved_Prin.Text = "ПІБ що отримала послугу";
            // 
            // lblFio_Ved_Vid
            // 
            this.lblFio_Ved_Vid.Location = new System.Drawing.Point(564, 348);
            this.lblFio_Ved_Vid.Name = "lblFio_Ved_Vid";
            this.lblFio_Ved_Vid.Size = new System.Drawing.Size(140, 14);
            this.lblFio_Ved_Vid.TabIndex = 19;
            this.lblFio_Ved_Vid.Text = "ПІБ що виконала послугу";
            // 
            // lblForma_Ved
            // 
            this.lblForma_Ved.Location = new System.Drawing.Point(564, 309);
            this.lblForma_Ved.Name = "lblForma_Ved";
            this.lblForma_Ved.Size = new System.Drawing.Size(125, 14);
            this.lblForma_Ved.TabIndex = 18;
            this.lblForma_Ved.Text = "Форма передачі послуг";
            // 
            // lblOpisan_Ved
            // 
            this.lblOpisan_Ved.Location = new System.Drawing.Point(564, 256);
            this.lblOpisan_Ved.Name = "lblOpisan_Ved";
            this.lblOpisan_Ved.Size = new System.Drawing.Size(125, 14);
            this.lblOpisan_Ved.TabIndex = 17;
            this.lblOpisan_Ved.Text = "Опис наданих послуг";
            // 
            // lblData_Oplata
            // 
            this.lblData_Oplata.Location = new System.Drawing.Point(564, 215);
            this.lblData_Oplata.Name = "lblData_Oplata";
            this.lblData_Oplata.Size = new System.Drawing.Size(126, 20);
            this.lblData_Oplata.TabIndex = 16;
            this.lblData_Oplata.Text = "Дата надання послуги";
            // 
            // lblDoc_Oplata
            // 
            this.lblDoc_Oplata.Location = new System.Drawing.Point(564, 100);
            this.lblDoc_Oplata.Name = "lblDoc_Oplata";
            this.lblDoc_Oplata.Size = new System.Drawing.Size(125, 20);
            this.lblDoc_Oplata.TabIndex = 15;
            this.lblDoc_Oplata.Text = "Документ про сплату";
            // 
            // lblData_Ved
            // 
            this.lblData_Ved.Location = new System.Drawing.Point(564, 77);
            this.lblData_Ved.Name = "lblData_Ved";
            this.lblData_Ved.Size = new System.Drawing.Size(83, 21);
            this.lblData_Ved.TabIndex = 14;
            this.lblData_Ved.Text = "Дата сплати";
            // 
            // lblOplata
            // 
            this.lblOplata.Location = new System.Drawing.Point(564, 53);
            this.lblOplata.Name = "lblOplata";
            this.lblOplata.Size = new System.Drawing.Size(125, 21);
            this.lblOplata.TabIndex = 13;
            this.lblOplata.Text = "Статус надання послуг";
            // 
            // lblTip_Doc_code
            // 
            this.lblTip_Doc_code.Location = new System.Drawing.Point(8, 169);
            this.lblTip_Doc_code.Name = "lblTip_Doc_code";
            this.lblTip_Doc_code.Size = new System.Drawing.Size(125, 20);
            this.lblTip_Doc_code.TabIndex = 12;
            this.lblTip_Doc_code.Text = "Код типа документа";
            // 
            // lblServicesProvided
            // 
            this.lblServicesProvided.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblServicesProvided.Location = new System.Drawing.Point(564, 147);
            this.lblServicesProvided.Name = "lblServicesProvided";
            this.lblServicesProvided.Size = new System.Drawing.Size(188, 19);
            this.lblServicesProvided.TabIndex = 32;
            this.lblServicesProvided.Text = "Наданні послуги";
            // 
            // txtKod_Z
            // 
            this.txtKod_Z.Location = new System.Drawing.Point(143, 77);
            this.txtKod_Z.Name = "txtKod_Z";
            this.txtKod_Z.Size = new System.Drawing.Size(372, 20);
            this.txtKod_Z.TabIndex = 3;
            this.txtKod_Z.Validating += new System.ComponentModel.CancelEventHandler(this.txtKod_Z_Validating);
            // 
            // btnKod_Z
            // 
            this.btnKod_Z.Location = new System.Drawing.Point(521, 76);
            this.btnKod_Z.Name = "btnKod_Z";
            this.btnKod_Z.Size = new System.Drawing.Size(22, 23);
            this.btnKod_Z.TabIndex = 34;
            this.btnKod_Z.Text = "...";
            this.btnKod_Z.UseVisualStyleBackColor = true;
            this.btnKod_Z.Click += new System.EventHandler(this.btnKod_Z_Click);
            // 
            // txtTip_Doc
            // 
            this.txtTip_Doc.Location = new System.Drawing.Point(143, 146);
            this.txtTip_Doc.Name = "txtTip_Doc";
            this.txtTip_Doc.Size = new System.Drawing.Size(372, 20);
            this.txtTip_Doc.TabIndex = 5;
            this.txtTip_Doc.Validating += new System.ComponentModel.CancelEventHandler(this.txtTip_Doc_Validating);
            // 
            // btnTip_Doc
            // 
            this.btnTip_Doc.Location = new System.Drawing.Point(521, 145);
            this.btnTip_Doc.Name = "btnTip_Doc";
            this.btnTip_Doc.Size = new System.Drawing.Size(22, 23);
            this.btnTip_Doc.TabIndex = 36;
            this.btnTip_Doc.Text = "...";
            this.btnTip_Doc.UseVisualStyleBackColor = true;
            this.btnTip_Doc.Click += new System.EventHandler(this.btnTip_Doc_Click);
            // 
            // txtPrim
            // 
            this.txtPrim.Location = new System.Drawing.Point(143, 321);
            this.txtPrim.Multiline = true;
            this.txtPrim.Name = "txtPrim";
            this.txtPrim.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPrim.Size = new System.Drawing.Size(400, 50);
            this.txtPrim.TabIndex = 10;
            // 
            // lblPrim
            // 
            this.lblPrim.Location = new System.Drawing.Point(8, 336);
            this.lblPrim.Name = "lblPrim";
            this.lblPrim.Size = new System.Drawing.Size(125, 20);
            this.lblPrim.TabIndex = 38;
            this.lblPrim.Text = "Примечание";
            // 
            // btnFio_Ved_Vid
            // 
            this.btnFio_Ved_Vid.Location = new System.Drawing.Point(1083, 344);
            this.btnFio_Ved_Vid.Name = "btnFio_Ved_Vid";
            this.btnFio_Ved_Vid.Size = new System.Drawing.Size(22, 23);
            this.btnFio_Ved_Vid.TabIndex = 40;
            this.btnFio_Ved_Vid.Text = "...";
            this.btnFio_Ved_Vid.UseVisualStyleBackColor = true;
            this.btnFio_Ved_Vid.Click += new System.EventHandler(this.btnFio_Ved_Vid_Click);
            // 
            // txtFio_Ved_Vid
            // 
            this.txtFio_Ved_Vid.Location = new System.Drawing.Point(705, 345);
            this.txtFio_Ved_Vid.Name = "txtFio_Ved_Vid";
            this.txtFio_Ved_Vid.Size = new System.Drawing.Size(372, 20);
            this.txtFio_Ved_Vid.TabIndex = 21;
            this.txtFio_Ved_Vid.Validating += new System.ComponentModel.CancelEventHandler(this.txtFio_Ved_Vid_Validating);
            // 
            // btnFio_Ved_Prin
            // 
            this.btnFio_Ved_Prin.Location = new System.Drawing.Point(1083, 368);
            this.btnFio_Ved_Prin.Name = "btnFio_Ved_Prin";
            this.btnFio_Ved_Prin.Size = new System.Drawing.Size(22, 23);
            this.btnFio_Ved_Prin.TabIndex = 42;
            this.btnFio_Ved_Prin.Text = "...";
            this.btnFio_Ved_Prin.UseVisualStyleBackColor = true;
            this.btnFio_Ved_Prin.Click += new System.EventHandler(this.btnFio_Ved_Prin_Click);
            // 
            // txtFio_Ved_Prin
            // 
            this.txtFio_Ved_Prin.Location = new System.Drawing.Point(705, 369);
            this.txtFio_Ved_Prin.Name = "txtFio_Ved_Prin";
            this.txtFio_Ved_Prin.Size = new System.Drawing.Size(372, 20);
            this.txtFio_Ved_Prin.TabIndex = 22;
            this.txtFio_Ved_Prin.Validating += new System.ComponentModel.CancelEventHandler(this.txtFio_Ved_Prin_Validating);
            // 
            // txtOpisan_Ved
            // 
            this.txtOpisan_Ved.Location = new System.Drawing.Point(705, 238);
            this.txtOpisan_Ved.Multiline = true;
            this.txtOpisan_Ved.Name = "txtOpisan_Ved";
            this.txtOpisan_Ved.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOpisan_Ved.Size = new System.Drawing.Size(400, 50);
            this.txtOpisan_Ved.TabIndex = 19;
            // 
            // txtDoc_Oplata
            // 
            this.txtDoc_Oplata.Location = new System.Drawing.Point(705, 100);
            this.txtDoc_Oplata.Name = "txtDoc_Oplata";
            this.txtDoc_Oplata.Size = new System.Drawing.Size(400, 20);
            this.txtDoc_Oplata.TabIndex = 15;
            // 
            // cbOplata
            // 
            this.cbOplata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOplata.FormattingEnabled = true;
            this.cbOplata.Location = new System.Drawing.Point(705, 53);
            this.cbOplata.Name = "cbOplata";
            this.cbOplata.Size = new System.Drawing.Size(130, 21);
            this.cbOplata.TabIndex = 13;
            this.cbOplata.SelectedIndexChanged += new System.EventHandler(this.cbOplata_SelectedIndexChanged);
            // 
            // txtTip_Doc_code
            // 
            this.txtTip_Doc_code.Location = new System.Drawing.Point(143, 169);
            this.txtTip_Doc_code.Name = "txtTip_Doc_code";
            this.txtTip_Doc_code.ReadOnly = true;
            this.txtTip_Doc_code.Size = new System.Drawing.Size(130, 20);
            this.txtTip_Doc_code.TabIndex = 15;
            // 
            // cbOtkaz
            // 
            this.cbOtkaz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOtkaz.FormattingEnabled = true;
            this.cbOtkaz.Location = new System.Drawing.Point(705, 169);
            this.cbOtkaz.Name = "cbOtkaz";
            this.cbOtkaz.Size = new System.Drawing.Size(130, 21);
            this.cbOtkaz.TabIndex = 16;
            this.cbOtkaz.SelectedIndexChanged += new System.EventHandler(this.cbOtkaz_SelectedIndexChanged);
            // 
            // txtCane
            // 
            this.txtCane.Location = new System.Drawing.Point(705, 30);
            this.txtCane.Name = "txtCane";
            this.txtCane.Size = new System.Drawing.Size(130, 20);
            this.txtCane.TabIndex = 12;
            // 
            // txtPr_Otkaz
            // 
            this.txtPr_Otkaz.Location = new System.Drawing.Point(705, 192);
            this.txtPr_Otkaz.Name = "txtPr_Otkaz";
            this.txtPr_Otkaz.Size = new System.Drawing.Size(400, 20);
            this.txtPr_Otkaz.TabIndex = 17;
            // 
            // btnFio_Z
            // 
            this.btnFio_Z.Location = new System.Drawing.Point(521, 393);
            this.btnFio_Z.Name = "btnFio_Z";
            this.btnFio_Z.Size = new System.Drawing.Size(22, 23);
            this.btnFio_Z.TabIndex = 45;
            this.btnFio_Z.Text = "...";
            this.btnFio_Z.UseVisualStyleBackColor = true;
            this.btnFio_Z.Click += new System.EventHandler(this.btnFio_Z_Click);
            // 
            // txtFio_Z
            // 
            this.txtFio_Z.Location = new System.Drawing.Point(143, 394);
            this.txtFio_Z.Name = "txtFio_Z";
            this.txtFio_Z.Size = new System.Drawing.Size(372, 20);
            this.txtFio_Z.TabIndex = 11;
            this.txtFio_Z.Validating += new System.ComponentModel.CancelEventHandler(this.txtFio_Z_Validating);
            // 
            // txtKod_Z_code
            // 
            this.txtKod_Z_code.Location = new System.Drawing.Point(144, 100);
            this.txtKod_Z_code.Name = "txtKod_Z_code";
            this.txtKod_Z_code.ReadOnly = true;
            this.txtKod_Z_code.Size = new System.Drawing.Size(130, 20);
            this.txtKod_Z_code.TabIndex = 47;
            // 
            // lblKod_Z_code
            // 
            this.lblKod_Z_code.Location = new System.Drawing.Point(8, 100);
            this.lblKod_Z_code.Name = "lblKod_Z_code";
            this.lblKod_Z_code.Size = new System.Drawing.Size(125, 20);
            this.lblKod_Z_code.TabIndex = 46;
            this.lblKod_Z_code.Text = "Код заявника";
            // 
            // txtDodatok
            // 
            this.txtDodatok.Location = new System.Drawing.Point(143, 268);
            this.txtDodatok.Multiline = true;
            this.txtDodatok.Name = "txtDodatok";
            this.txtDodatok.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDodatok.Size = new System.Drawing.Size(400, 50);
            this.txtDodatok.TabIndex = 9;
            // 
            // lblDodatok
            // 
            this.lblDodatok.Location = new System.Drawing.Point(12, 286);
            this.lblDodatok.Name = "lblDodatok";
            this.lblDodatok.Size = new System.Drawing.Size(125, 14);
            this.lblDodatok.TabIndex = 48;
            this.lblDodatok.Text = "Перелік доданніх матеріалів";
            // 
            // frmReestrZayav_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 476);
            this.Controls.Add(this.txtDodatok);
            this.Controls.Add(this.lblDodatok);
            this.Controls.Add(this.txtKod_Z_code);
            this.Controls.Add(this.lblKod_Z_code);
            this.Controls.Add(this.btnFio_Z);
            this.Controls.Add(this.txtFio_Z);
            this.Controls.Add(this.txtPr_Otkaz);
            this.Controls.Add(this.btnFio_Ved_Prin);
            this.Controls.Add(this.txtFio_Ved_Prin);
            this.Controls.Add(this.btnFio_Ved_Vid);
            this.Controls.Add(this.txtFio_Ved_Vid);
            this.Controls.Add(this.txtPrim);
            this.Controls.Add(this.lblPrim);
            this.Controls.Add(this.btnTip_Doc);
            this.Controls.Add(this.txtTip_Doc);
            this.Controls.Add(this.btnKod_Z);
            this.Controls.Add(this.txtKod_Z);
            this.Controls.Add(this.lblServicesProvided);
            this.Controls.Add(this.txtDoc_Oplata);
            this.Controls.Add(this.txtCane);
            this.Controls.Add(this.txtForma_Ved);
            this.Controls.Add(this.txtOpisan_Ved);
            this.Controls.Add(this.txtTip_Doc_code);
            this.Controls.Add(this.cbOplata);
            this.Controls.Add(this.cbOtkaz);
            this.Controls.Add(this.dtpData_Ved);
            this.Controls.Add(this.lblN_Z);
            this.Controls.Add(this.dtpData_Oplata);
            this.Controls.Add(this.lblFio_Ved_Prin);
            this.Controls.Add(this.txtN_Z);
            this.Controls.Add(this.lblFio_Ved_Vid);
            this.Controls.Add(this.txtSodergan);
            this.Controls.Add(this.lblForma_Ved);
            this.Controls.Add(this.lblKod_Z);
            this.Controls.Add(this.lblOpisan_Ved);
            this.Controls.Add(this.txtTel_Z);
            this.Controls.Add(this.lblData_Oplata);
            this.Controls.Add(this.lblData_Z);
            this.Controls.Add(this.lblDoc_Oplata);
            this.Controls.Add(this.lblData_Ved);
            this.Controls.Add(this.lblN_Ish_Z);
            this.Controls.Add(this.lblOplata);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.lblTip_Doc_code);
            this.Controls.Add(this.lblData_Ish);
            this.Controls.Add(this.lblTip_Doc);
            this.Controls.Add(this.dtpData_Ish);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtN_Ish_Z);
            this.Controls.Add(this.lblTel_Z);
            this.Controls.Add(this.lblCane);
            this.Controls.Add(this.lblSodergan);
            this.Controls.Add(this.lblPr_Otkaz);
            this.Controls.Add(this.lblOtkaz);
            this.Controls.Add(this.dtpData_Z);
            this.Controls.Add(this.lblFio_Z);
            this.Controls.Add(this.lblFirstData);
            this.Controls.Add(this.lblDoppData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReestrZayav_doc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Заява / звернення";
            this.Load += new System.EventHandler(this.frmReestrZayav_doc_Load);
            this.Controls.SetChildIndex(this.lblDoppData, 0);
            this.Controls.SetChildIndex(this.lblFirstData, 0);
            this.Controls.SetChildIndex(this.lblFio_Z, 0);
            this.Controls.SetChildIndex(this.dtpData_Z, 0);
            this.Controls.SetChildIndex(this.lblOtkaz, 0);
            this.Controls.SetChildIndex(this.lblPr_Otkaz, 0);
            this.Controls.SetChildIndex(this.lblSodergan, 0);
            this.Controls.SetChildIndex(this.lblCane, 0);
            this.Controls.SetChildIndex(this.lblTel_Z, 0);
            this.Controls.SetChildIndex(this.txtN_Ish_Z, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.dtpData_Ish, 0);
            this.Controls.SetChildIndex(this.lblTip_Doc, 0);
            this.Controls.SetChildIndex(this.lblData_Ish, 0);
            this.Controls.SetChildIndex(this.lblTip_Doc_code, 0);
            this.Controls.SetChildIndex(this.cbStatus, 0);
            this.Controls.SetChildIndex(this.lblOplata, 0);
            this.Controls.SetChildIndex(this.lblN_Ish_Z, 0);
            this.Controls.SetChildIndex(this.lblData_Ved, 0);
            this.Controls.SetChildIndex(this.lblDoc_Oplata, 0);
            this.Controls.SetChildIndex(this.lblData_Z, 0);
            this.Controls.SetChildIndex(this.lblData_Oplata, 0);
            this.Controls.SetChildIndex(this.txtTel_Z, 0);
            this.Controls.SetChildIndex(this.lblOpisan_Ved, 0);
            this.Controls.SetChildIndex(this.lblKod_Z, 0);
            this.Controls.SetChildIndex(this.lblForma_Ved, 0);
            this.Controls.SetChildIndex(this.txtSodergan, 0);
            this.Controls.SetChildIndex(this.lblFio_Ved_Vid, 0);
            this.Controls.SetChildIndex(this.txtN_Z, 0);
            this.Controls.SetChildIndex(this.lblFio_Ved_Prin, 0);
            this.Controls.SetChildIndex(this.dtpData_Oplata, 0);
            this.Controls.SetChildIndex(this.lblN_Z, 0);
            this.Controls.SetChildIndex(this.dtpData_Ved, 0);
            this.Controls.SetChildIndex(this.cbOtkaz, 0);
            this.Controls.SetChildIndex(this.cbOplata, 0);
            this.Controls.SetChildIndex(this.txtTip_Doc_code, 0);
            this.Controls.SetChildIndex(this.txtOpisan_Ved, 0);
            this.Controls.SetChildIndex(this.txtForma_Ved, 0);
            this.Controls.SetChildIndex(this.txtCane, 0);
            this.Controls.SetChildIndex(this.txtDoc_Oplata, 0);
            this.Controls.SetChildIndex(this.lblServicesProvided, 0);
            this.Controls.SetChildIndex(this.txtKod_Z, 0);
            this.Controls.SetChildIndex(this.btnKod_Z, 0);
            this.Controls.SetChildIndex(this.txtTip_Doc, 0);
            this.Controls.SetChildIndex(this.btnTip_Doc, 0);
            this.Controls.SetChildIndex(this.lblPrim, 0);
            this.Controls.SetChildIndex(this.txtPrim, 0);
            this.Controls.SetChildIndex(this.txtFio_Ved_Vid, 0);
            this.Controls.SetChildIndex(this.btnFio_Ved_Vid, 0);
            this.Controls.SetChildIndex(this.txtFio_Ved_Prin, 0);
            this.Controls.SetChildIndex(this.btnFio_Ved_Prin, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtPr_Otkaz, 0);
            this.Controls.SetChildIndex(this.txtFio_Z, 0);
            this.Controls.SetChildIndex(this.btnFio_Z, 0);
            this.Controls.SetChildIndex(this.lblKod_Z_code, 0);
            this.Controls.SetChildIndex(this.txtKod_Z_code, 0);
            this.Controls.SetChildIndex(this.lblDodatok, 0);
            this.Controls.SetChildIndex(this.txtDodatok, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtSodergan;
        private TextBox txtTel_Z;
        private ComboBox cbStatus;
        private DateTimePicker dtpData_Ish;
        private TextBox txtN_Ish_Z;
        private Label lblCane;
        private Label lblPr_Otkaz;
        private Label lblOtkaz;
        private Label lblFio_Z;
        private Label lblDoppData;
        private Label lblFirstData;
        private DateTimePicker dtpData_Z;
        private Label lblSodergan;
        private Label lblTel_Z;
        private Label lblStatus;
        private Label lblTip_Doc;
        private Label lblData_Ish;
        private Label lblN_Ish_Z;
        private Label lblData_Z;
        private Label lblKod_Z;
        private TextBox txtN_Z;
        private Label lblN_Z;
        private TextBox txtForma_Ved;
        private DateTimePicker dtpData_Ved;
        private DateTimePicker dtpData_Oplata;
        private Label lblFio_Ved_Prin;
        private Label lblFio_Ved_Vid;
        private Label lblForma_Ved;
        private Label lblOpisan_Ved;
        private Label lblData_Oplata;
        private Label lblDoc_Oplata;
        private Label lblData_Ved;
        private Label lblOplata;
        private Label lblTip_Doc_code;
        private Label lblServicesProvided;
        private TextBox txtKod_Z;
        private Button btnKod_Z;
        private TextBox txtTip_Doc;
        private Button btnTip_Doc;
        private TextBox txtPrim;
        private Label lblPrim;
        private Button btnFio_Ved_Vid;
        private TextBox txtFio_Ved_Vid;
        private Button btnFio_Ved_Prin;
        private TextBox txtFio_Ved_Prin;
        private TextBox txtOpisan_Ved;
        private TextBox txtDoc_Oplata;
        private ComboBox cbOplata;
        private TextBox txtTip_Doc_code;
        private ComboBox cbOtkaz;
        private TextBox txtCane;
        private TextBox txtPr_Otkaz;
        private Button btnFio_Z;
        private TextBox txtFio_Z;
        private TextBox txtKod_Z_code;
        private Label lblKod_Z_code;
        private TextBox txtDodatok;
        private Label lblDodatok;
    }
}