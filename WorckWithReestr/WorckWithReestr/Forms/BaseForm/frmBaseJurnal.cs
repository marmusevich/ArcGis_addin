//#define CONSTRUCT_FORM

using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmBaseJurnal : Form
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected bool IsSelectMode;
        protected string NameWorkspace = "";
        protected string NameTable = "";
        protected string NameSortFild = "";
        protected string NameDataFilteredFild = "";
        protected ITable table;
        protected TableWraper tableWrapper = null;

        public int SelectID { get; protected set; }
        public string FilteredString { protected get; set; }
        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - call back
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //вернуть форму документа
        protected virtual frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            return null;
        }
        //установка порядка колонок по умолчанию      
        protected virtual void SetDefaultDisplayOrder()
        {

        }
        //доп настройка грида
        protected virtual void OtherSetupDGV()
        {

        }

        //вернуть строку доаолнительных условий
        protected virtual string GetStringAddetConditions()
        {
            return "";
        }


        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - base
        //---------------------------------------------------------------------------------------------------------------------------------------------

        protected virtual void AddRec()
        {
            frmBaseDocument frm = GetDocumentForm(-1, frmBaseDocument.EditMode.ADD);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            tableWrapper.UpdateData();
            dgv.Refresh();
        }

        protected virtual void EditRec(int objectID)
        {
            frmBaseDocument frm = GetDocumentForm(objectID, frmBaseDocument.EditMode.EDIT);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            tableWrapper.UpdateData();
            dgv.Refresh();
        }

        protected virtual void DeleteRec(int objectID)
        {
            frmBaseDocument frm = GetDocumentForm(objectID, frmBaseDocument.EditMode.DELETE);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            tableWrapper.UpdateData();
            dgv.Refresh();
        }

        protected virtual bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);

                this.Text = (table as IObjectClass).AliasName;

                tableWrapper = new TableWraper(table, NameSortFild, BuildConditions());
                tableWrapper.AllowNew = false;
                tableWrapper.AllowRemove = false;

                BindingSource dsBinding = new BindingSource();
                dsBinding.DataSource = tableWrapper;

                dgv.AutoGenerateColumns = false;

                SharedClass.CreateColumIn(dgv, table);
                OtherSetupDGV();
                dgv.DataSource = dsBinding;
                SetupDGV();
                dgv.Refresh();
                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Чтиение журнала документов  '{0}'", NameTable));
                SharedClass.ShowErrorMessage(string.Format("Проблема при чтиение журнала документов  '{0}'", NameTable));
                ret = false;
            }
            return ret;
        }

        private IQueryFilter BuildConditions()
        {
            IQueryFilter ret = null;
            string dopUsl = GetStringAddetConditions();
            string usl = "";


            if (NameDataFilteredFild != "")
            {
                usl = string.Format(" ({0} >= '{1:yyy.MM.dd}' and {0} <= '{2:yyy.MM.dd}  23:59:59.9999999') ", NameDataFilteredFild, dtpDataOt.Value, dtpDatePo.Value);

                if (dopUsl != "")
                    usl += " and " + dopUsl;
            }
            else
            {
                if (dopUsl != "")
                    usl = dopUsl;
            }

            if (usl != "")
            {
                ret = new QueryFilter();// Class();
                ret.WhereClause = usl;
            }


            return ret;
        }


        private void SetupDGV()
        {
            int width = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                width += dgv.Columns[i].Width;
            }
            if (Screen.PrimaryScreen.WorkingArea.Width < width)
            {
                this.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;

                //   this.WindowState = FormWindowState.Maximized;
            }
            else
                this.Width = width;

            this.StartPosition = FormStartPosition.CenterScreen;


            if (!SharedClass.SetDisplayOrder(dgv, NameTable))
                SetDefaultDisplayOrder();

            if (dgv.Columns.Count > 0)
                dgv.Columns[0].Visible = false;

        }

        protected virtual void SelectRec(int objectID)
        {
            SelectID = objectID;
            Close();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region form  events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmBaseJurnal() : this(false, "") { }

        public frmBaseJurnal(bool isSelectMode = false, string filteredString = "")
        {
            InitializeComponent();
            FilteredString = filteredString;
            IsSelectMode = isSelectMode;

        }


        private void frmBaseJurnal_Load(object sender, System.EventArgs e)
        {
            dtpDataOt.Value = SharedClass.GetFirstMonthDayDate( DateTime.Now );
            dtpDatePo.Value = SharedClass.GetLastMonthDayDate( DateTime.Now );
#if (!CONSTRUCT_FORM)
            if (!this.ReadData()) // -ok
            {
                this.Close();
            }
#endif
        }

        private void frmBaseJurnal_FormClosing(object sender, FormClosingEventArgs e)
        {
            SharedClass.GetDisplayOrder(dgv, NameTable);
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            AddRec();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                DeleteRec(id);
            }
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                EditRec(id);
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            AddRec();
        }

        private void tsdEdit_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                EditRec(id);
            }
        }

        private void tsdDelete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                DeleteRec(id);
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = (int)dgv.Rows[e.RowIndex].Cells[0].Value;
                if (IsSelectMode)
                    SelectRec(id);
                else
                    EditRec(id);
            }
        }

        private void dtpDataOt_ValueChanged(object sender, EventArgs e)
        {
            if (tableWrapper != null)
            {
                tableWrapper.QueryFilter = BuildConditions();
                tableWrapper.UpdateData();
                dgv.Refresh();
            }
        }

        private void dtpDatePo_ValueChanged(object sender, EventArgs e)
        {
            if (tableWrapper != null)
            {
                tableWrapper.QueryFilter = BuildConditions();
                tableWrapper.UpdateData();
                dgv.Refresh();
            }
        }
        #endregion



    }
}
