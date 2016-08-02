namespace WorckWithCadastr_V6
{
    public class arcBtn_Open_Vrb_Bdl_Spr : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Vrb_Bdl_Spr()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmVrb_Bdl_Spr_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
