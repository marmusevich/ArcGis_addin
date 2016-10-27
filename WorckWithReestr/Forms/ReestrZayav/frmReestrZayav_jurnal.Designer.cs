using System.Windows.Forms;

namespace WorckWithReestr
{
    partial class frmReestrZayav_jurnal
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
            this.cbHaveReferense = new System.Windows.Forms.ComboBox();
            this.cbReferenceClose = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dtpDataOt
            // 
            this.dtpDataOt.Value = new System.DateTime(2016, 10, 1, 0, 0, 0, 0);
            // 
            // dtpDatePo
            // 
            this.dtpDatePo.Value = new System.DateTime(2016, 10, 31, 0, 0, 0, 0);
            // 
            // cbHaveReferense
            // 
            this.cbHaveReferense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHaveReferense.Items.AddRange(new object[] {
            "Все",
            "Имеющие кадастровую справку",
            "Не имеющие кадастровую справку"});
            this.cbHaveReferense.Location = new System.Drawing.Point(424, 26);
            this.cbHaveReferense.Name = "cbHaveReferense";
            this.cbHaveReferense.Size = new System.Drawing.Size(121, 21);
            this.cbHaveReferense.TabIndex = 0;
            this.cbHaveReferense.SelectedIndexChanged += new System.EventHandler(this.cbHaveReferense_SelectedIndexChanged);
            // 
            // cbReferenceClose
            // 
            this.cbReferenceClose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReferenceClose.FormattingEnabled = true;
            this.cbReferenceClose.Items.AddRange(new object[] {
            "Все",
            "Закрыта справка",
            "Открыта справка"});
            this.cbReferenceClose.Location = new System.Drawing.Point(551, 26);
            this.cbReferenceClose.Name = "cbReferenceClose";
            this.cbReferenceClose.Size = new System.Drawing.Size(81, 21);
            this.cbReferenceClose.TabIndex = 0;
            this.cbReferenceClose.SelectedIndexChanged += new System.EventHandler(this.cbReferenceClose_SelectedIndexChanged);
            // 
            // frmReestrZayav_jurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = this.ClientSize;
            this.Controls.Add(this.cbHaveReferense);
            this.Controls.Add(this.cbReferenceClose);
            this.Name = "frmReestrZayav_jurnal";
            this.Text = "frmReestrZayav_jurnal";
            this.Controls.SetChildIndex(this.cbReferenceClose, 0);
            this.Controls.SetChildIndex(this.cbHaveReferense, 0);
            this.Controls.SetChildIndex(this.dtpDataOt, 0);
            this.Controls.SetChildIndex(this.dtpDatePo, 0);
            this.Controls.SetChildIndex(this.lblDataOt, 0);
            this.Controls.SetChildIndex(this.lblDatePo, 0);
            this.Controls.SetChildIndex(this.btnForvard, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ComboBox cbHaveReferense;
        private System.Windows.Forms.ComboBox cbReferenceClose;

        #endregion
    }
}