using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;

namespace WorckWithReestr
{
    //общие функции для работы с документами
    class DocumentWork
    {
        //---------------------------------------------------------------------------------------
        #region 
        #endregion
        //---------------------------------------------------------------------------------------

        // возвращает максимальное значение цыфрового поля из таблицы например для автоинкриманта порядковых номеров
        public static int GetMaxNumerForAutoicrement(string tableName, string fildName)
        {
            int ret = 0;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = "reestr.DBO." + tableName;
                queryDef2.SubFields = "MAX( " + fildName + " )";
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    ret = row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("WorckWithReestr.GetMaxNumerForAutoicrement('{0}', '{1}')", tableName, fildName));
            }
            return ret;
        }
        //вернуть следующий порядковый номер для рееста заявлений / обращений
        public static int GetNextNumerToReestrZayav()
        {
            return GetMaxNumerForAutoicrement("Kn_Reg_Zayv", "N_Z") + 1;
        }
        //проверить существование порядкового номера для рееста заявлений / обращений
        public static bool IsNumerReestrZayavExist(int numer)
        {
            return SharedClass.GetIDByIntValue(numer, "Kn_Reg_Zayv", "N_Z") != -1;
        }

        //вернуть следующий порядковый номер для рееста заявлений / обращений
        public static int GetNextNumerToReestrVedomostey()
        {
            return GetMaxNumerForAutoicrement("Kn_Reg_Ved", "N_Vh") + 1;
        }
        //проверить существование порядкового номера для рееста заявлений / обращений
        public static bool IsNumerReestrVedomosteyExist(int numer)
        {
            return SharedClass.GetIDByIntValue(numer, "Kn_Reg_Ved", "N_Vh") != -1;
        }

    }
}
