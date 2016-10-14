using System;
using System.Windows.Forms;
using CadastralReference;

namespace WorckWithReestr
{
    /// <summary>
    /// 
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

        ToolTip toolTipZayavkaDiscriptionsk;
        ToolTip toolTipObjectMapIDDiscriptions;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // иницилизация компонента
        private void InitControls()
        {
            Generate_AndAddToTableLayoutPanel();
            WorkCadastralReference.GetCadastralReferenceData().Image_Change += new EventHandler<EventArgs>(OnImage_Change);
            WorkCadastralReference.GetCadastralReferenceData().ZayavkaID_Change += new EventHandler<EventArgs>(ZayavkaID_Change);
            WorkCadastralReference.GetCadastralReferenceData().ObjektInMapID_Change += new EventHandler<EventArgs>(ObjektInMapID_Change);
            WorkCadastralReference.GetCadastralReferenceData().IsReferenceClose_Change += new EventHandler<EventArgs>(IsReferenceClose_Change);

            WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf_Change += new EventHandler<EventArgs>(AllDocumentPdf_Change);
            WorkCadastralReference.GetCadastralReferenceData().BodyText_Change += new EventHandler<EventArgs>(BodyText_Change);

            EnablePanels();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region создать элементы управления

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // генерировать автоматом
        private void Generate_AndAddToTableLayoutPanel()
        {

            tlpPages.SuspendLayout();
            SuspendLayout();

            tlpPages.Controls.Clear();
            tlpPages.RowCount = 1;
            //tlpPages.Visible = false;

            int colum = 0;

            //графические листы
            foreach (OnePageDescriptions pd in WorkCadastralReference.GetCadastralReferenceData().Pages)
            {
                if (!pd.Enable && pd.Image==null)
                    continue;

                tlpPages.RowCount++;
                tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
                tlpPages.Controls.Add(Create_rbSelect(pd), 0, colum * 2);
                tlpPages.RowCount++;
                tlpPages.RowStyles.Add(new System.Windows.Forms.RowStyle());
                tlpPages.Controls.Add(Create_pnlPageMaket(pd), 0, colum * 2 + 1);

                //первичная иницилизация
                //xbSelect_OnChecked(pd);
                SetTextToxbSelect(pd);

                colum++;
            }

            //
            tlpPages.Visible = false;
            tlpPages.ResumeLayout(false);
            ResumeLayout(false);
            tlpPages.Visible = true; 
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // элементы выбора листов
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // элементы выбора листов
        private CheckBox Create_cbSelect(OnePageDescriptions pd)
        {
            CheckBox cb = new CheckBox();
            cb.AutoSize = true;
            cb.Dock = System.Windows.Forms.DockStyle.Fill;
            cb.Location = new System.Drawing.Point(5, 5);
            cb.Name = prefix_xbSelect + pd.PagesID.ToString();
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
            rb.Name = prefix_xbSelect + pd.PagesID.ToString();
            rb.Size = new System.Drawing.Size(396, 17);
            rb.Text = pd.Caption;
            rb.UseVisualStyleBackColor = true;
            rb.Tag = pd;
            rb.CheckedChanged += new System.EventHandler(this.xbSelect_CheckedChanged);
            rb.Click += new System.EventHandler(this.rbSelect_Click);

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
            p.Name = prefix_pnlPage + pd.PagesID.ToString();
            p.Size = new System.Drawing.Size(215, 89);
            p.Tag = pd;
            p.Visible = false;
            p.ResumeLayout(false);
            p.PerformLayout();
            return p;
        }

        private PictureBox Create_pbPreviewMaket(OnePageDescriptions pd)
        {
            PictureBox pb = new PictureBox();
            pb.Location = new System.Drawing.Point(4, 4);
            pb.BackColor = System.Drawing.Color.White;
            pb.Name = prefix_pbPrev + pd.PagesID.ToString(); ;
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
            b.Name = prefix_btnGenerate + pd.PagesID.ToString();
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
            b.Name = prefix_btnSaveToDB + pd.PagesID.ToString();
            b.Size = new System.Drawing.Size(121, 23);
            b.Text = "Сохранить в базу";
            b.UseVisualStyleBackColor = true;
            b.Tag = pd;
            b.Click += new System.EventHandler(this.btnSaveToDBMaket_Click);
            return b;
        }

        private Button Create_btnPreviewMaket(OnePageDescriptions pd)
        {
            Button b = new Button();
            b.Location = new System.Drawing.Point(91, 56);
            b.Name = prefix_btnPrev + pd.PagesID.ToString();
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
        private void xbSelect_OnChecked(OnePageDescriptions opd)
        {
            Panel p = GetControlByName(prefix_pnlPage + opd.PagesID.ToString()) as Panel;
            Control c = GetControlByName(prefix_xbSelect + opd.PagesID.ToString());
            RadioButton rb = c as RadioButton;
            CheckBox сb = c as CheckBox;
            if (rb != null)
            {
                if (p != null)
                    p.Visible = rb.Checked;
                //if(rb.Checked)
                //    WorkCadastralReference.EnableLayersFropPageAndSetScale(opd);
            }
            if (сb != null)
            {
                if (p != null)
                    p.Visible = сb.Checked;
            }

            SetTextToxbSelect(opd);
        }

        // изменение текста выбора листов в зависимости от наличия макета
        private void SetTextToxbSelect(OnePageDescriptions opd)
        {
            Control c = GetControlByName(prefix_xbSelect + opd.PagesID.ToString());

            if (opd.Image == null)
            {
                c.Text = "(нет) " + opd.Caption;
                c.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                c.Text = opd.Caption;
                c.ForeColor = System.Drawing.Color.Green;
            }
        }

        //включение/выключение панелей от наличия заявки
        private void EnablePanels()
        {
            bool f = (WorkCadastralReference.GetCadastralReferenceData().ZayavkaID != -1) && (WorkCadastralReference.GetCadastralReferenceData().MapObjectID != -1);
            pnRtf.Enabled = f;
            tlpPages.Enabled = f;
            EnableControlsForCloseReference();
        }

        //включение/выключение элементов управления при закрытии открытии справки
        private void EnableControlsForCloseReference()
        {
            bool f = ! WorkCadastralReference.GetCadastralReferenceData().IsReferenceClose;

            btnSetObject.Enabled = f && (WorkCadastralReference.GetCadastralReferenceData().ZayavkaID != -1);
            btnCloseEdit.Enabled = f && (WorkCadastralReference.GetCadastralReferenceData().ZayavkaID != -1);

            btnPDFGenerate.Enabled = f;
            btnSavePDFToDB.Enabled = f;

            Control c;
            foreach (OnePageDescriptions pd in WorkCadastralReference.GetCadastralReferenceData().Pages)
            {
                c = GetControlByName(prefix_btnGenerate + pd.PagesID.ToString());
                if(c!= null)
                    c.Enabled = f;
                c = GetControlByName(prefix_btnSaveToDB + pd.PagesID.ToString());
                if (c != null)
                    c.Enabled = f;
            }

            OnePageDescriptions opd = new OnePageDescriptions("Описательная часть", true);
            c = GetControlByName(prefix_btnGenerate + opd.PagesID.ToString());
            if (c != null)
                c.Enabled = f;
            c = GetControlByName(prefix_btnSaveToDB + opd.PagesID.ToString());
            if (c != null)
                c.Enabled = f;
        }

        private void SetTextToPDFLabel()
        {

            bool isPDF = WorkCadastralReference.GetCadastralReferenceData().AllDocumentPdf != null;
            bool isBodyText = WorkCadastralReference.GetCadastralReferenceData().BodyText != "";

            string str = "";

            if (isPDF)
                str += "Есть PDF. ";
            else
                str += "Нет PDF. ";

            if (isBodyText)
                str += "Есть Динамическая часть.";
            else
                str += "Нет Динамическая части.";

            lblPDF.Text = str;
        }
        #endregion вспомогательные функции


        #region События элементов управления

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void xbSelect_CheckedChanged(object sender, EventArgs e)
        {
            xbSelect_OnChecked(GetPageDescriptionsFromControlTag(sender as Control));
        }
        private void rbSelect_Click(object sender, EventArgs e)
        {
            //OnePageDescriptions opd = GetPageDescriptionsFromControlTag(sender as Control);
            //RadioButton rb = GetControlByName(prefix_xbSelect + opd.PagesID.ToString()) as RadioButton;
            //if (rb != null)
            //{
            //    //if (rb.Checked)
            //    //    WorkCadastralReference.EnableLayersFropPageAndSetScale(opd);
            //}
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
            OnePageDescriptions opd = GetPageDescriptionsFromControlTag(sender as Control);
            //WorkCadastralReference.EnableLayersFropPageAndSetScale(opd);
            WorkCadastralReference.GenerateMaket(opd);
        }

        private void btnSaveToDBMaket_Click(object sender, EventArgs e)
        {
            OnePageDescriptions opd = this.GetPageDescriptionsFromControlTag(sender as Control);
            WorkCadastralReference.SaveToDBImage(opd);
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // PDF
        private void btnPDFGenerate_Click(object sender, EventArgs e)
        {
            WorkCadastralReference_text.GeneratePDF();
        }

        private void btnPDFSaveToDB_Click(object sender, EventArgs e)
        {
            WorkCadastralReference.SaveToDBPDF();
        }

        private void btnPDFPrev_Click(object sender, EventArgs e)
        {
            WorkCadastralReference_text.ShowPDF();
        }

        private void btnEditText_Click(object sender, EventArgs e)
        {
            frmEditHTML frm = new frmEditHTML();
            frm.tbHTML.Text = WorkCadastralReference.GetCadastralReferenceData().BodyText;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                WorkCadastralReference.GetCadastralReferenceData().BodyText = frm.tbHTML.Text;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSetting_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //пересоздать
                Generate_AndAddToTableLayoutPanel();
            }
        }

        private void ZayavkaChange_Click(object sender, EventArgs e)
        {
                WorkCadastralReference.SelectZayavka();
        }


        private void btnCloseEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрыть справку для редактирования? добавить проверки", "Кадастровая справка", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                WorkCadastralReference.GetCadastralReferenceData().IsReferenceClose = true;
                WorkCadastralReference_DB.EditZayavkaData(WorkCadastralReference.GetCadastralReferenceData().ZayavkaID, WorkCadastralReference.GetCadastralReferenceData().MapObjectID, null, true);
            }
        }

        private void btnSetObject_Click(object sender, EventArgs e)
        {
            if (WorkCadastralReference.CheckReferenceToExistPages(WorkCadastralReference.GetCadastralReferenceData().ZayavkaID))
                WorkCadastralReference_MAP.EnableSelectToo();
        }

        private void llblClearData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Сбросить редактор?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                WorkCadastralReference.SetZayavka(-1);
            }
        }

        #endregion События элементов управления

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region нашы события
        // изменение кода заявления
        private void ZayavkaID_Change(object sender, EventArgs e)
        {
            lblZayavkaDiscriptions.Text = WorkCadastralReference.GetZayavkaDiscription(WorkCadastralReference.GetCadastralReferenceData().ZayavkaID);
            toolTipZayavkaDiscriptionsk.SetToolTip(lblZayavkaDiscriptions, lblZayavkaDiscriptions.Text);

            btnSetObject.Enabled = WorkCadastralReference.GetCadastralReferenceData().ZayavkaID != -1;
            lblObjectMapIDDiscriptions.Enabled = WorkCadastralReference.GetCadastralReferenceData().ZayavkaID != -1;
        }

        // изменение кода выделенного объекта
        private void ObjektInMapID_Change(object sender, EventArgs e)
        {
            EnablePanels();
            lblObjectMapIDDiscriptions.Text = WorkCadastralReference.GetObjektInMapDiscription();
            toolTipObjectMapIDDiscriptions.SetToolTip(lblObjectMapIDDiscriptions, lblObjectMapIDDiscriptions.Text);
        }

        //изменение изабражения
        private void OnImage_Change(object sender, EventArgs e)
        {
            OnePageDescriptions opd = (OnePageDescriptions )sender;
            PictureBox controlByName = this.GetControlByName(this.prefix_pbPrev + opd.PagesID.ToString()) as PictureBox;
            if (controlByName != null)
                controlByName.Image = opd.Image;

            SetTextToxbSelect(opd);

        }

        //при закрытии справки
        private void IsReferenceClose_Change(object sender, EventArgs e)
        {
            EnableControlsForCloseReference();
        }


        /// <summary>///смена итоговый документ/// </summary>
        private void AllDocumentPdf_Change(object sender, EventArgs e)
        {
            SetTextToPDFLabel();
        }
        /// <summary>///смена основная часть/// </summary>
        private void BodyText_Change(object sender, EventArgs e)
        {
            SetTextToPDFLabel();
        }
        #endregion нашы события


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region штатные функции
        public arcDW_CadastralReference(object hook)
        {
            AppStartPoint.Init();
            InitializeComponent();
            this.Hook = hook;
            InitControls();

            toolTipZayavkaDiscriptionsk = new ToolTip();
            toolTipZayavkaDiscriptionsk.SetToolTip(lblZayavkaDiscriptions, "");
            toolTipObjectMapIDDiscriptions = new ToolTip();
            toolTipObjectMapIDDiscriptions.SetToolTip(lblObjectMapIDDiscriptions, "");
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
        #endregion

    }
}

