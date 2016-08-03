using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace SharedClasses
{
    // обертка для индикации пользователю
    class IndicateWaitingOperation
    {
        private static IStatusBar statusBar = null;
        private static ESRI.ArcGIS.Framework.IMouseCursor appCursor = null;

        public static void Init(string Message, int min, int max, int Step, bool onePanel = false, object cursorID = null)
        {
            AddInsAppInfo ai = GeneralApp.GetAddInsAppInfo();

            if (ai != null && ai.GetThisAddInnApp() != null)
            {
                statusBar = ai.GetThisAddInnApp().StatusBar;
                statusBar.ShowProgressBar(Message, min, max, Step, onePanel);
                statusBar.ProgressBar.Position = 0;
                statusBar.StepProgressBar();


                if (cursorID == null)
                    cursorID = 2;

                appCursor = new ESRI.ArcGIS.Framework.MouseCursorClass();
                appCursor.SetCursor(cursorID);
            }
        }

        public static void Do(string Message, object cursorID = null)
        {

            if (appCursor != null)
            {
                if (cursorID == null)
                    cursorID = 2;

                appCursor.SetCursor(cursorID);
            }

            if (statusBar != null)
            {
                statusBar.ProgressBar.Message = Message;
                statusBar.StepProgressBar();
            }
        }

        public static void Finalize(object cursorID = null)
        {
            if (cursorID == null)
                cursorID = 0;

            if (statusBar != null)
            {
                statusBar.HideProgressBar();
                statusBar = null;
            }
            if (appCursor != null)
            {
                appCursor.SetCursor(cursorID);
                appCursor = null;
            }
        }


    }
}
