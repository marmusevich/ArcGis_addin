using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_ArhivDocument : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ArhivDocument()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            //frm.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
