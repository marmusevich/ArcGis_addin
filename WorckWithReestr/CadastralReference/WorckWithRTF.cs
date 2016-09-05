using System.Drawing;
using System.Windows.Forms;


namespace CadastralReference
{
    //работа с РТФ
    class WorckWithRTF
    {
        // создать документ
        public static void GenerateRTF()
        {
            System.Windows.Forms.RichTextBox rtb = new RichTextBox();
            rtb.Text = "";

            AddToRTF_Titul(ref rtb);
            //AddToRTF_NewPage(ref rtb);

            AddToRTF_Page1(ref rtb);
            AddToRTF_Dinamic(ref rtb);
            AddToRTF_Const(ref rtb);

            //AddToRTF_NewPage(ref rtb);
            //AddToRTF_AllImage(ref rtb);

            //AddToRTF_NewPage(ref rtb);
            AddToRTF_Raspiska(ref rtb);

            // итоговый документ

            rtb.Text += "ada";

            WorkCadastralReference.GetCadastralReferenceData().AllRTF = rtb.Rtf;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region ---
        // это шаблоны, сгенерировать из шаблонов
        private static void AddToRTF_Titul(ref RichTextBox rtb)
        {
            rtb.Text += WorkCadastralReference.GetCadastralReferenceData().TitulRTF;
        }
        private static void AddToRTF_Page1(ref RichTextBox rtb)
        {
            rtb.Text += WorkCadastralReference.GetCadastralReferenceData().Page1RTF;
        }
        private static void AddToRTF_Dinamic(ref RichTextBox rtb)
        {
            rtb.Text += "";
        }
        private static void AddToRTF_Const(ref RichTextBox rtb)
        {
            rtb.Text += WorkCadastralReference.GetCadastralReferenceData().ConstRTF;
        }

        private static void AddToRTF_AllImage(ref RichTextBox rtb)
        {
            for (int index = 0; index < WorkCadastralReference.GetCadastralReferenceData().Pages.Length; ++index)
            {
                Image image = WorkCadastralReference.GetCadastralReferenceData().Pages[index].Image;
                if (image != null)
                {
                    object dataObject = (object)Clipboard.GetDataObject();
                    Clipboard.SetImage(image);
                    rtb.Paste();
                    Clipboard.SetDataObject(dataObject);
                    WorckWithRTF.AddToRTF_NewPage(ref rtb);
                }
            }
        }


        private static void AddToRTF_Raspiska(ref RichTextBox rtb)
        {
            rtb.Text += WorkCadastralReference.GetCadastralReferenceData().RaspiskaRTF;
        }

        //новая страница
        private static void AddToRTF_NewPage(ref RichTextBox rtb)
        {
            //rtb.Rtf += @"\page";

            int i = rtb.Rtf.LastIndexOf("}");
            string str = rtb.Rtf;
            str = str.Insert(i, @" \page ");
            rtb.Rtf = str;

        }
        #endregion //работа с РТФ


    }
}
