using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
