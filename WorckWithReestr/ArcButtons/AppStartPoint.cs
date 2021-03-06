﻿using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;

namespace WorckWithReestr
{

    public class AppStartPoint : SharedClasses.AddInsAppInfo
    {
        static SharedClasses.AddInsAppInfo m_AddInsAppInfo = null;

        private AppStartPoint()
        {
            //здесь описать действия при старте

        }

        public static void Init()
        {
            if (SharedClasses.GeneralApp.GetAddInsAppInfo() == null)
            {
                m_AddInsAppInfo = new AppStartPoint();
                SharedClasses.GeneralApp.SetAddInsAppInfo(m_AddInsAppInfo);
            }
        }

        public override IApplication GetThisAddInnApp()
        {
            return ArcMap.Application;
        }

        public override IMxDocument GetDocument()
        {
            return ArcMap.Document;
        }
        public override IMxApplication GetHostApplication()
        {
            return ArcMap.ThisApplication;
        }
        public override IDockableWindowManager GetDockableWindowManager()
        {
            return ArcMap.DockableWindowManager;
        }
        public override IDocumentEvents_Event GetEvents()
        {
            return ArcMap.Events;
        }


        public override string GetNameApp()
        {
            return ThisAddIn.Name;
        }
    }
}
