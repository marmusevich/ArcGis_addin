using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DemoTestMap
{
    public class arcBtn_Open_Select_Object_In_Map : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_Select_Object_In_Map()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;

            frmFindAndSelect_MapObject frm = new frmFindAndSelect_MapObject();
            frm.ShowDialog();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

    }
}
