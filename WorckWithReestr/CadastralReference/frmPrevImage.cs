using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmPrevImage : Form
    {
        public OnePageDescriptions page;


        public frmPrevImage()
        {
            InitializeComponent();
        }

        private void frmPrevImage_Load(object sender, EventArgs e)
        {
            this.Text = page.Caption;
            this.pbPrev.Image = page.Image;
            prntPageSetupDialog.PageSettings.Margins = new Margins(0, 0, 0, 0);
        }


        private void pictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPrevImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                sfd.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                sfd.Title = "Сохранить лист макета как ...";
                sfd.ShowDialog();

                if (sfd.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();
                    switch (sfd.FilterIndex)
                    {
                        case 1:
                            this.pbPrev.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                            break;

                        case 2:
                            this.pbPrev.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 3:
                            this.pbPrev.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 4:
                            this.pbPrev.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                // сообщить про ошибку
                SharedClasses.Logger.Write(ex, string.Format("Проблема сохранения макета на диск ({0})", page.Caption));
                SharedClasses.GeneralApp.ShowErrorMessage(string.Format("Проблема сохранения макета на диск ({0})", page.Caption));
            }
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            prntPageSetupDialog.ShowDialog();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            prntPrintPreviewDialog.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (prntPrintDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                prntDocument.Print();
        }



        private void prntDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pbPrev.Image, e.MarginBounds); //Картинка на печать
        }

        private void prntDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //MessageBox.Show("Печать окончина");
        }

    }
}
