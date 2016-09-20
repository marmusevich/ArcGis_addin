namespace CadastralReference
{
    partial class frmHelpTemplateView
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblOpisanie = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(148, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblOpisanie
            // 
            this.lblOpisanie.BackColor = System.Drawing.SystemColors.Window;
            this.lblOpisanie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOpisanie.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOpisanie.Location = new System.Drawing.Point(0, 0);
            this.lblOpisanie.Name = "lblOpisanie";
            this.lblOpisanie.Size = new System.Drawing.Size(372, 239);
            this.lblOpisanie.TabIndex = 1;
            this.lblOpisanie.Text = "{_МаштабКарты_} - текущий маштаб карты;\r\n{_ОписательныйАдрес_} - адресс из заявки" +
    "";
            // 
            // frmHelpTemplateView
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(372, 277);
            this.Controls.Add(this.lblOpisanie);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmHelpTemplateView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Описание переменных шаблона";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblOpisanie;
    }
}