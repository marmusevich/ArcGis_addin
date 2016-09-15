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
using System.Collections.Generic;
using System.Collections.Specialized;
using SharedClasses;

namespace CadastralReference
{
    public static class WorkCadastralReference_DB
    {
        //
        private const string DB_NameWorkspace = "Kadastr2016";
        private const string ReestrZayav_NameTable = "Kn_Reg_Zayv";
        private const string CadastralReferenceData_NameTable = "CadastralReferenceData";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  группа сохранения и чтения из/в базу
        #endregion

        //сохранение листа
        public static void SaveToDBPage(int zayavkaId, string zayavkaDiscription, int pageId, string pageCaption, object data )
        {
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(DB_NameWorkspace) as IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                ITable table = fws.OpenTable(CadastralReferenceData_NameTable);
                IRow row = null;

                int referensePageId = GetReferensePageIdFromDB(fws, zayavkaId, pageId);
                if (referensePageId != -1)
                    row = table.GetRow(referensePageId);
                else
                    row = table.CreateRow();

                //сохранить в базу значение элемента управления тип число
                row.set_Value(table.FindField("zayavkaId"), zayavkaId);
                row.set_Value(table.FindField("pageId"), pageId);
                //блоб
                //row.set_Value(table.FindField("data"), data);
                //текст
                SaveStringValueFromTextBoxToDB(ref table, ref row, "zayavkaDiscription", zayavkaDiscription);
                SaveStringValueFromTextBoxToDB(ref table, ref row, "pageCaption", pageCaption);


                row.Store();

                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);
            }
            catch (Exception ex) // обработка ошибок
            {

                Logger.Write(ex, string.Format("Сохранение листа справки '{0}' заявка id {1}", pageCaption, zayavkaId));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при сохранение листа справки '{0}' заявка id {1}", pageCaption, zayavkaId));
            }
            finally
            {
                if ((wse != null) && wse.IsBeingEdited())
                {
                    wse.StopEditing(false);
                }
            }

        }

        // получить один лист
        public static object LoadFromDBPage(int zayavkaId, int pageId, string pageCaption)
        {
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(DB_NameWorkspace) as IFeatureWorkspace;

                int referensePageId = GetReferensePageIdFromDB(fws, zayavkaId, pageId);
                if (referensePageId != -1)
                { 
                    ITable table = fws.OpenTable(CadastralReferenceData_NameTable);
                    IRow row = table.GetRow(referensePageId);
                    return row.get_Value(table.FindField("data"));
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Чтиение листа справки '{0}' заявка id {1}", pageCaption, zayavkaId));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при чтиение листа справки '{0}' заявка id {1}", pageCaption, zayavkaId));
            }

            return null;
        }

        //изменение заявки - еще чтение этих полей
        public static void EditZayavkaData(int ZayavkaID, object MapObjectID = null, object IsHaveReferense = null, object IsReferenceClose = null)
        {
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(DB_NameWorkspace) as IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                ITable  table = fws.OpenTable(ReestrZayav_NameTable);
                IRow row = null;

                row = table.GetRow(ZayavkaID);
                if (row != null)
                {

                    // здесь проверять и сохранять

                    ////int MapObjectID,
                    ////bool IsHaveReferense, 
                    ////bool IsReferenceClose


                    row.Store();
                }
                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Не удается изменить/сохранить данные заявки id '{0}'", ZayavkaID));
                GeneralApp.ShowErrorMessage(string.Format("Не удается изменить/сохранить данные заявки id '{0}'", ZayavkaID));
            }
            finally
            {
                if ((wse != null) && wse.IsBeingEdited())
                {
                    wse.StopEditing(false);
                }
            }
        }


        // возвращает словарь данных по заявке
        public static Dictionary<string, object> GetZayavkaData(int ZayavkaID)
        {
            Dictionary<string, object> zayavkaData = null;

            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(DB_NameWorkspace) as IFeatureWorkspace;
                ITable table = fws.OpenTable(ReestrZayav_NameTable);
                IRow row = table.GetRow(ZayavkaID);

                if (row != null)
                {
                    zayavkaData = new Dictionary<string, object>();
                    zayavkaData.Add("Data_Z", GeneralApp.ConvertVolueToDateTime(row.get_Value(table.FindField("Data_Z"))));
                    zayavkaData.Add("N_Z", "" + row.get_Value(table.FindField("N_Z")) as string);

                    int Kod_Z = (int)row.get_Value(table.FindField("Kod_Z"));
                    zayavkaData.Add("Kod_Z", Kod_Z);

                    object status = row.get_Value(table.FindField("Status"));
                    if (!(status == null || Convert.IsDBNull(status)))
                    {
                        int intStatus = Convert.ToInt16(status);
                        zayavkaData.Add("Status", intStatus);
                        if (intStatus == 0)
                            zayavkaData.Add("strKod_Z", ReestrDictionaryWork.GetNameByIDFromJurOsoby(Kod_Z));
                        else
                            zayavkaData.Add("strKod_Z", ReestrDictionaryWork.GetFIOByIDFromFizLic(Kod_Z));
                    }
                    zayavkaData.Add("Adress_Text", "" + row.get_Value(table.FindField("Adress_Text")) as string);
                    zayavkaData.Add("Cane", "" + row.get_Value(table.FindField("Cane")) as string);
                    zayavkaData.Add("Cane_Date", GeneralApp.ConvertVolueToDateTime(row.get_Value(table.FindField("Cane_Date"))));
                    int Rajon = (int)row.get_Value(table.FindField("Rajon"));
                    zayavkaData.Add("Rajon", Rajon);
                    zayavkaData.Add("strRajon", ReestrDictionaryWork.GetNazvaByIDFromAdmRaj(Rajon));

                    object mapObjectID = row.get_Value(table.FindField("MapObjectID"));
                    if(mapObjectID is int)
                        zayavkaData.Add("MapObjectID", (int)mapObjectID );
                    else 
                        zayavkaData.Add("MapObjectID", -1 );

                    object isReferenceClose = row.get_Value(table.FindField("IsReferenceClose"));
                    if (!(isReferenceClose == null || Convert.IsDBNull(isReferenceClose)))
                    {
                        int intIsReferenceClose = Convert.ToInt16(isReferenceClose);
                        zayavkaData.Add("IsReferenceClose", intIsReferenceClose != 0);
                    }
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Не удается получмить данные заявки id '{0}'", ZayavkaID));
                GeneralApp.ShowErrorMessage(string.Format("Не удается получмить данные заявки id '{0}'", ZayavkaID));
                zayavkaData = null;
            }
            return zayavkaData;
        }


        private static int GetReferensePageIdFromDB(IFeatureWorkspace fws, int zayavkaId, int pageId)
        {
            int ret = -1;
            IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
            queryDef2.Tables = CadastralReferenceData_NameTable;
            queryDef2.SubFields = "DISTINCT TOP 1 OBJECTID";
            queryDef2.WhereClause = "zayavkaId = " + zayavkaId.ToString() + " and pageId = " + pageId.ToString();
            ICursor cursor = queryDef2.Evaluate2(true);
            IRow row = null;
            if ((row = cursor.NextRow()) != null)
            {
                object o = row.get_Value(0);
                if ((o != null) && !Convert.IsDBNull(o))
                    ret = (int)o;
            }
            return ret;
        }

        private static void SaveStringValueFromTextBoxToDB(ref ITable table, ref IRow row, string fildName, string text)
        {
            int MaxLength = row.Fields.get_Field(row.Fields.FindField(fildName)).Length;
            if (text.Length > MaxLength)
            {
                string s = text.Trim().Substring(0, MaxLength - 1);
                text = s;
            }
            row.set_Value(table.FindField(fildName), text);
        }


    }
}
