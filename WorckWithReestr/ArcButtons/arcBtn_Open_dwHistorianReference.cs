using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public class arcBtn_Open_dwHistorianReference : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_dwHistorianReference()
        {
            AppStartPoint.Init();
        }

        protected override void OnClick()
        {
            CadastralReference.WorkCadastralReference.Show(-1, CadastralReference.WorkCadastralReference.TypeReference.Historian);
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
