using System;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmPrevImage : Form
    {
        public frmPrevImage()
        {
            InitializeComponent();
        }

        public OnePageDescriptions page;


        private void pictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPrevImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void frmPrevImage_Load(object sender, EventArgs e)
        {
            this.Text = page.Caption;
            this.pbPrev.Image = page.Image;

        }

        // save image as..
        private void button2_Click(object sender, System.EventArgs e)
        {
            //// Displays a SaveFileDialog so the user can save the Image
            //// assigned to Button2.
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            //saveFileDialog1.Title = "Save an Image File";
            //saveFileDialog1.ShowDialog();

            //// If the file name is not an empty string open it for saving.
            //if (saveFileDialog1.FileName != "")
            //{
            //    // Saves the Image via a FileStream created by the OpenFile method.
            //    System.IO.FileStream fs =
            //       (System.IO.FileStream)saveFileDialog1.OpenFile();
            //    // Saves the Image in the appropriate ImageFormat based upon the
            //    // File type selected in the dialog box.
            //    // NOTE that the FilterIndex property is one-based.
            //    switch (saveFileDialog1.FilterIndex)
            //    {
            //        case 1:
            //            this.button2.Image.Save(fs,
            //               System.Drawing.Imaging.ImageFormat.Jpeg);
            //            break;

            //        case 2:
            //            this.button2.Image.Save(fs,
            //               System.Drawing.Imaging.ImageFormat.Bmp);
            //            break;

            //        case 3:
            //            this.button2.Image.Save(fs,
            //               System.Drawing.Imaging.ImageFormat.Gif);
            //            break;
            //    }

            //    fs.Close();
            //}
        }



    }
}
