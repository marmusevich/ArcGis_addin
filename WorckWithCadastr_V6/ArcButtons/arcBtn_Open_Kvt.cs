using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_Kvt : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Kvt()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmKvt_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
