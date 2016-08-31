using System;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmSetting : Form
    {
        private CadastralReferenceData m_CadastralReferenceData;



        public frmSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // продвинутое сохранение

            //WorkCadastralReference.SetCadastralReferenceData(m_CadastralReferenceData);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            m_CadastralReferenceData = WorkCadastralReference.GetCadastralReferenceData();
        }
    }
}
