﻿namespace WorckWithKadastr2016
{
    public class arcBtn_Open_Rej_Adm_Raj_Mis : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Adm_Raj_Mis()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmRej_Adm_Raj_Mis_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
