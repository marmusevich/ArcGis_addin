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
    public partial class InputBox : Form
    {
        public string Value = "";

        public InputBox( string caption, string value="")
        {
            InitializeComponent();
            this.Text = caption;
            lblCaption.Text = caption;
            Value = value;
            txtValue.Focus();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            txtValue.Focus();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Value = txtValue.Text;
            this.Close();

        }
    }
}
