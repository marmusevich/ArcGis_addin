using System;
using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr2016
{
    public partial class frmBase_list :  frmBaseSpr_list
    {
        protected System.Windows.Forms.ToolStripMenuItem cmsShowOnMap;
        protected System.Windows.Forms.ToolStripButton tsdShowOnMap;


        //добавить запись
        protected override void AddRec()
        {
            MessageBox.Show("Добавление новых записей только средствами ArkGIS", "Функция заблокирована", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //удалить запись
        protected override void DeleteRec(int objectID)
        {
            MessageBox.Show("Удаление записей только средствами ArkGIS", "Функция заблокирована", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        public frmBase_list()
            : base()
        {
            InitializeComponent();
        }

        public frmBase_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();
        }


        private void cmsShowOnMap_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                GeneralMapWork.ShowOnMap(table, id);
            }
        }

        private void tsbtsdShowOnMap_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                GeneralMapWork.ShowOnMap(table, id);
            }
        }

        private void frmBase_list_Load(object sender, EventArgs e)
        {
            // 
            // dgv
            // 
            //this.dgv.Size = new System.Drawing.Size(396, 261);
            // 
            // cmsShowOnMap
            // 
            cmsShowOnMap = new System.Windows.Forms.ToolStripMenuItem();
            cmsShowOnMap.Name = "cmsShowOnMap";
            cmsShowOnMap.Size = new System.Drawing.Size(173, 22);
            cmsShowOnMap.Text = "Показать на карте";
            cmsShowOnMap.Click += new System.EventHandler(cmsShowOnMap_Click);
            cmsMain.Items.Add(cmsShowOnMap);
            // 
            // tsdShowOnMap
            // 
            tsdShowOnMap = new System.Windows.Forms.ToolStripButton();
            tsdShowOnMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsdShowOnMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsdShowOnMap.Name = "tsdShowOnMap";
            tsdShowOnMap.Size = new System.Drawing.Size(110, 22);
            tsdShowOnMap.Text = "Показать на карте";
            tsdShowOnMap.Click += new System.EventHandler(tsbtsdShowOnMap_Click);
            tsMain.Items.Add(tsdShowOnMap);

            //this.tsbAdd.Enabled = false;
            //this.tsdDelete.Enabled = false;
            //this.cmsAdd.Enabled = false;
            //this.cmsDelete.Enabled = false;
        }
    }
}
