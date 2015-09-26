﻿using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;

namespace SharedClasses
{
    //общие функции для работы с документами
    public static class ReestrDocumentWork
    {
        //---------------------------------------------------------------------------------------
        #region 
        #endregion
        //---------------------------------------------------------------------------------------


        //вернуть следующий порядковый номер для рееста заявлений / обращений
        public static int GetNextNumerToReestrZayav()
        {
            return GeneralDBWork.GetMaxNumerForAutoicrement("reestr", "Kn_Reg_Zayv", "N_Z") + 1;
        }
        //проверить существование порядкового номера для рееста заявлений / обращений
        public static bool IsNumerReestrZayavExist(int numer)
        {
            return GeneralDBWork.GetIDByIntValue("reestr", numer, "Kn_Reg_Zayv", "N_Z") != -1;
        }

        //вернуть следующий порядковый номер для рееста заявлений / обращений
        public static int GetNextNumerToReestrVedomostey()
        {
            return GeneralDBWork.GetMaxNumerForAutoicrement("reestr", "Kn_Reg_Ved", "N_Vh") + 1;
        }
        //проверить существование порядкового номера для рееста заявлений / обращений
        public static bool IsNumerReestrVedomosteyExist(int numer)
        {
            return GeneralDBWork.GetIDByIntValue("reestr", numer, "Kn_Reg_Ved", "N_Vh") != -1;
        }

    }
}
