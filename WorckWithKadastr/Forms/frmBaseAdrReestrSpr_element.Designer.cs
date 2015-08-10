namespace WorckWithKadastr
{
    partial class frmBaseAdrReestrSpr_element
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
            this.lblAdminRajo = new System.Windows.Forms.Label();
            this.txtAdminRajon = new System.Windows.Forms.TextBox();
            this.btnAdminRajo = new System.Windows.Forms.Button();
            this.btnDocument = new System.Windows.Forms.Button();
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.lblDocument = new System.Windows.Forms.Label();
            this.btnKodKategorii = new System.Windows.Forms.Button();
            this.txtKodKategorii = new System.Windows.Forms.TextBox();
            this.lblKodKategorii = new System.Windows.Forms.Label();
            this.cbStatusObject = new System.Windows.Forms.ComboBox();
            this.lblStatusObject = new System.Windows.Forms.Label();
            this.cbLocalType = new System.Windows.Forms.ComboBox();
            this.lblLocalType = new System.Windows.Forms.Label();
            this.cbDzhereloKoord = new System.Windows.Forms.ComboBox();
            this.lblDzhereloKoord = new System.Windows.Forms.Label();
            this.txtKodObject = new System.Windows.Forms.TextBox();
            this.lblKodObject = new System.Windows.Forms.Label();
            this.txtNazvaKorotkaRus = new System.Windows.Forms.TextBox();
            this.lblNazvaKorotkaRus = new System.Windows.Forms.Label();
            this.txtNazvaPovnaRus = new System.Windows.Forms.TextBox();
            this.lblNazvaPovnaRus = new System.Windows.Forms.Label();
            this.txtNazvaKorotkaUkr = new System.Windows.Forms.TextBox();
            this.lblNazvaKorotkaUkr = new System.Windows.Forms.Label();
            this.txtNazvaPovnaUkr = new System.Windows.Forms.TextBox();
            this.lblNazvaPovnaUkr = new System.Windows.Forms.Label();
            this.txtNazvaPovnaLat = new System.Windows.Forms.TextBox();
            this.lblNazvaPovnaLat = new System.Windows.Forms.Label();
            this.txtNazvaKorotkaLat = new System.Windows.Forms.TextBox();
            this.lblNazvaKorotkaLat = new System.Windows.Forms.Label();
            this.txtNazvaDocument = new System.Windows.Forms.TextBox();
            this.lblNazvaDocument = new System.Windows.Forms.Label();
            this.txtNomerDocument = new System.Windows.Forms.TextBox();
            this.lblNomerDocument = new System.Windows.Forms.Label();
            this.txtLinkDocument = new System.Windows.Forms.TextBox();
            this.lblLinkDocument = new System.Windows.Forms.Label();
            this.txtOpys = new System.Windows.Forms.TextBox();
            this.lblOpys = new System.Windows.Forms.Label();
            this.lblDataDocument = new System.Windows.Forms.Label();
            this.dtpDataDocument = new System.Windows.Forms.DateTimePicker();
            this.btnShowOnMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(592, 462);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 462);
            // 
            // lblAdminRajo
            // 
            this.lblAdminRajo.Location = new System.Drawing.Point(403, 7);
            this.lblAdminRajo.Name = "lblAdminRajo";
            this.lblAdminRajo.Size = new System.Drawing.Size(100, 26);
            this.lblAdminRajo.TabIndex = 9;
            this.lblAdminRajo.Text = "Адміністративний район";
            // 
            // txtAdminRajon
            // 
            this.txtAdminRajon.Location = new System.Drawing.Point(510, 10);
            this.txtAdminRajon.Name = "txtAdminRajon";
            this.txtAdminRajon.Size = new System.Drawing.Size(150, 20);
            this.txtAdminRajon.TabIndex = 10;
            this.txtAdminRajon.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // btnAdminRajo
            // 
            this.btnAdminRajo.Location = new System.Drawing.Point(670, 9);
            this.btnAdminRajo.Name = "btnAdminRajo";
            this.btnAdminRajo.Size = new System.Drawing.Size(21, 21);
            this.btnAdminRajo.TabIndex = 11;
            this.btnAdminRajo.Text = "...";
            this.btnAdminRajo.UseVisualStyleBackColor = true;
            this.btnAdminRajo.Click += new System.EventHandler(this.btnAdminRajo_Click);
            // 
            // btnDocument
            // 
            this.btnDocument.Location = new System.Drawing.Point(366, 220);
            this.btnDocument.Name = "btnDocument";
            this.btnDocument.Size = new System.Drawing.Size(21, 21);
            this.btnDocument.TabIndex = 14;
            this.btnDocument.Text = "...";
            this.btnDocument.UseVisualStyleBackColor = true;
            this.btnDocument.Click += new System.EventHandler(this.btnDocument_Click);
            // 
            // txtDocument
            // 
            this.txtDocument.Location = new System.Drawing.Point(144, 220);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(212, 20);
            this.txtDocument.TabIndex = 13;
            this.txtDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblDocument
            // 
            this.lblDocument.Location = new System.Drawing.Point(6, 214);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(132, 32);
            this.lblDocument.TabIndex = 12;
            this.lblDocument.Text = "Тип документу про найменування";
            // 
            // btnKodKategorii
            // 
            this.btnKodKategorii.Location = new System.Drawing.Point(366, 33);
            this.btnKodKategorii.Name = "btnKodKategorii";
            this.btnKodKategorii.Size = new System.Drawing.Size(21, 21);
            this.btnKodKategorii.TabIndex = 17;
            this.btnKodKategorii.Text = "...";
            this.btnKodKategorii.UseVisualStyleBackColor = true;
            this.btnKodKategorii.Click += new System.EventHandler(this.btnKodKategorii_Click);
            // 
            // txtKodKategorii
            // 
            this.txtKodKategorii.Location = new System.Drawing.Point(144, 34);
            this.txtKodKategorii.Name = "txtKodKategorii";
            this.txtKodKategorii.Size = new System.Drawing.Size(212, 20);
            this.txtKodKategorii.TabIndex = 16;
            this.txtKodKategorii.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKodKategorii
            // 
            this.lblKodKategorii.Location = new System.Drawing.Point(6, 34);
            this.lblKodKategorii.Name = "lblKodKategorii";
            this.lblKodKategorii.Size = new System.Drawing.Size(132, 21);
            this.lblKodKategorii.TabIndex = 15;
            this.lblKodKategorii.Text = "Код категорії об\'єкта";
            // 
            // cbStatusObject
            // 
            this.cbStatusObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatusObject.FormattingEnabled = true;
            this.cbStatusObject.Location = new System.Drawing.Point(510, 36);
            this.cbStatusObject.Name = "cbStatusObject";
            this.cbStatusObject.Size = new System.Drawing.Size(150, 21);
            this.cbStatusObject.TabIndex = 18;
            this.cbStatusObject.SelectedIndexChanged += new System.EventHandler(this.cbStatusObject_SelectedIndexChanged);
            // 
            // lblStatusObject
            // 
            this.lblStatusObject.Location = new System.Drawing.Point(403, 39);
            this.lblStatusObject.Name = "lblStatusObject";
            this.lblStatusObject.Size = new System.Drawing.Size(100, 21);
            this.lblStatusObject.TabIndex = 19;
            this.lblStatusObject.Text = "Статус";
            // 
            // cbLocalType
            // 
            this.cbLocalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocalType.FormattingEnabled = true;
            this.cbLocalType.Location = new System.Drawing.Point(538, 424);
            this.cbLocalType.Name = "cbLocalType";
            this.cbLocalType.Size = new System.Drawing.Size(150, 21);
            this.cbLocalType.TabIndex = 20;
            this.cbLocalType.SelectedIndexChanged += new System.EventHandler(this.cbLocalType_SelectedIndexChanged);
            // 
            // lblLocalType
            // 
            this.lblLocalType.Location = new System.Drawing.Point(366, 424);
            this.lblLocalType.Name = "lblLocalType";
            this.lblLocalType.Size = new System.Drawing.Size(165, 21);
            this.lblLocalType.TabIndex = 21;
            this.lblLocalType.Text = "Тип просторової локалізації";
            // 
            // cbDzhereloKoord
            // 
            this.cbDzhereloKoord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDzhereloKoord.FormattingEnabled = true;
            this.cbDzhereloKoord.Location = new System.Drawing.Point(144, 424);
            this.cbDzhereloKoord.Name = "cbDzhereloKoord";
            this.cbDzhereloKoord.Size = new System.Drawing.Size(179, 21);
            this.cbDzhereloKoord.TabIndex = 22;
            this.cbDzhereloKoord.SelectedIndexChanged += new System.EventHandler(this.cbDzhereloKoord_SelectedIndexChanged);
            // 
            // lblDzhereloKoord
            // 
            this.lblDzhereloKoord.Location = new System.Drawing.Point(6, 424);
            this.lblDzhereloKoord.Name = "lblDzhereloKoord";
            this.lblDzhereloKoord.Size = new System.Drawing.Size(132, 21);
            this.lblDzhereloKoord.TabIndex = 23;
            this.lblDzhereloKoord.Text = "Джерело координат";
            // 
            // txtKodObject
            // 
            this.txtKodObject.Enabled = false;
            this.txtKodObject.Location = new System.Drawing.Point(144, 9);
            this.txtKodObject.Name = "txtKodObject";
            this.txtKodObject.Size = new System.Drawing.Size(100, 20);
            this.txtKodObject.TabIndex = 25;
            this.txtKodObject.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtKodObject.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKodObject
            // 
            this.lblKodObject.Location = new System.Drawing.Point(6, 7);
            this.lblKodObject.Name = "lblKodObject";
            this.lblKodObject.Size = new System.Drawing.Size(132, 21);
            this.lblKodObject.TabIndex = 24;
            this.lblKodObject.Text = "Код об\'єкту";
            // 
            // txtNazvaKorotkaRus
            // 
            this.txtNazvaKorotkaRus.Location = new System.Drawing.Point(144, 141);
            this.txtNazvaKorotkaRus.Name = "txtNazvaKorotkaRus";
            this.txtNazvaKorotkaRus.Size = new System.Drawing.Size(544, 20);
            this.txtNazvaKorotkaRus.TabIndex = 27;
            this.txtNazvaKorotkaRus.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaKorotkaRus.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaKorotkaRus
            // 
            this.lblNazvaKorotkaRus.Location = new System.Drawing.Point(6, 141);
            this.lblNazvaKorotkaRus.Name = "lblNazvaKorotkaRus";
            this.lblNazvaKorotkaRus.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaKorotkaRus.TabIndex = 26;
            this.lblNazvaKorotkaRus.Text = "Коротка назва (рус)";
            // 
            // txtNazvaPovnaRus
            // 
            this.txtNazvaPovnaRus.Location = new System.Drawing.Point(144, 115);
            this.txtNazvaPovnaRus.Name = "txtNazvaPovnaRus";
            this.txtNazvaPovnaRus.Size = new System.Drawing.Size(544, 20);
            this.txtNazvaPovnaRus.TabIndex = 29;
            this.txtNazvaPovnaRus.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaPovnaRus.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaPovnaRus
            // 
            this.lblNazvaPovnaRus.Location = new System.Drawing.Point(6, 115);
            this.lblNazvaPovnaRus.Name = "lblNazvaPovnaRus";
            this.lblNazvaPovnaRus.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaPovnaRus.TabIndex = 28;
            this.lblNazvaPovnaRus.Text = "Повна назва (рус)";
            // 
            // txtNazvaKorotkaUkr
            // 
            this.txtNazvaKorotkaUkr.Location = new System.Drawing.Point(144, 89);
            this.txtNazvaKorotkaUkr.Name = "txtNazvaKorotkaUkr";
            this.txtNazvaKorotkaUkr.Size = new System.Drawing.Size(544, 20);
            this.txtNazvaKorotkaUkr.TabIndex = 31;
            this.txtNazvaKorotkaUkr.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaKorotkaUkr.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaKorotkaUkr
            // 
            this.lblNazvaKorotkaUkr.Location = new System.Drawing.Point(6, 89);
            this.lblNazvaKorotkaUkr.Name = "lblNazvaKorotkaUkr";
            this.lblNazvaKorotkaUkr.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaKorotkaUkr.TabIndex = 30;
            this.lblNazvaKorotkaUkr.Text = "Коротка назва (укр)";
            // 
            // txtNazvaPovnaUkr
            // 
            this.txtNazvaPovnaUkr.Location = new System.Drawing.Point(144, 61);
            this.txtNazvaPovnaUkr.Name = "txtNazvaPovnaUkr";
            this.txtNazvaPovnaUkr.Size = new System.Drawing.Size(544, 20);
            this.txtNazvaPovnaUkr.TabIndex = 33;
            this.txtNazvaPovnaUkr.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaPovnaUkr.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaPovnaUkr
            // 
            this.lblNazvaPovnaUkr.Location = new System.Drawing.Point(6, 61);
            this.lblNazvaPovnaUkr.Name = "lblNazvaPovnaUkr";
            this.lblNazvaPovnaUkr.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaPovnaUkr.TabIndex = 32;
            this.lblNazvaPovnaUkr.Text = "Повна назва (укр)";
            // 
            // txtNazvaPovnaLat
            // 
            this.txtNazvaPovnaLat.Location = new System.Drawing.Point(144, 167);
            this.txtNazvaPovnaLat.Name = "txtNazvaPovnaLat";
            this.txtNazvaPovnaLat.Size = new System.Drawing.Size(544, 20);
            this.txtNazvaPovnaLat.TabIndex = 35;
            this.txtNazvaPovnaLat.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaPovnaLat.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaPovnaLat
            // 
            this.lblNazvaPovnaLat.Location = new System.Drawing.Point(6, 167);
            this.lblNazvaPovnaLat.Name = "lblNazvaPovnaLat";
            this.lblNazvaPovnaLat.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaPovnaLat.TabIndex = 34;
            this.lblNazvaPovnaLat.Text = "Повна назва (лат)";
            // 
            // txtNazvaKorotkaLat
            // 
            this.txtNazvaKorotkaLat.Location = new System.Drawing.Point(144, 193);
            this.txtNazvaKorotkaLat.Name = "txtNazvaKorotkaLat";
            this.txtNazvaKorotkaLat.Size = new System.Drawing.Size(544, 20);
            this.txtNazvaKorotkaLat.TabIndex = 37;
            this.txtNazvaKorotkaLat.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaKorotkaLat.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaKorotkaLat
            // 
            this.lblNazvaKorotkaLat.Location = new System.Drawing.Point(6, 193);
            this.lblNazvaKorotkaLat.Name = "lblNazvaKorotkaLat";
            this.lblNazvaKorotkaLat.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaKorotkaLat.TabIndex = 36;
            this.lblNazvaKorotkaLat.Text = "Коротка назва (лат)";
            // 
            // txtNazvaDocument
            // 
            this.txtNazvaDocument.Location = new System.Drawing.Point(144, 251);
            this.txtNazvaDocument.Multiline = true;
            this.txtNazvaDocument.Name = "txtNazvaDocument";
            this.txtNazvaDocument.Size = new System.Drawing.Size(544, 21);
            this.txtNazvaDocument.TabIndex = 39;
            this.txtNazvaDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaDocument
            // 
            this.lblNazvaDocument.Location = new System.Drawing.Point(6, 251);
            this.lblNazvaDocument.Name = "lblNazvaDocument";
            this.lblNazvaDocument.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaDocument.TabIndex = 38;
            this.lblNazvaDocument.Text = "Назва документу";
            // 
            // txtNomerDocument
            // 
            this.txtNomerDocument.Location = new System.Drawing.Point(144, 282);
            this.txtNomerDocument.Name = "txtNomerDocument";
            this.txtNomerDocument.Size = new System.Drawing.Size(100, 20);
            this.txtNomerDocument.TabIndex = 41;
            this.txtNomerDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNomerDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNomerDocument
            // 
            this.lblNomerDocument.Location = new System.Drawing.Point(6, 282);
            this.lblNomerDocument.Name = "lblNomerDocument";
            this.lblNomerDocument.Size = new System.Drawing.Size(132, 21);
            this.lblNomerDocument.TabIndex = 40;
            this.lblNomerDocument.Text = "Номер документу";
            // 
            // txtLinkDocument
            // 
            this.txtLinkDocument.Location = new System.Drawing.Point(144, 311);
            this.txtLinkDocument.Multiline = true;
            this.txtLinkDocument.Name = "txtLinkDocument";
            this.txtLinkDocument.Size = new System.Drawing.Size(544, 47);
            this.txtLinkDocument.TabIndex = 43;
            this.txtLinkDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtLinkDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblLinkDocument
            // 
            this.lblLinkDocument.Location = new System.Drawing.Point(6, 320);
            this.lblLinkDocument.Name = "lblLinkDocument";
            this.lblLinkDocument.Size = new System.Drawing.Size(132, 29);
            this.lblLinkDocument.TabIndex = 42;
            this.lblLinkDocument.Text = "Посилання на документ";
            // 
            // txtOpys
            // 
            this.txtOpys.Location = new System.Drawing.Point(144, 367);
            this.txtOpys.Multiline = true;
            this.txtOpys.Name = "txtOpys";
            this.txtOpys.Size = new System.Drawing.Size(544, 47);
            this.txtOpys.TabIndex = 45;
            this.txtOpys.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtOpys.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblOpys
            // 
            this.lblOpys.Location = new System.Drawing.Point(6, 378);
            this.lblOpys.Name = "lblOpys";
            this.lblOpys.Size = new System.Drawing.Size(132, 24);
            this.lblOpys.TabIndex = 44;
            this.lblOpys.Text = "Опис розташування";
            // 
            // lblDataDocument
            // 
            this.lblDataDocument.Location = new System.Drawing.Point(424, 282);
            this.lblDataDocument.Name = "lblDataDocument";
            this.lblDataDocument.Size = new System.Drawing.Size(100, 21);
            this.lblDataDocument.TabIndex = 46;
            this.lblDataDocument.Text = "Дата документу";
            // 
            // dtpDataDocument
            // 
            this.dtpDataDocument.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpDataDocument.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataDocument.Location = new System.Drawing.Point(538, 282);
            this.dtpDataDocument.Name = "dtpDataDocument";
            this.dtpDataDocument.Size = new System.Drawing.Size(150, 20);
            this.dtpDataDocument.TabIndex = 47;
            this.dtpDataDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // btnShowOnMap
            // 
            this.btnShowOnMap.Location = new System.Drawing.Point(257, 462);
            this.btnShowOnMap.Name = "btnShowOnMap";
            this.btnShowOnMap.Size = new System.Drawing.Size(131, 23);
            this.btnShowOnMap.TabIndex = 48;
            this.btnShowOnMap.Text = "Показать на карте";
            this.btnShowOnMap.UseVisualStyleBackColor = true;
            this.btnShowOnMap.Click += new System.EventHandler(this.btnShowOnMap_Click);
            // 
            // frmBaseAdrReestrSpr_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 490);
            this.Controls.Add(this.btnShowOnMap);
            this.Controls.Add(this.dtpDataDocument);
            this.Controls.Add(this.lblDataDocument);
            this.Controls.Add(this.txtOpys);
            this.Controls.Add(this.lblOpys);
            this.Controls.Add(this.txtLinkDocument);
            this.Controls.Add(this.lblLinkDocument);
            this.Controls.Add(this.txtNomerDocument);
            this.Controls.Add(this.lblNomerDocument);
            this.Controls.Add(this.txtNazvaDocument);
            this.Controls.Add(this.lblNazvaDocument);
            this.Controls.Add(this.txtNazvaKorotkaLat);
            this.Controls.Add(this.lblNazvaKorotkaLat);
            this.Controls.Add(this.txtNazvaPovnaLat);
            this.Controls.Add(this.lblNazvaPovnaLat);
            this.Controls.Add(this.txtNazvaPovnaUkr);
            this.Controls.Add(this.lblNazvaPovnaUkr);
            this.Controls.Add(this.txtNazvaKorotkaUkr);
            this.Controls.Add(this.lblNazvaKorotkaUkr);
            this.Controls.Add(this.txtNazvaPovnaRus);
            this.Controls.Add(this.lblNazvaPovnaRus);
            this.Controls.Add(this.txtNazvaKorotkaRus);
            this.Controls.Add(this.lblNazvaKorotkaRus);
            this.Controls.Add(this.txtKodObject);
            this.Controls.Add(this.lblKodObject);
            this.Controls.Add(this.cbDzhereloKoord);
            this.Controls.Add(this.lblDzhereloKoord);
            this.Controls.Add(this.cbLocalType);
            this.Controls.Add(this.lblLocalType);
            this.Controls.Add(this.cbStatusObject);
            this.Controls.Add(this.lblStatusObject);
            this.Controls.Add(this.btnKodKategorii);
            this.Controls.Add(this.txtKodKategorii);
            this.Controls.Add(this.lblKodKategorii);
            this.Controls.Add(this.btnDocument);
            this.Controls.Add(this.txtDocument);
            this.Controls.Add(this.lblDocument);
            this.Controls.Add(this.btnAdminRajo);
            this.Controls.Add(this.txtAdminRajon);
            this.Controls.Add(this.lblAdminRajo);
            this.Name = "frmBaseAdrReestrSpr_element";
            this.Text = "frmBaseGeoSpr_element";
            this.Load += new System.EventHandler(this.frmBaseAdrReestrSpr_element_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblAdminRajo;
        protected System.Windows.Forms.TextBox txtAdminRajon;
        protected System.Windows.Forms.Button btnAdminRajo;
        protected System.Windows.Forms.Button btnDocument;
        protected System.Windows.Forms.TextBox txtDocument;
        protected System.Windows.Forms.Label lblDocument;
        protected System.Windows.Forms.Button btnKodKategorii;
        protected System.Windows.Forms.TextBox txtKodKategorii;
        protected System.Windows.Forms.Label lblKodKategorii;
        protected System.Windows.Forms.ComboBox cbStatusObject;
        protected System.Windows.Forms.Label lblStatusObject;
        protected System.Windows.Forms.ComboBox cbLocalType;
        protected System.Windows.Forms.Label lblLocalType;
        protected System.Windows.Forms.ComboBox cbDzhereloKoord;
        protected System.Windows.Forms.Label lblDzhereloKoord;
        protected System.Windows.Forms.TextBox txtKodObject;
        protected System.Windows.Forms.Label lblKodObject;
        protected System.Windows.Forms.TextBox txtNazvaKorotkaRus;
        protected System.Windows.Forms.Label lblNazvaKorotkaRus;
        protected System.Windows.Forms.TextBox txtNazvaPovnaRus;
        protected System.Windows.Forms.Label lblNazvaPovnaRus;
        protected System.Windows.Forms.TextBox txtNazvaKorotkaUkr;
        protected System.Windows.Forms.Label lblNazvaKorotkaUkr;
        protected System.Windows.Forms.TextBox txtNazvaPovnaUkr;
        protected System.Windows.Forms.Label lblNazvaPovnaUkr;
        protected System.Windows.Forms.TextBox txtNazvaPovnaLat;
        protected System.Windows.Forms.Label lblNazvaPovnaLat;
        protected System.Windows.Forms.TextBox txtNazvaKorotkaLat;
        protected System.Windows.Forms.Label lblNazvaKorotkaLat;
        protected System.Windows.Forms.TextBox txtNazvaDocument;
        protected System.Windows.Forms.Label lblNazvaDocument;
        protected System.Windows.Forms.TextBox txtNomerDocument;
        protected System.Windows.Forms.Label lblNomerDocument;
        protected System.Windows.Forms.TextBox txtLinkDocument;
        protected System.Windows.Forms.Label lblLinkDocument;
        protected System.Windows.Forms.TextBox txtOpys;
        protected System.Windows.Forms.Label lblOpys;
        protected System.Windows.Forms.Label lblDataDocument;
        protected System.Windows.Forms.DateTimePicker dtpDataDocument;
        protected System.Windows.Forms.Button btnShowOnMap;

    }
}