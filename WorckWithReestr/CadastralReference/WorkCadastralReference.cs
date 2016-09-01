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

namespace CadastralReference
{
    // работа со справкой
    class WorkCadastralReference
    {
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
                m_CadastralReferenceData.InitPagesDescription();
            }
            return m_CadastralReferenceData; 
        }
        #endregion 

        // открыть окно выбора заявки
        public static void SelectZayavka()
        {
            MessageBox.Show("Выбрать заявку");
            m_CadastralReferenceData.ZayavkaID = 1111;
            GetOrCreateCadastralReference();
        }
        // проверить существование справки
        // если нет создать
        public static void GetOrCreateCadastralReference()
        {
            m_CadastralReferenceData.CadastralReferenceID = 1111;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  группа сохранения и чтения из/в базу
        //IMAGE
        public static void SaveToDBImage(OnePageDescriptions opd)
        {
            Image img = GetImageFromArcGis();

            m_CadastralReferenceData.Pages[opd.index].Image = img;
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


        //Настроить макет
        public static void GenerateMaket(OnePageDescriptions opd)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            CheckAndSetPageLayoutMode();

            
            AddScalebar();
            AddNorthArrowTool();
            AddText();


            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            activeView.Refresh();
        }

        //Переключить слои
        public static void EnableLawrsFropPage(OnePageDescriptions opd, bool enable)
        {
            if (GetCadastralReferenceData().ZayavkaID == -1 || GetCadastralReferenceData().CadastralReferenceID == -1)
                return;
            //MessageBox.Show("EnableLawrsFropPage ->" + opd.Caption + "  \n enable =" + enable);
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            CheckAndSetPageLayoutMode();

            mxdoc.FocusMap.MapScale = 5000;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region  дополнительные функции
        //переключится в режим разметки страницы при необходимости
        private static void CheckAndSetPageLayoutMode()
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

        // получить изображенние карты из Арк ГИСа 
        private static Image GetImageFromArcGis()
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


        private static void f1()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxdoc.ActiveView;
            CheckAndSetPageLayoutMode();
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////

        private static void AddNorthArrowTool()
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

        private static void DeleteNordArrow()
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

        protected static void AddScalebar()
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
            envelope.PutCoords(pageSaze.X - 15, 1, pageSaze.X, 2.5); // Specify the location and size of the scalebar
            
            MSElement.Geometry = envelope as IGeometry;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(MSElement, 0);
        }

        private static void DeleteScalebar()
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

        private static void AddText()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            ITextElement textElement = new TextElementClass();
            textElement.Text = "Маштаб ( 1:" + Math.Round(GetShowMapScale()).ToString() + ")";
            ((TextElementClass)textElement).Size = 22;

            IPoint pageSaze = GetPageSaze();
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords(pageSaze.X / 2, pageSaze.Y - 2);
            IElement element = textElement as IElement;
            element.Geometry = point;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(element, 0);
        }

        private static IPoint GetPageSaze()
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

        //The following code shows one method for adding a new text element onto the page layout.In this example, ITool is used to get a mouse down event so users can place the text element anywhere on the page layout. The script only adds a new element if ArcMap is in layout view.To use this sample, paste the code into the OnMouseDown event in a newly created ITool. 
        //Note: This sub would be the MouseDown Event for AxPageLayoutControl in Engine or the PageLayout in ArcMap.
        //Where the following member variables would have already been set in other code: 
        //m_hookHelper is a ESRI.ArcGIS.Controls.IHookHelper
        //m_PageLayout is a ESRI.ArcGIS.Carto.IPageLayout
        public static void AddText(int Button, int Shift, int X, int Y)
        {
            //IActiveView activeView = m_PageLayout as IActiveView;
            //IGraphicsContainer graphicsContainer = m_PageLayout as IGraphicsContainer;

            ////Use the hookhelper to obtain the loaded doc's page layout.
            //m_PageLayout = m_hookHelper.PageLayout;

            ////Verify ArcMap is in layout view.
            //if (m_hookHelper.ActiveView is IPageLayout)
            //{
            //    //Create a point from the x,y coordinate parameters.
            //    IScreenDisplay screenDisplay = activeView.ScreenDisplay;
            //    IDisplayTransformation displayTransformation =
            //      screenDisplay.DisplayTransformation;
            //    ESRI.ArcGIS.Geometry.IPoint point = displayTransformation.ToMapPoint(X, Y);

            //    ITextElement textElement = new TextElementClass();
            //    textElement.Text = "My Map";

            //    IElement element = textElement as IElement;
            //    element.Geometry = point;

            //    graphicsContainer.AddElement(element, 0);

            //    //Refresh only the page layout's graphics.
            //    activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //}
            //else
            //{
            //    MessageBox.Show("This tool only works in layout view");
            //}
        }







        //показать текущий моштаб карты
        private static double GetShowMapScale()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IMap m = mxdoc.FocusMap;
            return m.MapScale;
        }


        //изменить рразмер на экране
        public void ZoomToPercent(IActiveView activeView)
        {
            IPageLayout pageLayout = activeView as IPageLayout;
            if (activeView is IPageLayout)
            {
                pageLayout.ZoomToPercent(50);
            }
            else
            {
                MessageBox.Show("This tool only functions in layout view");
            }
            activeView.Refresh();
        }



        //IGraphicsContainer
        //IGraphicsContainer provides access to the PageLayout object's graphic elements. Use this interface to add new elements or access existing ones. For example, a title at the top of a layout is a text element stored in the layout's graphics container.
        //The following code example moves all the elements in the layout 1 inch to the right:
        public void MoveAllElements(IActiveView activeView)
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


        //IGraphicsContainerSelect(selecting graphics)
        //Most objects that are graphics containers, such as PageLayout and Map, implement the IGraphicsContainerSelect interface to expose additional members for managing their element selection.For example, IGraphicsContainerSelect.UnselectAllElements can be used to clear an object's graphic element selection.
        //The following example returns the number of elements currently selected in the focus Map and PageLayout object:
        public void GraphicSelectionCount(IActiveView activeView)
        {
            IMap map = activeView.FocusMap;
            IPageLayout pageLayout = activeView as IPageLayout;
            IGraphicsContainer graphicsContainer = map as IGraphicsContainer;

            IGraphicsContainerSelect graphicsContainerSelect_Map = graphicsContainer as IGraphicsContainerSelect;
            IGraphicsContainerSelect graphicsContainerSelect_PageLayout = pageLayout as IGraphicsContainerSelect;

            int elementSelectionCount_Map =graphicsContainerSelect_Map.ElementSelectionCount;
            MessageBox.Show("Selected elements in the map: " + elementSelectionCount_Map.ToString());

            int elementSelectionCount_PageLayout = graphicsContainerSelect_PageLayout.ElementSelectionCount;
            MessageBox.Show("Selected elements in the page layout: " + elementSelectionCount_PageLayout.ToString());
        }

        //IPage is the primary interface on the Page object. Use this interface to access all the properties of an ArcMap page, including the page's border, background, background color, orientation, and size.
        //The esriPageFormID enumeration provides a convenient list of preselected page sizes for use by the Page object. For example, to change the layout to standard legal page size, pass in esriPageFormLegal to IPage.FormID.This is much quicker than setting a custom size with IPage.PutCustomSize.
        //The following code uses the esriPageFormID enumeration to quickly change the page size.It is beneficial if you used the previous code sample to change the page's size and color.
        public void SetLegalPageSize(IPageLayout pageLayout)
        {
            IPage page = new Page();
            double x = 0;
            double y = 0;
            page = pageLayout.Page;
            page.FormID = esriPageFormID.esriPageFormLegal;
            page.QuerySize(out x, out y);
            MessageBox.Show("The page size is now: " + x + " x " + y);
        }



        #endregion
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


