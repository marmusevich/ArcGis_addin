using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using WorckWithReestr;
using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;
using System.IO;



//  прочитать все листы справки
// еще событие на запрет редактирования

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
                LoadSettingFromDB();
            }
            return m_CadastralReferenceData; 
        }
        #endregion


        public static void SaveSettingToDB()
        {
            try
            {
                string filename = System.IO.Path.Combine(GeneralApp.GetAppDataPathAndCreateDirIfNeed(), string.Format("CadastralReferenceData_{0:yyy.MM.dd_H-mm-ss}.setting.xml", DateTime.Now));
                string xml = GetCadastralReferenceData().SaveSettingToXMLString();
                File.WriteAllText(filename, xml);

                WorkCadastralReference_DB.SaveToDBPage(-1, "Настройки", -1, "Настройки" , xml);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "SaveSettingToDB Error");
                //GeneralApp.ShowErrorMessage(string.Format("Проблема при сохранение данных справочника '{0}' id {1}", NameTable, objectID));
            }
        }
        public static void LoadSettingFromDB()
        {
            m_CadastralReferenceData.InitPagesDescription();

            //string xml = (string)WorkCadastralReference_DB.LoadFromDBPage(-1, -1, "Настройки");
            //m_CadastralReferenceData.LoadSettingFromXMLString(xml);
        }

        public static void SaveToDBImage(OnePageDescriptions opd)
        {
            Image img = WorkCadastralReference_MAP.GetImageFromArcGis();
            opd.Image = img;

            //сохранить картинку в базу
            WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(), opd.PagesID, opd.Caption, opd.Image);
        }

        public static void SaveToDBRTF()
        {
            WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(), 0, "текстовая часть", GetCadastralReferenceData().AllRTF);
        }

        public static void LoadFromDB()
        {
            object tmp = null;
            tmp = WorkCadastralReference_DB.LoadFromDBPage(GetCadastralReferenceData().ZayavkaID, 0, "текстовая часть");
            if (tmp != null && tmp is string)
                GetCadastralReferenceData().AllRTF = (string)tmp;
            foreach (OnePageDescriptions opd in GetCadastralReferenceData().Pages)
            {
                tmp = WorkCadastralReference_DB.LoadFromDBPage(GetCadastralReferenceData().ZayavkaID, opd.PagesID, opd.Caption);
                if (tmp != null && tmp is Image)
                    opd.Image = (Image)tmp;
            }
        }




        /// <summary>
        /// открыть окно выбора заявки 
        /// </summary>
        public static void SelectZayavka()
        {
            string filteredString = "";
            int i = frmReestrZayav_jurnal.ShowForSelect(filteredString);

            SetZayavka(i);
        }
        /// <summary>
        /// указать заявку
        /// </summary>
        /// <param name="zayavkaID"> код заявка</param>
        public static void SetZayavka(int zayavkaID)
        {
            if (zayavkaID == -1)
            {
                GetCadastralReferenceData().ClearData();
            }
            else if (zayavkaID >= 0)
            {
                GetCadastralReferenceData().ZayavkaData = WorkCadastralReference_DB.GetZayavkaData(zayavkaID);
                GetCadastralReferenceData().ZayavkaID = zayavkaID;

                GetCadastralReferenceData().MapObjectID = (int)GetCadastralReferenceData().ZayavkaData["MapObjectID"] ;
                GetCadastralReferenceData().IsReferenceClose = (bool)GetCadastralReferenceData().ZayavkaData["IsReferenceClose"];

                if (GetCadastralReferenceData().MapObjectID != -1)
                    LoadFromDB();
            }
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
            GetCadastralReferenceData().MapObjectID = objectID;

            if (GetCadastralReferenceData().MapObjectID != -1)
                LoadFromDB();

            }
        /// <summary>
        /// Описание объекта карты
        /// </summary>
        /// <returns></returns>
        public static string GetObjektInMapDiscription()
        {
            string ret = "Объект не выбран";
            if(GetCadastralReferenceData().MapObjectID != -1)
            {
                LoadFromDB();
                ret = "Указан объект №" + GetCadastralReferenceData().MapObjectID.ToString();
            }
            return ret;
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

            if (GetCadastralReferenceData().ZayavkaID == -1 || GetCadastralReferenceData().MapObjectID == -1)
                return;

            WorkCadastralReference_MAP.EnableLayersFromPages(opd);

            WorkCadastralReference_MAP.SetScaleAndCentred();

            WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
            WorkCadastralReference_MAP.SetStandartMapSkale();

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            mxdoc.PageLayout.ZoomToWhole();
            mxdoc.ActiveView.Refresh();
        }
    }
}

