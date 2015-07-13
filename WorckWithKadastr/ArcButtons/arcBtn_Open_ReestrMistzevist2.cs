using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_ReestrMistzevist2 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrMistzevist2()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmReestrMistzevist2_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
