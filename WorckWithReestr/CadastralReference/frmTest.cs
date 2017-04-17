using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using WorckWithReestr;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

using ESRI.ArcGIS.ArcMapUI;



namespace WorckWithReestr
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }



        // переключить слои
        public static void EnableLayersFromPages(CadastralReference.OnePageDescriptions opd)
        {
            IMxDocument mxdoc = ArcMap.Application.Document as IMxDocument;

            StringCollection tmp = new StringCollection();
            foreach (string s in opd.Layers)
                tmp.Add(s.ToLower());

            IEnumLayer enumLayer = mxdoc.FocusMap.Layers;
            ILayer layer = enumLayer.Next();
            while (layer != null)
            {
                string layerName = layer.Name.ToLower();
                int index = tmp.IndexOf(layerName);
                if (index != -1)
                {
                    layer.Visible = true;
                    tmp.RemoveAt(index);
                }

                else
                    layer.Visible = false;


                layer = enumLayer.Next();
            }

            if (tmp.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("Лист \"" + opd.Caption + "\". Отсутствуют слои:\n");
                foreach (string s in tmp)
                {
                    sb.Append("[");
                    sb.Append(s);
                    sb.Append("]\n");
                }
                MessageBox.Show(sb.ToString());
            }
            mxdoc.ActiveView.ContentsChanged();
        }




        string data = "";


        void getData(ILayer selectedLayer)
        {

            if (selectedLayer != null)
            {

                if (selectedLayer is IFeatureLayer2)
                {

                    data += "Feature\t" + selectedLayer.Name + "\t";

                    IFeatureLayer2 selectedFL = selectedLayer as IFeatureLayer2;
                    data += "Data Source Type: " + selectedFL.DataSourceType + "\t";

                    if (selectedFL.FeatureClass is IDataset)
                    {
                        IDataset dataset = (IDataset)(selectedFL.FeatureClass); // Explicit Cast
                        data += "dataset: " + (dataset.Workspace.PathName + " --> " + dataset.Name) + "\t";
                    }

                    data += " \n";
                }

                if (selectedLayer is IRasterLayer)
                {
                    data += "Raster\t" + selectedLayer.Name + "\t";
                    IRasterLayer selectedRL = selectedLayer as IRasterLayer;
                    data += "File Path: " + selectedRL.FilePath + "\t";

                    data += "\n";
                }




                //if (selectedLayer is IGroupLayer) // не даёт перечень узлов
                //{
                //    IGroupLayer group = (IGroupLayer)selectedLayer;
                //    data += "Layer.Name=" + selectedLayer.Name + "->    Expanded = " + group.Expanded + "\n";
                //}


                //if (selectedLayer is ICompositeLayer2) // не всегда
                //{
                //    ICompositeLayer2 compositeLayer2 = (ICompositeLayer2)selectedLayer;
                //    data += "      ICompositeLayer2   " + selectedLayer.Name + " ->Count = " + compositeLayer2.Count + "\n";
                //}

                if (selectedLayer is ICompositeLayer) //для групповых узлов 
                {
                    ICompositeLayer compositeLayer = (ICompositeLayer)selectedLayer;
                    data += "IComposite\t" + selectedLayer.Name + "\tCount = " + compositeLayer.Count + "\t";
                    data += " \n";
                }
            }
        }


//        IMap myMap = ArcMap.Document.ActivatedView.FocusMap;
//for (int i = 0; i<myMap.LayerCount; i++)
//{
//     if (myMap.Layer[i] is ICompositeLayer)
//     {
//           ICompositeLayer igroup = myMap.Layer[i] as ICompositeLayer;
//           for (int j = 0; j<igroup.Count; j++)
//           {
//                 IFeatureLayer ilayer = igroup.Layer(j) as IFeatureLayer;
//        // Do whatever you need.
//    }
//}
//}


        //public List<ILayer> GetLayers(string groupLayerName)
        //{

        //    ICompositeLayer compositeLayer = GetGroupLayer(groupLayerName);
        //    List<ILayer> layers = new List<ILayer>();

        //    if (compositeLayer != null)
        //    {
        //        for (int j = 0; j < compositeLayer.Count; j++)
        //        {
        //            layers.Add(new Layer(compositeLayer.Layer[j]));
        //        }
        //    }
        //    return layers;
        //}

        //ICompositeLayer GetGroupLayer(string groupLayerName)
        //{
        //    var mapLayers = _map.Layers;
        //    for (var layer = mapLayers.Next(); layer != null; layer = mapLayers.Next())
        //    {
        //        var comLayer = layer as ICompositeLayer;
        //        var groupLayer = layer as IGroupLayer;
        //        if ((comLayer != null) && (groupLayer != null) && (layer.Name.Equals(groupLayerName)))
        //            return groupLayer;
        //    }
        //}



        private void frmTest_Load(object sender, EventArgs e)
        {


            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
            ILayer layer = enumLayer.Next();
            while (layer != null)
            {
                getData(layer);
                layer = enumLayer.Next();
            }




            txt.Text = data;
        }




        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (retVal == null)
            //    retVal = new StringCollection();

            //retVal.Clear();

            //foreach (object o in lbSelectedLayers.Items)
            //{
            //    string tmp = (string)o;
            //    if (tmp != null)
            //    {
            //        retVal.Add(tmp);
            //    }
            //}
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
