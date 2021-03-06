﻿using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using SharedClasses;

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
            CadastralReference.WorkCadastralReference.Show(-1, CadastralReference.WorkCadastralReference.TypeReference.Cadastr);
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }
}
