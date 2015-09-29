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
            AppStartPoint.Init();
            ArcMap.Application.CurrentTool = null;
            frmRej_Vul_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
