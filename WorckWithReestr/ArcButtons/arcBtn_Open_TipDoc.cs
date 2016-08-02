namespace WorckWithReestr
{
    public class arcBtn_Open_TipDoc : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_TipDoc()
        {
            AppStartPoint.Init();
        }
        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmTipDoc_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
