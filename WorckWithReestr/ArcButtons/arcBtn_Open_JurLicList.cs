namespace WorckWithReestr
{
    public class arcBtn_Open_JurLicList : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_JurLicList()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmJurLic_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
