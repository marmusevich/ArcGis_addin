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
using System.Collections.Specialized;


//Кадастровая_справка.DBO.KS_OBJ_FOR_ALEX


namespace CadastralReference
{
    // работа со справкой
    public static class WorkCadastralReference
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
            string filteredString = "";
            int i = frmReestrZayav_jurnal.ShowForSelect(filteredString);
            if (i == 0) i = -1;

            SetZayavka(i);
        }
        /// <summary>
        /// указать заявку
        /// </summary>
        /// <param name="zayavkaID"> код заявка</param>
        public static void SetZayavka(int zayavkaID)
        {
            GetCadastralReferenceData().ZayavkaData = WorkCadastralReference_DB.GetZayavkaData(zayavkaID);
            GetCadastralReferenceData().ZayavkaID = zayavkaID;
            GetCadastralReferenceData().ObjektInMapID = -1;
        }
        /// <summary>
        /// Описание заявки
        /// </summary>
        /// <returns></returns>
        public static string GetZayavkaDiscription()
        {
            if (GetCadastralReferenceData().ZayavkaID == -1)
                return "Не выбрана заявка.";


            string strKod_Z = "";
            string N_Z = "";
            DateTime Data_Z = new DateTime();
            if (GetCadastralReferenceData().ZayavkaData != null)
            {
                strKod_Z = GetCadastralReferenceData().ZayavkaData["strKod_Z"] as string;
                N_Z = GetCadastralReferenceData().ZayavkaData["N_Z"] as string;
                Data_Z = (DateTime)GetCadastralReferenceData().ZayavkaData["Data_Z"];
            }
            return string.Format("Заявка №{0} от {1}г. {2} (id = {3}))", N_Z, Data_Z, strKod_Z,  GetCadastralReferenceData().ZayavkaID.ToString() );
        }


        /// <summary>
        /// Запоменание выбранного юъекта на карте с проверками
        /// </summary>
        public static void SelectObjektInMap()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            
            int objectID = -1;

            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            //переберем все выбранные объекты на карте
            IEnumFeature enumFeature = mxDoc.FocusMap.FeatureSelection as IEnumFeature;
            IFeature feature = enumFeature.Next();
            while (feature != null)
            {
                string tabName = "";
                if (feature.Class != null)
                {
                    if ((feature.Class) is IDataset)
                    {
                        tabName = (feature.Class as IDataset).Name;
                        //проверка на принадлежность нашему проекту
                        //if (GetCadastralReferenceData().ObjectLayerName.ToLower() == tabName.ToLower())
                        {
                            objectID = feature.OID;
                        }
                    }
                }
                feature = enumFeature.Next();
            }
            GetCadastralReferenceData().ObjektInMapID = objectID;
        }
        /// <summary>
        /// Описание объекта карты
        /// </summary>
        /// <returns></returns>
        public static string GetObjektInMapDiscription()
        {
            return "Указан объект №" + GetCadastralReferenceData().ObjektInMapID.ToString();
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

            WorkCadastralReference_MAP.AddLegend(mxdoc.PageLayout, mxdoc.FocusMap, 5, 5, 20);

            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            activeView.Refresh();
        }

        /// <summary>
        /// Переключить слои
        /// </summary>
        /// <param name="opd"> описание листа</param>
        public static void EnableLayersFropPageAndSetScale(OnePageDescriptions opd)
        {

            if (GetCadastralReferenceData().ZayavkaID == -1 || GetCadastralReferenceData().ObjektInMapID == -1)
                return;

            WorkCadastralReference_MAP.EnableLayersFromPages(opd);

            WorkCadastralReference_MAP.SetScaleAndCentred();

            WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
            WorkCadastralReference_MAP.SetStandartMapSkale();

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            mxdoc.PageLayout.ZoomToWhole();
            mxdoc.ActiveView.Refresh();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  дополнительные функции







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

