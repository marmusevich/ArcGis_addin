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


        //------------------------------
        //делегат для выполнения в рекурсивном обходе
        private delegate void DelegateWorkWithTreeNode(TreeNode treeNode);
        // - включить для дочерних выбор
        private void CheckedTreeNode(TreeNode treeNode)
        {
            treeNode.Checked = true;
        }
        // - отключить для дочерних выбор
        private void UnCheckedTreeNode(TreeNode treeNode)
        {
            treeNode.Checked = false;
        }
        // - включить из List<OneLayerDescriptions> 
        List<OneLayerDescriptions> tmpOdl;
        private void LayerDescriptionsToTreeNode(TreeNode treeNode)
        {
            OneLayerDescriptions old = (OneLayerDescriptions)treeNode.Tag;
            if (old != null)
            {
                int index = tmpOdl.IndexOf(old);
                if (index != -1)
                {
                    treeNode.Checked = true;
                    tmpOdl.RemoveAt(index);
                }
            }
        }
        // - сохранить в List<OneLayerDescriptions> 
        private void LayerDescriptionsFromTreeNode(TreeNode treeNode)
        {
            OneLayerDescriptions old = (OneLayerDescriptions)treeNode.Tag;
            if (treeNode.Checked && old != null)
                retVal2.Add(old);
        }
        // - включить из StringCollection 
        StringCollection tmpSc;
        private void StringCollectionToTreeNode(TreeNode treeNode)
        {
            OneLayerDescriptions old = (OneLayerDescriptions)treeNode.Tag;
            if (old != null)
            {
                string layerName = treeNode.Name.ToLower();
                int index = tmpSc.IndexOf(layerName);
                if (index != -1)
                {
                    treeNode.Checked = true;
                    tmpSc.RemoveAt(index);
                }
            }
        }
        //------------------------------

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

            //int t = TreeViewCallRecursive(tvLayers, this.testDelegateWorkWithTreeNode);
            //txt.Text += System.Environment.NewLine + "1 treeNodeCount = " + t.ToString() + System.Environment.NewLine;
            //int tt = TreeViewCallRecursive(tvLayers, delegate (TreeNode treeNode) {; });
            //txt.Text += System.Environment.NewLine + "2 treeNodeCount = " + tt.ToString() + System.Environment.NewLine;


            // чтение LayerDescriptions из List<OneLayerDescriptions> retVal2
            if (retVal2 != null && retVal2.Count > 0)
            {
                tmpOdl = new List<OneLayerDescriptions>();
                foreach (OneLayerDescriptions old in retVal2)
                    tmpOdl.Add(old);

                TreeViewCallRecursive(tvLayers, this.LayerDescriptionsToTreeNode);

                if (tmpOdl.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("LayerDescriptions Отсутствуют слои:\n");
                    foreach (OneLayerDescriptions old in tmpOdl)
                    {
                        sb.Append("[");
                        sb.Append(old.Caption + " - data path = " + old.DataPath);
                        sb.Append("]\n");
                    }
                    MessageBox.Show(sb.ToString());
                }
            }

            // чтение для перехода StringCollection retVal если != NULL
            if (retVal != null && retVal.Count > 0)
            {
                tmpSc = new StringCollection();
                foreach (string s in retVal)
                    tmpSc.Add(s.ToLower());

                TreeViewCallRecursive(tvLayers, this.StringCollectionToTreeNode);

                if (tmpSc.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("StringBuilder Отсутствуют слои:\n");
                    foreach (string s in tmpSc)
                    {
                        sb.Append("[");
                        sb.Append(s);
                        sb.Append("]\n");
                    }
                    MessageBox.Show(sb.ToString());
                }
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // сохранить LayerDescriptions в List<OneLayerDescriptions> retVal2
            if (retVal2 == null)
                retVal2 = new List<OneLayerDescriptions>();
            else
                retVal2.Clear();

            TreeViewCallRecursive(tvLayers, this.LayerDescriptionsFromTreeNode);
            

            // удалить данные из StringCollection retVal
            if (retVal != null)
                retVal.Clear();


            DialogResult = DialogResult.OK;
            this.Close();
        }



        private void tvLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //int t = TreeNodeCallRecursive(e.Node, this.testDelegateWorkWithTreeNode);
            //txt.Text += System.Environment.NewLine + "1 AfterSelect treeNodeCount = " +
            //    t.ToString() +
            //    System.Environment.NewLine;
        }

        // при включении / отключении спрашивать и рекурсивно вкл/откл для всех дочерних
        private void tvLayers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            OneLayerDescriptions old = (OneLayerDescriptions)e.Node.Tag;
            if (old != null && old.Type == OneLayerDescriptions.LayerType.Group)
            {
                if (e.Node.Checked)
                {
                    DialogResult res = MessageBox.Show("Включить все вложенные слои?", "слои => '+'", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.OK)
                        TreeViewCallRecursive(tvLayers, this.CheckedTreeNode);
                }
                else
                {
                    DialogResult res = MessageBox.Show("Выключить все вложенные слои?", "слои => '-'", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.OK)
                        TreeViewCallRecursive(tvLayers, this.UnCheckedTreeNode);
                }
            }
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
