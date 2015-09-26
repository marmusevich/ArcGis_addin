using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorckWithReestr
{

    //
    //1
    ////      MessageBox.Show( ThisAddIn.Name );
    //
    //2
    ////ESRI.ArcGIS.Framework.IApplication app = ArcMap.Application;
    ////ESRI.ArcGIS.esriSystem.IStatusBar statusBar = app.StatusBar;
    ////statusBar.set_Message(0, "dfslkgsdfkglksdlf!!!");
    //
    ////IMouseCursor appCursor = new MouseCursorClass();
    ////appCursor.SetCursor(2);
    //
    ////IMouseCursor appCursor = new MouseCursorClass();
    ////appCursor.SetCursor(2);
    ////for (int i = 0; i <= 10000; i++)
    ////    app.StatusBar.set_Message(0, i.ToString());



    //public class AppStartPoint : SharedClasses.AddInsAppInfo
    //{
    //    static SharedClasses.AddInsAppInfo m_AddInsAppInfo = null;

    //    private AppStartPoint()
    //    { 
    //        //здесь описать действия при старте
        
    //    }

    //    public static void Init()
    //    {
    //        //if (SharedClasses.GeneralApp.GetAddInsAppInfo() == null)
    //        if (m_AddInsAppInfo == null)
    //        {

    //            m_AddInsAppInfo = new AppStartPoint();
    //            SharedClasses.GeneralApp.SetAddInsAppInfo(m_AddInsAppInfo);

    //        }
    //    }

    //    public override IApplication GetThisAddInnApp()
    //    {
    //        return ArcMap.Application;
    //    }

    //    public override IMxDocument GetDocument()
    //    {
    //        return ArcMap.Document;
    //    }
    //    public override IMxApplication GetHostApplication()
    //    {
    //        return ArcMap.ThisApplication;
    //    }
    //    public override IDockableWindowManager GetDockableWindowManager()
    //    {
    //        return ArcMap.DockableWindowManager;
    //    }
    //    public override IDocumentEvents_Event GetEvents()
    //    {
    //        return ArcMap.Events;
    //    }


    //    public override string GetNameApp()
    //    {
    //        return ThisAddIn.Name;
    //    }
    //}
}
