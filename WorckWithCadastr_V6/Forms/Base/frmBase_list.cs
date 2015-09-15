using System;
using SharedClasses;
using ESRI.ArcGIS.Framework;

namespace WorckWithCadastr_V6
{
    public partial class frmBase_list :  frmBaseSpr_list
    {
        protected System.Windows.Forms.ToolStripMenuItem cmsShowOnMap;
        protected System.Windows.Forms.ToolStripButton tsdShowOnMap;


        
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
            //AdresReestrWork.ShowOnMap(NameTable);
        }

        private void tsbtsdShowOnMap_Click(object sender, EventArgs e)
        {
            //AdresReestrWork.ShowOnMap(NameTable);
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
            this.cmsShowOnMap = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsShowOnMap.Name = "cmsShowOnMap";
            this.cmsShowOnMap.Size = new System.Drawing.Size(173, 22);
            this.cmsShowOnMap.Text = "Показать на карте";
            this.cmsShowOnMap.Click += new System.EventHandler(this.cmsShowOnMap_Click);
            this.cmsMain.Items.Add(this.cmsShowOnMap);
            // 
            // tsdShowOnMap
            // 
            this.tsdShowOnMap = new System.Windows.Forms.ToolStripButton();
            this.tsdShowOnMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsdShowOnMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdShowOnMap.Name = "tsdShowOnMap";
            this.tsdShowOnMap.Size = new System.Drawing.Size(110, 22);
            this.tsdShowOnMap.Text = "Показать на карте";
            this.tsdShowOnMap.Click += new System.EventHandler(this.tsbtsdShowOnMap_Click);
            this.tsMain.Items.Add(tsdShowOnMap);

        }
    }
}
