using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_Rej_Bud_Adr : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Bud_Adr()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmRej_Bud_Adr_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
