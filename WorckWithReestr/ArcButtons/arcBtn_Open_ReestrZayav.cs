using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithReestr
{
    public class arcBtn_Open_ReestrZayav : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrZayav()
        {
            AppStartPoint.Init();
        }
        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmReestrZayav_jurnal.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
