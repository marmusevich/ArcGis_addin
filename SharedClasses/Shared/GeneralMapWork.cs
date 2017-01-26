
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;




namespace SharedClasses
{
    public static class GeneralMapWork
    {
        //вспомогательный для выбора значений
        public class SelectedTableNameAndObjectID
        {
            public readonly string aliasName;
            public readonly string tabName;
            public readonly int id;

            public SelectedTableNameAndObjectID(string aliasName, string tabName, int id)
            {
                this.aliasName = aliasName;
                this.tabName = tabName;
                this.id = id;
            }

            public override string ToString()
            {
                return aliasName;
            }
        }

        //---------------------------------------------------------------------------------------
        #region ---

        //радиус чуствительности
        private static int g_pixelTolRound = 1;
        //коэфициэнт маштобирования при просмотре объекта на карте
        private static double g_scaleFactorForShowObject = 3.0;

        //текущий документ 
        public static IMxDocument GetMxDocument()
        {
            AddInsAppInfo ai = GeneralApp.GetAddInsAppInfo();
            if (ai != null && ai.GetDocument() != null)
            {
                return ai.GetDocument() as IMxDocument;
            }
            else
                return null;
        }

        // вернуть имя выбраного слоя и объекта
        // если несколько - запрос какой
        public static void GetSelectedTableNameAndObjectID(out string table_name, out int objectID, ref string[] allTableName)
        {
            table_name = "";
            objectID = -1;

            List<SelectedTableNameAndObjectID> arr = new List<SelectedTableNameAndObjectID>();
            SelectedTableNameAndObjectID tmp = null;

            IMxDocument mxDoc = GetMxDocument();
            if (mxDoc != null)
            {
                //переберем все выбранные объекты на карте
                IEnumFeature enumFeature = mxDoc.FocusMap.FeatureSelection as IEnumFeature;
                IFeature feature = enumFeature.Next();
                while (feature != null)
                {
                    //если можем добовляем в масив для выбора
                    tmp = null;
                    string aliasName = "";
                    string tabName = "";
                    if (feature.Class != null)
                    {
                        aliasName = feature.Class.AliasName;
                        if ((feature.Class) is IDataset)
                        {
                            tabName = (feature.Class as IDataset).Name;
                            //проверка на принадлежность нашему проекту
                            if (ChekTablNameIsThisPrj(ref tabName, ref allTableName))
                                tmp = new SelectedTableNameAndObjectID(string.Format("{0} (ID = {1})", aliasName, feature.get_Value(feature.Table.FindField("ID_MSB_OBJ"))), tabName, feature.OID);
                        }
                    }
                    if (tmp != null)
                        arr.Add(tmp);

                    feature = enumFeature.Next();
                }
            }
            SelectedTableNameAndObjectID sel = null;

            //если отобрано больше чем 1, спросить у пользователя
            if (arr.Count > 1)
            {
                frmSelectObjAndLayerForToolShowObjInfo frm = new frmSelectObjAndLayerForToolShowObjInfo();
                frm.lsbSelectedLayers.Items.AddRange(arr.ToArray());
                frm.ShowDialog();
                sel = frm.lsbSelectedLayers.SelectedItem as SelectedTableNameAndObjectID;
                frm.Dispose();
            }
            else // или последний
            {
                sel = tmp;
            }

            // если есть что вернуть то вернуть
            if (sel != null)
            {
                table_name = sel.tabName;
                objectID = sel.id;
            }
        }

        //проверка на принадлежность нашему проекту имини таблицы
        private static bool ChekTablNameIsThisPrj(ref string tabName, ref string[] allTableName)
        {
            bool ret = false;
            foreach (string str in allTableName)
                if (tabName.Contains(str))
                {
                    ret = true;
                    break;
                }
            return ret;
        }

        //показать на карте
        public static void ShowOnMap(ITable table, int objectID)
        {
            try
            {

                bool isShow = false;
                //- проверить и получить связаный слой
                IMxDocument mxDoc = GetMxDocument();
                if (mxDoc != null)
                {
                    IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
                    ILayer layer = enumLayer.Next();
                    while (layer != null)
                    {
                        if (layer is IFeatureLayer2)
                        {
                            IFeatureClass fc = (layer as IFeatureLayer2).FeatureClass;
                            if (fc != null && fc.CLSID.Compare(table.CLSID))
                            {
                                //выбрать в слое объект
                                SelectLayersFeatures(layer as IFeatureLayer, string.Format("OBJECTID = {0}", objectID));
                                //спозиционироваться на выбранном объекте, установить масштаб
                                PositionedOnSelectedObjectAndSetScale(mxDoc, mxDoc.FocusMap);
                                isShow = true;
                                break;
                            }
                        }
                        layer = enumLayer.Next();
                    }
                }

                if (!isShow)
                    System.Windows.Forms.MessageBox.Show("Немогу получить связаный слой.\n(Добавте нужный слой в таблицу содержания.)");

            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Показать на карте объект '{0}' id {1}", table, objectID));
                //GeneralApp.ShowErrorMessage(string.Format("Ошибка при показе на карте объекта '{0}' id {1}", table, objectID));
            }
        }

        //спозиционироваться на выбранном объекте, установить масштаб
        public static void PositionedOnSelectedObjectAndSetScale(IMxDocument mxDoc, IMap map)
        {
            IFeature selectedFeature = (map.FeatureSelection as IEnumFeature).Next();
            if (selectedFeature != null)
            {
                IEnvelope envelope = selectedFeature.Shape.Envelope;
                envelope.Expand(g_scaleFactorForShowObject, g_scaleFactorForShowObject, true);
                mxDoc.ActiveView.Extent = envelope;
                mxDoc.ActiveView.Refresh();
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------
        #region выбор объекта на карте, отмена выбора
        //выбрать по клику мышы видемый объект на текущей карте
        public static void SelectFeaturesScreenAndPartialRefresh(int x, int y)
        {
            IMxDocument mxDoc = GetMxDocument();
            if (mxDoc != null)
            {
                IActiveView m_focusMap = mxDoc.FocusMap as IActiveView;
                SelectFeaturesScreenPoint(mxDoc.FocusMap, x, y, g_pixelTolRound);
                m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
        }
        //очистить выделение в текущей карте
        public static void ClearMapSelection()
        {
            IMxDocument mxDoc = GetMxDocument();
            if (mxDoc != null)
            {
                ClearMapSelection(mxDoc.FocusMap);
            }
        }
        //очистить выделение во всех слоях
        public static void ClearAllLayerSelection()
        {
            IMxDocument mxDoc = GetMxDocument();
            if (mxDoc != null)
            {
                IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
                ILayer layer = enumLayer.Next();
                while (layer != null)
                {
                    if (layer is IFeatureLayer)
                    {
                        ClearLayerSelection(layer as IFeatureLayer);
                    }
                    layer = enumLayer.Next();
                }
            }
        }

        //выбрать по клику мышы видемый объект
        public static void SelectFeaturesScreenPoint(IMap pMap, int x, int y, int pixelTol)
        {
            tagRECT r;
            //Construct a small rectangle out of the x,y coordinates' pixel tolerance.
            r.left = x - pixelTol; //Upper left x, top left is 0,0.  
            r.top = y - pixelTol; //Upper left y, top left is 0,0.
            r.right = x + pixelTol; //Lower right x, top left is 0,0. 
            r.bottom = y + pixelTol; //Lower right y, top left is 0,0.

            //Transform the device rectangle into a geographic rectangle via the display transformation.  
            IEnvelope pEnvelope = new EnvelopeClass();
            IActiveView pActiveView = pMap as IActiveView;
            IDisplayTransformation pDisplayTrans = pActiveView.ScreenDisplay.DisplayTransformation;
            pDisplayTrans.TransformRect(pEnvelope, ref r, 5);

            pEnvelope.SpatialReference = pMap.SpatialReference;

            ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
            pSelectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            pMap.SelectByShape(pEnvelope, pSelectionEnvironment, false);
        }
        //выбрать в слое объект
        public static void SelectLayersFeatures(IFeatureLayer pFeatureLayer, string WhereClause)
        {
            IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
            if (pFeatureSelection != null)
            {
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = WhereClause;
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

                //System.Windows.Forms.MessageBox.Show("pFeatureSelection.SelectionSet.Count = " + pFeatureSelection.SelectionSet.Count);
            }
        }
        //выбрать по полигону
        public static void SelectFeaturesPolygon(IMap pMap, IPolygon pPolygon)
        {
            ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
            pSelectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            pMap.SelectByShape(pPolygon, pSelectionEnvironment, false);
        }
        //очистить выделение
        public static void ClearMapSelection(IMap pMap)
        {
            pMap.ClearSelection();
        }
        //очистить выделение в слое
        public static void ClearLayerSelection(IFeatureLayer pFeatureLayer)
        {
            IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
            if (pFeatureSelection != null)
            {
                pFeatureSelection.Clear();
            }
        }
        #endregion
    }
}

