﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithReestr
{
    public class arcBtn_Open_TipDoc : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_TipDoc()
        {
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