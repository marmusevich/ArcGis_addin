using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_KategoriiPlanRecreatsija : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_KategoriiPlanRecreatsija()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmKategoriiPlanRecreatsija_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
