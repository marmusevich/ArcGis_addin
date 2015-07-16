using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_ReestrDorog_polygon : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrDorog_polygon()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmReestrDorogy_polygon_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
