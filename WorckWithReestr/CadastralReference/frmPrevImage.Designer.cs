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
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrinterSetting = new System.Windows.Forms.Button();
            this.prntPrintDialog = new System.Windows.Forms.PrintDialog();
            this.prntPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.prntPrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.prntPageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.btnPageSetap = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPrev
            // 
            this.pbPrev.BackColor = System.Drawing.Color.White;
            this.pbPrev.Location = new System.Drawing.Point(0, 32);
            this.pbPrev.Name = "pbPrev";
            this.pbPrev.Size = new System.Drawing.Size(794, 1122);
            this.pbPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPrev.TabIndex = 2;
            this.pbPrev.TabStop = false;
            this.pbPrev.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(0, 3);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(98, 23);
            this.btnPrintPreview.TabIndex = 3;
            this.btnPrintPreview.Text = "Предпромотр";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrinterSetting
            // 
            this.btnPrinterSetting.Location = new System.Drawing.Point(104, 3);
            this.btnPrinterSetting.Name = "btnPrinterSetting";
            this.btnPrinterSetting.Size = new System.Drawing.Size(136, 23);
            this.btnPrinterSetting.TabIndex = 4;
            this.btnPrinterSetting.Text = "Печать";
            this.btnPrinterSetting.UseVisualStyleBackColor = true;
            this.btnPrinterSetting.Click += new System.EventHandler(this.btnPrinterSetting_Click);
            // 
            // prntPrintDialog
            // 
            this.prntPrintDialog.AllowPrintToFile = false;
            this.prntPrintDialog.Document = this.prntPrintDocument;
            this.prntPrintDialog.UseEXDialog = true;
            // 
            // prntPrintDocument
            // 
            this.prntPrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.prntPprintDocument_EndPrint);
            this.prntPrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prntPprintDocument_PrintPage);
            // 
            // prntPrintPreviewDialog
            // 
            this.prntPrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.prntPrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.prntPrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.prntPrintPreviewDialog.Document = this.prntPrintDocument;
            this.prntPrintPreviewDialog.Enabled = true;
            this.prntPrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("prntPrintPreviewDialog.Icon")));
            this.prntPrintPreviewDialog.Name = "prntPrintPreviewDialog";
            this.prntPrintPreviewDialog.ShowIcon = false;
            this.prntPrintPreviewDialog.Visible = false;
            // 
            // prntPageSetupDialog
            // 
            this.prntPageSetupDialog.Document = this.prntPrintDocument;
            // 
            // btnPageSetap
            // 
            this.btnPageSetap.Location = new System.Drawing.Point(246, 3);
            this.btnPageSetap.Name = "btnPageSetap";
            this.btnPageSetap.Size = new System.Drawing.Size(136, 23);
            this.btnPageSetap.TabIndex = 5;
            this.btnPageSetap.Text = "Параметры страницы";
            this.btnPageSetap.UseVisualStyleBackColor = true;
            this.btnPageSetap.Click += new System.EventHandler(this.btnPageSetap_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(388, 3);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(134, 23);
            this.btnSaveAs.TabIndex = 6;
            this.btnSaveAs.Text = "Сохранить на диск";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // frmPrevImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(548, 362);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnPageSetap);
            this.Controls.Add(this.btnPrinterSetting);
            this.Controls.Add(this.btnPrintPreview);
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
        private Button btnPrintPreview;
        private Button btnPrinterSetting;
        private PrintDialog prntPrintDialog;
        private PrintPreviewDialog prntPrintPreviewDialog;
        private PageSetupDialog prntPageSetupDialog;
        private System.Drawing.Printing.PrintDocument prntPrintDocument;
        private Button btnPageSetap;
        private Button btnSaveAs;




    }
}