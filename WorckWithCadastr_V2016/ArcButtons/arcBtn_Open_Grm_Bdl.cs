﻿namespace WorckWithKadastr2016
{
    public class arcBtn_Open_Grm_Bdl : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Grm_Bdl()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmGrm_Bdl_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
