using System;
using System.Windows.Forms;

namespace CadastralReference

{
    public partial class frmHelpTemplateView : Form
    {
        public frmHelpTemplateView()
        {
            InitializeComponent();
        }

        private void frmHelpTemplateView_Load(object sender, EventArgs e)
        {
            this.lblOpisanie.Text = TextTemplateConverter.GetDiscription();
        }
    }
}
