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
    public static class WorkCadastralReference_MAP
    {
        //переключится в режим разметки страницы при необходимости
        public static void CheckAndSetPageLayoutMode()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            if (!(mxdoc.ActiveView is IPageLayout))
            {
                //прямой способ
                mxdoc.ActiveView = mxdoc.PageLayout as IActiveView;

                ////способ через команду
                //UID uid = new UID();
                //uid.Value = "{6570248A-A258-11D1-8740-0000F8751720}";
                //ESRI.ArcGIS.Framework.ICommandItem cmdItem = ArcMap.Application.Document.CommandBars.Find(uid, false, false);
                //cmdItem.Execute();
            }
        }

        //спозиционировать на выбраном объекте, отценриовать
        public static void SetScaleAndCentred()
        {
            //
            //показать на карте
            //SharedClasses.GeneralMapWork.ShowOnMap(ITable table, int objectID)
            //спозиционироваться на выбранном объекте, установить масштаб
            //SharedClasses.GeneralMapWork.PositionedOnSelectedObjectAndSetScale(IMxDocument mxDoc, IMap map)

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            if ((mxdoc.ActiveView is IPageLayout))
                mxdoc.ActiveView = mxdoc.FocusMap as IActiveView;

            IFeature selectedFeature = null;
            selectedFeature = (mxdoc.FocusMap.FeatureSelection as IEnumFeature).Next();

            if (selectedFeature != null)
            {
                IEnvelope envelope = selectedFeature.Shape.Envelope;
                envelope.Expand(2, 2, true);
                mxdoc.ActiveView.Extent = envelope;
                //mxdoc.ActiveView.Refresh();
            }
        }

        // переключить слои
        public static void EnableLayersFromPages(OnePageDescriptions opd)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            StringCollection tmp = new StringCollection();
            foreach (string s in opd.Layers)
                tmp.Add(s.ToLower());

            IEnumLayer enumLayer = mxdoc.FocusMap.Layers;
            ILayer layer = enumLayer.Next();
            while (layer != null)
            {
                if (tmp.Contains(layer.Name.ToLower()))
                {
                    layer.Visible = true;
                    tmp.Remove(layer.Name.ToLower());
                }
                else
                    layer.Visible = false;
                // всегда показывать слой где расположены объекты
                //if (WorkCadastralReference.GetCadastralReferenceData().ObjectLayerName.ToLower() == layer.Name.ToLower())
                //    layer.Visible = true;

                layer = enumLayer.Next();
            }

            if (tmp.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("Лист \"" + opd.Caption + "\". Отсутствуют слои:\n");
                foreach (string s in tmp)
                {
                    sb.Append("[");
                    sb.Append(s);
                    sb.Append("]\n");
                }
                MessageBox.Show(sb.ToString());
            }
            mxdoc.ActiveView.ContentsChanged();
        }



        // получить изображенние карты из Арк ГИСа 
        public static Image GetImageFromArcGis()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            CheckAndSetPageLayoutMode();

            string tmpFileName = System.IO.Path.GetTempFileName();

            IExport exporter = new ExportPNGClass();
            exporter.ExportFileName = tmpFileName;
            exporter.Resolution = 96;

            IEnvelope pixelBBOX = new EnvelopeClass();
            pixelBBOX.XMin = activeView.ExportFrame.left;
            pixelBBOX.XMax = activeView.ExportFrame.right;
            pixelBBOX.YMin = activeView.ExportFrame.top;
            pixelBBOX.YMax = activeView.ExportFrame.bottom;
            exporter.PixelBounds = pixelBBOX;

            int hdc = exporter.StartExporting();
            tagRECT exporterRectangle = activeView.ExportFrame;
            activeView.Output(hdc, (int)exporter.Resolution, ref exporterRectangle, null, null);
            exporter.FinishExporting();
            exporter.Cleanup();

            Image img = Image.FromFile(tmpFileName);
            //System.IO.File.Delete(tmpFileName);
            return img;
        }


        public static StringCollection GetListOfAllLaers()

        {
            StringCollection ret = new StringCollection();
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;

                IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
                ILayer layer = enumLayer.Next();
                string str = "";
                string str1 = "";
                while (layer != null)
                {
                    string ln = layer.Name;
                //string tn = "";
                //int iii = -1;

                IFeatureLayer pFeatureLayer = layer as IFeatureLayer;
                if (pFeatureLayer != null)
                {
                    //string s = string.Format("LN({0}) FC_AN({1}) OC_ID({2})", pFeatureLayer.Name, pFeatureLayer.FeatureClass.AliasName, pFeatureLayer.FeatureClass.ObjectClassID);
                    //MessageBox.Show(s);
                    //iii = pFeatureLayer.FeatureClass.ObjectClassID;
                    //ret.Add(s);
                    //ret.Add(layer.Name);
                }
                else
                    str += "\n" + layer.Name;

                IDisplayTable pDisplayTable = layer as IDisplayTable;
                if (pDisplayTable != null)
                {
                    IDataset dsTable = pDisplayTable.DisplayTable as IDataset;
                    if (dsTable != null)
                    {
                        //MessageBox.Show(string.Format("dsTable.Name({0})", dsTable.Name ));
                        //tn = dsTable.Name;
                    }
                    else
                        str1 += "\n" + layer.Name;
                }

                //string s = string.Format("({0})[{1}]-{2}", iii , tn, ln );
                ret.Add(ln);

                    layer = enumLayer.Next();
                }
                if(str != "")
                    MessageBox.Show("not IFeatureLayer: " + str);
                if (str1 != "")
                    MessageBox.Show("not IDataset: " + str1);
            return ret;
        }


        /// <summary>
        /// выбрать ближайшеий больший стандартный маштаб
        /// </summary>
        public static void SetStandartMapSkale()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            int curSkale = Convert.ToInt32(mxdoc.FocusMap.MapScale);
            int newSkale = -1;
            int[] standartSkale = { 1, 4, 5, 10, 15, 20, 25, 40, 50, 75, 100, 200, 400, 500, 800, 1000, 2000, 5000, 10000, 20000, 25000, 50000 };
            foreach (int ss in standartSkale)
            {
                if (ss >= curSkale)
                {
                    newSkale = ss;
                    break;
                }
            }
            //MessageBox.Show(string.Format("curSkale ={0} newSkale = {1}", curSkale, newSkale));
            mxdoc.FocusMap.MapScale = newSkale;
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////

        public static void AddNorthArrowTool()
        {
            DeleteNordArrow();

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            //IPoint pageSaze = GetPageSaze();
            //point.PutCoords(pageSaze.X / 2, pageSaze.Y - 2);
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(1, 1, 6, 6); // Specify the location and size of the scalebar

            IStyleGallery styleGallery = mxdoc.StyleGallery;
            IEnumStyleGalleryItem enumStyleGallery = styleGallery.get_Items("North Arrows", "ESRI.STYLE", "Default");

            IStyleGalleryItem northArrowStyle = enumStyleGallery.Next();
            while (northArrowStyle != null)
            {
                if (northArrowStyle.Name == "ESRI North 1")
                {
                    break;
                }
                northArrowStyle = enumStyleGallery.Next();
            }

            INorthArrow northArrow = northArrowStyle.Item as INorthArrow;
            northArrow.Map = mxdoc.FocusMap;

            IMapSurroundFrame pMSFrame = new MapSurroundFrameClass();
            pMSFrame.MapSurround = northArrow;
            IElement MSElement = pMSFrame as IElement;
            MSElement.Geometry = envelope as IGeometry;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(MSElement, 0);
        }

        public static void DeleteNordArrow()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.Reset();

            //only one North Arrow should be in a Layout
            IElement element = gc.Next();
            while (element != null)
            {
                if (element is IMapSurroundFrame)
                {
                    IMapSurroundFrame MSF = element as IMapSurroundFrame;
                    if (MSF.MapSurround is INorthArrow)
                    {
                        gc.DeleteElement(element);
                    }
                }
                element = gc.Next();
            }

        }

        public static void AddScalebar()
        {
            DeleteScalebar();

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IStyleGallery styleGallery = mxdoc.StyleGallery;
            IEnumStyleGalleryItem enumStyleGallery = styleGallery.get_Items("Scale Bars", "ESRI.Style", "");

            IStyleGalleryItem scalebarStyle = enumStyleGallery.Next();
            for (int i = 0; i < 4; i++)
            {
                scalebarStyle = enumStyleGallery.Next();
            }

            IScaleBar scalebar = scalebarStyle.Item as IScaleBar;
            scalebar.Map = mxdoc.FocusMap;

            scalebar.Units = esriUnits.esriKilometers;

            IMapSurroundFrame pMSFrame = new MapSurroundFrameClass();
            pMSFrame.MapSurround = scalebar;
            IElement MSElement = pMSFrame as IElement;


            IPoint pageSaze = GetPageSaze();

            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(pageSaze.X - 8, 1, pageSaze.X - 1, 2.5); // Specify the location and size of the scalebar

            MSElement.Geometry = envelope as IGeometry;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(MSElement, 0);
        }

        public static void DeleteScalebar()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;

            //only one scale bar should be in a Layout
            gc.Reset();
            IElement element = gc.Next();
            while (element != null)
            {
                if (element is IMapSurroundFrame)
                {
                    IMapSurroundFrame MSF = element as IMapSurroundFrame;
                    if (MSF.MapSurround is IScaleBar)
                    {
                        gc.DeleteElement(element);
                    }
                }
                element = gc.Next();
            }
        }

        public static void AddText()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            string name = "ScaleCaption";
            DeleteElementByName(name);

            ITextElement textElement = new TextElementClass();
            textElement.Text = "Маштаб ( 1:" + Math.Round(GetShowMapScale()).ToString() + ")";
            ((TextElementClass)textElement).Size = 22;
            ((TextElementClass)textElement).Name = name;

            IPoint pageSaze = GetPageSaze();
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords(pageSaze.X / 2, pageSaze.Y - 2);
            IElement element = textElement as IElement;
            element.Geometry = point;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(element, 0);
        }


        public static void DeleteElementByName(string name)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;

            //only one scale bar should be in a Layout
            gc.Reset();
            IElement element = gc.Next();
            while (element != null)
            {
                IElementProperties elementProp = element as IElementProperties;
                if (elementProp.Name == name)
                    gc.DeleteElement(element);
                element = gc.Next();
            }
        }



        ///<summary>Add a Legend to the Page Layout from the Map.</summary>
        ///<param name="pageLayout">An IPageLayout interface.</param>
        ///<param name="map">An IMap interface.</param>
        ///<param name="posX">A System.Double that is X coordinate value in page units for the start of the Legend. Example: 2.0</param>
        ///<param name="posY">A System.Double that is Y coordinate value in page units for the start of the Legend. Example: 2.0</param>
        ///<param name="legW">A System.Double that is length in page units of the Legend in both the X and Y direction. Example: 5.0</param>
        /// 
        ///<remarks></remarks>
        public static void AddLegend(ESRI.ArcGIS.Carto.IPageLayout pageLayout, ESRI.ArcGIS.Carto.IMap map, System.Double posX, System.Double posY, System.Double legW)
        {

            if (pageLayout == null || map == null)
            {
                return;
            }
            ESRI.ArcGIS.Carto.IGraphicsContainer graphicsContainer = pageLayout as ESRI.ArcGIS.Carto.IGraphicsContainer; // Dynamic Cast
            ESRI.ArcGIS.Carto.IMapFrame mapFrame = graphicsContainer.FindFrame(map) as ESRI.ArcGIS.Carto.IMapFrame; // Dynamic Cast
            ESRI.ArcGIS.esriSystem.IUID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = "esriCarto.Legend";
            ESRI.ArcGIS.Carto.IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame((ESRI.ArcGIS.esriSystem.UID)uid, null); // Explicit Cast

            //Get aspect ratio
            ESRI.ArcGIS.Carto.IQuerySize querySize = mapSurroundFrame.MapSurround as ESRI.ArcGIS.Carto.IQuerySize; // Dynamic Cast
            System.Double w = 0;
            System.Double h = 0;
            querySize.QuerySize(ref w, ref h);
            System.Double aspectRatio = w / h;

            ESRI.ArcGIS.Geometry.IEnvelope envelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            envelope.PutCoords(posX, posY, (posX * legW), (posY * legW / aspectRatio));
            ESRI.ArcGIS.Carto.IElement element = mapSurroundFrame as ESRI.ArcGIS.Carto.IElement; // Dynamic Cast
            element.Geometry = envelope;
            graphicsContainer.AddElement(element, 0);
        }


        // изменить размер фрейма данных
        public static void ChangeSizeDateFrame()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IGraphicsContainer graphicsContainer = mxdoc.PageLayout as IGraphicsContainer;
            graphicsContainer.Reset();

            IElement dateFrameElement = null;
            IElement element = graphicsContainer.Next();
            while (element != null)
            {
                //use IElementProperties interface to get or set the Name of an 
                //Element
                IElementProperties elementProp = element as IElementProperties;
                if (elementProp.Type == "Data Frame")
                {
                    dateFrameElement = element;
                    break;
                }
                element = graphicsContainer.Next();
            }

            if (dateFrameElement != null)
            {
                IPoint pageSaze = GetPageSaze();

                // старый размер 
                // dateFrameElement.Geometry.Envelope

                IEnvelope envelope = new EnvelopeClass();
                envelope.PutCoords(1, 2.5, pageSaze.X - 1, pageSaze.Y - 2.5);
                dateFrameElement.Geometry = envelope as IGeometry;
            }
        }



        //получить размер листа
        public static IPoint GetPageSaze()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IPage page = new Page();
            double x = 0;
            double y = 0;
            page = mxdoc.PageLayout.Page;
            page.QuerySize(out x, out y);

            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords(x, y);
            return point;
        }

        //получить текущий моштаб карты
        public static double GetShowMapScale()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IMap m = mxdoc.FocusMap;
            return m.MapScale;
        }


        //изменить рразмер на экране
        public static void ZoomToPercent(int zoomFactor = 50)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            IPageLayout pageLayout = activeView as IPageLayout;
            if (activeView is IPageLayout)
            {
                pageLayout.ZoomToPercent(zoomFactor);

                //The current zoom percent. 100 means 1:1. 200 means twice normal size, etc.
                //double ZoomPercent { get; }
                //Magnify the page by a certain percentage. 100 means actual size. 200 means twice normal size, etc.
                //void ZoomToPercent(int percent);
                //Fit the whole page in the window.
                //void ZoomToWhole();
                //Fit the width of the page to the screen.
                //void ZoomToWidth();
            }
            else
            {
                MessageBox.Show("This tool only functions in layout view");
            }
            activeView.Refresh();
        }


        // примеры

        //IGraphicsContainer provides access to the PageLayout object's graphic elements. Use this interface to add new elements or access existing ones. For example, a title at the top of a layout is a text element stored in the layout's graphics container.
        //The following code example moves all the elements in the layout 1 inch to the right:
        public static void MoveAllElements(IActiveView activeView)
        {
            IPageLayout pageLayout = new PageLayoutClass();

            if (activeView is IPageLayout)
            {
                pageLayout = activeView as IPageLayout;
                IGraphicsContainer graphicsContainer = pageLayout as IGraphicsContainer;

                //Loop through all the elements and move each one inch.
                graphicsContainer.Reset();
                ITransform2D transform2D = null;
                IElement element = graphicsContainer.Next();
                while (element != null)
                {
                    transform2D = element as ITransform2D;
                    transform2D.Move(1, 0);
                    element = graphicsContainer.Next();
                }
            }
            else
            {
                MessageBox.Show("This tool only works in pagelayout view.");
            }

            //Refresh only the page layout's graphics.
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }


   
    }
}


////Listening to map events
////The following code example demonstrates listening to map events:
////private IMapEvents_FeatureClassChangedEventHandler dFeatClsChangedE;
////private IMapEvents_VersionChangedEventHandler dVerChangedE;
//private void ListenToMapEvents(IApplication application)
//{
//    IDocument document = application.Document;
//    IMapDocument mapDoc = document as IMapDocument;
//    IActiveView activeView = mapDoc.ActiveView;
//    IMap map = activeView.FocusMap;

//    IMapEvents_Event mapEvents = map as IMapEvents_Event;

//    //Initialize the delegate to point to a function where you respond to the event being raised.
//    dFeatClsChangedE = new IMapEvents_FeatureClassChangedEventHandler(OnFeatureClassChangedFunction);
//    dVerChangedE = new IMapEvents_VersionChangedEventHandler(OnVersionChangedFunction);

//    mapEvents.VersionChanged += dVerChangedE;

//    // Wire the delegate to the FeatureClassChanged event of the mapEvents variable.
//    mapEvents.FeatureClassChanged += dFeatClsChangedE;
//}

//private void OnFeatureClassChangedFunction(IFeatureClass oldClass,
//  IFeatureClass newClass)
//{
//    // Listen to the FeatureClassChanged event of IMapEvents.
//    MessageBox.Show("Feature Class changed");
//}

//private void OnVersionChangedFunction(IVersion oldVersion, IVersion newVersion)
//{
//    MessageBox.Show("Version Changed");
//}



////Loading a table
////The following code example loads a table into the focus map:
//public void AddTable(IMap map, IMxDocument mxDocument)
//{
//    ITableCollection tableCollection = map as ITableCollection;
//    string tablePathName =
//      "C:\\Program Files\\ArcGIS\\DeveloperKit\\SamplesNET\\data\\Y2000HurricaneData";
//    string tableName = "2000_hrcn.dbf";
//    ITable table = OpenTable(tablePathName, tableName);

//    if (table == null)
//    {
//        return;
//    }
//    else
//    {
//        tableCollection.AddTable(table);
//        mxDocument.UpdateContents();
//    }
//}

//public ITable OpenTable(string pathName, string tableName)
//{
//    // Create the workspace name object.
//    IWorkspaceName workspaceName = new WorkspaceNameClass();
//    workspaceName.PathName = pathName;
//    workspaceName.WorkspaceFactoryProgID =
//      "esriDataSourcesFile.shapefileworkspacefactory";

//    // Create the table name object.
//    IDatasetName dataSetName = new TableNameClass();
//    dataSetName.Name = tableName;
//    dataSetName.WorkspaceName = workspaceName;

//    // Open the table.
//    IName name = dataSetName as IName;
//    ITable table = name.Open() as ITable;
//    return table;
//}


