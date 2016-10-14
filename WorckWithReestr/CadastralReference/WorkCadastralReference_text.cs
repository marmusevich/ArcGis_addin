using System;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System.IO;
using PdfSharp.Drawing;
using System.Drawing;
using System.Diagnostics;
using System.Text;

namespace CadastralReference
{
    class WorkCadastralReference_text
    {
        private static void AddPageFromHTML(string html, ref PdfDocument pdf)
        {
            PdfDocument pdftmp = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
            using (MemoryStream ms = new System.IO.MemoryStream((int)pdftmp.FileSize))
            {
                pdftmp.Save(ms, false);

                PdfDocument inputDocument = PdfReader.Open(ms, PdfDocumentOpenMode.Import);
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    PdfPage page = inputDocument.Pages[idx];
                    pdf.AddPage(page);
                }
            }
            pdftmp.Close();
        }

        private static void AddPageFromImage(Image image, ref PdfDocument pdf)
        {
            PdfPage pdfPage = new PdfPage();
            XImage img = XImage.FromGdiPlusImage(image);
            PdfSharp.Drawing.XSize size = img.Size;

            pdfPage.Width = size.Width;
            pdfPage.Height = size.Height;

            pdf.Pages.Add(pdfPage);

            XGraphics xgr = XGraphics.FromPdfPage(pdfPage);
            xgr.DrawImage(img, 0, 0);
        }


        public static void EditHTML(ref string html)
        {
            frmEditHTML frm = new frmEditHTML();
            frm.tbHTML.Text = html;
            if(frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                html = frm.tbHTML.Text;
        }

        public static void ShowPDF(PdfDocument pdf)
        {

            if (pdf != null)
            {
                string tmpFileName = System.IO.Path.GetTempFileName() + ".pdf";

                pdf.Save(tmpFileName);

                Process.Start(tmpFileName);
            }

        }

        public static string Implement(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            sb.Replace("{_масштаб_}", "текущий масштаб карты в формате 1:XXX");
            return sb.ToString();
        }

        public static PdfDocument GeneratePDF(ref CadastralReferenceData crd)
        {
            PdfDocument pdf = new PdfDocument();

            string str = "";

            str = crd.Titul_Template;
            //обработать шаблон
            //str = TextTemplateConverter.Implement(crd.Titul_Template);
            str = Implement(str);
            AddPageFromHTML(str, ref pdf);

            str = crd.Body_Begin_Template;
            //обработать шаблон
            //str = TextTemplateConverter.Implement(crd.Body_Begin_Template);
            str = Implement(str);
            str += "<P>" + crd.BodyText + "</P>";
            str += crd.Body_End_Template;
            //обработать шаблон
            //str += TextTemplateConverter.Implement(crd.Body_End_Template);
            str = Implement(str);
            AddPageFromHTML(str, ref pdf);


            str = crd.Raspiska_Template;
            //обработать шаблон
            //str = TextTemplateConverter.Implement(crd.Raspiska_Template);
            str = Implement(str);
            AddPageFromHTML(str, ref pdf);


            AddPageFromImage(Image.FromFile(@"d:\a4.png"), ref pdf);
            AddPageFromImage(Image.FromFile(@"d:\a3.png"), ref pdf);
            AddPageFromImage(Image.FromFile(@"d:\1.png"), ref pdf);



            pdf.Close();
            ShowPDF(pdf);
            return pdf;
        }

    }
}
