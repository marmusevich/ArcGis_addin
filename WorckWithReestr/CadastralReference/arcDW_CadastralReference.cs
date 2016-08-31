using System;
using System.Windows.Forms;
using CadastralReference;

namespace WorckWithReestr
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class arcDW_CadastralReference : UserControl
    {
	
	
        //префиксы к элементам управления
        private string prefix_xbSelect = "xbSelect_";
        private string prefix_pnlPage = "pnlPageMaket_";
        private string prefix_pbPrev = "pbPreviewMaket_";
        private string prefix_btnGenerate = "btnGenerateMaket_";
        private string prefix_btnSaveToDB = "btnSaveToDBMaket_";
        private string prefix_btnPrev = "btnPreviewMaket_";


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // иницилизация компонента
        private void InitControls()
        {
            Generate_AndAddToTableLayoutPanel();
            WorkCadastralReference.GetCadastralReferenceData().CadastralReferenceID_Change += new EventHandler<EventArgs>(CadastralReferenceID_Change);
            WorkCadastralReference.GetCadastralReferenceData().Image_Change += new EventHandler<EventArgs>(OnImage_Change);

            EnablePanels();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region создать элементы управления
        // добовляет всплывающую подсказку
        //ToolTip toolTipOk = new ToolTip();
        //toolTipOk.SetToolTip(cbSelect, "cbSelect");


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // генерировать автоматом
        private void Generate_AndAddToTableLayoutPanel()
        {
            //
            this.tlpPages.SuspendLayout();
            this.SuspendLayout();

            //графические листы
            for (int i = 0; i < WorkCadastralReference.GetCadastralReferenceData().Pages.Length; i++)
            {
                tlpPages.RowCount++;
                tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
                //tlpPages.Controls.Add(Create_cbSelect(m_CadastralReferenceData[i]), 0, i * 2);
                tlpPages.Controls.Add(Create_rbSelect(WorkCadastralReference.GetCadastralReferenceData().Pages[i]), 0, i * 2);
                tlpPages.RowCount++;
                tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
                tlpPages.Controls.Add(Create_pnlPageMaket(WorkCadastralReference.GetCadastralReferenceData().Pages[i]), 0, i * 2 + 1);

                //первичная иницилизация
                xbSelect_OnChecked(WorkCadastralReference.GetCadastralReferenceData().Pages[i]);
            }

            //текстовый лист
            OnePageDescriptions pd = new OnePageDescriptions();
            pd.Caption = "Описательная часть";
            pd.NameFromDB = "WorkText";
            tlpPages.RowCount++;
            tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpPages.Controls.Add(Create_cbSelect(pd), 0, WorkCadastralReference.GetCadastralReferenceData().Pages.Length * 2);
            tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpPages.Controls.Add(Create_pnlWorckText(prefix_pnlPage + pd.NameFromDB), 0, WorkCadastralReference.GetCadastralReferenceData().Pages.Length * 2 + 1);
            //
            xbSelect_OnChecked(pd);

            //
            this.tlpPages.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // лист текстового описания справки
        private Panel Create_pnlWorckText(string panelName)
        {
            Panel pnlTexts = new Panel();
            return pnlTexts;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // элементы выбора листов
        private CheckBox Create_cbSelect(OnePageDescriptions pd)
        {
            CheckBox cb = new CheckBox();
            cb.AutoSize = true;
            cb.Dock = System.Windows.Forms.DockStyle.Fill;
            cb.Location = new System.Drawing.Point(5, 5);
            cb.Name = prefix_xbSelect + pd.NameFromDB;
            cb.Size = new System.Drawing.Size(396, 17);
            cb.Text = pd.Caption;
            cb.UseVisualStyleBackColor = true;
            cb.Tag = pd;
            cb.CheckedChanged += new System.EventHandler(this.xbSelect_CheckedChanged);
            return cb;
        }

        private RadioButton Create_rbSelect(OnePageDescriptions pd)
        {
            RadioButton rb = new RadioButton();
            rb.AutoSize = true;
            rb.Dock = System.Windows.Forms.DockStyle.Fill;
            rb.Location = new System.Drawing.Point(5, 5);
            rb.Name = prefix_xbSelect + pd.NameFromDB;
            rb.Size = new System.Drawing.Size(396, 17);
            rb.Text = pd.Caption;
            rb.UseVisualStyleBackColor = true;
            rb.Tag = pd;
            rb.CheckedChanged += new System.EventHandler(this.xbSelect_CheckedChanged);
            return rb;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // для графических листов 
        private Panel Create_pnlPageMaket(OnePageDescriptions pd)
        {
            Panel p = new Panel();
            p.SuspendLayout();
            p.Controls.Add(Create_btnSaveToDBMaket(pd));
            p.Controls.Add(Create_pbPreviewMaket(pd));
            p.Controls.Add(Create_btnPreviewMaket(pd));
            p.Controls.Add(Create_btnGenerateMaket(pd));
            p.Location = new System.Drawing.Point(5, 30);
            p.Name = prefix_pnlPage + pd.NameFromDB;
            p.Size = new System.Drawing.Size(215, 89);
            p.Tag = pd;
            p.ResumeLayout(false);
            p.PerformLayout();
            return p;
        }

        private PictureBox Create_pbPreviewMaket(OnePageDescriptions pd)
        {
            PictureBox pb = new PictureBox();
            pb.Location = new System.Drawing.Point(4, 4);
            pb.BackColor = System.Drawing.Color.White;
            pb.Name = prefix_pbPrev + pd.NameFromDB; ;
            pb.Size = new System.Drawing.Size(61, 60);
            pb.TabStop = false;
            pb.Tag = pd;
            pb.Click += new System.EventHandler(this.PreviewMaket_Click);
            pb.Image = pd.Image;
            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            return pb;
        }

        private Button Create_btnGenerateMaket(OnePageDescriptions pd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(91, 4);
            b.Name = prefix_btnGenerate + pd.NameFromDB;
            b.Size = new System.Drawing.Size(121, 23);
            b.Text = "Генерировать";
            b.UseVisualStyleBackColor = true;
            b.Tag = pd;
            b.Click += new System.EventHandler(this.btnGenerateMaket_Click);
            return b;
        }

        private Button Create_btnSaveToDBMaket(OnePageDescriptions pd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(91, 30);
            b.Name = prefix_btnSaveToDB + pd.NameFromDB;
            b.Size = new System.Drawing.Size(121, 23);
            b.Text = "Сохрвеить в базу";
            b.UseVisualStyleBackColor = true;
            b.Tag = pd;
            b.Click += new System.EventHandler(this.btnSaveToDBMaket_Click);
            return b;
        }

        private Button Create_btnPreviewMaket(OnePageDescriptions pd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(91, 56);
            b.Name = prefix_btnPrev + pd.NameFromDB;
            b.Size = new System.Drawing.Size(121, 23);
            b.Text = "Посмотреть";
            b.UseVisualStyleBackColor = true;
            b.Tag = pd;
            b.Click += new System.EventHandler(this.PreviewMaket_Click);
            return b;
        }
        #endregion //создать элементы управления


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region вспомогательные функции
        // получить элемент упровления по имени
        private Control GetControlByName(string name)
        {
            Control[] ca = this.tlpPages.Controls.Find(name, true);
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

        //действия при выборе листа
        private void xbSelect_OnChecked(OnePageDescriptions opg)
        {
            Panel p = GetControlByName(prefix_pnlPage + opg.NameFromDB) as Panel;
            Control c = GetControlByName(prefix_xbSelect + opg.NameFromDB);

            RadioButton rb = c as RadioButton;
            CheckBox сb = c as CheckBox;

            if (rb != null)
            {
                if (p != null)
                    p.Visible = rb.Checked;
                WorkCadastralReference.EnableLawrsFropPage(opg, rb.Checked);
            }
            if (сb != null)
            {
                if (p != null)
                    p.Visible = сb.Checked;
                WorkCadastralReference.EnableLawrsFropPage(opg, сb.Checked);
            }
        }

        //включение/выключение панелей от наличия заявки
        private void EnablePanels()
        {
            bool f = (WorkCadastralReference.GetCadastralReferenceData().ZayavkaID != -1);
            pnRtf.Enabled = f;
            tlpPages.Enabled = f;
        }
        #endregion вспомогательные функции


        #region События элементов управления
  
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void xbSelect_CheckedChanged(object sender, EventArgs e)
        {
            xbSelect_OnChecked(GetPageDescriptionsFromControlTag(sender as Control));
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Maket
        private void PreviewMaket_Click(object sender, EventArgs e)
        {
            frmPrevImage frm = new frmPrevImage();
            frm.page = GetPageDescriptionsFromControlTag(sender as Control);
            frm.ShowDialog();
        }

        private void btnGenerateMaket_Click(object sender, EventArgs e)
        {
            WorkCadastralReference.GenerateMaket(GetPageDescriptionsFromControlTag(sender as Control));
        }

        private void btnSaveToDBMaket_Click(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            WorkCadastralReference.SaveToDBImage(opd);
            //PictureBox controlByName = this.GetControlByName(this.prefix_pbPrev + opd.NameFromDB) as PictureBox;
            //if (controlByName != null)
            //    controlByName.Image = opd.Image;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // RTF
        private void btnRtfGenerate_Click(object sender, EventArgs e)
        {
            WorckWithRTF.GenerateRTF();
        }

        private void btnRTFSaveToDB_Click(object sender, EventArgs e)
        {
            WorkCadastralReference.SaveToDBRTF();
        }

        private void btnRTFPrev_Click(object sender, EventArgs e)
        {
            frmRTF frm = new frmRTF();
            frm.ShowDialog();
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();
            frm.ShowDialog();
        }

        private void ZayavkaChange_Click(object sender, EventArgs e)
        {
            WorkCadastralReference.SelectZayavka();
        }
        #endregion События элементов управления


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region нашы события
        // изменение кода кадастровой справки
        private void CadastralReferenceID_Change(object sender, EventArgs e)
        {
            this.EnablePanels();
        }
        //изменение изабражения
        private void OnImage_Change(object sender, EventArgs e)
        {
            OnePageDescriptions opd = (OnePageDescriptions )sender;
            PictureBox controlByName = this.GetControlByName(this.prefix_pbPrev + opd.NameFromDB) as PictureBox;
            if (controlByName != null)
                controlByName.Image = opd.Image;
        }
        #endregion нашы события
	
	
	
        public arcDW_CadastralReference(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
			
            InitControls();
			
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private arcDW_CadastralReference m_windowUI;

            public AddinImpl()
            {
            }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new arcDW_CadastralReference(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }

        }
    }
}
