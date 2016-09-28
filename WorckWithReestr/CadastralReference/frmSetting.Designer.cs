using System;
using System.Drawing;
using System.Windows.Forms;
namespace CadastralReference
{
    partial class frmSetting
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tcSetting = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.txtObjectLayerName = new System.Windows.Forms.TextBox();
            this.lblObjectLayerName = new System.Windows.Forms.Label();
            this.txtObjectTableName = new System.Windows.Forms.TextBox();
            this.lblObjectTableName = new System.Windows.Forms.Label();
            this.txtRukovoditelDoljnost = new System.Windows.Forms.TextBox();
            this.lblRukovoditelDoljnost = new System.Windows.Forms.Label();
            this.txtRukovoditelFIO = new System.Windows.Forms.TextBox();
            this.lblRukovoditelFIO = new System.Windows.Forms.Label();
            this.pnListOfPages = new System.Windows.Forms.Panel();
            this.btnAddPage = new System.Windows.Forms.Button();
            this.lblListOfPages = new System.Windows.Forms.Label();
            this.clbListOfPages = new System.Windows.Forms.CheckedListBox();
            this.tpRTF = new System.Windows.Forms.TabPage();
            this.tpPages = new System.Windows.Forms.TabPage();
            this.tcPages = new System.Windows.Forms.TabControl();
            this.tcSetting.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.pnListOfPages.SuspendLayout();
            this.tpPages.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(6, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(590, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tcSetting
            // 
            this.tcSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSetting.Controls.Add(this.tpMain);
            this.tcSetting.Controls.Add(this.tpRTF);
            this.tcSetting.Controls.Add(this.tpPages);
            this.tcSetting.Location = new System.Drawing.Point(6, 12);
            this.tcSetting.Name = "tcSetting";
            this.tcSetting.SelectedIndex = 0;
            this.tcSetting.Size = new System.Drawing.Size(659, 447);
            this.tcSetting.TabIndex = 2;
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.txtObjectLayerName);
            this.tpMain.Controls.Add(this.lblObjectLayerName);
            this.tpMain.Controls.Add(this.txtObjectTableName);
            this.tpMain.Controls.Add(this.lblObjectTableName);
            this.tpMain.Controls.Add(this.txtRukovoditelDoljnost);
            this.tpMain.Controls.Add(this.lblRukovoditelDoljnost);
            this.tpMain.Controls.Add(this.txtRukovoditelFIO);
            this.tpMain.Controls.Add(this.lblRukovoditelFIO);
            this.tpMain.Controls.Add(this.pnListOfPages);
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(651, 421);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Главное";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // txtObjectLayerName
            // 
            this.txtObjectLayerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjectLayerName.Enabled = false;
            this.txtObjectLayerName.Location = new System.Drawing.Point(349, 152);
            this.txtObjectLayerName.Name = "txtObjectLayerName";
            this.txtObjectLayerName.Size = new System.Drawing.Size(296, 20);
            this.txtObjectLayerName.TabIndex = 10;
            // 
            // lblObjectLayerName
            // 
            this.lblObjectLayerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObjectLayerName.Location = new System.Drawing.Point(349, 131);
            this.lblObjectLayerName.Name = "lblObjectLayerName";
            this.lblObjectLayerName.Size = new System.Drawing.Size(296, 20);
            this.lblObjectLayerName.TabIndex = 9;
            this.lblObjectLayerName.Text = "Имя слоя размещения объектов";
            // 
            // txtObjectTableName
            // 
            this.txtObjectTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjectTableName.Enabled = false;
            this.txtObjectTableName.Location = new System.Drawing.Point(349, 200);
            this.txtObjectTableName.Name = "txtObjectTableName";
            this.txtObjectTableName.Size = new System.Drawing.Size(296, 20);
            this.txtObjectTableName.TabIndex = 8;
            // 
            // lblObjectTableName
            // 
            this.lblObjectTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObjectTableName.Location = new System.Drawing.Point(349, 179);
            this.lblObjectTableName.Name = "lblObjectTableName";
            this.lblObjectTableName.Size = new System.Drawing.Size(296, 20);
            this.lblObjectTableName.TabIndex = 7;
            this.lblObjectTableName.Text = "Имя таблицы размещения объектов";
            // 
            // txtRukovoditelDoljnost
            // 
            this.txtRukovoditelDoljnost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRukovoditelDoljnost.Location = new System.Drawing.Point(349, 36);
            this.txtRukovoditelDoljnost.Name = "txtRukovoditelDoljnost";
            this.txtRukovoditelDoljnost.Size = new System.Drawing.Size(296, 20);
            this.txtRukovoditelDoljnost.TabIndex = 6;
            // 
            // lblRukovoditelDoljnost
            // 
            this.lblRukovoditelDoljnost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRukovoditelDoljnost.Location = new System.Drawing.Point(349, 15);
            this.lblRukovoditelDoljnost.Name = "lblRukovoditelDoljnost";
            this.lblRukovoditelDoljnost.Size = new System.Drawing.Size(296, 20);
            this.lblRukovoditelDoljnost.TabIndex = 5;
            this.lblRukovoditelDoljnost.Text = "Должность руководителя";
            // 
            // txtRukovoditelFIO
            // 
            this.txtRukovoditelFIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRukovoditelFIO.Location = new System.Drawing.Point(349, 84);
            this.txtRukovoditelFIO.Name = "txtRukovoditelFIO";
            this.txtRukovoditelFIO.Size = new System.Drawing.Size(296, 20);
            this.txtRukovoditelFIO.TabIndex = 4;
            // 
            // lblRukovoditelFIO
            // 
            this.lblRukovoditelFIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRukovoditelFIO.Location = new System.Drawing.Point(349, 63);
            this.lblRukovoditelFIO.Name = "lblRukovoditelFIO";
            this.lblRukovoditelFIO.Size = new System.Drawing.Size(296, 20);
            this.lblRukovoditelFIO.TabIndex = 3;
            this.lblRukovoditelFIO.Text = "ФИО руководителя";
            // 
            // pnListOfPages
            // 
            this.pnListOfPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnListOfPages.Controls.Add(this.btnAddPage);
            this.pnListOfPages.Controls.Add(this.lblListOfPages);
            this.pnListOfPages.Controls.Add(this.clbListOfPages);
            this.pnListOfPages.Location = new System.Drawing.Point(3, 3);
            this.pnListOfPages.Name = "pnListOfPages";
            this.pnListOfPages.Size = new System.Drawing.Size(340, 415);
            this.pnListOfPages.TabIndex = 2;
            // 
            // btnAddPage
            // 
            this.btnAddPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPage.Location = new System.Drawing.Point(118, 388);
            this.btnAddPage.Name = "btnAddPage";
            this.btnAddPage.Size = new System.Drawing.Size(101, 23);
            this.btnAddPage.TabIndex = 4;
            this.btnAddPage.Text = "Добавить лист";
            this.btnAddPage.UseVisualStyleBackColor = true;
            this.btnAddPage.Click += new System.EventHandler(this.btnAddPage_Click);
            // 
            // lblListOfPages
            // 
            this.lblListOfPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListOfPages.Location = new System.Drawing.Point(3, 3);
            this.lblListOfPages.Name = "lblListOfPages";
            this.lblListOfPages.Size = new System.Drawing.Size(334, 20);
            this.lblListOfPages.TabIndex = 3;
            this.lblListOfPages.Text = "Перечень листов справки";
            // 
            // clbListOfPages
            // 
            this.clbListOfPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbListOfPages.FormattingEnabled = true;
            this.clbListOfPages.Location = new System.Drawing.Point(3, 20);
            this.clbListOfPages.Name = "clbListOfPages";
            this.clbListOfPages.Size = new System.Drawing.Size(334, 364);
            this.clbListOfPages.TabIndex = 2;
            this.clbListOfPages.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbListOfPages_ItemCheck);
            // 
            // tpRTF
            // 
            this.tpRTF.Location = new System.Drawing.Point(4, 22);
            this.tpRTF.Name = "tpRTF";
            this.tpRTF.Padding = new System.Windows.Forms.Padding(3);
            this.tpRTF.Size = new System.Drawing.Size(651, 421);
            this.tpRTF.TabIndex = 1;
            this.tpRTF.Text = "Текстовый документ";
            this.tpRTF.UseVisualStyleBackColor = true;
            // 
            // tpPages
            // 
            this.tpPages.Controls.Add(this.tcPages);
            this.tpPages.Location = new System.Drawing.Point(4, 22);
            this.tpPages.Name = "tpPages";
            this.tpPages.Padding = new System.Windows.Forms.Padding(3);
            this.tpPages.Size = new System.Drawing.Size(651, 421);
            this.tpPages.TabIndex = 2;
            this.tpPages.Text = "Графические листы";
            this.tpPages.UseVisualStyleBackColor = true;
            // 
            // tcPages
            // 
            this.tcPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPages.Location = new System.Drawing.Point(3, 3);
            this.tcPages.Name = "tcPages";
            this.tcPages.SelectedIndex = 0;
            this.tcPages.Size = new System.Drawing.Size(645, 415);
            this.tcPages.TabIndex = 0;
            // 
            // frmSetting
            // 
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(672, 493);
            this.Controls.Add(this.tcSetting);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки кадастровой справки";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.tcSetting.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.pnListOfPages.ResumeLayout(false);
            this.tpPages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private TabControl tcSetting;
        private TabPage tpMain;
        private TabPage tpRTF;
        private TabPage tpPages;
        private TabControl tcPages;
        private Panel pnListOfPages;
        private Label lblListOfPages;
        private CheckedListBox clbListOfPages;
        private Button btnAddPage;
        private TextBox txtRukovoditelDoljnost;
        private Label lblRukovoditelDoljnost;
        private TextBox txtRukovoditelFIO;
        private Label lblRukovoditelFIO;
        private TextBox txtObjectLayerName;
        private Label lblObjectLayerName;
        private TextBox txtObjectTableName;
        private Label lblObjectTableName;
    }
}