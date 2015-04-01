using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;

namespace WorckWithReestr
{
    class JurnalWork
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
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
            }
            return ret;
        }


    }
}
