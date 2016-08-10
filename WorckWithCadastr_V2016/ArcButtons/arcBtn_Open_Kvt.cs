namespace WorckWithKadastr2016
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
