using System;
using System.Drawing;
using System.Windows.Forms;


namespace CadastralReference
{
    partial class frmPrevImage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param NameFromDB="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.pbPrev = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPrev
            // 
            this.pbPrev.BackColor = System.Drawing.Color.White;
            this.pbPrev.Location = new System.Drawing.Point(0, 0);
            this.pbPrev.Name = "pbPrev";
            this.pbPrev.Size = new System.Drawing.Size(794, 1122);
            this.pbPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPrev.TabIndex = 2;
            this.pbPrev.TabStop = false;
            this.pbPrev.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // frmPrevImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(409, 364);
            this.Controls.Add(this.pbPrev);
            this.MinimizeBox = false;
            this.Name = "frmPrevImage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPrevImage";
            this.Load += new System.EventHandler(this.frmPrevImage_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrevImage_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbPrev)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPrev;




    }
}