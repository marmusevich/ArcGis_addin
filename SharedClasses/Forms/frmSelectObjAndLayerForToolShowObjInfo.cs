using System;
using System.Windows.Forms;

namespace SharedClasses
{
    public partial class frmSelectObjAndLayerForToolShowObjInfo : Form
    {
        public frmSelectObjAndLayerForToolShowObjInfo()
        {
            InitializeComponent();
        }

        private void lsbSelectedLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
