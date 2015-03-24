using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithReestr
{
    public class ArcGISAddin1 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ArcGISAddin1()
        {
        }

        protected override void OnClick()
        {

            ArcMap.Application.CurrentTool = null;
            Form1.ShowForView();

        }

        protected override void OnUpdate()
        {
        }
    }
}
