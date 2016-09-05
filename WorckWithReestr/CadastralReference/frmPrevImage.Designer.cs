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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrevImage));
            this.pbPrev = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPageSetup = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.prntPageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.prntPrintDialog = new System.Windows.Forms.PrintDialog();
            this.prntDocument = new System.Drawing.Printing.PrintDocument();
            this.prntPrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPrev
            // 
            this.pbPrev.BackColor = System.Drawing.Color.White;
            this.pbPrev.Location = new System.Drawing.Point(2, 31);
            this.pbPrev.Name = "pbPrev";
            this.pbPrev.Size = new System.Drawing.Size(794, 1122);
            this.pbPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPrev.TabIndex = 2;
            this.pbPrev.TabStop = false;
            this.pbPrev.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(144, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить изображение";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.Location = new System.Drawing.Point(152, 2);
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Size = new System.Drawing.Size(135, 23);
            this.btnPageSetup.TabIndex = 4;
            this.btnPageSetup.Text = "Параметры страницы";
            this.btnPageSetup.UseVisualStyleBackColor = true;
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(293, 2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(99, 23);
            this.btnPreview.TabIndex = 5;
            this.btnPreview.Text = "Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(398, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // prntPageSetupDialog
            // 
            this.prntPageSetupDialog.Document = this.prntDocument;
            // 
            // prntPrintDialog
            // 
            this.prntPrintDialog.Document = this.prntDocument;
            this.prntPrintDialog.UseEXDialog = true;
            // 
            // prntDocument
            // 
            this.prntDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.prntDocument_EndPrint);
            this.prntDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prntDocument_PrintPage);
            // 
            // prntPrintPreviewDialog
            // 
            this.prntPrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.prntPrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.prntPrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.prntPrintPreviewDialog.Document = this.prntDocument;
            this.prntPrintPreviewDialog.Enabled = true;
            this.prntPrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("prntPrintPreviewDialog.Icon")));
            this.prntPrintPreviewDialog.Name = "prntPrintPreviewDialog";
            this.prntPrintPreviewDialog.ShowIcon = false;
            this.prntPrintPreviewDialog.Visible = false;
            // 
            // frmPrevImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(580, 364);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnPageSetup);
            this.Controls.Add(this.btnSave);
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
        private Button btnSave;
        private Button btnPageSetup;
        private Button btnPreview;
        private Button btnPrint;
        private PageSetupDialog prntPageSetupDialog;
        private System.Drawing.Printing.PrintDocument prntDocument;
        private PrintDialog prntPrintDialog;
        private PrintPreviewDialog prntPrintPreviewDialog;
    }
}