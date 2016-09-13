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


namespace CadastralReference
{
    public static class WorkCadastralReference_DB
    {
        //
        public const string ReestrZayav_NameWorkspace = "Kadastr2016";
        public const string ReestrZayav_NameTable = "Kn_Reg_Zayv";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  группа сохранения и чтения из/в базу
        //IMAGE
        public static void SaveToDBImage(OnePageDescriptions opd)
        {
            Image img = WorkCadastralReference_MAP.GetImageFromArcGis();
            opd.Image = img;
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

        // возвращает словарь данных по заявке
        public static Dictionary<string, object> GetZayavkaData(int ZayavkaID)
        {
            Dictionary<string, object> zayavkaData = null;

            try
            {
                IFeatureWorkspace fws = SharedClasses.GeneralDBWork.GetWorkspace(ReestrZayav_NameWorkspace) as IFeatureWorkspace;
                ITable table = fws.OpenTable(ReestrZayav_NameTable);
                IRow row = table.GetRow(ZayavkaID);

                if (row != null)
                {
                    zayavkaData = new Dictionary<string, object>();
                    zayavkaData.Add("Data_Z", SharedClasses.GeneralApp.ConvertVolueToDateTime(row.get_Value(table.FindField("Data_Z"))));
                    zayavkaData.Add("N_Z","" + row.get_Value(table.FindField("N_Z")) as string);

                    int Kod_Z = (int)row.get_Value(table.FindField("Kod_Z"));
                    zayavkaData.Add("Kod_Z", Kod_Z);

                    object status = row.get_Value(table.FindField("Status"));
                    if (!(status == null || Convert.IsDBNull(status)))
                    {
                        int intStatus = Convert.ToInt16(status);
                        zayavkaData.Add("Status", intStatus);
                        if ( intStatus == 0)
                            zayavkaData.Add("strKod_Z", SharedClasses.ReestrDictionaryWork.GetNameByIDFromJurOsoby(Kod_Z));
                        else
                            zayavkaData.Add("strKod_Z", SharedClasses.ReestrDictionaryWork.GetFIOByIDFromFizLic(Kod_Z));
                    }
                    zayavkaData.Add("Adress_Text", "" + row.get_Value(table.FindField("Adress_Text")) as string);
                    zayavkaData.Add("Cane", "" + row.get_Value(table.FindField("Cane")) as string);
                    zayavkaData.Add("Cane_Date", SharedClasses.GeneralApp.ConvertVolueToDateTime(row.get_Value(table.FindField("Cane_Date"))));
                    int Rajon = (int)row.get_Value(table.FindField("Rajon"));
                    zayavkaData.Add("Rajon", Rajon);
                    zayavkaData.Add("strRajon", SharedClasses.ReestrDictionaryWork.GetNazvaByIDFromAdmRaj(Rajon));
                }

            }
            catch (Exception ex) // обработка ошибок
            {
                SharedClasses.Logger.Write(ex, string.Format("CadastralReference.WorkCadastralReference.GetZayavkaData('{0}', '{1}', '{2}')", ReestrZayav_NameWorkspace, ReestrZayav_NameTable, ZayavkaID));
            }
            return zayavkaData;
        }

    }
}
