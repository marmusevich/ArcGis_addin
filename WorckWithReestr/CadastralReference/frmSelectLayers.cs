using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Carto;
using WorckWithReestr;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CadastralReference
{
    public partial class frmSelectLayers : Form
    {

        public StringCollection retVal = null;

        public frmSelectLayers()
        {
            InitializeComponent();
        }

        private void frmSelectLayers_Load(object sender, EventArgs e)
        {
            StringCollection tmp = WorkCadastralReference_MAP.GetListOfAllLaers();
            if (tmp == null || tmp.Count == 0)
            {
                MessageBox.Show("В карте нет слоев. \n Добавте слои на карту.");
                DialogResult = DialogResult.Abort;
                this.Close();

            }
            String[] arr = new String[tmp.Count];
            tmp.CopyTo(arr, 0);
            lbAllLayers.Items.AddRange(arr);

            if (retVal != null)
            {
                String[] ar = new String[retVal.Count];
                retVal.CopyTo(arr, 0);
                lbSelectedLayers.Items.AddRange(ar);
            }


        }

        private void Add()
        {
            string tmp = (string)lbAllLayers.SelectedItem;
            if (tmp != null)
            {
                lbSelectedLayers.Items.Add(tmp);
                lbAllLayers.Items.Remove(tmp);
                lbAllLayers.SelectedIndex = -1;
            }
        }

        private void Del()
        {
            string tmp = (string)lbSelectedLayers.SelectedItem;
            if (tmp != null)
            {
                lbAllLayers.Items.Add(tmp);
                lbSelectedLayers.Items.Remove(tmp);
                lbSelectedLayers.SelectedIndex = -1;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(retVal == null)
                retVal = new StringCollection();

            foreach (object o in lbSelectedLayers.Items)
            {
                string tmp = (string)o;
                if (tmp != null)
                { 
                    retVal.Add(tmp);
                }
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void lbAllLayers_DoubleClick(object sender, EventArgs e)
        {
            Add();
        }

        private void lbSelectedLayers_DoubleClick(object sender, EventArgs e)
        {
            Del();
        }

    }
}
