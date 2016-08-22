using System.Windows.Forms;

namespace WorckWithReestr
{
    partial class frmReestrVedomostey_jurnal
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
            this.SuspendLayout();
            // 
            // dtpDataOt
            // 
            this.dtpDataOt.Value = new System.DateTime(2016, 8, 1, 0, 0, 0, 0);
            // 
            // dtpDatePo
            // 
            this.dtpDatePo.Value = new System.DateTime(2016, 8, 31, 0, 0, 0, 0);
            // 
            // frmReestrVedomostey_jurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 275);
            this.Name = "frmReestrVedomostey_jurnal";
            this.Text = "frmReestrVedomostey_jurnal";
            this.Controls.SetChildIndex(this.dtpDataOt, 0);
            this.Controls.SetChildIndex(this.dtpDatePo, 0);
            this.Controls.SetChildIndex(this.lblDataOt, 0);
            this.Controls.SetChildIndex(this.lblDatePo, 0);
            this.Controls.SetChildIndex(this.btnForvard, 0);
            this.Controls.SetChildIndex(this.btnBack, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}