namespace WorckWithKadastr2016
{
    public class arcBtn_Open_Obj_Scl_Sft_Ecn : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Obj_Scl_Sft_Ecn()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmObj_Scl_Sft_Ecn_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
