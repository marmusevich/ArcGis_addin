﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace WorckWithKadastr
{
    public class arcBtn_Open_ReestrMistzevist3 : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public arcBtn_Open_ReestrMistzevist3()
        {
        }

        protected override void OnClick()
        {
            ArcMap.Application.CurrentTool = null;
            ReestrMistzevist3_list.ShowForView();
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
