using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DemoTest
{
    public class arcBtn_Open_FizLicList : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_FizLicList()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;

            frmFizLic_list frm = new frmFizLic_list();
            frm.ShowDialog();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
