using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_ReestrMistzevist_polygon : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrMistzevist_polygon()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmReestrMistzevist_polygon_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
