using System;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmRTF : Form
    {
        public frmRTF()
        {
            InitializeComponent();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "rtf files (*.rtf)|*.rtf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                this.rtbMain.SaveFile(saveFileDialog.FileName);
            int num = (int)MessageBox.Show("Save ok \n rtbMain.Rtf->" + this.rtbMain.Rtf);
        }

        private void frmRTF_Load(object sender, EventArgs e)
        {
            this.rtbMain.Rtf = WorkCadastralReference.GetCadastralReferenceData().AllRTF;
        }
    }
}
