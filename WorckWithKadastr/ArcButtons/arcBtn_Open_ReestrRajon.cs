using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_ReestrRajon : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrRajon()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            ReestrRajon_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
