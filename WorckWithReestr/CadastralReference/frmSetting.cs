﻿using System;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmSetting : Form
    {
        private string prefix_TabsTotcPages = "tpPage_";

        private string prefix_txtSelectedLayers = "txtSelectedLayers_";
        private string prefix_lblLayers = "lblLayers_";
        private string prefix_btnSelectedLayers = "btnSelectedLayers_";


        public frmSetting()
        {
            InitializeComponent();
        }


        private void frmSetting_Load(object sender, EventArgs e)
        {
            CreateUI();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region //создать элементы управления
        private void CreateUI()
        {
            this.tcSetting.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.pnListOfPages.SuspendLayout();
            this.tpPages.SuspendLayout();
            this.SuspendLayout();
            //

            CreateTabsTotcPages();
            FillclbListOfPages();

            //
            this.tcSetting.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.pnListOfPages.ResumeLayout(false);
            this.tpPages.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void FillclbListOfPages()
        {
            foreach (OnePageDescriptions pd in WorkCadastralReference.GetCadastralReferenceData().Pages)
            {
                clbListOfPages.Items.Add(pd, pd.Enable);
            }
        }


        private void CreateTabsTotcPages()
        {

            foreach (OnePageDescriptions pd in WorkCadastralReference.GetCadastralReferenceData().Pages)
            {
                TabPage tp = new TabPage();
                tp.SuspendLayout();
                tp.Location = new System.Drawing.Point(4, 22);
                tp.Name = prefix_TabsTotcPages + pd.PagesID.ToString();
                tp.Padding = new System.Windows.Forms.Padding(3);
                tp.Size = new System.Drawing.Size(637, 389);
                //tp.TabIndex = 0;
                tp.Text = pd.Caption;
                tp.UseVisualStyleBackColor = true;
                tp.Visible = pd.Enable;

                tp.Controls.Add(this.Create_txtSelectedLayers(pd));
                tp.Controls.Add(this.Create_lblLayers(pd));
                tp.Controls.Add(this.Create_btnSelectedLayers(pd));

                //остальные элементы управления
                    
                this.tcPages.Controls.Add(tp);
                tp.ResumeLayout(false);
                tp.PerformLayout();
            }
        }

        private TextBox Create_txtSelectedLayers(OnePageDescriptions pd)
        {
            TextBox t = new TextBox();
            t.Anchor = (AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right) ));
            t.Enabled = false;
            t.Location = new System.Drawing.Point(109, 33);
            t.Name = prefix_txtSelectedLayers + pd.PagesID.ToString();
            t.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            t.Size = new System.Drawing.Size(522, 20);
            t.Tag = pd;
            t.Text = GetShortListOfEnabledLayers(pd);
            t.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtSelectedLayers_MouseDoubleClick);
            return t;
        }
        private Label Create_lblLayers(OnePageDescriptions pd)
        {
            Label l = new Label();
            l.Dock = System.Windows.Forms.DockStyle.Top;
            l.Location = new System.Drawing.Point(3, 3);
            l.Name = prefix_lblLayers + pd.PagesID.ToString();
            l.Size = new System.Drawing.Size(631, 23);
            l.Tag = pd;
            l.Text = "Включаемые слои";
            return l;
        }
        private Button Create_btnSelectedLayers(OnePageDescriptions pd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(3, 30);
            b.Name = prefix_btnSelectedLayers + pd.PagesID.ToString();
            b.Size = new System.Drawing.Size(100, 23);
            b.Tag = pd;
            b.Text = "Выбрать слои";
            b.UseVisualStyleBackColor = true;
            b.Click += new System.EventHandler(this.btnSelectedLayers_Click);
            return b;
        }

        //остальные элементы управления

        #endregion //создать элементы управления

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region вспомогательные функции
        // получить элемент упровления по имени
        private Control GetControlByName(string name)
        {
            Control[] ca = this.Controls.Find(name, true);
            if (ca != null && ca.Length > 0)
                return ca[0];
            return null;
        }

        // получить описание листа из элемента управления
        private OnePageDescriptions GetPageDescriptionsFromControlTag(Control c)
        {
            if (c != null && c.Tag != null && c.Tag is OnePageDescriptions)
                return ((OnePageDescriptions)c.Tag);
            return null;

        }



        private void ShowForm_SelectLayers(OnePageDescriptions pd)
        {
            frmSelectLayers frm = new frmSelectLayers();
            frm.retVal = pd.Layers;

            if(frm.ShowDialog() == DialogResult.OK)
                pd.Layers = frm.retVal;

            TextBox t = GetControlByName(prefix_txtSelectedLayers + pd.PagesID.ToString()) as TextBox;
            if(t != null)
                t.Text = GetShortListOfEnabledLayers(pd);


        }
        private string GetShortListOfEnabledLayers(OnePageDescriptions pd)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in pd.Layers)
            {
                sb.Append("[");
                sb.Append(s);
                sb.Append("] ");
            }
            return sb.ToString();
        }





        #endregion вспомогательные функции


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 
        private void btnSelectedLayers_Click(object sender, EventArgs e)
        {
            OnePageDescriptions pd = this.GetPageDescriptionsFromControlTag(sender as Control);
            ShowForm_SelectLayers(pd);
        }

        private void txtSelectedLayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            ShowForm_SelectLayers(opd);
        }
        #endregion


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 
        private void btnSave_Click(object sender, EventArgs e)
        {
            // продвинутое сохранение
            for (int i = 0; i < clbListOfPages.Items.Count; i++)
            {
                OnePageDescriptions pd = clbListOfPages.Items[i] as OnePageDescriptions;
                // проверки
                int index = WorkCadastralReference.GetCadastralReferenceData().Pages.IndexOf(pd);
                if (index != -1)
                {
                    WorkCadastralReference.GetCadastralReferenceData().Pages[index] = pd;
                }
                else
                    WorkCadastralReference.GetCadastralReferenceData().Pages.Add(pd);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void clbListOfPages_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            OnePageDescriptions pd = clbListOfPages.Items[e.Index] as OnePageDescriptions;
            pd.Enable = e.NewValue == CheckState.Checked;
            TabControl p = GetControlByName(prefix_TabsTotcPages + pd.PagesID.ToString()) as TabControl;
            if (p != null)
                p.Visible = pd.Enable;
        }
        #endregion 

    }
}
