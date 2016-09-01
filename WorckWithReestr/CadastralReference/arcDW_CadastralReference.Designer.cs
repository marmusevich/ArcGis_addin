using System;
using System.Drawing;
using System.Windows.Forms;


namespace WorckWithReestr
{
    partial class arcDW_CadastralReference
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
            this.tlpPages = new System.Windows.Forms.TableLayoutPanel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.pnRtf = new System.Windows.Forms.Panel();
            this.lblRTF = new System.Windows.Forms.Label();
            this.btnRTFPrev = new System.Windows.Forms.Button();
            this.btnRTFSaveToDB = new System.Windows.Forms.Button();
            this.btnRtfGenerate = new System.Windows.Forms.Button();
            this.lblZayavka = new System.Windows.Forms.Label();
            this.lblZayavkaDiscriptions = new System.Windows.Forms.Label();
            this.btnZayavkaChange = new System.Windows.Forms.Button();
            this.pnRtf.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpPages
            // 
            this.tlpPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpPages.AutoScroll = true;
            this.tlpPages.BackColor = System.Drawing.SystemColors.Control;
            this.tlpPages.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlpPages.ColumnCount = 1;
            this.tlpPages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPages.Location = new System.Drawing.Point(4, 32);
            this.tlpPages.Name = "tlpPages";
            this.tlpPages.RowCount = 1;
            this.tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPages.Size = new System.Drawing.Size(449, 358);
            this.tlpPages.TabIndex = 0;
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(378, 481);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Text = "Настройки";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // pnRtf
            // 
            this.pnRtf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnRtf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnRtf.Controls.Add(this.lblRTF);
            this.pnRtf.Controls.Add(this.btnRTFPrev);
            this.pnRtf.Controls.Add(this.btnRTFSaveToDB);
            this.pnRtf.Controls.Add(this.btnRtfGenerate);
            this.pnRtf.Location = new System.Drawing.Point(4, 396);
            this.pnRtf.Name = "pnRtf";
            this.pnRtf.Size = new System.Drawing.Size(449, 79);
            this.pnRtf.TabIndex = 3;
            // 
            // lblRTF
            // 
            this.lblRTF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRTF.Location = new System.Drawing.Point(6, 4);
            this.lblRTF.Name = "lblRTF";
            this.lblRTF.Size = new System.Drawing.Size(438, 23);
            this.lblRTF.TabIndex = 3;
            this.lblRTF.Text = "RTF";
            this.lblRTF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRTFPrev
            // 
            this.btnRTFPrev.Location = new System.Drawing.Point(219, 30);
            this.btnRTFPrev.Name = "btnRTFPrev";
            this.btnRTFPrev.Size = new System.Drawing.Size(93, 23);
            this.btnRTFPrev.TabIndex = 2;
            this.btnRTFPrev.Text = "Просмотр";
            this.btnRTFPrev.UseVisualStyleBackColor = true;
            this.btnRTFPrev.Click += new System.EventHandler(this.btnRTFPrev_Click);
            // 
            // btnRTFSaveToDB
            // 
            this.btnRTFSaveToDB.Location = new System.Drawing.Point(107, 30);
            this.btnRTFSaveToDB.Name = "btnRTFSaveToDB";
            this.btnRTFSaveToDB.Size = new System.Drawing.Size(106, 23);
            this.btnRTFSaveToDB.TabIndex = 1;
            this.btnRTFSaveToDB.Text = "Сохранить в ДБ";
            this.btnRTFSaveToDB.UseVisualStyleBackColor = true;
            this.btnRTFSaveToDB.Click += new System.EventHandler(this.btnRTFSaveToDB_Click);
            // 
            // btnRtfGenerate
            // 
            this.btnRtfGenerate.Location = new System.Drawing.Point(6, 30);
            this.btnRtfGenerate.Name = "btnRtfGenerate";
            this.btnRtfGenerate.Size = new System.Drawing.Size(95, 23);
            this.btnRtfGenerate.TabIndex = 0;
            this.btnRtfGenerate.Text = "Генерировать";
            this.btnRtfGenerate.UseVisualStyleBackColor = true;
            this.btnRtfGenerate.Click += new System.EventHandler(this.btnRtfGenerate_Click);
            // 
            // lblZayavka
            // 
            this.lblZayavka.Location = new System.Drawing.Point(1, 3);
            this.lblZayavka.Name = "lblZayavka";
            this.lblZayavka.Size = new System.Drawing.Size(52, 23);
            this.lblZayavka.TabIndex = 4;
            this.lblZayavka.Text = "Заявка:";
            this.lblZayavka.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZayavkaDiscriptions
            // 
            this.lblZayavkaDiscriptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblZayavkaDiscriptions.BackColor = System.Drawing.SystemColors.Window;
            this.lblZayavkaDiscriptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblZayavkaDiscriptions.Location = new System.Drawing.Point(59, 3);
            this.lblZayavkaDiscriptions.Name = "lblZayavkaDiscriptions";
            this.lblZayavkaDiscriptions.Size = new System.Drawing.Size(313, 23);
            this.lblZayavkaDiscriptions.TabIndex = 5;
            this.lblZayavkaDiscriptions.Tag = "null";
            this.lblZayavkaDiscriptions.Text = "Нажмите что бы выбрать заявку";
            this.lblZayavkaDiscriptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblZayavkaDiscriptions.Click += new System.EventHandler(this.ZayavkaChange_Click);
            // 
            // btnZayavkaChange
            // 
            this.btnZayavkaChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZayavkaChange.Location = new System.Drawing.Point(379, 3);
            this.btnZayavkaChange.Name = "btnZayavkaChange";
            this.btnZayavkaChange.Size = new System.Drawing.Size(75, 23);
            this.btnZayavkaChange.TabIndex = 6;
            this.btnZayavkaChange.Text = "Выбрать";
            this.btnZayavkaChange.UseVisualStyleBackColor = true;
            this.btnZayavkaChange.Click += new System.EventHandler(this.ZayavkaChange_Click);
            // 
            // arcDW_CadastralReference
            // 
            this.Controls.Add(this.btnZayavkaChange);
            this.Controls.Add(this.pnRtf);
            this.Controls.Add(this.lblZayavkaDiscriptions);
            this.Controls.Add(this.lblZayavka);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.tlpPages);
            this.Name = "arcDW_CadastralReference";
            this.Size = new System.Drawing.Size(460, 511);
            this.pnRtf.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    private TableLayoutPanel tlpPages;
    private Button btnSetting;
    private Panel pnRtf;
    private Label lblRTF;
    private Button btnRTFPrev;
    private Button btnRTFSaveToDB;
    private Button btnRtfGenerate;
    private Label lblZayavka;
    private Label lblZayavkaDiscriptions;
    private Button btnZayavkaChange;
    }
}
