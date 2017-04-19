using System.Windows.Forms;
using System;
using System.Collections.Specialized;
using ESRI.ArcGIS.Carto;
using WorckWithReestr;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.ArcMapUI;
using System.Collections.Generic;

namespace CadastralReference
{
    public partial class frmSelectLayers : Form
    {
        public StringCollection retVal = null;
        public List<OneLayerDescriptions> retVal2 = null;


        public frmSelectLayers()
        {
            InitializeComponent();
        }


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

        private TreeNode addNode(OneLayerDescriptions onl, TreeNode parent)
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

        //------------------------------
        //делегат для выполнения в рекурсивном обходе
        private delegate void DelegateWorkWithTreeNode(TreeNode treeNode);

        // обход всего дерева
        private int TreeViewCallRecursive(TreeView treeView, DelegateWorkWithTreeNode func)
        {
            int count = 0;
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode tn in nodes)
            {
                count += TreeNodeCallRecursive(tn, func);
            }
            return count;
        }

        //обход узла
        private int TreeNodeCallRecursive(TreeNode treeNode, DelegateWorkWithTreeNode func)
        {
            func(treeNode);
            int count = 1;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                count += TreeNodeCallRecursive(tn, func);
            }
            return count;
        }
        //-----------------------------------------


        private void testDelegateWorkWithTreeNode(TreeNode treeNode)
        {
            txt.Text += System.Environment.NewLine + " Text = " + treeNode.Text;
        }



        private void frmSelectLayers_Load(object sender, EventArgs e)
        {
            int layerCount = BuldTree();
            txt.Text += "layerCount = " + layerCount + System.Environment.NewLine;

            if (layerCount == 0)
            {
                MessageBox.Show("В карте нет слоев. " + System.Environment.NewLine + "Добавьте слои на карту.");
                DialogResult = DialogResult.Abort;
                this.Close();
            }


            txt.Text += System.Environment.NewLine + "1 treeNodeCount = " +
                TreeViewCallRecursive(tvLayers, this.testDelegateWorkWithTreeNode) +
                System.Environment.NewLine;

            txt.Text += System.Environment.NewLine + "2 treeNodeCount = " +
                TreeViewCallRecursive(tvLayers, delegate (TreeNode treeNode) {; }) +
                System.Environment.NewLine;


            // чтение LayerDescriptions из List<OneLayerDescriptions> retVal2
            if (retVal2 != null)
            {

            }

            // чтение для перехода StringCollection retVal если != NULL
            if (retVal != null)
            {
                String[] ar = new String[retVal.Count];
                retVal.CopyTo(ar, 0);

                //lbSelectedLayers.Items.AddRange(ar);

                //// убрать выбранное
                //foreach (object o in lbSelectedLayers.Items)
                //{
                //    if (lbAllLayers.Items.Contains(o))
                //    {
                //        lbAllLayers.Items.Remove(o);
                //    }
                //}
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // сохранить LayerDescriptions в List<OneLayerDescriptions> retVal2

            
            // удалить данные из StringCollection retVal
            //    if(retVal == null)
            //        retVal = new StringCollection();
            //    retVal.Clear();

            //    foreach (object o in lbSelectedLayers.Items)
            //    {
            //        string tmp = (string)o;
            //        if (tmp != null)
            //        { 
            //            retVal.Add(tmp);
            //        }
            //    }

            DialogResult = DialogResult.OK;
            this.Close();
        }



        private void tvLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txt.Text += System.Environment.NewLine + "1 AfterSelect treeNodeCount = " +
                TreeNodeCallRecursive(e.Node, this.testDelegateWorkWithTreeNode) +
                System.Environment.NewLine;

        }

        // при включении / отключении спрашивать и рекурсивно вкл/откл для всех дочерних
        private void tvLayers_AfterCheck(object sender, TreeViewEventArgs e)
        {
        }

        private void tvLayers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //txt.Text = "FullPath = " + e.Node.FullPath + System.Environment.NewLine +
            //    "Name = " + e.Node.Name + System.Environment.NewLine +
            //    "Text = " + e.Node.Text + System.Environment.NewLine + System.Environment.NewLine +
            //    "onl.Caption = " + ((OneLayerDescriptions)e.Node.Tag).Caption + System.Environment.NewLine +
            //    "onl.Type = " + ((OneLayerDescriptions)e.Node.Tag).Type.ToString() + System.Environment.NewLine +
            //    "onl.DataPath = " + ((OneLayerDescriptions)e.Node.Tag).DataPath;

        }




        //private void frmSelectLayers_Load(object sender, EventArgs e)
        //{
        //    StringCollection tmp = WorkCadastralReference_MAP.GetListOfAllLaers();
        //    if (tmp == null || tmp.Count == 0)
        //    {
        //        MessageBox.Show("В карте нет слоев. \n Добавьте слои на карту.");
        //        DialogResult = DialogResult.Abort;
        //        this.Close();

        //    }
        //    String[] arr = new String[tmp.Count];
        //    tmp.CopyTo(arr, 0);
        //    lbAllLayers.Items.AddRange(arr);

        //    if (retVal != null)
        //    {
        //        String[] ar = new String[retVal.Count];
        //        retVal.CopyTo(ar, 0);
        //        lbSelectedLayers.Items.AddRange(ar);

        //        // убрать выбранное
        //        foreach (object o in lbSelectedLayers.Items)
        //        {
        //            if (lbAllLayers.Items.Contains(o))
        //            {
        //                lbAllLayers.Items.Remove(o);
        //            }
        //        }
        //    }
        //}

        //private void Add()
        //{
        //    System.Array arr = new object[lbAllLayers.SelectedItems.Count];
        //    lbAllLayers.SelectedItems.CopyTo(arr, 0);
        //    foreach (object o in arr)
        //    {
        //        if (o != null)
        //        {
        //            lbSelectedLayers.Items.Add(o);
        //            lbAllLayers.Items.Remove(o);
        //        }
        //    }
        //}

        //private void Del()
        //{
        //    System.Array arr = new object[lbSelectedLayers.SelectedItems.Count];
        //    lbSelectedLayers.SelectedItems.CopyTo(arr, 0);
        //    foreach (object o in arr)
        //    {
        //        if (o != null)
        //        {
        //            lbAllLayers.Items.Add(o);
        //            lbSelectedLayers.Items.Remove(o);
        //        }
        //    }
        //}

        //private void btnOk_Click(object sender, EventArgs e)
        //{
        //    if(retVal == null)
        //        retVal = new StringCollection();

        //    retVal.Clear();

        //    foreach (object o in lbSelectedLayers.Items)
        //    {
        //        string tmp = (string)o;
        //        if (tmp != null)
        //        { 
        //            retVal.Add(tmp);
        //        }
        //    }
        //    DialogResult = DialogResult.OK;
        //    this.Close();
        //}



        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Add();
        //}

        //private void btnDel_Click(object sender, EventArgs e)
        //{
        //    Del();
        //}

        //private void lbAllLayers_DoubleClick(object sender, EventArgs e)
        //{
        //    Add();
        //}

        //private void lbSelectedLayers_DoubleClick(object sender, EventArgs e)
        //{
        //    Del();
        //}
    }
}
