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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;


namespace CadastralReference
{
    // работа со справкой
    public static class WorkCadastralReference
    {
        // для хранения настроек
        private const int zayavkaId_ForNastroyka = -2;
        private static TypeReference TypeReference_ForNastroyka = TypeReference.Cadastr;

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


        // 
        public enum TypeReference
        {
            Cadastr = -1,
            Historian = -2
        }


        public static void SetTypeReference(TypeReference type)
        {
            // после проверки
            //if(type != null || TypeReference.)
            {
                //очистить текущие данные
                GetCadastralReferenceData().ClearData();
                GetCadastralReferenceData().ClearSeting();

                //перезагрузить настройки из базы
                TypeReference_ForNastroyka = type;
                LoadSettingFromDB();

                //пересоздать элементы управления - послать событие
                GetCadastralReferenceData().OnTypeReference_Change();
            }
        }

        public static void Show(int id, TypeReference type)
        {
            ArcMap.Application.CurrentTool = null;

            UID dockableWinUID = new UIDClass();
            dockableWinUID.Value = ThisAddIn.IDs.arcDW_CadastralReference;

            IDockableWindow statsticsDockableWin = ArcMap.DockableWindowManager.GetDockableWindow(dockableWinUID);

            if(type == TypeReference.Cadastr)
                statsticsDockableWin.Caption = "Кадастровая справка";
            else if (type == TypeReference.Historian)
                statsticsDockableWin.Caption = "Историко-архитектурная справка";


            if (TypeReference_ForNastroyka == type)
                // если равны то просто вкл / выкл
                statsticsDockableWin.Show(!statsticsDockableWin.IsVisible());
            else
            {
                //если разные то установить и включить
                SetTypeReference(type);
                statsticsDockableWin.Show(true);
            }

            SetZayavka(id);
        }


        public static void SaveSettingToDB()
        {
            try
            {
                string xml = GetCadastralReferenceData().SaveSettingToXMLString();
#if DEBUG
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                // test
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                string filename = System.IO.Path.Combine(GeneralApp.GetAppDataPathAndCreateDirIfNeed(), string.Format("CadastralReferenceData_{0:yyy.MM.dd_H-mm-ss}.setting.xml", DateTime.Now));
                File.WriteAllText(filename, xml);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                // test
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
                WorkCadastralReference_DB.SaveToDBPage(zayavkaId_ForNastroyka, "Настройки", (int)TypeReference_ForNastroyka, "Настройки" , WorkCadastralReference_DB.StringToByteArray(xml) );
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "Запись настроек кадастровой справки");
                GeneralApp.ShowErrorMessage("Ошибка при записи настроек кадастровой справки");
            }
        }

        public static void LoadSettingFromDB()
        {
            try
            {
                string xml = WorkCadastralReference_DB.ByteArrayToString(WorkCadastralReference_DB.LoadFromDBPage(zayavkaId_ForNastroyka, (int)TypeReference_ForNastroyka, "Настройки"));
                m_CadastralReferenceData.LoadSettingFromXMLString(xml);
            }
            catch (Exception ex) // обработка ошибок
            {
                m_CadastralReferenceData.InitDefaultSetting();
                Logger.Write(ex, "Ошибка при чтение настроек кадастровой справки");
                GeneralApp.ShowErrorMessage("Ошибка при чтение настроек кадастровой справки \n\r Установлены значения по умолчанию. ");
            }
        }

        public static void SaveToDBImage(OnePageDescriptions opd)
        {
            try
            {
                if (opd.Image != null)
                {
                    WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(GetCadastralReferenceData().ZayavkaID), opd.PagesID, opd.Caption, WorkCadastralReference_DB.ImageToByteArray(opd.Image));
                    WorkCadastralReference_DB.EditZayavkaData(GetCadastralReferenceData().ZayavkaID, GetCadastralReferenceData().MapObjectID, true, null);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "SaveToDBImage Error");
            }
        }

        public static void SaveToDBPDF()
        {
            try
            {
                bool f = false;
                if (GetCadastralReferenceData().AllDocumentPdf != null)
                {
                    WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(GetCadastralReferenceData().ZayavkaID), 0, "PDF", WorkCadastralReference_DB.PDFToByteArray(GetCadastralReferenceData().AllDocumentPdf));
                    f = true;
                }
                if (GetCadastralReferenceData().BodyText != null && GetCadastralReferenceData().BodyText != "")
                {
                    WorkCadastralReference_DB.SaveToDBPage(GetCadastralReferenceData().ZayavkaID, GetZayavkaDiscription(GetCadastralReferenceData().ZayavkaID), 1, "изменяемая часть", WorkCadastralReference_DB.StringToByteArray(GetCadastralReferenceData().BodyText));
                    f = true;
                }

                if (f)
                    WorkCadastralReference_DB.EditZayavkaData(GetCadastralReferenceData().ZayavkaID, GetCadastralReferenceData().MapObjectID, true, null);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "SaveToDBPDF Error");
            }
        }

        public static void LoadFromDB()
        {
            try
            {
                GetCadastralReferenceData().AllDocumentPdf = WorkCadastralReference_DB.ByteArrayToPDF(WorkCadastralReference_DB.LoadFromDBPage(GetCadastralReferenceData().ZayavkaID, 0, "PDF"));
                GetCadastralReferenceData().BodyText = WorkCadastralReference_DB.ByteArrayToString(WorkCadastralReference_DB.LoadFromDBPage(GetCadastralReferenceData().ZayavkaID, 1, "изменяемая часть"));

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

                int MapObjectID = (int)GetCadastralReferenceData().ZayavkaData["MapObjectID"];
              
                GetCadastralReferenceData().IsReferenceClose = (bool)GetCadastralReferenceData().ZayavkaData["IsReferenceClose"];
                if (MapObjectID != -1)
                {
                    LoadFromDB();
                    WorkCadastralReference_MAP.ShowOnMap(MapObjectID);
                }
                GetCadastralReferenceData().MapObjectID = MapObjectID;
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
                ret = "Указан объект '" + GetCadastralReferenceData().MapObjectID_Discription + "'  (№" + GetCadastralReferenceData().MapObjectID.ToString()+")";
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
                IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
                WorkCadastralReference_MAP.EnableLayersFromPages(opd);

                if (opd.ScaleMode == 2)
                {
                    WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
                    mxdoc.FocusMap.MapScale = opd.Scale_Manual;
                }
                else if (opd.ScaleMode == 1)
                {
                    WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
                }
                else //if (opd.ScaleMode == 0)
                {
                    WorkCadastralReference_MAP.SetScaleAndCentred();
                    WorkCadastralReference_MAP.CheckAndSetPageLayoutMode();
                    WorkCadastralReference_MAP.SetStandartMapSkale();
                }


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

                IActiveView activeView = mxdoc.ActiveView;

                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                activeView.Refresh();
                mxdoc.PageLayout.ZoomToWhole();
                //mxdoc.ActiveView.Refresh();

                Image img = WorkCadastralReference_MAP.GetImageFromArcGis();
                opd.Image = img;

            }
            catch (Exception ex) // обработка ошибок
            {

                Logger.Write(ex, string.Format("Построение макета '{0}' заявка id {1}", opd.Caption, GetCadastralReferenceData().ZayavkaID));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при построение листа '{0}' заявка id {1}", opd.Caption, GetCadastralReferenceData().ZayavkaID));
            }
        }
    }
}

