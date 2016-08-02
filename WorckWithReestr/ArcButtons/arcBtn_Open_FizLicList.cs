namespace WorckWithReestr
{
    public class arcBtn_Open_FizLicList : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_FizLicList()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmFizLic_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
