using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace SharedClasses
{
    public static class GeneralMapWork
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

            string l = "Laers:\n";


            AddInsAppInfo ai = GeneralApp.GetAddInsAppInfo();
            if (ai != null && ai.GetDocument() != null)
            {
                IMxDocument mxdoc = ai.GetDocument() as IMxDocument;
                IMap map = mxdoc.FocusMap;

                IEnumLayer enumLayer = map.Layers;
                ILayer layer = enumLayer.Next();

                while (layer != null)
                {
                    IDataLayer2 idl = layer as IDataLayer2;
                    if (idl != null)
                        l += idl.DataSourceName.NameString + "\n";
                    
                    layer = enumLayer.Next();
                }

            }


            System.Windows.Forms.MessageBox.Show(tablName + "\n" + l);
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


        // string selectedState = null;
        // IFeatureLayer2 citiesFL = null;
        // IFeatureLayer2 statesLayer = null;

		
		// //заполнить список даннымы из слоя
        // private void btnPopulateList_Click(object sender, EventArgs e)
        // {
            // lstStates.Items.Clear();
            // IMxDocument mxdoc = m_application.Document as IMxDocument;
            // IMap map = mxdoc.FocusMap;

            // IEnumLayer enumLayer = map.Layers;
            // ILayer layer = enumLayer.Next();

            // while (layer != null)
            // {
                // if (layer is IFeatureLayer2 && layer.Name == "U.S. States (Generalized)")
                // {
                    // statesLayer = layer as IFeatureLayer2;
                // }
                // layer = enumLayer.Next();
            // }

            // if (statesLayer == null)
            // { return; }

            // IFeatureCursor statesFCursor = statesLayer.FeatureClass.Search(null, true);
            // int state_nameIndex = statesFCursor.Fields.FindField("STATE_NAME");
            // IFeature state = statesFCursor.NextFeature();

            // if (state_nameIndex < 0)
            // { return; }

            // while (state != null)
            // {
                // lstStates.Items.Add(state.Value[state_nameIndex]);
                // state = statesFCursor.NextFeature();
            // }
        // }

		// // обработчик выбора в списке
        // private void lstStates_SelectedIndexChanged(object sender, EventArgs e)
        // {
            // if (lstStates.SelectedIndex >= 0)
            // {
                // selectedState = lstStates.SelectedItem.ToString();
            // }
        // }

		
		// //показать выбронное на карте
        // private void btnSelect_Click(object sender, EventArgs e)
        // {
            // IMxDocument mxdoc = m_application.Document as IMxDocument;
            // IMap map = mxdoc.FocusMap;
            // IEnumLayer enumLayer = map.Layers;
            // ILayer layer = enumLayer.Next();

            // while (layer != null)
            // {
                // if (layer.Name == "U.S. Cities" && layer is IFeatureLayer2)
                // {
                    // citiesFL = layer as IFeatureLayer2;
                // }
                // layer = enumLayer.Next();
            // }

            // IFeatureClass stateFC = statesLayer.FeatureClass;
            // IQueryFilter2 qF = new QueryFilterClass();
            // qF.WhereClause = string.Format("STATE_NAME='{0}'", selectedState);
            // IFeatureCursor stateFCursor = stateFC.Search(qF, true);
            // //just one state is selected
            // IFeature selectedStateFeature = stateFCursor.NextFeature();
            // IGeometry5 shapeOfSelectedState = selectedStateFeature.Shape as IGeometry5;


            // ISpatialFilter sF = new SpatialFilterClass();
            // sF.Geometry = shapeOfSelectedState;
            // sF.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

            // IFeatureSelection citiesFeatureSelection = citiesFL as IFeatureSelection;
            // citiesFeatureSelection.SelectFeatures(sF, esriSelectionResultEnum.esriSelectionResultNew, false);


            // //zoom to selected features
            // mxdoc.ActiveView.Extent = shapeOfSelectedState.Envelope;
            // //IFeatureClass citiesFC = citiesFL.FeatureClass;
            // mxdoc.ActiveView.Refresh();

            // //***   - 

            // ICursor citiesCursor = null;
            // citiesFeatureSelection.SelectionSet.Search(null, true, out citiesCursor);
            // int pop1990Index = citiesCursor.Fields.FindField("POP1990");
            // long totalPopulation = 0;
            // IRow city = citiesCursor.NextRow();
            // while (city != null)
            // {
                // totalPopulation += long.Parse(city.Value[pop1990Index].ToString());
                // city = citiesCursor.NextRow();
            // }
            // lblReport.Text = String.Format("Number of Selected Cities: {0} \n",
            // citiesFeatureSelection.SelectionSet.Count);
            // lblReport.Text += String.Format("Total Population: {0}", totalPopulation);
        // }

		// //очистить список и т.д.
        // private void btnClear_Click(object sender, EventArgs e)
        // {
            // selectedState = "";
            // lstStates.ClearSelected();
            // lblReport.Text = "";
            // //todo: clear selected features in specified layer
            // (citiesFL as IFeatureSelection).Clear();

            // IMxDocument mxdoc = m_application.Document as IMxDocument;
            // if (statesLayer != null)
            // {
                // mxdoc.ActiveView.Extent = (statesLayer as ILayer).AreaOfInterest;
            // }
            // mxdoc.ActiveView.Refresh();
        // }






















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

