using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Carto;
using WorckWithReestr;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System;

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

        /// <summary>
        /// открыть окно выбора заявки 
        /// </summary>
        public static void SelectZayavka()
        {
            Random rnd = new Random();
            SetZayavka(rnd.Next());
        }
        /// <summary>
        /// указать заявку
        /// </summary>
        /// <param name="zayavkaID"> код заявка</param>
        public static void SetZayavka(int zayavkaID)
        {
            GetCadastralReferenceData().ZayavkaID = zayavkaID;
            GetCadastralReferenceData().ObjektInMapID = -1;
        }
        /// <summary>
        /// Описание заявки
        /// </summary>
        /// <returns></returns>
        public static string GetZayavkaDiscription()
        {
            return "Заявка №123 от 01.01.2000г. ООО \"Рога и копыта\" (id=" + GetCadastralReferenceData().ZayavkaID.ToString() + ")";
        }

        /// <summary>
        /// Запоменание выбранного юъекта на карте с проверками
        /// </summary>
        public static void SelectObjektInMap()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;

            string table_name = "";
            int objectID = -1;

            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            if (mxDoc != null)
            {
                //переберем все выбранные объекты на карте
                IEnumFeature enumFeature = mxDoc.FocusMap.FeatureSelection as IEnumFeature;
                IFeature feature = enumFeature.Next();
                while (feature != null)
                {
                    //если можем добовляем в масив для выбора
                    string aliasName = "";
                    string tabName = "";
                    if (feature.Class != null)
                    {
                        aliasName = feature.Class.AliasName;
                        if ((feature.Class) is IDataset)
                        {
                            tabName = (feature.Class as IDataset).Name;
                            //проверка на принадлежность нашему проекту
                            objectID = feature.OID;
                            //MessageBox.Show(string.Format("{0} (ID = {1})", aliasName, objectID));
                        }
                    }

                    feature = enumFeature.Next();
                }
            }

            GetCadastralReferenceData().ObjektInMapID = objectID;
        }
        /// <summary>
        /// Описание объекта карты
        /// </summary>
        /// <returns></returns>
        public static string GetObjektInMapDiscription()
        {
            return "Указан объект №" + GetCadastralReferenceData().ObjektInMapID.ToString(); ;
        }


        /// <summary>
        /// Настроить макет
        /// </summary>
        /// <param name="opd">описание листа</param>
        public static void GenerateMaket(OnePageDescriptions opd)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();

            WorkCadastralReference_MAP.ChangeSizeDateFrame();
            WorkCadastralReference_MAP.AddScalebar();
            WorkCadastralReference_MAP.AddNorthArrowTool();
            WorkCadastralReference_MAP.AddText();

            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            activeView.Refresh();
        }

        /// <summary>
        /// Переключить слои
        /// </summary>
        /// <param name="opd"> описание листа</param>
        public static void EnableLawrsFropPage(OnePageDescriptions opd)
        {
            if (GetCadastralReferenceData().ZayavkaID == -1 || GetCadastralReferenceData().ObjektInMapID == -1)
                return;

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
           //
           //зделать - сначала выбрать нужный объект потом спозиционировать
            SharedClasses.GeneralMapWork.PositionedOnSelectedObjectAndSetScale(mxdoc, mxdoc.FocusMap);

            WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();

            SetStandartMapSkale();

            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            activeView.Refresh();

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  дополнительные функции

        /// <summary>
        /// выбрать ближайшеий больший стандартный маштаб
        /// </summary>
        private static void SetStandartMapSkale()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            int curSkale = Convert.ToInt32( mxdoc.FocusMap.MapScale);
            int newSkale = -1;
            int[] standartSkale = { 1, 4, 5, 10, 15, 20, 25, 40, 50, 75, 100, 200, 400, 500, 800, 1000, 2000, 5000, 10000, 20000, 25000, 50000};
            foreach (int ss in standartSkale)
            {
                if(ss >= curSkale)
                {
                    newSkale = ss;
                    break;
                }
            }

            MessageBox.Show(string.Format("curSkale ={0} newSkale = {1}", curSkale, newSkale));
            mxdoc.FocusMap.MapScale = newSkale;
        }


        //заготовка
        private static void f1()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
        }

        #endregion

    }
}

