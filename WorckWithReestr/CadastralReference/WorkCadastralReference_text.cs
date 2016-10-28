using System;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System.IO;
using PdfSharp.Drawing;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Windows.Forms;
using PdfSharp.Pdf.Annotations;

namespace CadastralReference
{
    class WorkCadastralReference_text
    {
        private static void AddPageFromHTML(string html, ref PdfDocument pdf)
        {
            PdfGenerateConfig pgc = new PdfGenerateConfig();
            pgc.PageSize = PdfSharp.PageSize.A4;
            // переконветировать еденицы измерения
            XSize a1 = PdfGenerateConfig.MilimitersToUnits(WorkCadastralReference.GetCadastralReferenceData().PDFTextMarningDown * 10, WorkCadastralReference.GetCadastralReferenceData().PDFTextMarningUp * 10);
            XSize a2 = PdfGenerateConfig.MilimitersToUnits(WorkCadastralReference.GetCadastralReferenceData().PDFTextMarningLeft * 10, WorkCadastralReference.GetCadastralReferenceData().PDFTextMarningRight * 10);
            pgc.MarginBottom = (int)a1.Width;
            pgc.MarginTop = (int)a1.Height;
            pgc.MarginLeft = (int)a2.Width;
            pgc.MarginRight = (int)a2.Height;
            PdfDocument pdftmp = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, pgc);
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
            XImage img = XImage.FromGdiPlusImage(image);
            PdfSharp.Drawing.XSize size = img.Size;

            PdfPage pdfPage = pdf.AddPage();
            pdfPage.Width = XUnit.FromPoint(size.Width);
            pdfPage.Height = XUnit.FromPoint(size.Height);

            XGraphics xgr = XGraphics.FromPdfPage(pdfPage);
            xgr.DrawImage(img, 0, 0);
        }

        public static void EditHTML(ref string html,  bool isUseTemplate = false)
        {
            frmEditHTML frm = new frmEditHTML();
            frm.btnAddTemplate.Visible = isUseTemplate;
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


        public static void GeneratePDF()
        {
            PdfDocument pdf = new PdfDocument();
            string str = "";

            str = TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Titul_Template);
            AddPageFromHTML(str, ref pdf);

            str = TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Body_Begin_Template);
            str += "<P>" + WorkCadastralReference.GetCadastralReferenceData().BodyText + "</P>";
            str += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Body_End_Template);
            AddPageFromHTML(str, ref pdf);

            str = TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Raspiska_Template);
            AddPageFromHTML(str, ref pdf);

            if (WorkCadastralReference.GetCadastralReferenceData().Pages != null)
                foreach (OnePageDescriptions opd in WorkCadastralReference.GetCadastralReferenceData().Pages)
                    if (opd.Image != null)
                        AddPageFromImage(opd.Image, ref pdf);

            MemoryStream ms = new MemoryStream();
            pdf.Save(ms, false);
            pdf.Close();

            WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf = new PdfDocument(); ;
            PdfDocument PDFDoc = PdfReader.Open(ms, PdfDocumentOpenMode.Import);
            for (int Pg = 0; Pg < PDFDoc.Pages.Count; Pg++)
            {
                WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf.AddPage(PDFDoc.Pages[Pg]);
            }


            WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf.Info.Author = @"Worck With Reestr addIn";
            WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf.Info.Title = @"Cadastral Reference";

            ShowPDF();
        }

    }
}
