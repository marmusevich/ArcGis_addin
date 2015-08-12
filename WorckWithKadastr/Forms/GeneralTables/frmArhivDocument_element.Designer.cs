namespace WorckWithKadastr
{
    partial class frmArhivDocument_element
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
            this.dtpDataDocument = new System.Windows.Forms.DateTimePicker();
            this.lblDataDocument = new System.Windows.Forms.Label();
            this.txtNomerDocument = new System.Windows.Forms.TextBox();
            this.lblNomerDocument = new System.Windows.Forms.Label();
            this.txtNazvaDocument = new System.Windows.Forms.TextBox();
            this.lblNazvaDocument = new System.Windows.Forms.Label();
            this.txtKodObject = new System.Windows.Forms.TextBox();
            this.lblKodObject = new System.Windows.Forms.Label();
            this.btnDocument = new System.Windows.Forms.Button();
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.lblDocument = new System.Windows.Forms.Label();
            this.txtOpysDocument = new System.Windows.Forms.TextBox();
            this.lblOpysDocument = new System.Windows.Forms.Label();
            this.txtLinkDocument = new System.Windows.Forms.TextBox();
            this.lbLinkDocument = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(613, 227);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 227);
            // 
            // dtpDataDocument
            // 
            this.dtpDataDocument.CustomFormat = "dd.MMM.yyyy HH.mm";
            this.dtpDataDocument.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataDocument.Location = new System.Drawing.Point(538, 91);
            this.dtpDataDocument.Name = "dtpDataDocument";
            this.dtpDataDocument.Size = new System.Drawing.Size(150, 20);
            this.dtpDataDocument.TabIndex = 4;
            // 
            // lblDataDocument
            // 
            this.lblDataDocument.Location = new System.Drawing.Point(432, 91);
            this.lblDataDocument.Name = "lblDataDocument";
            this.lblDataDocument.Size = new System.Drawing.Size(100, 21);
            this.lblDataDocument.TabIndex = 57;
            this.lblDataDocument.Text = "Дата документу";
            // 
            // txtNomerDocument
            // 
            this.txtNomerDocument.Location = new System.Drawing.Point(144, 91);
            this.txtNomerDocument.Name = "txtNomerDocument";
            this.txtNomerDocument.Size = new System.Drawing.Size(100, 20);
            this.txtNomerDocument.TabIndex = 3;
            this.txtNomerDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNomerDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNomerDocument
            // 
            this.lblNomerDocument.Location = new System.Drawing.Point(9, 91);
            this.lblNomerDocument.Name = "lblNomerDocument";
            this.lblNomerDocument.Size = new System.Drawing.Size(132, 21);
            this.lblNomerDocument.TabIndex = 56;
            this.lblNomerDocument.Text = "Номер документу";
            // 
            // txtNazvaDocument
            // 
            this.txtNazvaDocument.Location = new System.Drawing.Point(144, 64);
            this.txtNazvaDocument.Multiline = true;
            this.txtNazvaDocument.Name = "txtNazvaDocument";
            this.txtNazvaDocument.Size = new System.Drawing.Size(544, 21);
            this.txtNazvaDocument.TabIndex = 2;
            this.txtNazvaDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtNazvaDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblNazvaDocument
            // 
            this.lblNazvaDocument.Location = new System.Drawing.Point(9, 64);
            this.lblNazvaDocument.Name = "lblNazvaDocument";
            this.lblNazvaDocument.Size = new System.Drawing.Size(132, 21);
            this.lblNazvaDocument.TabIndex = 55;
            this.lblNazvaDocument.Text = "Назва документу";
            // 
            // txtKodObject
            // 
            this.txtKodObject.Location = new System.Drawing.Point(144, 12);
            this.txtKodObject.Name = "txtKodObject";
            this.txtKodObject.Size = new System.Drawing.Size(100, 20);
            this.txtKodObject.TabIndex = 0;
            this.txtKodObject.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtKodObject.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblKodObject
            // 
            this.lblKodObject.Location = new System.Drawing.Point(9, 10);
            this.lblKodObject.Name = "lblKodObject";
            this.lblKodObject.Size = new System.Drawing.Size(132, 21);
            this.lblKodObject.TabIndex = 54;
            this.lblKodObject.Text = "Код об\'єкту";
            // 
            // btnDocument
            // 
            this.btnDocument.Location = new System.Drawing.Point(368, 38);
            this.btnDocument.Name = "btnDocument";
            this.btnDocument.Size = new System.Drawing.Size(21, 21);
            this.btnDocument.TabIndex = 53;
            this.btnDocument.Text = "...";
            this.btnDocument.UseVisualStyleBackColor = true;
            this.btnDocument.Click += new System.EventHandler(this.btnDocument_Click);
            // 
            // txtDocument
            // 
            this.txtDocument.Location = new System.Drawing.Point(144, 38);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(212, 20);
            this.txtDocument.TabIndex = 1;
            this.txtDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblDocument
            // 
            this.lblDocument.Location = new System.Drawing.Point(9, 32);
            this.lblDocument.Name = "lblDocument";
            this.lblDocument.Size = new System.Drawing.Size(132, 32);
            this.lblDocument.TabIndex = 51;
            this.lblDocument.Text = "Тип документу про найменування";
            // 
            // txtOpysDocument
            // 
            this.txtOpysDocument.Location = new System.Drawing.Point(144, 170);
            this.txtOpysDocument.Multiline = true;
            this.txtOpysDocument.Name = "txtOpysDocument";
            this.txtOpysDocument.Size = new System.Drawing.Size(544, 47);
            this.txtOpysDocument.TabIndex = 6;
            this.txtOpysDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtOpysDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lblOpysDocument
            // 
            this.lblOpysDocument.Location = new System.Drawing.Point(9, 173);
            this.lblOpysDocument.Name = "lblOpysDocument";
            this.lblOpysDocument.Size = new System.Drawing.Size(132, 24);
            this.lblOpysDocument.TabIndex = 59;
            this.lblOpysDocument.Text = "Опис документу";
            // 
            // txtLinkDocument
            // 
            this.txtLinkDocument.Location = new System.Drawing.Point(144, 117);
            this.txtLinkDocument.Multiline = true;
            this.txtLinkDocument.Name = "txtLinkDocument";
            this.txtLinkDocument.Size = new System.Drawing.Size(544, 47);
            this.txtLinkDocument.TabIndex = 5;
            this.txtLinkDocument.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.txtLinkDocument.Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);
            // 
            // lbLinkDocument
            // 
            this.lbLinkDocument.Location = new System.Drawing.Point(6, 126);
            this.lbLinkDocument.Name = "lbLinkDocument";
            this.lbLinkDocument.Size = new System.Drawing.Size(132, 29);
            this.lbLinkDocument.TabIndex = 61;
            this.lbLinkDocument.Text = "Посилання на документ";
            // 
            // frmArhivDocument_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 259);
            this.Controls.Add(this.txtLinkDocument);
            this.Controls.Add(this.lbLinkDocument);
            this.Controls.Add(this.txtOpysDocument);
            this.Controls.Add(this.lblOpysDocument);
            this.Controls.Add(this.dtpDataDocument);
            this.Controls.Add(this.lblDataDocument);
            this.Controls.Add(this.txtNomerDocument);
            this.Controls.Add(this.lblNomerDocument);
            this.Controls.Add(this.txtNazvaDocument);
            this.Controls.Add(this.lblNazvaDocument);
            this.Controls.Add(this.txtKodObject);
            this.Controls.Add(this.lblKodObject);
            this.Controls.Add(this.btnDocument);
            this.Controls.Add(this.txtDocument);
            this.Controls.Add(this.lblDocument);
            this.Name = "frmArhivDocument_element";
            this.Text = "ArhivDocument_element";
            this.Load += new System.EventHandler(this.frmArhivDocument_element_Load);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblDocument, 0);
            this.Controls.SetChildIndex(this.txtDocument, 0);
            this.Controls.SetChildIndex(this.btnDocument, 0);
            this.Controls.SetChildIndex(this.lblKodObject, 0);
            this.Controls.SetChildIndex(this.txtKodObject, 0);
            this.Controls.SetChildIndex(this.lblNazvaDocument, 0);
            this.Controls.SetChildIndex(this.txtNazvaDocument, 0);
            this.Controls.SetChildIndex(this.lblNomerDocument, 0);
            this.Controls.SetChildIndex(this.txtNomerDocument, 0);
            this.Controls.SetChildIndex(this.lblDataDocument, 0);
            this.Controls.SetChildIndex(this.dtpDataDocument, 0);
            this.Controls.SetChildIndex(this.lblOpysDocument, 0);
            this.Controls.SetChildIndex(this.txtOpysDocument, 0);
            this.Controls.SetChildIndex(this.lbLinkDocument, 0);
            this.Controls.SetChildIndex(this.txtLinkDocument, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DateTimePicker dtpDataDocument;
        protected System.Windows.Forms.Label lblDataDocument;
        protected System.Windows.Forms.TextBox txtNomerDocument;
        protected System.Windows.Forms.Label lblNomerDocument;
        protected System.Windows.Forms.TextBox txtNazvaDocument;
        protected System.Windows.Forms.Label lblNazvaDocument;
        protected System.Windows.Forms.TextBox txtKodObject;
        protected System.Windows.Forms.Label lblKodObject;
        protected System.Windows.Forms.Button btnDocument;
        protected System.Windows.Forms.TextBox txtDocument;
        protected System.Windows.Forms.Label lblDocument;
        protected System.Windows.Forms.TextBox txtOpysDocument;
        protected System.Windows.Forms.Label lblOpysDocument;
        protected System.Windows.Forms.TextBox txtLinkDocument;
        protected System.Windows.Forms.Label lbLinkDocument;
    }
}