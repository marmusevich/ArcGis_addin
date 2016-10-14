using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmEditHTML : Form
    {
        public frmEditHTML()
        {
            InitializeComponent();
            tbHTML.Focus();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnHelpTemplate_Click(object sender, EventArgs e)
        {
            //Form frm = new frmHelpTemplateView();
            //frm.ShowDialog();
        }

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            frmSelectTextTemplate frm = new frmSelectTextTemplate();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //html = frm.tbHTML.Text;
            }
        }
    }
}
