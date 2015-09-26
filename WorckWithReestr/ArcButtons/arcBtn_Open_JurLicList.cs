using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithReestr
{
    public class arcBtn_Open_JurLicList : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_JurLicList()
        {
        }

        protected override void OnClick()
        {
            //AppStartPoint.Init();
            ArcMap.Application.CurrentTool = null;
            frmJurLic_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
