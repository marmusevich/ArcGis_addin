using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace DemoTestMap
{
    public class arcTool_Show_Info_About_Map_Obj : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public arcTool_Show_Info_About_Map_Obj()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseUp(Tool.MouseEventArgs arg)
        {

            IMxDocument mxDoc = ArcMap.Document;
            IActiveView  m_focusMap = mxDoc.FocusMap as IActiveView;
            IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y) as IPoint;
            //(m_focusMap as IMap).SelectByShape(point, null, false);
            ArcMap.Document.FocusMap.SelectByShape(point, null, false);
            m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);


            frmShowInfoAboutMapObj frm = new frmShowInfoAboutMapObj();
            frm.ShowDialog();
        }


    }
}

//public abstract class Tool 
//{
//    protected Tool();

//    protected Cursor Cursor { set; }
//    protected IntPtr CursorHandle { set; }
//    protected bool Enabled { get; set; }
//    protected object Hook { get; }

//    public void Dispose();
//    protected virtual void Dispose(bool disposing);
//    protected internal virtual void OnActivate();
//    protected internal virtual bool OnContextMenu(int x, int y);
//    protected internal virtual bool OnDeactivate();
//    protected internal virtual void OnDoubleClick();
//    protected virtual void OnKeyDown(Tool.KeyEventArgs arg);
//    protected virtual void OnKeyUp(Tool.KeyEventArgs arg);
//    protected virtual void OnMouseDown(Tool.MouseEventArgs arg);
//    protected virtual void OnMouseMove(Tool.MouseEventArgs arg);
//    protected virtual void OnMouseUp(Tool.MouseEventArgs arg);
//    protected internal virtual void OnRefresh(int hDC);
//    protected virtual void OnUpdate();

//    protected class KeyEventArgs
//    {
//        public bool Alt { get; }
//        public bool Control { get; }
//        public Keys KeyCode { get; }
//        public Keys ModifierKeys { get; }
//        public bool Shift { get; }
//    }

//    protected class MouseEventArgs : MouseEventArgs
//    {
//        public bool Alt { get; }
//        public bool Control { get; }
//        public Keys ModifierKeys { get; }
//        public bool Shift { get; }
//    }
//}
