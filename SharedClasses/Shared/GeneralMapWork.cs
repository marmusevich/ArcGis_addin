using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace SharedClasses
{
    public class GeneralMapWork
    {
















        //---------------------------------------------------------------------------------------
        #region общее
        //***
        //private static string m_senpl = null;

        //
        //public static string senpl()
        //{
        //    return null;
        //}


        #endregion


    }
}

// примеры

// текущий документ / карта
//IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
//IMap map = mxDoc.FocusMap as IMap;


// выбрать по клику мышы видемый объект
//IMxDocument mxDoc = ArcMap.Document;
//IActiveView  m_focusMap = mxDoc.FocusMap as IActiveView;
//IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y) as IPoint;
//ArcMap.Document.FocusMap.SelectByShape(point, null, false);
//m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Runtime.InteropServices;
//using ESRI.ArcGIS.esriSystem;
//using ESRI.ArcGIS.SystemUI;
//using ESRI.ArcGIS.Framework;
//using ESRI.ArcGIS.ADF.CATIDs;

//using ESRI.ArcGIS.Geodatabase;
//using ESRI.ArcGIS.ArcMapUI;
//using ESRI.ArcGIS.Carto;
//using ESRI.ArcGIS.Geometry;
//namespace DockableSelection
//{
//    [Guid("2aafe950-e69c-4592-866e-f73f2d1fbfc1")]
//    [ClassInterface(ClassInterfaceType.None)]
//    [ProgId("DockableSelection.selectionDockable")]
//    public partial class selectionDockable : UserControl, IDockableWindowDef
//    {
//        private IApplication m_application;

//        #region COM Registration Function(s)
//        [ComRegisterFunction()]
//        [ComVisible(false)]
//        static void RegisterFunction(Type registerType)
//        {
//            // Required for ArcGIS Component Category Registrar support
//            ArcGISCategoryRegistration(registerType);
//            //
//            // TODO: Add any COM registration code here
//            //
//        }

//        [ComUnregisterFunction()]
//        [ComVisible(false)]
//        static void UnregisterFunction(Type registerType)
//        {
//            // Required for ArcGIS Component Category Registrar support
//            ArcGISCategoryUnregistration(registerType);

//            //
//            // TODO: Add any COM unregistration code here
//            //
//        }

//        #region ArcGIS Component Category Registrar generated code
//        /// <summary>
//        /// Required method for ArcGIS Component Category registration -
//        /// Do not modify the contents of this method with the code editor.
//        /// </summary>
//        private static void ArcGISCategoryRegistration(Type registerType)
//        {
//            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
//            MxDockableWindows.Register(regKey);

//        }
//        /// <summary>
//        /// Required method for ArcGIS Component Category unregistration -
//        /// Do not modify the contents of this method with the code editor.
//        /// </summary>
//        private static void ArcGISCategoryUnregistration(Type registerType)
//        {
//            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
//            MxDockableWindows.Unregister(regKey);

//        }

//        #endregion
//        #endregion

//        public selectionDockable()
//        {
//            InitializeComponent();
//        }

//        #region IDockableWindowDef Members

//        string IDockableWindowDef.Caption
//        {
//            get
//            {
//                //TODO: Replace with locale-based initial title bar caption
//                return "Selection Dockable window";
//            }
//        }

//        int IDockableWindowDef.ChildHWND
//        {
//            get { return this.Handle.ToInt32(); }
//        }

//        string IDockableWindowDef.Name
//        {
//            get
//            {
//                //TODO: Replace with any non-localizable string
//                return this.Name;
//            }
//        }

//        void IDockableWindowDef.OnCreate(object hook)
//        {
//            m_application = hook as IApplication;
//        }

//        void IDockableWindowDef.OnDestroy()
//        {
//            //TODO: Release resources and call dispose of any ActiveX control initialized
//        }

//        object IDockableWindowDef.UserData
//        {
//            get { return null; }
//        }

//        #endregion

//        string selectedState = null;
//        IFeatureLayer2 citiesFL = null;
//        IFeatureLayer2 statesLayer = null;

//        private void btnPopulateList_Click(object sender, EventArgs e)
//        {
//            lstStates.Items.Clear();
//            IMxDocument mxdoc = m_application.Document as IMxDocument;
//            IMap map = mxdoc.FocusMap;

//            IEnumLayer enumLayer = map.Layers;
//            ILayer layer = enumLayer.Next();

//            while (layer != null)
//            {
//                if (layer is IFeatureLayer2 && layer.Name == "U.S. States (Generalized)")
//                {
//                    statesLayer = layer as IFeatureLayer2;
//                }
//                layer = enumLayer.Next();
//            }

//            if (statesLayer == null)
//            { return; }

//            IFeatureCursor statesFCursor = statesLayer.FeatureClass.Search(null, true);
//            int state_nameIndex = statesFCursor.Fields.FindField("STATE_NAME");
//            IFeature state = statesFCursor.NextFeature();

//            if (state_nameIndex < 0)
//            { return; }

//            while (state != null)
//            {
//                lstStates.Items.Add(state.Value[state_nameIndex]);
//                state = statesFCursor.NextFeature();
//            }
//        }

//        private void lstStates_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (lstStates.SelectedIndex >= 0)
//            {
//                selectedState = lstStates.SelectedItem.ToString();
//            }
//        }

//        private void btnSelect_Click(object sender, EventArgs e)
//        {
//            IMxDocument mxdoc = m_application.Document as IMxDocument;

//            IFeatureClass stateFC = statesLayer.FeatureClass;
//            IQueryFilter2 qF = new QueryFilterClass();
//            qF.WhereClause = string.Format("STATE_NAME='{0}'", selectedState);
//            IFeatureCursor stateFCursor = stateFC.Search(qF, true);
//            //just one state is selected
//            IFeature selectedStateFeature = stateFCursor.NextFeature();
//            IGeometry5 shapeOfSelectedState = selectedStateFeature.Shape as IGeometry5;

//            IMap map = mxdoc.FocusMap;
//            IEnumLayer enumLayer = map.Layers;
//            ILayer layer = enumLayer.Next();



//            while (layer != null)
//            {
//                if (layer.Name == "U.S. Cities" && layer is IFeatureLayer2)
//                {
//                    citiesFL = layer as IFeatureLayer2;
//                }
//                layer = enumLayer.Next();
//            }

//            ISpatialFilter sF = new SpatialFilterClass();
//            sF.Geometry = shapeOfSelectedState;
//            sF.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

//            IFeatureSelection citiesFeatureSelection = citiesFL as IFeatureSelection;
//            citiesFeatureSelection.SelectFeatures(sF, esriSelectionResultEnum.esriSelectionResultNew, false);


//            ICursor citiesCursor = null;
//            citiesFeatureSelection.SelectionSet.Search(null, true, out citiesCursor);
//            int pop1990Index = citiesCursor.Fields.FindField("POP1990");
//            long totalPopulation = 0;
//            IRow city = citiesCursor.NextRow();
//            while (city != null)
//            {

//                totalPopulation += long.Parse(city.Value[pop1990Index].ToString());

//                city = citiesCursor.NextRow();
//            }


//            //zoom to selected features
//            mxdoc.ActiveView.Extent = shapeOfSelectedState.Envelope;
//            //IFeatureClass citiesFC = citiesFL.FeatureClass;
//            mxdoc.ActiveView.Refresh();


//            lblReport.Text = String.Format("Number of Selected Cities: {0} \n",
//             citiesFeatureSelection.SelectionSet.Count);
//            lblReport.Text += String.Format("Total Population: {0}", totalPopulation);
            
//        }

//        private void btnClear_Click(object sender, EventArgs e)
//        {
//            selectedState = "";
//            lstStates.ClearSelected();
//            lblReport.Text = "";
//            //todo: clear selected features in specified layer
//            (citiesFL as IFeatureSelection).Clear();

//            IMxDocument mxdoc = m_application.Document as IMxDocument;
//            if (statesLayer != null)
//            {
//                mxdoc.ActiveView.Extent = (statesLayer as ILayer).AreaOfInterest;
//            }
//            mxdoc.ActiveView.Refresh();
//        }
//    }
//}
