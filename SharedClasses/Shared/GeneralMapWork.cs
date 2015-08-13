using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace SharedClasses
{
    public class GeneralMapWork
    {
        //соответствие таблиц данных с таблицами графики
        //    - пространства имен, имена таблиц, имена полей

        //показать на карте
        //    - проверить и получить связаный слой
        //    - показать, отмаштабировать

        //из карты в карточьку
        //    - получит все выделеные объекты
        //    - запросить (форма список) (если объект один не спрашивать) нужный объект
        //    - открыть карточьку
        //        - получть имя формы из соответствия
        //        - вызвать (имя формы).ShowForView();



        //
        public static void ShowOnMap(string tablName)
        {

        }



        //---------------------------------------------------------------------------------------
        #region общее
        //***
        //private static string m_senpl = null;

        //
        //public static string senpl()
        //{
        //    return null;
        //}


        #endregion


    }
}

// примеры

// текущий документ / карта
//IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
//IMap map = mxDoc.FocusMap as IMap;


// выбрать по клику мышы видемый объект
//IMxDocument mxDoc = ArcMap.Document;
//IActiveView  m_focusMap = mxDoc.FocusMap as IActiveView;
//IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y) as IPoint;
//ArcMap.Document.FocusMap.SelectByShape(point, null, false);
//m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

