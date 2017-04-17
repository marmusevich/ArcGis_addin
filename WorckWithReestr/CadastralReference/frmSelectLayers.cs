using System.Windows.Forms;
using System;
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
                MessageBox.Show("В карте нет слоев. \n Добавьте слои на карту.");
                DialogResult = DialogResult.Abort;
                this.Close();

            }
            String[] arr = new String[tmp.Count];
            tmp.CopyTo(arr, 0);
            lbAllLayers.Items.AddRange(arr);

            if (retVal != null)
            {
                String[] ar = new String[retVal.Count];
                retVal.CopyTo(ar, 0);
                lbSelectedLayers.Items.AddRange(ar);

                // убрать выбранное
                foreach (object o in lbSelectedLayers.Items)
                {
                    if (lbAllLayers.Items.Contains(o))
                    {
                        lbAllLayers.Items.Remove(o);
                    }
                }

            }

            
        }

        private void Add()
        {
            System.Array arr = new object[lbAllLayers.SelectedItems.Count];
            lbAllLayers.SelectedItems.CopyTo(arr, 0);
            foreach (object o in arr)
            {
                if (o != null)
                {
                    lbSelectedLayers.Items.Add(o);
                    lbAllLayers.Items.Remove(o);
                }
            }
        }

        private void Del()
        {
            System.Array arr = new object[lbSelectedLayers.SelectedItems.Count];
            lbSelectedLayers.SelectedItems.CopyTo(arr, 0);
            foreach (object o in arr)
            {
                if (o != null)
                {
                    lbAllLayers.Items.Add(o);
                    lbSelectedLayers.Items.Remove(o);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(retVal == null)
                retVal = new StringCollection();

            retVal.Clear();

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
