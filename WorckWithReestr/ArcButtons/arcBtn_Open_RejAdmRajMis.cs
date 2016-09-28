namespace WorckWithReestr
{
    public class arcBtn_Open_RejAdmRajMis : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_RejAdmRajMis()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmRejAdmRajMis_list.ShowForView();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
