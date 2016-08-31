using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Carto;

namespace CadastralReference
{
    // работа со справкой
    class WorkCadastralReference
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  вся информация по справке - синглитон
        private static CadastralReferenceData m_CadastralReferenceData = null;

        /// <summary>
        /// return CadastralReferenceData - сиглитон
        /// </summary>
        /// <returns></returns>
        public static CadastralReferenceData GetCadastralReferenceData()
        {
            if (m_CadastralReferenceData == null)
            {
                m_CadastralReferenceData = new CadastralReferenceData();
                m_CadastralReferenceData.InitPagesDescription();
            }
            return m_CadastralReferenceData; 
        }
        #endregion 

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  группа сохранения и чтения из/в базу
        //IMAGE
        public static void SaveToDBImage(OnePageDescriptions opd)
        {
            Image img = GetImageFromArcGis();

            m_CadastralReferenceData.Pages[opd.index].Image = img;
        }
        public static void LoadToDBImage(OnePageDescriptions opd)
        {
            MessageBox.Show("LoadToDBImage ->" + opd.Caption);
        }

        //RTF
        public static void SaveToDBRTF()
        {
            MessageBox.Show("SaveToDBRTF");
        }
        public static void LoadToDBRTF()
        {
            MessageBox.Show("LoadToDBRTF");
        }

        //All
        public static void SaveToDB()
        {
            MessageBox.Show("SaveToDB");
        }
        public static void LoadToDB()
        {
            MessageBox.Show("LoadToDB");
        }
        #endregion


        // получить изображенние карты из Арк ГИСа 
        public static Image GetImageFromArcGis()
        {
            string tmpFileName = System.IO.Path.GetTempFileName();

            IMxDocument mxdoc =  SharedClasses.GeneralMapWork.GetMxDocument();
            IActiveView activeView = mxdoc.ActiveView;

            IExport exporter = new ExportPNGClass();
            exporter.ExportFileName = tmpFileName;
            exporter.Resolution = 96;

            IEnvelope pixelBBOX = new EnvelopeClass();
            pixelBBOX.XMin = activeView.ExportFrame.left;
            pixelBBOX.XMax = activeView.ExportFrame.right;
            pixelBBOX.YMin = activeView.ExportFrame.top;
            pixelBBOX.YMax = activeView.ExportFrame.bottom;
            exporter.PixelBounds = pixelBBOX;

            int hdc = exporter.StartExporting();
            tagRECT exporterRectangle = activeView.ExportFrame;
            activeView.Output(hdc, (int)exporter.Resolution, ref exporterRectangle, null, null);
            exporter.FinishExporting();
            exporter.Cleanup();

            Image img = Image.FromFile(tmpFileName);
            System.IO.File.Delete(tmpFileName);
            return img;
        }



        // открыть окно выбора заявки
        public static void SelectZayavka()
        {
            MessageBox.Show("Выбрать заявку");
            m_CadastralReferenceData.ZayavkaID = 1111;

            GetOrCreateCadastralReference();

        }
        // проверить существование справки
        // если нет создать
        public static void GetOrCreateCadastralReference()
        {
            m_CadastralReferenceData.CadastralReferenceID = 1111;
        }

        //создать картинку текущего листа
        public static void GenerateMaket(OnePageDescriptions opd)
        {
            MessageBox.Show("GenerateMaket ->" + opd.Caption);
        }

        //создать картинку текущего листа
        public static void EnableLawrsFropPage(OnePageDescriptions opd, bool enable)
        {
            //MessageBox.Show("EnableLawrsFropPage ->" + opd.Caption + "  \n enable =" + enable);
        }
    }
}

