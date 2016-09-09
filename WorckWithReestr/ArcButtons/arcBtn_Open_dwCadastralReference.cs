using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;


namespace WorckWithReestr
{
    public class arcBtn_Open_dwCadastralReference : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_dwCadastralReference()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;

            UID dockableWinUID = new UIDClass();
            dockableWinUID.Value = ThisAddIn.IDs.arcDW_CadastralReference;

            IDockableWindow statsticsDockableWin = ArcMap.DockableWindowManager.GetDockableWindow(dockableWinUID);
            //statsticsDockableWin.Show(true);
            statsticsDockableWin.Show(!statsticsDockableWin.IsVisible());
            //if (statsticsDockableWin.IsVisible())
            //    CadastralReference.WorkCadastralReference.SetZayavka(111);
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
