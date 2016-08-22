using System.Windows.Forms;
namespace SharedClasses
{
    partial class frmBaseJurnal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaseJurnal));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbAddCopy = new System.Windows.Forms.ToolStripButton();
            this.tsdEdit = new System.Windows.Forms.ToolStripButton();
            this.tsdDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbReflesh = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAddCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsReflesh = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpDataOt = new System.Windows.Forms.DateTimePicker();
            this.dtpDatePo = new System.Windows.Forms.DateTimePicker();
            this.lblDataOt = new System.Windows.Forms.Label();
            this.lblDatePo = new System.Windows.Forms.Label();
            this.btnForvard = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
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
            this.tsMain.Size = new System.Drawing.Size(544, 25);
            this.tsMain.TabIndex = 8;
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
            this.tsdDelete.Size = new System.Drawing.Size(111, 22);
            this.tsdDelete.Text = "Удалить запись";
            this.tsdDelete.Click += new System.EventHandler(this.tsdDelete_Click);
            // 
            // tsbReflesh
            // 
            this.tsbReflesh.Image = ((System.Drawing.Image)(resources.GetObject("tsbReflesh.Image")));
            this.tsbReflesh.Name = "tsbReflesh";
            this.tsbReflesh.Size = new System.Drawing.Size(81, 22);
            this.tsbReflesh.Text = "Обновить";
            this.tsbReflesh.Click += new System.EventHandler(this.tsbReflesh_Click);
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
            this.dgv.Location = new System.Drawing.Point(12, 54);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(520, 209);
            this.dgv.TabIndex = 7;
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
            // dtpDataOt
            // 
            this.dtpDataOt.Location = new System.Drawing.Point(91, 27);
            this.dtpDataOt.Name = "dtpDataOt";
            this.dtpDataOt.Size = new System.Drawing.Size(119, 20);
            this.dtpDataOt.TabIndex = 9;
            this.dtpDataOt.ValueChanged += new System.EventHandler(this.dtpDataOt_ValueChanged);
            // 
            // dtpDatePo
            // 
            this.dtpDatePo.Location = new System.Drawing.Point(242, 27);
            this.dtpDatePo.Name = "dtpDatePo";
            this.dtpDatePo.Size = new System.Drawing.Size(119, 20);
            this.dtpDatePo.TabIndex = 10;
            this.dtpDatePo.ValueChanged += new System.EventHandler(this.dtpDatePo_ValueChanged);
            // 
            // lblDataOt
            // 
            this.lblDataOt.Location = new System.Drawing.Point(43, 29);
            this.lblDataOt.Name = "lblDataOt";
            this.lblDataOt.Size = new System.Drawing.Size(43, 20);
            this.lblDataOt.TabIndex = 11;
            this.lblDataOt.Text = "Дата с";
            // 
            // lblDatePo
            // 
            this.lblDatePo.Location = new System.Drawing.Point(216, 29);
            this.lblDatePo.Name = "lblDatePo";
            this.lblDatePo.Size = new System.Drawing.Size(19, 20);
            this.lblDatePo.TabIndex = 12;
            this.lblDatePo.Text = "по";
            // 
            // btnForvard
            // 
            this.btnForvard.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnForvard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnForvard.ForeColor = System.Drawing.Color.Yellow;
            this.btnForvard.Location = new System.Drawing.Point(368, 26);
            this.btnForvard.Name = "btnForvard";
            this.btnForvard.Size = new System.Drawing.Size(32, 23);
            this.btnForvard.TabIndex = 13;
            this.btnForvard.Tag = "+1";
            this.btnForvard.Text = ">>";
            this.btnForvard.UseVisualStyleBackColor = false;
            this.btnForvard.Click += new System.EventHandler(this.btnChangePeriod);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.ForeColor = System.Drawing.Color.Yellow;
            this.btnBack.Location = new System.Drawing.Point(8, 26);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(32, 23);
            this.btnBack.TabIndex = 14;
            this.btnBack.Tag = "-1";
            this.btnBack.Text = "<<";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnChangePeriod);
            // 
            // frmBaseJurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 275);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnForvard);
            this.Controls.Add(this.lblDatePo);
            this.Controls.Add(this.lblDataOt);
            this.Controls.Add(this.dtpDatePo);
            this.Controls.Add(this.dtpDataOt);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.dgv);
            this.KeyPreview = true;
            this.Name = "frmBaseJurnal";
            this.ShowIcon = false;
            this.Text = "frmBaseJurnal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBaseJurnal_FormClosing);
            this.Load += new System.EventHandler(this.frmBaseJurnal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBaseJurnal_KeyDown);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected ToolStrip tsMain;
        protected ToolStripButton tsbAdd;
        protected ToolStripButton tsdEdit;
        protected ToolStripButton tsdDelete;
        protected ToolStripButton tsbAddCopy;
        protected ToolStripButton tsbReflesh;
        protected DataGridView dgv;
        protected ContextMenuStrip cmsMain;
        protected ToolStripMenuItem cmsAdd;
        protected ToolStripMenuItem cmsEdit;
        protected ToolStripMenuItem cmsDelete;
        protected ToolStripMenuItem cmsReflesh;
        protected ToolStripMenuItem cmsAddCopy;

        protected DateTimePicker dtpDataOt;
        protected DateTimePicker dtpDatePo;
        protected Label lblDataOt;
        protected Label lblDatePo;
        protected Button btnForvard;
        protected Button btnBack;
    }
}
