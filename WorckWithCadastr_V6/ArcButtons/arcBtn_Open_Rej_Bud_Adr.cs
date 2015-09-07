using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithCadastr_V6.ArcButtons
{
    public class arcBtn_Open_Rej_Bud_Adr : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Bud_Adr()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
//            frm.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
