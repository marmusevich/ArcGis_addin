using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_KoordynatyVulyts : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_KoordynatyVulyts()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmKoordynatyVulyts_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
