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
        public frmTest()
        {
            InitializeComponent();
        }


        private void frmTest_Load(object sender, EventArgs e)
        {
            getData();
            data += "\n \n \n \n \t \t <<--->> \t \t \n \n \n \n ";
            getData2();



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
                "onl.DataPath = " + ((OneLayerDescriptions)e.Node.Tag).DataPath;

        }


        //------------------------------


        //построить дерево по методу getData2()



        public void init_treeView()
        {
            //List<OneLayerDescriptions> LayerDescriptions = new List<OneLayerDescriptions>();
            //foreach (OneLayerDescriptions old in opd.LayerDescriptions)
            //{
            //    OneLayerDescriptions tmp = new OneLayerDescriptions();
            //    tmp.CopySetingFrom(old);
            //    LayerDescriptions.Add(tmp);
            //}


            //LayerDescriptions = NewMethod(LayerDescriptions);

            //// фигня
            //LayerDescriptions = NewMethod(LayerDescriptions);


            ////----------
            ////System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ////foreach (OneLayerDescriptions old in LayerDescriptions)
            ////{
            ////    sb.Append("[");
            ////    sb.Append(old.Caption);
            ////    sb.Append("] ");
            ////}
            ////string str = sb.ToString();
            ////if (str != "")
            ////    MessageBox.Show(str);
        }


        private List<OneLayerDescriptions> NewMethod(List<OneLayerDescriptions> LayerDescriptions)
        {
            List<OneLayerDescriptions> tmp = new List<OneLayerDescriptions>();

            //while (LayerDescriptions.Count > 0)
            //{
            //    OneLayerDescriptions onl = LayerDescriptions[0];

            //    if (onl.ParentDataPath == "")
            //    {
            //        addNode(onl);
            //        LayerDescriptions.Remove(onl);
            //        onl = null;
            //    }
            //    else
            //    {
            //        TreeNode[] findTreeNodes = tvLayers.Nodes.Find(onl.ParentDataPath, true);
            //        if (findTreeNodes.Length > 0)
            //        {
            //            foreach (TreeNode tn in findTreeNodes)
            //            {
            //                OneLayerDescriptions olp_tmp = (OneLayerDescriptions)tn.Tag;
            //                if (olp_tmp.Type.Equals(onl.ParentType) && olp_tmp.DataPath.Equals(onl.ParentDataPath))
            //                {
            //                    addNode(onl, tn);
            //                    LayerDescriptions.Remove(onl);
            //                    onl = null;
            //                }
            //            }
            //        }
            //    }
            //    if (onl != null)
            //    {
            //        tmp.Add(onl);
            //        LayerDescriptions.Remove(onl);
            //        onl = null;
            //    }
            //}
            return tmp;
        }


        public void addNode(OneLayerDescriptions onl, TreeNode parent = null)
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
            //newNode.Text = onl.Caption;
            newNode.ToolTipText = newNode.FullPath;
            newNode.Tag = onl;
        }


        //-----------------------------------------



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
        

        void getData2()
        {
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IMap myMap = mxDoc.FocusMap;

            int layerCount = 0;


            for (int i = 0; i < myMap.LayerCount; i++)
            {
                ILayer layer = myMap.Layer[i];
                layerCount += NewMethod1(layer);
            }

            data += "layerCount = " + layerCount + "\n";

        }

        private int NewMethod1(ILayer layer)
        {
            int layerCount = 0;

            if (layer is ICompositeLayer)
            {
                ICompositeLayer igroup = layer as ICompositeLayer;
                data += "IComposite\t" + layer.Name + "\tCount = " + igroup.Count + "\t";

                for (int j = 0; j < igroup.Count; j++)
                {
                    ILayer layer2 = igroup.Layer[j];
                    layerCount += NewMethod1(layer2);
                }
            }

            // остольные случаи

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

            layerCount++;


            return layerCount;
        }






    }
}
