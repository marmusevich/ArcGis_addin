﻿namespace WorckWithKadastr2016
{
    public class arcBtn_Open_Rej_Adr_Osnov : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Adr_Osnov()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmRej_Adr_Osnov_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
