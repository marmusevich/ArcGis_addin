namespace CadastralReference
{
    partial class frmEditHTML
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
            this.tbHTML = new GvS.Controls.HtmlTextbox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHelpTemplate = new System.Windows.Forms.Button();
            this.btnAddTemplate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbHTML
            // 
            this.tbHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHTML.Fonts = new string[] {
        "Corbel",
        "Corbel, Verdana, Arial, Helvetica, sans-serif",
        "Georgia, Times New Roman, Times, serif",
        "Consolas, Courier New, Courier, monospace"};
            this.tbHTML.IllegalPatterns = new string[] {
        "<script.*?>",
        "<\\w+\\s+.*?(j|java|vb|ecma)script:.*?>",
        "<\\w+(\\s+|\\s+.*?\\s+)on\\w+\\s*=.+?>",
        "</?input.*?>"};
            this.tbHTML.Location = new System.Drawing.Point(-1, 35);
            this.tbHTML.Name = "tbHTML";
            this.tbHTML.Padding = new System.Windows.Forms.Padding(1);
            this.tbHTML.ShowHtmlSource = false;
            this.tbHTML.Size = new System.Drawing.Size(736, 448);
            this.tbHTML.TabIndex = 0;
            this.tbHTML.ToolbarStyle = GvS.Controls.ToolbarStyles.AlwaysInternal;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(647, 6);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnHelpTemplate
            // 
            this.btnHelpTemplate.Location = new System.Drawing.Point(145, 6);
            this.btnHelpTemplate.Name = "btnHelpTemplate";
            this.btnHelpTemplate.Size = new System.Drawing.Size(116, 23);
            this.btnHelpTemplate.TabIndex = 9;
            this.btnHelpTemplate.Text = "Описание шаблона";
            this.btnHelpTemplate.UseVisualStyleBackColor = true;
            this.btnHelpTemplate.Click += new System.EventHandler(this.btnHelpTemplate_Click);
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.Location = new System.Drawing.Point(347, 6);
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.Size = new System.Drawing.Size(144, 23);
            this.btnAddTemplate.TabIndex = 10;
            this.btnAddTemplate.Text = "Добавить из заготовки";
            this.btnAddTemplate.UseVisualStyleBackColor = true;
            this.btnAddTemplate.Click += new System.EventHandler(this.btnAddTemplate_Click);
            // 
            // frmEditHTML
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(734, 484);
            this.Controls.Add(this.btnAddTemplate);
            this.Controls.Add(this.btnHelpTemplate);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbHTML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmEditHTML";
            this.Text = "Редактирование текста...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        public GvS.Controls.HtmlTextbox tbHTML;
        private System.Windows.Forms.Button btnHelpTemplate;
        public System.Windows.Forms.Button btnAddTemplate;
    }
}