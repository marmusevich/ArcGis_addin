using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorckWithReestr
{
    class arcTool_SelectOdject : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public arcTool_SelectOdject()
        {
            AppStartPoint.Init();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }


        //protected override void OnActivate()
        //{
        //    MessageBox.Show("OnActivate");

        //    //this.Cursor = Cursors.Cross;
        //}
        //protected override bool OnDeactivate()
        //{
        //    MessageBox.Show("OnDeactivate");
        //    //this.Cursor = Cursors.Default;
        //    return false;
        //}


        ////this.Cursor = Cursors.Hand;

        //ESRI.ArcGIS.Framework.IMouseCursor appCursor = new ESRI.ArcGIS.Framework.MouseCursorClass();
        //appCursor.SetCursor(3);

        //    ESRI.ArcGIS.Framework.IMouseCursor appCursor = new ESRI.ArcGIS.Framework.MouseCursorClass();
        //appCursor.SetCursor(0);


        protected override void OnMouseMove(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            //ESRI.ArcGIS.Framework.IMouseCursor appCursor = new ESRI.ArcGIS.Framework.MouseCursorClass();
            //appCursor.SetCursor(3);
          //  this.Cursor = Cursors.Cross;
        }



        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            if (mxDoc != null)
            {
                SharedClasses.GeneralMapWork.SelectFeaturesScreenPoint(mxDoc.FocusMap, arg.X, arg.Y, 10);
                CadastralReference.WorkCadastralReference_MAP.SelectObjektInMap();
            }
            //ESRI.ArcGIS.Framework.IMouseCursor appCursor = new ESRI.ArcGIS.Framework.MouseCursorClass();
            //appCursor.SetCursor(0);

           // this.Cursor = Cursors.Default;
        }


        public static void SetToolActiveInToolBar(ESRI.ArcGIS.Framework.IApplication application)
        {
            ESRI.ArcGIS.Framework.ICommandBars commandBars = application.Document.CommandBars;
            ESRI.ArcGIS.esriSystem.UID commandID = new ESRI.ArcGIS.esriSystem.UIDClass();
            commandID.Value = "TKC_arcTool_SelectOdject"; // example: "esriArcMapUI.ZoomInTool";
            ESRI.ArcGIS.Framework.ICommandItem commandItem = commandBars.Find(commandID, false, false);
            if (commandItem != null)
            {
                application.CurrentTool = commandItem;
            }
        }
    }
}


