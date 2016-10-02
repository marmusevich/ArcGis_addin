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
            this.btnSetObject = new System.Windows.Forms.Button();
            this.btnCloseEdit = new System.Windows.Forms.Button();
            this.lblObjectMapIDDiscriptions = new System.Windows.Forms.Label();
            this.lblObjectMapID = new System.Windows.Forms.Label();
            this.llblClearData = new System.Windows.Forms.LinkLabel();
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
            this.tlpPages.Location = new System.Drawing.Point(4, 115);
            this.tlpPages.Name = "tlpPages";
            this.tlpPages.RowCount = 1;
            this.tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPages.Size = new System.Drawing.Size(319, 278);
            this.tlpPages.TabIndex = 0;
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(248, 481);
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
            this.pnRtf.Location = new System.Drawing.Point(3, 399);
            this.pnRtf.Name = "pnRtf";
            this.pnRtf.Size = new System.Drawing.Size(319, 63);
            this.pnRtf.TabIndex = 3;
            // 
            // lblRTF
            // 
            this.lblRTF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRTF.Location = new System.Drawing.Point(6, 4);
            this.lblRTF.Name = "lblRTF";
            this.lblRTF.Size = new System.Drawing.Size(308, 23);
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
            this.lblZayavka.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblZayavka.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblZayavka.Location = new System.Drawing.Point(4, 3);
            this.lblZayavka.Name = "lblZayavka";
            this.lblZayavka.Size = new System.Drawing.Size(319, 14);
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
            this.lblZayavkaDiscriptions.Location = new System.Drawing.Point(112, 21);
            this.lblZayavkaDiscriptions.Name = "lblZayavkaDiscriptions";
            this.lblZayavkaDiscriptions.Size = new System.Drawing.Size(211, 35);
            this.lblZayavkaDiscriptions.TabIndex = 5;
            this.lblZayavkaDiscriptions.Tag = "null";
            this.lblZayavkaDiscriptions.Text = "Заявка не указана";
            this.lblZayavkaDiscriptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnZayavkaChange
            // 
            this.btnZayavkaChange.Location = new System.Drawing.Point(4, 21);
            this.btnZayavkaChange.Name = "btnZayavkaChange";
            this.btnZayavkaChange.Size = new System.Drawing.Size(102, 35);
            this.btnZayavkaChange.TabIndex = 6;
            this.btnZayavkaChange.Text = "Выбрать";
            this.btnZayavkaChange.UseVisualStyleBackColor = true;
            this.btnZayavkaChange.Click += new System.EventHandler(this.ZayavkaChange_Click);
            // 
            // btnSetObject
            // 
            this.btnSetObject.Location = new System.Drawing.Point(4, 77);
            this.btnSetObject.Name = "btnSetObject";
            this.btnSetObject.Size = new System.Drawing.Size(102, 35);
            this.btnSetObject.TabIndex = 7;
            this.btnSetObject.Text = "Запомнить выделенный";
            this.btnSetObject.UseVisualStyleBackColor = true;
            this.btnSetObject.Click += new System.EventHandler(this.btnSetObject_Click);
            // 
            // btnCloseEdit
            // 
            this.btnCloseEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseEdit.ForeColor = System.Drawing.Color.Maroon;
            this.btnCloseEdit.Location = new System.Drawing.Point(4, 480);
            this.btnCloseEdit.Name = "btnCloseEdit";
            this.btnCloseEdit.Size = new System.Drawing.Size(152, 23);
            this.btnCloseEdit.TabIndex = 8;
            this.btnCloseEdit.Text = "Закрыть для изменения";
            this.btnCloseEdit.UseVisualStyleBackColor = true;
            this.btnCloseEdit.Click += new System.EventHandler(this.btnCloseEdit_Click);
            // 
            // lblObjectMapIDDiscriptions
            // 
            this.lblObjectMapIDDiscriptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObjectMapIDDiscriptions.BackColor = System.Drawing.SystemColors.Window;
            this.lblObjectMapIDDiscriptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblObjectMapIDDiscriptions.Location = new System.Drawing.Point(112, 77);
            this.lblObjectMapIDDiscriptions.Name = "lblObjectMapIDDiscriptions";
            this.lblObjectMapIDDiscriptions.Size = new System.Drawing.Size(211, 35);
            this.lblObjectMapIDDiscriptions.TabIndex = 9;
            this.lblObjectMapIDDiscriptions.Text = "Объект не указан";
            this.lblObjectMapIDDiscriptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblObjectMapID
            // 
            this.lblObjectMapID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObjectMapID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblObjectMapID.Location = new System.Drawing.Point(4, 60);
            this.lblObjectMapID.Name = "lblObjectMapID";
            this.lblObjectMapID.Size = new System.Drawing.Size(319, 14);
            this.lblObjectMapID.TabIndex = 10;
            this.lblObjectMapID.Text = "Объект на карте:";
            this.lblObjectMapID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // llblClearData
            // 
            this.llblClearData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llblClearData.AutoSize = true;
            this.llblClearData.LinkColor = System.Drawing.Color.Blue;
            this.llblClearData.Location = new System.Drawing.Point(284, 8);
            this.llblClearData.Name = "llblClearData";
            this.llblClearData.Size = new System.Drawing.Size(38, 13);
            this.llblClearData.TabIndex = 11;
            this.llblClearData.TabStop = true;
            this.llblClearData.Text = "Сброс";
            this.llblClearData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblClearData_LinkClicked);
            // 
            // arcDW_CadastralReference
            // 
            this.Controls.Add(this.llblClearData);
            this.Controls.Add(this.lblObjectMapID);
            this.Controls.Add(this.lblObjectMapIDDiscriptions);
            this.Controls.Add(this.btnCloseEdit);
            this.Controls.Add(this.btnSetObject);
            this.Controls.Add(this.btnZayavkaChange);
            this.Controls.Add(this.pnRtf);
            this.Controls.Add(this.lblZayavkaDiscriptions);
            this.Controls.Add(this.lblZayavka);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.tlpPages);
            this.Name = "arcDW_CadastralReference";
            this.Size = new System.Drawing.Size(330, 511);
            this.pnRtf.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button btnSetObject;
        private Button btnCloseEdit;
        private Label lblObjectMapIDDiscriptions;
        private Label lblObjectMapID;
        private LinkLabel llblClearData;
    }
}
