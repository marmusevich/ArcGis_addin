using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Framework;


namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_Rej_Vul : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Vul()
        {
        }

        protected override void OnClick()
        {
            //ESRI.ArcGIS.Framework.IApplication app = ArcMap.Application;
            //ESRI.ArcGIS.esriSystem.IStatusBar statusBar = app.StatusBar;
            //statusBar.set_Message(0, "dfslkgsdfkglksdlf!!!");

            //IMouseCursor appCursor = new MouseCursorClass();
            //appCursor.SetCursor(2);

            //IMouseCursor appCursor = new MouseCursorClass();
            //appCursor.SetCursor(2);
            //for (int i = 0; i <= 10000; i++)
            //    app.StatusBar.set_Message(0, i.ToString());



            ArcMap.Application.CurrentTool = null;
            frmRej_Vul_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
