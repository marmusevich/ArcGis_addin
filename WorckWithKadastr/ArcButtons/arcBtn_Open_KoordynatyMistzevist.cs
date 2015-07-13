using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_KoordynatyMistzevist : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_KoordynatyMistzevist()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            frmKoordynatyMistzevist_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
