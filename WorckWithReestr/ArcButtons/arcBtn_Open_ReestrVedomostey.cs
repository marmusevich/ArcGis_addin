﻿namespace WorckWithReestr
{
    public class arcBtn_Open_ReestrVedomostey : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrVedomostey()
        {
            AppStartPoint.Init();
        }
        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmReestrVedomostey_jurnal.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
