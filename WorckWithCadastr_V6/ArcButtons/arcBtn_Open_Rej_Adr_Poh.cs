using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_Rej_Adr_Poh : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Adr_Poh()
        {
        }

        protected override void OnClick()
        {
            AppStartPoint.Init();
            ArcMap.Application.CurrentTool = null;
            frmRej_Adr_Poh_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
