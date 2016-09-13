﻿using System.Drawing;
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

            string table_name = "Кадастровая_справка.DBO.KS_OBJ_FOR_ALEX";
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
                            if (table_name.ToLower() == tabName.ToLower())
                            {
                                objectID = feature.OID;
                                //MessageBox.Show(string.Format("{0}[{1}] (ID = {2})", aliasName, tabName, objectID));
                            }
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
        public static void EnableLayersFropPage(OnePageDescriptions opd)
        {
            if (GetCadastralReferenceData().ZayavkaID == -1 || GetCadastralReferenceData().ObjektInMapID == -1)
                return;

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;

            // выключить все слои
            if (mxdoc != null)
            {
                IMap map = mxdoc.FocusMap;
                IEnumLayer enumLayer = map.Layers;
                ILayer layer = enumLayer.Next();
                while (layer != null)
                {
                    layer.Visible = false;
                    layer = enumLayer.Next();
                }
                mxdoc.ActiveView.ContentsChanged();
                //activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                mxdoc.ActiveView.Refresh();

            }


            //зделать - сначала выбрать нужный объект потом спозиционировать
            {
                //
                //показать на карте
                //SharedClasses.GeneralMapWork.ShowOnMap(ITable table, int objectID)
                //спозиционироваться на выбранном объекте, установить масштаб
                //SharedClasses.GeneralMapWork.PositionedOnSelectedObjectAndSetScale(IMxDocument mxDoc, IMap map)


                if ((mxdoc.ActiveView is IPageLayout))
                    mxdoc.ActiveView = mxdoc.FocusMap as IActiveView;

                IFeature selectedFeature = null;
                selectedFeature = (mxdoc.FocusMap.FeatureSelection as IEnumFeature).Next();

                if (selectedFeature != null)
                {
                    IEnvelope envelope = selectedFeature.Shape.Envelope;
                    envelope.Expand(2, 2, true);
                    mxdoc.ActiveView.Extent = envelope;
                    //mxdoc.ActiveView.Refresh();
                }
            }

            WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();

            SetStandartMapSkale();

            mxdoc.PageLayout.ZoomToWhole();
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
            //MessageBox.Show(string.Format("curSkale ={0} newSkale = {1}", curSkale, newSkale));
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

