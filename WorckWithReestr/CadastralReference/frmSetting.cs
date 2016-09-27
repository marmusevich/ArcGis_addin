using SharedClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmSetting : Form
    {
        private string prefix_tpPages = "tpPage_";

        private string prefix_txtSelectedLayers = "txtSelectedLayers_";
        private string prefix_lblLayers = "lblLayers_";
        private string prefix_btnSelectedLayers = "btnSelectedLayers_";
        private string prefix_gbTextsElement = "_gbTextsElement";
        private string prefix_btnDeleteTextElement = "btnDeleteTextElement_";
        private string prefix_btnAddTextElement = "btnAddTextElement_";
        private string prefix_lbTextElement = "lbTextElement_";
        private string prefix_cbNordArrow = "cbNordArrow_";
        private string prefix_cbScaleBar = "cbScaleBar_";
        private string prefix_gbDataFrameSyze = "gbDataFrameSyze_";
        private string prefix_nudDataFrameSyze_Down = "nudDataFrameSyze_Down_";
        private string prefix_nudDataFrameSyze_Up = "nudDataFrameSyze_Up_";
        private string prefix_nudDataFrameSyze_Left = "nudDataFrameSyze_Left_";
        private string prefix_nudDataFrameSyze_Right = "nudDataFrameSyze_Right_";
        private string prefix_lblDataFrameSyze_Left = "lblDataFrameSyze_Left_";
        private string prefix_lblDataFrameSyze_Right = "lblDataFrameSyze_Right_";
        private string prefix_lblDataFrameSyze_Down = "lblDataFrameSyze_Down_";
        private string prefix_lblDataFrameSyze_Up = "lblDataFrameSyze_Up_";
        private string prefix_btnScaleBar = "btnScaleBar_";
        private string prefix_btnNordArrow = "btnNordArrow_";


        CadastralReferenceData m_crd = null;


        public frmSetting()
        {
            InitializeComponent();
            m_crd = new CadastralReferenceData(WorkCadastralReference.GetCadastralReferenceData());
        }


        private void frmSetting_Load(object sender, EventArgs e)
        {
            CreateUI();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region //создать элементы управления
        private void CreateUI()
        {
            foreach (OnePageDescriptions pd in m_crd.Pages)
            {
                CreateUiOnePage(pd);
            }
        }

        private void CreateUiOnePage(OnePageDescriptions pd)
        {
            this.tcSetting.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.pnListOfPages.SuspendLayout();
            this.tpPages.SuspendLayout();
            this.SuspendLayout();

            clbListOfPages.Items.Add(pd, pd.Enable);
            Create_tpPages(pd);

            this.tcSetting.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.pnListOfPages.ResumeLayout(false);
            this.tpPages.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void Create_tpPages(OnePageDescriptions opd)
        {
            TabPage tp = new TabPage();
            tp.SuspendLayout();
            tp.Location = new System.Drawing.Point(4, 22);
            tp.Name = prefix_tpPages + opd.PagesID.ToString();
            tp.Padding = new Padding(3);
            tp.Size = new System.Drawing.Size(637, 389);
            //tp.TabIndex = 0;
            tp.Text = opd.Caption;
            tp.UseVisualStyleBackColor = true;
            

            tp.Controls.Add(this.Create_txtSelectedLayers(opd));
            tp.Controls.Add(this.Create_lblLayers(opd));
            tp.Controls.Add(this.Create_btnSelectedLayers(opd));

            tp.Controls.Add(Create_btnScaleBar(opd));
            tp.Controls.Add(Create_btnNordArrow(opd));
            tp.Controls.Add(Create_gbDataFrameSyze(opd));
            tp.Controls.Add(Create_cbScaleBar(opd));
            tp.Controls.Add(Create_cbNordArrow(opd));
            tp.Controls.Add(Create_gbTextsElement(opd));

            this.tcPages.Controls.Add(tp);

            if (opd.Enable)
                tp.Parent = this.tcPages;// -- Показать
            else
                tp.Parent = null;// -- Скрыть

            tp.ResumeLayout(false);
            tp.PerformLayout();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private Label Create_lblLayers(OnePageDescriptions opd)
        {
            Label l = new Label();
            l.Dock = System.Windows.Forms.DockStyle.Top;
            l.Location = new System.Drawing.Point(0, 0);
            l.Name = prefix_lblLayers + opd.PagesID.ToString();
            l.Size = new System.Drawing.Size(637, 20);
            l.Tag = opd;
            l.Text = "Включаемые слои";
            return l;
        }
        private TextBox Create_txtSelectedLayers(OnePageDescriptions opd)
        {
            TextBox t = new TextBox();
            t.Anchor = (AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right)));
            t.Enabled = false;
            t.Location = new System.Drawing.Point(114, 22);
            t.Name = prefix_txtSelectedLayers + opd.PagesID.ToString();
            t.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            t.Size = new System.Drawing.Size(520, 23);
            t.Tag = opd;
            t.Text = opd.LayersToString();
            t.MouseDoubleClick += new MouseEventHandler(this.txtSelectedLayers_MouseDoubleClick);
            return t;
        }
        private Button Create_btnSelectedLayers(OnePageDescriptions opd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(3, 22);
            b.Name = prefix_btnSelectedLayers + opd.PagesID.ToString();
            b.Size = new System.Drawing.Size(100, 23);
            b.Tag = opd;
            b.Text = "Выбрать слои";
            b.UseVisualStyleBackColor = true;
            b.Click += new System.EventHandler(this.btnSelectedLayers_Click);
            return b;
        }
        private GroupBox Create_gbTextsElement(OnePageDescriptions opd)
        {
            GroupBox g = new GroupBox();
            g.SuspendLayout();
            g.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            g.Location = new System.Drawing.Point(3, 215);
            g.Name = prefix_gbTextsElement + opd.PagesID.ToString();
            g.Tag = opd;
            g.Size = new System.Drawing.Size(631, 174);
            g.TabStop = false;
            g.Text = "Надписи на листе";
            g.Controls.Add(Create_btnDeleteTextElement(opd));
            g.Controls.Add(Create_btnAddTextElement(opd));
            g.Controls.Add(Create_lbTextElement(opd));
            g.ResumeLayout(false);
            return g;
        }
        private Button Create_btnDeleteTextElement(OnePageDescriptions opd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(95, 20);
            b.Name = prefix_btnDeleteTextElement + opd.PagesID.ToString();
            b.Size = new System.Drawing.Size(75, 23);
            b.Tag = opd;
            b.Text = "Убрать";
            b.UseVisualStyleBackColor = true;
            b.Click += new System.EventHandler(this.btnDeleteTextElement_Click);
            return b;
        }
        private Button Create_btnAddTextElement(OnePageDescriptions opd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(3, 20);
            b.Name = prefix_btnAddTextElement + opd.PagesID.ToString();
            b.Tag = opd;
            b.Size = new System.Drawing.Size(75, 23);
            b.Text = "Добавить";
            b.UseVisualStyleBackColor = true;
            b.Click += new System.EventHandler(this.btnAddTextElement_Click);
            return b;
        }
        private ListBox Create_lbTextElement(OnePageDescriptions opd)
        {
            ListBox l = new ListBox();
            l.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left)| AnchorStyles.Right)));
            l.FormattingEnabled = true;
            l.Location = new System.Drawing.Point(3, 49);
            l.Name = prefix_lbTextElement + opd.PagesID.ToString();
            l.Tag = opd;
            l.Size = new System.Drawing.Size(626, 121);
            l.DoubleClick += new System.EventHandler(this.lbTextElement_DoubleClick);
            foreach (OneTextElementDescription oted in opd.TextElements)
            {
                l.Items.Add(oted);
            }
            return l;
        }
        private CheckBox Create_cbNordArrow(OnePageDescriptions opd)
        {
            CheckBox c = new CheckBox();
            c.AutoSize = true;
            c.Location = new System.Drawing.Point(20, 121);
            c.Name = prefix_cbNordArrow + opd.PagesID.ToString();
            c.Tag = opd;
            c.Size = new System.Drawing.Size(107, 17);
            c.Text = "Стрелка севера";
            c.UseVisualStyleBackColor = true;
            c.Checked = opd.IsHasNorthArrow;
            c.CheckedChanged += new System.EventHandler(this.cbNordArrow_CheckedChanged);
            return c;
        }
        private CheckBox Create_cbScaleBar(OnePageDescriptions opd)
        {
            CheckBox c = new CheckBox();
            c.AutoSize = true;
            c.Location = new System.Drawing.Point(20, 149);
            c.Name = prefix_cbScaleBar + opd.PagesID.ToString();
            c.Tag = opd;
            c.Size = new System.Drawing.Size(119, 17);
            c.Text = "Маштабная шкала";
            c.Checked = opd.IsHasScaleBar;
            c.UseVisualStyleBackColor = true;
            c.CheckedChanged += new System.EventHandler(this.cbScaleBar_CheckedChanged);
            return c;
        }
        private GroupBox Create_gbDataFrameSyze(OnePageDescriptions opd)
        {
            GroupBox g = new GroupBox();
            g.SuspendLayout();
            g.Location = new System.Drawing.Point(3, 48);
            g.Name = prefix_gbDataFrameSyze + opd.PagesID.ToString();
            g.Tag = opd;
            g.Size = new System.Drawing.Size(629, 64);
            g.TabStop = false;
            g.Text = "Размер области данных";
            g.Controls.Add(Create_nudDataFrameSyze_Down(opd));
            g.Controls.Add(Create_nudDataFrameSyze_Up(opd));
            g.Controls.Add(Create_nudDataFrameSyze_Left(opd));
            g.Controls.Add(Create_nudDataFrameSyze_Right(opd));
            g.Controls.Add(Create_lblDataFrameSyze_Left(opd));
            g.Controls.Add(Create_lblDataFrameSyze_Right(opd));
            g.Controls.Add(Create_lblDataFrameSyze_Down(opd));
            g.Controls.Add(Create_lblDataFrameSyze_Up(opd));
            g.ResumeLayout(false);
            return g;
        }
        private NumericUpDown Create_nudDataFrameSyze_Down(OnePageDescriptions opd)
        {
            NumericUpDown n = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(n)).BeginInit();
            n.DecimalPlaces = 2;
            n.Location = new System.Drawing.Point(126, 41);
            n.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            n.Minimum = new decimal(new int[] { 200, 0, 0, -2147483648 });
            n.Name = prefix_nudDataFrameSyze_Down + opd.PagesID.ToString();
            n.Value = (decimal)opd.DataFrameSyze_Down;
            n.Tag = opd;
            n.Size = new System.Drawing.Size(57, 20);
            n.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ((System.ComponentModel.ISupportInitialize)(n)).EndInit();
            return n;
        }
        private NumericUpDown Create_nudDataFrameSyze_Up(OnePageDescriptions opd)
        {
            NumericUpDown n = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(n)).BeginInit();
            n.DecimalPlaces = 2;
            n.Location = new System.Drawing.Point(126, 16);
            n.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            n.Minimum = new decimal(new int[] { 200, 0, 0, -2147483648 });
            n.Name = prefix_nudDataFrameSyze_Up + opd.PagesID.ToString();
            n.Value = (decimal)opd.DataFrameSyze_Up;
            n.Tag = opd;
            n.Size = new System.Drawing.Size(57, 20);
            n.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ((System.ComponentModel.ISupportInitialize)(n)).EndInit();
            return n;
        }
        private NumericUpDown Create_nudDataFrameSyze_Left(OnePageDescriptions opd)
        {
            NumericUpDown n = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(n)).BeginInit();
            n.DecimalPlaces = 2;
            n.Location = new System.Drawing.Point(309, 16);
            n.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            n.Minimum = new decimal(new int[] { 200, 0, 0, -2147483648 });
            n.Name = prefix_nudDataFrameSyze_Left + opd.PagesID.ToString();
            n.Value = (decimal)opd.DataFrameSyze_Left;
            n.Tag = opd;
            n.Size = new System.Drawing.Size(57, 20);
            n.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ((System.ComponentModel.ISupportInitialize)(n)).EndInit();
            return n;
        }
        private NumericUpDown Create_nudDataFrameSyze_Right(OnePageDescriptions opd)
        {
            NumericUpDown n = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(n)).BeginInit();
            n.DecimalPlaces = 2;
            n.Location = new System.Drawing.Point(309, 41);
            n.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            n.Minimum = new decimal(new int[] { 200, 0, 0, -2147483648 });
            n.Name = prefix_nudDataFrameSyze_Right + opd.PagesID.ToString();
            n.Value = (decimal)opd.DataFrameSyze_Right;
            n.Tag = opd;
            n.Size = new System.Drawing.Size(57, 20);
            n.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ((System.ComponentModel.ISupportInitialize)(n)).EndInit();
            return n;
        }
        private Label Create_lblDataFrameSyze_Left(OnePageDescriptions opd)
        {
            Label l = new Label();
            l.Location = new System.Drawing.Point(203, 19);
            l.Name = prefix_lblDataFrameSyze_Left + opd.PagesID.ToString();
            l.Tag = opd;
            l.Size = new System.Drawing.Size(100, 15);
            l.Text = "От левого края";
            return l;
        }
        private Label Create_lblDataFrameSyze_Right(OnePageDescriptions opd)
        {
            Label l = new Label();
            l.Location = new System.Drawing.Point(203, 44);
            l.Name = prefix_lblDataFrameSyze_Right + opd.PagesID.ToString();
            l.Tag = opd;
            l.Size = new System.Drawing.Size(100, 15);
            l.Text = "От правого края";
            return l;
        }
        private Label Create_lblDataFrameSyze_Down(OnePageDescriptions opd)
        {
            Label l = new Label();
            l.Location = new System.Drawing.Point(8, 44);
            l.Name = prefix_lblDataFrameSyze_Down + opd.PagesID.ToString();
            l.Tag = opd;
            l.Size = new System.Drawing.Size(100, 15);
            l.Text = "От нижнего края";
            return l;
        }
        private Label Create_lblDataFrameSyze_Up(OnePageDescriptions opd)
        {
            Label l = new Label();
            l.Location = new System.Drawing.Point(8, 19);
            l.Name = prefix_lblDataFrameSyze_Up + opd.PagesID.ToString();
            l.Tag = opd;
            l.Size = new System.Drawing.Size(100, 15);
            l.Text = "От верхнего края";
            return l;
        }
        private Button Create_btnScaleBar(OnePageDescriptions opd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(143, 146);
            b.Name = prefix_btnScaleBar + opd.PagesID.ToString();
            b.Tag = opd;
            b.Size = new System.Drawing.Size(75, 23);
            b.Text = "Параметры";
            b.UseVisualStyleBackColor = true;
            b.Click += new System.EventHandler(this.btnScaleBar_Click);
            b.Enabled = opd.IsHasScaleBar;

            return b;
        }
        private Button Create_btnNordArrow(OnePageDescriptions opd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(143, 118);
            b.Name = prefix_btnNordArrow + opd.PagesID.ToString();
            b.Tag = opd;
            b.Size = new System.Drawing.Size(75, 23);
            b.Text = "Параметры";
            b.UseVisualStyleBackColor = true;
            b.Click += new System.EventHandler(this.btnNordArrow_Click);
            b.Enabled = opd.IsHasNorthArrow;
            return b;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                t.Text = pd.LayersToString();
        }

        private void GetSettingFromForm()
        {
            foreach (OnePageDescriptions opd in m_crd.Pages)
            {
                opd.TextElements = new List<OneTextElementDescription>();
                ListBox l = GetControlByName(prefix_lbTextElement + opd.PagesID.ToString()) as ListBox;
                if (l != null)
                {
                    foreach (object o in l.Items)
                    {
                        OneTextElementDescription oted = (OneTextElementDescription)o;
                        if (oted != null)
                        {
                            opd.TextElements.Add(oted);
                        }
                    }
                }


                CheckBox cna = GetControlByName(prefix_cbNordArrow + opd.PagesID.ToString()) as CheckBox;
                if (cna != null)
                    opd.IsHasNorthArrow = cna.Checked;

                CheckBox csb = GetControlByName(prefix_cbScaleBar + opd.PagesID.ToString()) as CheckBox;
                if (csb != null)
                    opd.IsHasScaleBar = csb.Checked;

                NumericUpDown n_Down = GetControlByName(prefix_nudDataFrameSyze_Down + opd.PagesID.ToString()) as NumericUpDown;
                if (n_Down != null)
                    opd.DataFrameSyze_Down = (double)n_Down.Value;

                NumericUpDown n_Up = GetControlByName(prefix_nudDataFrameSyze_Up + opd.PagesID.ToString()) as NumericUpDown;
                if (n_Up != null)
                    opd.DataFrameSyze_Up = (double)n_Up.Value;

                NumericUpDown n_Left = GetControlByName(prefix_nudDataFrameSyze_Left + opd.PagesID.ToString()) as NumericUpDown;
                if (n_Left != null)
                    opd.DataFrameSyze_Left = (double)n_Left.Value;

                NumericUpDown n_Right = GetControlByName(prefix_nudDataFrameSyze_Right + opd.PagesID.ToString()) as NumericUpDown;
                if (n_Right != null)
                    opd.DataFrameSyze_Right = (double)n_Right.Value;
            }
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

        private void lbTextElement_DoubleClick(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            ListBox l = GetControlByName(prefix_lbTextElement + opd.PagesID.ToString()) as ListBox;
            if (l != null)
            {
                OneTextElementDescription oted = l.SelectedItem as OneTextElementDescription;
                if(oted != null)
                {
                    frmTextSetting frm = new frmTextSetting(oted);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        OneTextElementDescription oted1 = new OneTextElementDescription();
                        oted1.CopySetingFrom(frm.m_oted);
                        //l.SelectedItem= oted1;
                        int i = l.SelectedIndex;
                        l.Items.Remove(l.SelectedItem);
                        l.Items.Insert(i, oted1);
                        //((OneTextElementDescription)l.SelectedItem).CopySetingFrom(frm.m_oted);
                    }
                }
            }
        }

        private void btnAddTextElement_Click(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            ListBox l = GetControlByName(prefix_lbTextElement + opd.PagesID.ToString()) as ListBox;
            if (l != null)
            {
                OneTextElementDescription oted = new OneTextElementDescription();
                frmTextSetting frm = new frmTextSetting(oted);
                if (frm.ShowDialog() == DialogResult.OK)
                    l.Items.Add(frm.m_oted);
            }
        }

        private void btnDeleteTextElement_Click(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            ListBox l = GetControlByName(prefix_lbTextElement + opd.PagesID.ToString()) as ListBox;
            if (l != null)
            {
                if (l.SelectedItem != null)
                    l.Items.Remove(l.SelectedItem);
            }
        }

        private void btnScaleBar_Click(object sender, EventArgs e)
        {

        }

        private void btnNordArrow_Click(object sender, EventArgs e)
        {

        }

        private void cbNordArrow_CheckedChanged(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            Button b = GetControlByName(prefix_btnNordArrow + opd.PagesID.ToString()) as Button;
            CheckBox c = GetControlByName(prefix_cbNordArrow + opd.PagesID.ToString()) as CheckBox;
            if (b != null && c != null)
            {
                b.Enabled = c.Checked;
            }
        }

        private void cbScaleBar_CheckedChanged(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            Button b = GetControlByName(prefix_btnScaleBar + opd.PagesID.ToString()) as Button;
            CheckBox c = GetControlByName(prefix_cbScaleBar + opd.PagesID.ToString()) as CheckBox;
            if (b != null && c != null)
            {
                b.Enabled = c.Checked;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region 
        private void btnSave_Click(object sender, EventArgs e)
        {
            GetSettingFromForm();

            WorkCadastralReference.GetCadastralReferenceData().CopySetingFrom(m_crd);
            WorkCadastralReference.SaveSettingToDB();

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
            TabPage p = GetControlByName(prefix_tpPages + pd.PagesID.ToString()) as TabPage;

            if (p != null)
            {
                if (pd.Enable)
                    p.Parent = this.tcPages;// -- Показать
                else
                    p.Parent = null;// -- Скрыть
            }
        }

        private void btnAddPage_Click(object sender, EventArgs e)
        {
            InputBox inputBox = new InputBox("Название нового листа");
            if (inputBox.ShowDialog() == DialogResult.OK && inputBox.Value != "")
            {
                OnePageDescriptions opd = new OnePageDescriptions(inputBox.Value, true);
                //проверить уникальность имени
                if (m_crd.Pages.Contains(opd))
                {
                    MessageBox.Show("Название нового листа не уникально.");
                }
                else
                {
                    m_crd.Pages.Add(opd);
                    CreateUiOnePage(opd);
                }
            }
        }
        #endregion
    }
}
