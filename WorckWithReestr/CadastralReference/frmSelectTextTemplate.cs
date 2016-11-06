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
            if(lbTexts.SelectedIndex != -1)
            {
                ReturnString = (string)lbTexts.SelectedItem;
                DialogResult = DialogResult.OK;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbTexts.SelectedIndex != -1)
            {
                ReturnString = (string)lbTexts.SelectedItem;
                DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void frmSelectTextTemplate_Load(object sender, EventArgs e)
        {
            foreach(string str in WorkCadastralReference.GetCadastralReferenceData().Body_Template)
            {
                lbTexts.Items.Add(TextTemplateConverter.Convert(str));
            }
        }
    }
}
