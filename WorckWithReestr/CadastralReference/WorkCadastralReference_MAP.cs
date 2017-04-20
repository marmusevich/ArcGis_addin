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

        /// <summary>
        /// Запоменание выбранного юъекта на карте с проверками
        /// </summary>
        public static void EnableSelectToo()
        {
            //ESRI.ArcGIS.Desktop.AddIns.Tool tool = new arcTool_SelectOdject();
            //ESRI.ArcGIS.Framework.ICommandItem tool = new arcTool_SelectOdject();
            //ArcMap.Application.CurrentTool = tool;

            arcTool_SelectOdject.SetToolActiveInToolBar(ArcMap.Application);
        }

        public static void SelectObjektInMap()
        {
            if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaID == -1 || WorkCadastralReference.GetCadastralReferenceData().IsReferenceClose)
                return;

            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IActiveView activeView = mxDoc.ActiveView;

            int objectID = -1;
            string object_Discription = "";

            //переберем все выбранные объекты на карте
            IEnumFeature enumFeature = mxDoc.FocusMap.FeatureSelection as IEnumFeature;
            IFeature feature = enumFeature.Next();
            while (feature != null)
            {
                string tabName = "";
                if (feature.Class != null)
                {
                    if ((feature.Class) is IDataset)
                    {
                        tabName = (feature.Class as IDataset).Name;
                        //проверка на принадлежность нашему проекту
                        if (CadastralReferenceData.ObjectLayer.DataPath.ToLower() == tabName.ToLower())
                        {
                            objectID = feature.OID;
                            object_Discription = "" + feature.get_Value(feature.Fields.FindField("Adr_Obj")) as string;
                        }
                    }
                }
                feature = enumFeature.Next();
            }
            WorkCadastralReference.GetCadastralReferenceData().MapObjectID_Discription = object_Discription;
            WorkCadastralReference.GetCadastralReferenceData().MapObjectID = objectID;

            if (WorkCadastralReference.GetCadastralReferenceData().MapObjectID != -1)
            {
                WorkCadastralReference_DB.EditZayavkaData(WorkCadastralReference.GetCadastralReferenceData().ZayavkaID, WorkCadastralReference.GetCadastralReferenceData().MapObjectID, null, null);
                WorkCadastralReference.LoadFromDB();
            }

            //GeneralMapWork.ClearMapSelection();
            //GeneralMapWork.ClearAllLayerSelection();
            ArcMap.Application.CurrentTool = null;
        }


        //спозиционировать на выбраном объекте, отценриовать
        public static void SetScaleAndCentred()
        {
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


        //показать на карте
        public static void ShowOnMap(int objectID)
        {
            try
            {
                //- проверить и получить связаный слой
                IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
                if (mxDoc == null)
                    return;

                IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
                ILayer layer = enumLayer.Next();
                while (layer != null)
                {
                    OneLayerDescriptions onl = new OneLayerDescriptions(layer);

                    if (CadastralReferenceData.ObjectLayer.Equals(onl))
                    { 
                        //MessageBox.Show("CadastralReferenceData.ObjectLayerName=" + CadastralReferenceData.ObjectLayerName + "     layer.Name=" + layer.Name);

                        IFeatureClass fc = (layer as IFeatureLayer2).FeatureClass;
                        if (fc != null)
                        {
                            SharedClasses.GeneralMapWork.SelectLayersFeatures(layer as IFeatureLayer, string.Format("OBJECTID = {0}", objectID));
                            IFeature selectedFeature = (mxDoc.FocusMap.FeatureSelection as IEnumFeature).Next();


                            WorkCadastralReference.GetCadastralReferenceData().MapObjectID_Discription = "" + selectedFeature.get_Value(selectedFeature.Fields.FindField("Adr_Obj")) as string;
                            //MessageBox.Show("" + selectedFeature.get_Value(selectedFeature.Fields.FindField("Adr_Obj")) as string);

                            if ((mxDoc.ActiveView is IPageLayout))
                                mxDoc.ActiveView = mxDoc.FocusMap as IActiveView;

                            IEnvelope envelope = selectedFeature.Shape.Envelope;
                            envelope.Expand(10, 10, true);
                            mxDoc.ActiveView.Extent = envelope;
                            mxDoc.ActiveView.Refresh();
                            break;
                        }
                    }
                    layer = enumLayer.Next();
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Показать на карте объект id {0}", objectID));
            }
        }


        // переключить слои
        public static void EnableLayersFromPages(OnePageDescriptions opd)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            // чтение LayerDescriptions из List<OneLayerDescriptions> 
            if (opd.LayerDescriptions != null && opd.LayerDescriptions.Count > 0)
            {
                List<OneLayerDescriptions>  tmpOdl = new List<OneLayerDescriptions>();
                foreach (OneLayerDescriptions old in opd.LayerDescriptions)
                    tmpOdl.Add(old);

                IEnumLayer enumLayer = mxdoc.FocusMap.Layers;
                ILayer layer = enumLayer.Next();
                while (layer != null)
                {
                    OneLayerDescriptions onl = new OneLayerDescriptions(layer);
                    int index = tmpOdl.IndexOf(onl);
                    if (index != -1)
                    {
                        layer.Visible = true;
                        tmpOdl.RemoveAt(index);
                    }
                    else
                        layer.Visible = false;


                    // всегда показывать слой где расположены объекты
                    if (CadastralReferenceData.ObjectLayer.Equals(onl))
                        layer.Visible = true;

                    layer = enumLayer.Next();
                }

                if (tmpOdl.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("Лист \"" + opd.Caption + "\". Отсутствуют слои:\n");
                    foreach (OneLayerDescriptions old in tmpOdl)
                    {
                        sb.Append("[");
                        sb.Append(old.Caption + " - data path = " + old.DataPath);
                        sb.Append("]\n");
                    }
                    MessageBox.Show(sb.ToString());
                }
            }
            // чтение для перехода StringCollection если != NULL
            else //if(opd.Layers != null && opd.Layers.Count > 0)
            {
                StringCollection tmpSc = new StringCollection();
                foreach (string s in opd.Layers)
                    tmpSc.Add(s.ToLower());

                    IEnumLayer enumLayer = mxdoc.FocusMap.Layers;
                    ILayer layer = enumLayer.Next();
                    while (layer != null)
                    {
                        string layerName = layer.Name.ToLower();
                        int index = tmpSc.IndexOf(layerName);
                        if (index != -1)
                        {
                            layer.Visible = true;
                            tmpSc.RemoveAt(index);
                        }
                        else
                            layer.Visible = false;

                    // всегда показывать слой где расположены объекты
                    if (CadastralReferenceData.ObjectLayer.Caption.ToLower() == layer.Name.ToLower())
                        layer.Visible = true;

                    layer = enumLayer.Next();
                    }



                if (tmpSc.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("Лист \"" + opd.Caption + "\". Отсутствуют слои:\n");
                    foreach (string s in tmpSc)
                    {
                        sb.Append("[");
                        sb.Append(s);
                        sb.Append("]\n");
                    }
                    MessageBox.Show(sb.ToString());
                }
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

        // получить перечень листов
        //public static StringCollection GetListOfAllLaers()

        //{
        //    StringCollection ret = new StringCollection();
        //    IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
        //    IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
        //    ILayer layer = enumLayer.Next();
        //    while (layer != null)
        //    {
        //        ret.Add(layer.Name);

        //        layer = enumLayer.Next();
        //    }
        //    return ret;
        //}

        // выбрать ближайшеий больший стандартный маштаб
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
            double ret = -1;
            try
            {
                IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
                IMap m = mxdoc.FocusMap;
                ret = m.MapScale;
            }
            catch (Exception ex) // обработка ошибок
            {

                Logger.Write(ex, string.Format("Получение масштаба"));
                //GeneralApp.ShowErrorMessage(string.Format("Проблема при получении масштаба"));
            }
            return ret;
        }

        //изменить размер на экране
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


        //////////////////////////////////////////////////////////////////////////////////////////////////
        // работа с элементами

        // изменить размер фрейма данных
        public static void ChangeSizeDateFrame(OnePageDescriptions opd)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IGraphicsContainer graphicsContainer = mxdoc.PageLayout as IGraphicsContainer;
            graphicsContainer.Reset();

            IElement dateFrameElement = null;
            IElement element = graphicsContainer.Next();
            while (element != null)
            {
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
                envelope.PutCoords(opd.DataFrameSyze_Left, opd.DataFrameSyze_Down, pageSaze.X - opd.DataFrameSyze_Right, pageSaze.Y - opd.DataFrameSyze_Up);
                dateFrameElement.Geometry = envelope as IGeometry;
            }
        }

        //добавить текст
        public static void AddText(OneTextElementDescription oted)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            TextElementClass textElement = new TextElementClass();
            textElement.Name = oted.Text;
            textElement.Text = TextTemplateConverter.Convert(oted.Text);
            textElement.Symbol = oted.TextSymbolClass;
            textElement.HorizontalAlignment = oted.AncorHorizontal;
            textElement.VerticalAlignment = oted.AncorVertical;

            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            IPoint pageSaze = GetPageSaze();

            double x = 0, y = 0;
            if (oted.PagePosVertical == esriTextVerticalAlignment.esriTVATop)
                y = pageSaze.Y;
            else if (oted.PagePosVertical == esriTextVerticalAlignment.esriTVACenter)
                y = pageSaze.Y / 2;
            else
                y = 0;

            if (oted.PagePosHorizontal == esriTextHorizontalAlignment.esriTHARight)
                x = pageSaze.X;
            else if (oted.PagePosHorizontal == esriTextHorizontalAlignment.esriTHACenter)
                x = pageSaze.X / 2;
            else
                x = 0;

            point.PutCoords(x + oted.PosX, y + oted.PosY);

            IElement element = textElement as IElement;
            element.Geometry = point;
            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(element, 0);
        }

        public static void DeleteAllText()
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.Reset();

            List<IElement> forDel = new List<IElement>();

            IElement element = gc.Next();
            while (element != null)
            {
                if (element is ITextElement)
                {
                    forDel.Add(element);
                }
                element = gc.Next();
            }

            foreach (IElement e in forDel)
            {
                gc.DeleteElement(e);
            }
        }

        // удалить элементы по имени
        public static void DeleteElementByName(string name)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;
            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
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

        // добавить стрелку северу
        public static void AddNorthArrowTool(OnePageDescriptions opd)
        {
            if (!opd.IsHasNorthArrow || opd.NorthArrow == null) return;

            IPoint pageSaze = GetPageSaze();
            double x = 0, y = 0;
            if (opd.NorthArrow_PagePosVertical == esriTextVerticalAlignment.esriTVATop)
                y = pageSaze.Y;
            else if (opd.NorthArrow_PagePosVertical == esriTextVerticalAlignment.esriTVACenter)
                y = pageSaze.Y / 2;
            else
                y = 0;

            if (opd.NorthArrow_PagePosHorizontal == esriTextHorizontalAlignment.esriTHARight)
                x = pageSaze.X;
            else if (opd.NorthArrow_PagePosHorizontal == esriTextHorizontalAlignment.esriTHACenter)
                x = pageSaze.X / 2;
            else
                x = 0;


            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(x + opd.NorthArrow_PosX, y + opd.NorthArrow_PosY, x + opd.NorthArrow_PosX, y + opd.NorthArrow_PosY);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IMapSurroundFrame pMSFrame = new MapSurroundFrameClass();
            //((ESRI.ArcGIS.Carto.IElementProperties3)pMSFrame).AnchorPoint = ESRI.ArcGIS.Carto.esriAnchorPointEnum.esriTopRightCorner;
            pMSFrame.MapSurround = opd.NorthArrow;
            IElement MSElement = pMSFrame as IElement;
            MSElement.Geometry = envelope as IGeometry;

            IGraphicsContainer gc = mxdoc.PageLayout as IGraphicsContainer;
            gc.AddElement(MSElement, 0);
        }

        // удалить все стрелки севера
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


        public static void AddScalebar(OnePageDescriptions opd)
        {
            if (!opd.IsHasScaleBar || opd.ScaleBar == null) return;

            IPoint pageSaze = GetPageSaze();
            double x = 0, y = 0;
            if (opd.ScaleBar_PagePosVertical == esriTextVerticalAlignment.esriTVATop)
                y = pageSaze.Y;
            else if (opd.ScaleBar_PagePosVertical == esriTextVerticalAlignment.esriTVACenter)
                y = pageSaze.Y / 2;
            else
                y = 0;

            if (opd.ScaleBar_PagePosHorizontal == esriTextHorizontalAlignment.esriTHARight)
                x = pageSaze.X;
            else if (opd.ScaleBar_PagePosHorizontal == esriTextHorizontalAlignment.esriTHACenter)
                x = pageSaze.X / 2;
            else
                x = 0;

            x += opd.ScaleBar_PosX;
            y += opd.ScaleBar_PosY;

            double x1 = 0, y1 = 0;
            double x2 = 0, y2 = 0;

            if (opd.ScaleBar_AncorVertical == esriTextVerticalAlignment.esriTVATop)
            {
                y1 = y - opd.ScaleBar_Height;
                y2 = y;
            }
            else if (opd.ScaleBar_AncorVertical == esriTextVerticalAlignment.esriTVACenter)
            {
                y1 = y - opd.ScaleBar_Height / 2;
                y2 = y + opd.ScaleBar_Height / 2;
            }
            else
            {
                y1 = y;
                y2 = y + opd.ScaleBar_Height;
            }

            if (opd.ScaleBar_AncorHorizontal == esriTextHorizontalAlignment.esriTHARight)
            {
                x1 = x - opd.ScaleBar_Width;
                x2 = x;
            }
            else if (opd.ScaleBar_AncorHorizontal == esriTextHorizontalAlignment.esriTHACenter)
            {
                x1 = x - opd.ScaleBar_Width / 2;
                x2 = x + opd.ScaleBar_Width / 2;
            }
            else
            {
                x1 = x;
                x2 = x + opd.ScaleBar_Width;
            }

            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(x1, y1, x2, y2);

            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            IMapSurroundFrame pMSFrame = new MapSurroundFrameClass();
            //((ESRI.ArcGIS.Carto.IElementProperties3)pMSFrame).AnchorPoint = ESRI.ArcGIS.Carto.esriAnchorPointEnum.esriTopRightCorner;
            pMSFrame.MapSurround = opd.ScaleBar;
            IElement MSElement = pMSFrame as IElement;
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


    }
}




