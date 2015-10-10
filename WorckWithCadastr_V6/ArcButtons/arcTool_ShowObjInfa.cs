using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WorckWithCadastr_V6
{
    public class arcTool_ShowObjInfa : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public arcTool_ShowObjInfa()
        {
            AppStartPoint.Init();
//            ArcMap.Application.CurrentTool = null;

        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            System.Windows.Forms.MessageBox.Show("arcTool_ShowObjInfa.OnMouseUp");

            ArcMap.Application.CurrentTool = null;
        }

    }
}
