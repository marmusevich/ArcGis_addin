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
using SharedClasses;

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
            frm.btnHelpTemplate.Visible = !isUseTemplate;
            frm.tbHTML.Text = html;
            if(frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                html = frm.tbHTML.Text;
        }

        public static void ShowPDF()
        {
            try
            {
                if (WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf != null
                     && WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf.PageCount > 0)
                {
                    string tmpFileName = System.IO.Path.GetTempFileName() + ".pdf";
                    WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf.Save(tmpFileName);
                    Process.Start(tmpFileName);
                }
            }
            catch (Exception ex)
            {
                // сообщить про ошибку
                Logger.Write(ex, string.Format("На могу отобразить PDF"));
                GeneralApp.ShowErrorMessage(string.Format("На могу отобразить PDF " ));
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




        public static void SaveRTF()
        {
            IDataObject obj = Clipboard.GetDataObject();
            SaveFileDialog sfd = new SaveFileDialog();
            try
            {
                sfd.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                sfd.Filter = "rtf|*.rtf";
                sfd.Title = "Сохранить..";

                if ((sfd.ShowDialog() == DialogResult.OK) && sfd.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();
                    string html = "";

                    html += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Titul_Template);
                    //html += " <br clear=all style='mso-special-character:line-break;page-break-before:always'> ";
                    html += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Body_Begin_Template);
                    html += "<P>" + WorkCadastralReference.GetCadastralReferenceData().BodyText + "</P>";
                    html += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Body_End_Template);
                    //html += " <br clear=all style='mso-special-character:line-break;page-break-before:always'> ";
                    html += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Raspiska_Template);
                    //html += " <br clear=all style='mso-special-character:line-break;page-break-before:always'> ";


                    RichTextBox rtbTemp = new RichTextBox();
                    HtmlToRtf(html, ref rtbTemp);

                    ////****

                    //HtmlToRtf(TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Titul_Template), ref rtbTemp);
                    //rtbTemp.Text += Environment.NewLine;

                    ////html += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Body_Begin_Template);
                    ////html += "<P>" + WorkCadastralReference.GetCadastralReferenceData().BodyText + "</P>";
                    ////html += TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Body_End_Template);
                    ////HtmlToRtf(html, ref rtbTemp);
                    ////rtbTemp.Text += Environment.NewLine;


                    //HtmlToRtf(TextTemplateConverter.Convert(WorkCadastralReference.GetCadastralReferenceData().Raspiska_Template), ref rtbTemp);
                    //rtbTemp.Text += Environment.NewLine;


                    if (WorkCadastralReference.GetCadastralReferenceData().Pages != null)
                        foreach (OnePageDescriptions opd in WorkCadastralReference.GetCadastralReferenceData().Pages)
                            if (opd.Image != null)
                            {
                                Clipboard.Clear();
                                Clipboard.SetImage(opd.Image);
                                rtbTemp.Paste();
                            }
                    Clipboard.Clear();

                    rtbTemp.SaveFile(fs, RichTextBoxStreamType.RichText);
                    fs.Close();

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                // сообщить про ошибку
                Logger.Write(ex, string.Format("На могу сохранить RTF в '{0}'", sfd.FileName));
                GeneralApp.ShowErrorMessage(string.Format("На могу сохранить RTF в '{0}'", sfd.FileName));
            }

            Clipboard.SetDataObject(obj);

        }

        private static void HtmlToRtf(string html, ref RichTextBox rtbTemp)
        {
            WebBrowser wb = new WebBrowser();
            wb.Navigate("about:blank");

            wb.Document.Write(html);
            wb.Document.ExecCommand("SelectAll", false, null);
            wb.Document.ExecCommand("Copy", false, null);
            //rtbTemp.SelectAll();
            rtbTemp.Paste();
        }
    }
}
