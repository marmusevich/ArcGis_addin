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

        public static void ShowPDF()
        {
            if (WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf != null)
            {
                string tmpFileName = System.IO.Path.GetTempFileName() + ".pdf";

                WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf.Save(tmpFileName);

                Process.Start(tmpFileName);
            }
        }


        public static PdfDocument GeneratePDF()
        {
            PdfDocument pdf = new PdfDocument();
            string str = "";

            str = TextTemplateConverter.Implement(WorkCadastralReference.GetCadastralReferenceData().Titul_Template);
            AddPageFromHTML(str, ref pdf);

            str = TextTemplateConverter.Implement(WorkCadastralReference.GetCadastralReferenceData().Body_Begin_Template);
            str += "<P>" + WorkCadastralReference.GetCadastralReferenceData().BodyText + "</P>";
            str += TextTemplateConverter.Implement(WorkCadastralReference.GetCadastralReferenceData().Body_End_Template);
            AddPageFromHTML(str, ref pdf);

            str = TextTemplateConverter.Implement(WorkCadastralReference.GetCadastralReferenceData().Raspiska_Template);
            AddPageFromHTML(str, ref pdf);

            if (WorkCadastralReference.GetCadastralReferenceData().Pages != null)
                foreach (OnePageDescriptions opd in WorkCadastralReference.GetCadastralReferenceData().Pages)
                    if(opd.Image != null)
                        AddPageFromImage(opd.Image, ref pdf);


            pdf.Close();
            WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf = pdf;

            ShowPDF();
            return pdf;
        }

    }
}
