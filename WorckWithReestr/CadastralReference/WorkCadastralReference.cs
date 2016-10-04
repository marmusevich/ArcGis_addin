using System.Drawing;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using WorckWithReestr;
using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

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
                LoadSettingFromDB();
            }
            return m_CadastralReferenceData; 
        }
        #endregion


        public static void SaveSettingToDB()
        {
            try
            {
                string xml = GetCadastralReferenceData().SaveSettingToXMLString();
                
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                // test
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                string filename = System.IO.Path.Combine(GeneralApp.GetAppDataPathAndCreateDirIfNeed(), string.Format("CadastralReferenceData_{0:yyy.MM.dd_H-mm-ss}.setting.xml", DateTime.Now));
                File.WriteAllText(filename, xml);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                // test
                /////////////////////////////////////////////////////////////////////////////////////////////////////////

                WorkCadastralReference_DB.SaveToDBPage(-1, "Настройки", -1, "Настройки" , WorkCadastralReference_DB.StringToByteArray(xml) );
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "Запись настроек кадастровой справки");
                GeneralApp.ShowErrorMessage("Ошибка при записи настроек кадастровой справки");
            }
        }
        public static void LoadSettingFromDB()
        {
            m_CadastralReferenceData = new CadastralReferenceData();
            try
            {
                string xml = WorkCadastralReference_DB.ByteArrayToString(WorkCadastralReference_DB.LoadFromDBPage(-1, -1, "Настройки"));
                m_CadastralReferenceData.LoadSettingFromXMLString(xml);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "Ошибка при чтение  настроек кадастровой справки");
                GeneralApp.ShowErrorMessage("Ошибка при чтение  настроек кадастровой справки \n\r Установлены значения по умолчанию. ");

                m_CadastralReferenceData.InitDefaultSetting();
            }
        }

        public static void SaveToDBImage(OnePageDescriptions opd)
        {
            try
            {
                Image img = WorkCadastralReference_MAP.GetImageFromArcGis();
                opd.Image = img;
                WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(GetCadastralReferenceData().ZayavkaID), opd.PagesID, opd.Caption, WorkCadastralReference_DB.ImageToByteArray(opd.Image) );
                WorkCadastralReference_DB.EditZayavkaData(GetCadastralReferenceData().ZayavkaID, GetCadastralReferenceData().MapObjectID, true, null);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "SaveToDBImage Error");
            }
        }

        public static void SaveToDBRTF()
        {
            try
            {
                WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(GetCadastralReferenceData().ZayavkaID), 0, "текстовая часть", WorkCadastralReference_DB.StringToByteArray(GetCadastralReferenceData().AllRTF));
                WorkCadastralReference_DB.EditZayavkaData(GetCadastralReferenceData().ZayavkaID, GetCadastralReferenceData().MapObjectID, true, null);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "SaveToDBRTF Error");
            }
        }

        public static void LoadFromDB()
        {
            try
            {
                GetCadastralReferenceData().AllRTF = WorkCadastralReference_DB.ByteArrayToString(WorkCadastralReference_DB.LoadFromDBPage(GetCadastralReferenceData().ZayavkaID, 0, "текстовая часть"));
                foreach (OnePageDescriptions opd in GetCadastralReferenceData().Pages)
                {
                    opd.Image = WorkCadastralReference_DB.ByteArrayToImage(WorkCadastralReference_DB.LoadFromDBPage(GetCadastralReferenceData().ZayavkaID, opd.PagesID, opd.Caption)); 
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "LoadFromDB Error");
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
            else if (zayavkaID > 0)
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
        public static string GetZayavkaDiscription(int zayavkaID)
        {
            if (zayavkaID == -1)
                return "Не выбрана заявка.";

            string strKod_Z = "";
            string N_Z = "";
            DateTime Data_Z = new DateTime();
            if (GetCadastralReferenceData().ZayavkaData == null)
                GetCadastralReferenceData().ZayavkaData = WorkCadastralReference_DB.GetZayavkaData(zayavkaID);

            strKod_Z = GetCadastralReferenceData().ZayavkaData["strKod_Z"] as string;
            N_Z = GetCadastralReferenceData().ZayavkaData["N_Z"] as string;
            Data_Z = (DateTime)GetCadastralReferenceData().ZayavkaData["Data_Z"];
            return string.Format("Заявка №{0} от {1:d}г. {2} (id = {3})", N_Z, Data_Z, strKod_Z,  GetCadastralReferenceData().ZayavkaID.ToString() );
        }

        public static bool CheckReferenceToExistPages(int zayavkaID)
        {
            bool ret = false;
            if (zayavkaID != -1)
            {
                if (GetCadastralReferenceData().IsReferenceClose)
                    MessageBox.Show("Справка закрыта для редактирования.");
                else
                {
                    Dictionary<int, string> data = WorkCadastralReference_DB.GetFromDBListExistingPages(zayavkaID);
                    if (data != null && data.Count > 0)
                    {
                        string str = "";
                        foreach (var v in data)
                            str += string.Format("   - {0}\r\n", v.Value);

                        DialogResult dr = MessageBox.Show("Имеютсяследующие готовые листы:\r\n" + str+ "\r\n  Удалить их?", "Имеются данные", MessageBoxButtons.YesNoCancel);
                        if(dr == DialogResult.Yes)
                        {
                            //удалить готовые листы
                            bool flag = true;
                            foreach (var v in data)
                            {
                                // проверка на удачность
                                flag = WorkCadastralReference_DB.DeleteOnePageFromDB(zayavkaID, v.Key, v.Value ) && flag;
                            }
                            if (flag)
                            {
                                GetCadastralReferenceData().ClearData();
                                WorkCadastralReference_DB.EditZayavkaData(GetCadastralReferenceData().ZayavkaID, -1, false, false);
                            }
                            SetZayavka(zayavkaID);
                            ret = true && flag;
                        }
                        else
                            ret = false;
                    }
                    else
                        ret = true;
                }
            }
            return ret;
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
            if (GetCadastralReferenceData().ZayavkaID == -1 || GetCadastralReferenceData().MapObjectID == -1)
                return;

            try
            {
                WorkCadastralReference_MAP.EnableLayersFromPages(opd);
                WorkCadastralReference_MAP.SetScaleAndCentred();
                WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
                WorkCadastralReference_MAP.SetStandartMapSkale();

                WorkCadastralReference_MAP.ChangeSizeDateFrame(opd);

                WorkCadastralReference_MAP.DeleteNordArrow();
                WorkCadastralReference_MAP.AddNorthArrowTool(opd);

                WorkCadastralReference_MAP.DeleteScalebar();
                WorkCadastralReference_MAP.AddScalebar(opd);

                WorkCadastralReference_MAP.DeleteAllText();
                //нанаести все надписи листа
                foreach (OneTextElementDescription oted in opd.TextElements)
                {
                    //WorkCadastralReference_MAP.DeleteElementByName(oted.Text);
                    WorkCadastralReference_MAP.AddText(oted);
                }

                IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
                IActiveView activeView = mxdoc.ActiveView;

                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                activeView.Refresh();
                mxdoc.PageLayout.ZoomToWhole();
                //mxdoc.ActiveView.Refresh();
            }
            catch (Exception ex) // обработка ошибок
            {

                Logger.Write(ex, string.Format("Построение макета '{0}' заявка id {1}", opd.Caption, GetCadastralReferenceData().ZayavkaID));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при построение листа '{0}' заявка id {1}", opd.Caption, GetCadastralReferenceData().ZayavkaID));
            }
        }
    }
}

