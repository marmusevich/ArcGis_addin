namespace WorckWithKadastr2016
{
    public class arcBtn_Open_Rej_Vul : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Rej_Vul()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmRej_Vul_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
