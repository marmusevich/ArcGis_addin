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
        public StringCollection retString = null;
        public List<OneLayerDescriptions> retLayerDescriptions = null;

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
                retLayerDescriptions.Add(old);
        }
        // - включить из StringCollection 
        StringCollection tmpSc;
        private void StringCollectionToTreeNode(TreeNode treeNode)
        {
            OneLayerDescriptions old = (OneLayerDescriptions)treeNode.Tag;
            if (old != null)
            {
                string layerName = treeNode.Text.ToLower();
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

            if (layerCount == 0)
            {
                MessageBox.Show("В карте нет слоев. " + System.Environment.NewLine + "Добавьте слои на карту.");
                DialogResult = DialogResult.Abort;
                this.Close();
            }

            // чтение LayerDescriptions из List<OneLayerDescriptions> 
            if (retLayerDescriptions != null && retLayerDescriptions.Count > 0)
            {
                tmpOdl = new List<OneLayerDescriptions>();
                foreach (OneLayerDescriptions old in retLayerDescriptions)
                    tmpOdl.Add(old);

                TreeViewCallRecursive(tvLayers, this.LayerDescriptionsToTreeNode);

                if (tmpOdl.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("Чтение новым способом. Отсутствуют слои:\n");
                    foreach (OneLayerDescriptions old in tmpOdl)
                    {
                        sb.Append("[");
                        sb.Append(old.Caption + " - data path = " + old.DataPath);
                        sb.Append("]\n");
                    }
                    MessageBox.Show(sb.ToString());
                }
            }
            // чтение для перехода StringCollection если != NULL
            else if (retString != null && retString.Count > 0)
            {
                tmpSc = new StringCollection();
                foreach (string s in retString)
                    tmpSc.Add(s.ToLower());

                TreeViewCallRecursive(tvLayers, this.StringCollectionToTreeNode);

                if (tmpSc.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("Чтение старым способом. Отсутствуют слои:\n");
                    foreach (string s in tmpSc)
                    {
                        sb.Append("[");
                        sb.Append(s);
                        sb.Append("]\n");
                    }
                    MessageBox.Show(sb.ToString());
                }
            }

            this.tvLayers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvLayers_AfterCheck);
            //this.tvLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLayers_AfterSelect);
            //this.tvLayers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLayers_NodeMouseClick);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // сохранить LayerDescriptions в List<OneLayerDescriptions> retVal2
            if (retLayerDescriptions == null)
                retLayerDescriptions = new List<OneLayerDescriptions>();
            else
                retLayerDescriptions.Clear();

            TreeViewCallRecursive(tvLayers, this.LayerDescriptionsFromTreeNode);

            // пока храним паролельно
            // удалить данные из StringCollection 
            //if (retVal != null)
            //    retVal.Clear();

            DialogResult = DialogResult.OK;
            this.Close();
        }


        // при включении / отключении спрашивать и рекурсивно вкл/откл для всех дочерних
        private void tvLayers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                this.tvLayers.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.tvLayers_AfterCheck);
                if (e.Node.Checked)
                {
                    DialogResult res = MessageBox.Show("Включить все вложенные слои?", "слои => '+'", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                        TreeNodeCallRecursive(e.Node, this.CheckedTreeNode);
                }
                else
                {
                    DialogResult res = MessageBox.Show("Выключить все вложенные слои?", "слои => '-'", MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                        TreeNodeCallRecursive(e.Node, this.UnCheckedTreeNode);
                }

                this.tvLayers.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvLayers_AfterCheck);
            }
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

        private void tvLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }


    }
}
