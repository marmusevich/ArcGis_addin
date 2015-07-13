using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_KategoriiVulyts : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_KategoriiVulyts()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            KategoriiVulyts_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
