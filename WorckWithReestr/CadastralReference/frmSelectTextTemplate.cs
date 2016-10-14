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
    public partial class frmSelectTextTemplate : Form
    {
        public string ReturnString = "";

        public frmSelectTextTemplate()
        {
            InitializeComponent();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ReturnString = (string)lbTexts.SelectedItem;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ReturnString = (string)lbTexts.SelectedItem;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmSelectTextTemplate_Load(object sender, EventArgs e)
        {

        }
    }
}
