﻿using System.Windows.Forms;
namespace SharedClasses
{

    //[System.ComponentModel.Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
    //public class myDataGridView : DataGridView
    //{
    //}
    
    partial class frmBaseSpr_list
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaseSpr_list));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAddCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsReflesh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbAddCopy = new System.Windows.Forms.ToolStripButton();
            this.tsdEdit = new System.Windows.Forms.ToolStripButton();
            this.tsdDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbReflesh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.cmsMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ContextMenuStrip = this.cmsMain;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.Location = new System.Drawing.Point(12, 28);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(263, 226);
            this.dgv.TabIndex = 4;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsAdd,
            this.cmsAddCopy,
            this.cmsEdit,
            this.cmsDelete,
            this.cmsReflesh});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(212, 136);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmsAdd.Image")));
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(211, 22);
            this.cmsAdd.Text = "Добавить";
            this.cmsAdd.Click += new System.EventHandler(this.cmsAdd_Click);
            // 
            // cmsAddCopy
            // 
            this.cmsAddCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmsAddCopy.Image")));
            this.cmsAddCopy.Name = "cmsAddCopy";
            this.cmsAddCopy.Size = new System.Drawing.Size(211, 22);
            this.cmsAddCopy.Text = "Добавить копированием";
            this.cmsAddCopy.Click += new System.EventHandler(this.cmsAddCopy_Click);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmsEdit.Image")));
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(211, 22);
            this.cmsEdit.Text = "Редактировать";
            this.cmsEdit.Click += new System.EventHandler(this.cmsEdit_Click);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmsDelete.Image")));
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(211, 22);
            this.cmsDelete.Text = "Удалить";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // cmsReflesh
            // 
            this.cmsReflesh.Image = ((System.Drawing.Image)(resources.GetObject("cmsReflesh.Image")));
            this.cmsReflesh.Name = "cmsReflesh";
            this.cmsReflesh.Size = new System.Drawing.Size(211, 22);
            this.cmsReflesh.Text = "Обновить";
            this.cmsReflesh.Click += new System.EventHandler(this.cmsReflesh_Click);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbAddCopy,
            this.tsdEdit,
            this.tsdDelete,
            this.tsbReflesh});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(287, 25);
            this.tsMain.TabIndex = 6;
            this.tsMain.Text = "Панель управления";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(79, 22);
            this.tsbAdd.Text = "Добавить";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbAddCopy
            // 
            this.tsbAddCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddCopy.Image")));
            this.tsbAddCopy.Name = "tsbAddCopy";
            this.tsbAddCopy.Size = new System.Drawing.Size(164, 22);
            this.tsbAddCopy.Text = "Добавить копированием";
            this.tsbAddCopy.Click += new System.EventHandler(this.tsbAddCopy_Click);
            // 
            // tsdEdit
            // 
            this.tsdEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsdEdit.Image")));
            this.tsdEdit.Name = "tsdEdit";
            this.tsdEdit.Size = new System.Drawing.Size(107, 22);
            this.tsdEdit.Text = "Редактировать";
            this.tsdEdit.Click += new System.EventHandler(this.tsdEdit_Click);
            // 
            // tsdDelete
            // 
            this.tsdDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsdDelete.Image")));
            this.tsdDelete.Name = "tsdDelete";
            this.tsdDelete.Size = new System.Drawing.Size(111, 20);
            this.tsdDelete.Text = "Удалить запись";
            this.tsdDelete.Click += new System.EventHandler(this.tsdDelete_Click);
            // 
            // tsbReflesh
            // 
            this.tsbReflesh.Image = ((System.Drawing.Image)(resources.GetObject("tsbReflesh.Image")));
            this.tsbReflesh.Name = "tsbReflesh";
            this.tsbReflesh.Size = new System.Drawing.Size(81, 20);
            this.tsbReflesh.Text = "Обновить";
            this.tsbReflesh.Click += new System.EventHandler(this.tsbReflesh_Click);
            // 
            // frmBaseSpr_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 266);
            this.ContextMenuStrip = this.cmsMain;
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.dgv);
            this.KeyPreview = true;
            this.Name = "frmBaseSpr_list";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBaseSpr_list_FormClosing);
            this.Load += new System.EventHandler(this.frmBaseSpr_list_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBaseSpr_list_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.cmsMain.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStripMenuItem cmsAdd;
        protected System.Windows.Forms.ToolStripButton tsbAdd;
        protected System.Windows.Forms.ToolStripButton tsdEdit;
        protected System.Windows.Forms.ToolStripButton tsdDelete;
        protected System.Windows.Forms.ToolStripButton tsbAddCopy;
        protected System.Windows.Forms.ToolStripButton tsbReflesh;
        protected System.Windows.Forms.ToolStripMenuItem cmsEdit;
        protected System.Windows.Forms.ToolStripMenuItem cmsDelete;
        protected System.Windows.Forms.ToolStripMenuItem cmsReflesh;
        protected System.Windows.Forms.ToolStripMenuItem cmsAddCopy;
        protected System.Windows.Forms.ContextMenuStrip cmsMain;
        protected System.Windows.Forms.ToolStrip tsMain;
        protected System.Windows.Forms.DataGridView dgv;
    }
}