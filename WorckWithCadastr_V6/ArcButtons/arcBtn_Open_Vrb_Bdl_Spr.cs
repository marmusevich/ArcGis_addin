using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_Vrb_Bdl_Spr : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Vrb_Bdl_Spr()
        {
        }

        protected override void OnClick()
        {
            AppStartPoint.Init();
            ArcMap.Application.CurrentTool = null;
            frmVrb_Bdl_Spr_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
