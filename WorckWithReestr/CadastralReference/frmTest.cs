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



namespace CadastralReference
{
    public partial class frmTest : Form
    {

        //------------------------------
        //построить дерево
        private int BuldTree()
        {
            int layerCount = 0;

            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IMap myMap = mxDoc.FocusMap;
            for (int i = 0; i < myMap.LayerCount; i++)
            {
                layerCount += BuldNode(myMap.Layer[i], null);
            }
            return layerCount;
        }

        private int BuldNode(ILayer layer, TreeNode parent)
        {
            int layerCount = 0;


            OneLayerDescriptions onl = new OneLayerDescriptions(layer);
            TreeNode currNode = addNode(onl, parent);
            if (layer is ICompositeLayer)
            {
                ICompositeLayer igroup = layer as ICompositeLayer;
                for (int j = 0; j < igroup.Count; j++)
                {
                    layerCount += BuldNode(igroup.Layer[j], currNode);
                }
            }
            layerCount++;
            return layerCount;
        }

        public TreeNode addNode(OneLayerDescriptions onl, TreeNode parent)
        {
            TreeNode newNode = new TreeNode();

            if (parent == null)
            {
                this.tvLayers.Nodes.Add(newNode);
            }
            else
            {
                parent.Nodes.Add(newNode);
            }

            newNode.Name = onl.DataPath;
            newNode.Text = onl.Caption;
            newNode.ToolTipText = newNode.FullPath;
            newNode.Tag = onl;

            return newNode;
        }
        //-----------------------------------------


        public frmTest()
        {
            InitializeComponent();
        }


        private void frmTest_Load(object sender, EventArgs e)
        {
            //getData();
            //data += "\n \n \n \n \t \t <<--->> \t \t \n \n \n \n ";

            int layerCount = BuldTree();

            data += "layerCount = " + layerCount + "\n";

            txt.Text = data;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
        }

        private void tvLayers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            txt.Text = "FullPath = " + e.Node.FullPath + System.Environment.NewLine +
                "Name = " + e.Node.Name + System.Environment.NewLine +
                "Text = " + e.Node.Text + System.Environment.NewLine + System.Environment.NewLine +
                "onl.Caption = " + ((OneLayerDescriptions)e.Node.Tag).Caption + System.Environment.NewLine +
                "onl.Type = " + ((OneLayerDescriptions)e.Node.Tag).Type.ToString() + System.Environment.NewLine +
                "onl.DataPath = " + ((OneLayerDescriptions)e.Node.Tag).DataPath;

        }



        string data = "";
        void getData()
        {
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IEnumLayer enumLayer = mxDoc.FocusMap.Layers;
            ILayer layer = enumLayer.Next();
            int layerCount = 0;
            while (layer != null)
            {
                if (layer is ICompositeLayer) //для групповых узлов 
                {
                    ICompositeLayer compositeLayer = (ICompositeLayer)layer;
                    data += "IComposite\t" + layer.Name + "\tCount = " + compositeLayer.Count + "\t";
                    //data += " \n";
                }

                if (layer is IFeatureLayer2)
                {
                    data += "Feature\t" + layer.Name + "\t";
                    IFeatureLayer2 selectedFL = layer as IFeatureLayer2;

                    if (selectedFL.FeatureClass is IDataset)
                    {
                        IDataset dataset = (IDataset)(selectedFL.FeatureClass); // Explicit Cast
                        //data += "dataset: " + (dataset.Workspace.PathName + " --> " + dataset.Name) + "\t";
                        data += "dataset: " + dataset.Name + "\t";
                    }
                    else
                    {
                        data += "NO dataset \t";
                    }
                    if (selectedFL.FeatureClass != null)
                        data += "FeatureClass.ID=" + selectedFL.FeatureClass.ObjectClassID + "\t";
                    else
                        data += "FeatureClass= null \t";

                    data += "Data Source Type: " + selectedFL.DataSourceType + "\t";
                }
                else if (layer is IRasterLayer)
                {
                    data += "Raster\t" + layer.Name + "\t";
                    IRasterLayer selectedRL = layer as IRasterLayer;
                    data += "File Path: " + selectedRL.FilePath + "\t";
                }
                else
                {
                    data += "!!!!\t" + layer.Name + "\t";
                }


                data += " \n";



                layer = enumLayer.Next();
                layerCount++;
            }

            data += "layerCount = " + layerCount + "\n";
        }

    }
}
