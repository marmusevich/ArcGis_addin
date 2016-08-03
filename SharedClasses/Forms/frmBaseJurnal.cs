
using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace SharedClasses
{
    public partial class frmBaseJurnal : Form, IListFormFilterMetods
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        // это режим выбора
        protected bool IsSelectMode;
        //имя пространства данных
        protected string NameWorkspace = "";
        //имя таблицы
        protected string NameTable = "";
        //поле для сортировки
        protected string NameSortFild = "";
        //поле типа дата для отбора в периоде
        protected string NameDataFilteredFild = "";
        //строка фильтрации
        public string FilteredString { protected get; set; }
        //собственно таблица
        protected ITable table;
        //обертка над таблицей
        protected TableWraper tableWrapper = null;
        //результат выбраного значения
        public int SelectID { get; protected set; }
        //
        protected bool enable_dtpDataEvent = true;
        // признак того что не надо считывать данные, конструируется форма
        private bool IsNotReadData_FormIsConstruct = false;

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
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public virtual bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            dictionaryTableName = "";
            return false;
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - base
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //добавить запись
        protected virtual void AddRec()
        {
            frmBaseDocument frm = GetDocumentForm(-1, frmBaseDocument.EditMode.ADD);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();
            frm.Dispose();

            tableWrapper.UpdateData();
            dgv.Refresh();
        }
        //редактировать запись
        protected virtual void EditRec(int objectID)
        {
            frmBaseDocument frm = GetDocumentForm(objectID, frmBaseDocument.EditMode.EDIT);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();
            frm.Dispose();

            tableWrapper.UpdateData();
            dgv.Refresh();
        }
        //удалить запись
        protected virtual void DeleteRec(int objectID)
        {
            frmBaseDocument frm = GetDocumentForm(objectID, frmBaseDocument.EditMode.DELETE);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();
            frm.Dispose();
            tableWrapper.UpdateData();
            dgv.Refresh();
        }
        //прочесть список
        protected virtual bool ReadData()
        {
            bool ret = false;

            // статус бар, иницилизация
            IndicateWaitingOperation.Init("Connect to DataBase...", 0, 3, 1);

            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);

                // статус бар
                IndicateWaitingOperation.Do("Read Data...");

                Text = (table as IObjectClass).AliasName;
                tableWrapper = new TableWraper(table, NameSortFild, BuildConditions());
                tableWrapper.AllowNew = false;
                tableWrapper.AllowRemove = false;

                BindingSource dsBinding = new BindingSource();
                dsBinding.DataSource = tableWrapper;

                dgv.AutoGenerateColumns = false;

                GeneralApp.CreateColumIn(ref dgv, ref table);
                dgv.DataSource = dsBinding;
                SetupDGV();
                OtherSetupDGV();
                dgv.Refresh();
                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Чтиение журнала документов  '{0}'", NameTable));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при чтиение журнала документов  '{0}'", NameTable));
                ret = false;
            }
            finally
            {
                // спрятоть статусную строку
                IndicateWaitingOperation.Finalize();
            }

            return ret;
        }
        //построить фильтр
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
        //настроить грид
        private void SetupDGV()
        {
            int width = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                width += dgv.Columns[i].Width;
            }
            if (Screen.PrimaryScreen.WorkingArea.Width < width)
            {
                Width = Screen.PrimaryScreen.WorkingArea.Width / 2;

                //   this.WindowState = FormWindowState.Maximized;
            }
            else
                Width = width;

            StartPosition = FormStartPosition.CenterScreen;


            if (!GeneralApp.LoadDisplayOrderFromDisk_AndSetToDGV(ref dgv, NameTable))
                SetDefaultDisplayOrder();

            if (dgv.Columns.Count > 0)
                dgv.Columns[0].Visible = false;

        }
        //действия при выборе записи (режим выбор)
        protected virtual void SelectRec(int objectID)
        {
            SelectID = objectID;
            Close();
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region form  events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmBaseJurnal() : this(false, "") 
        {
            IsNotReadData_FormIsConstruct = true;
        }
        public frmBaseJurnal(bool isSelectMode, string filteredString)
        {
            InitializeComponent();
            FilteredString = filteredString;
            IsSelectMode = isSelectMode;

        }
        private void frmBaseJurnal_Load(object sender, System.EventArgs e)
        {
            dtpDataOt.Value = GeneralApp.GetFirstMonthDayDate(DateTime.Now);
            dtpDatePo.Value = GeneralApp.GetLastMonthDayDate(DateTime.Now);
            if (!IsNotReadData_FormIsConstruct)
            {
                if (!ReadData()) // -ok
                {
                    Close();
                }
            }
        }
        private void frmBaseJurnal_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralApp.SaveDisplayOrderToDisk(ref dgv, NameTable);
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
        private void frmBaseJurnal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
                {
                    e.Handled = true;
                    int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                    if (IsSelectMode)
                        SelectRec(id);
                    else
                        EditRec(id);
                }
            }
            else if (e.KeyCode == Keys.F && e.Control)
            {
                //frmSelectSearchInListForm.ShowForView(this, table);
            }
        }
        private void dtpDataOt_ValueChanged(object sender, EventArgs e)
        {
            if (tableWrapper != null && enable_dtpDataEvent)
            {
                tableWrapper.QueryFilter = BuildConditions();
                tableWrapper.UpdateData();
                dgv.Refresh();
            }
        }
        private void dtpDatePo_ValueChanged(object sender, EventArgs e)
        {
            if (tableWrapper != null && enable_dtpDataEvent)
            {
                tableWrapper.QueryFilter = BuildConditions();
                tableWrapper.UpdateData();
                dgv.Refresh();
            }
        }

        private void btnChangePeriod(object sender, EventArgs e)
        {
            Button but = sender as Button;
            if (but != null && but.Tag != null)
            {
                try
                {
                    short add = Convert.ToInt16(but.Tag);
                    enable_dtpDataEvent = false;
                    dtpDataOt.Value = GeneralApp.GetFirstMonthDayDate(new DateTime(dtpDataOt.Value.Year, dtpDataOt.Value.Month + add, 15));
                    dtpDatePo.Value = GeneralApp.GetLastMonthDayDate(new DateTime(dtpDatePo.Value.Year, dtpDatePo.Value.Month + add, 15));
                    tableWrapper.QueryFilter = BuildConditions();
                    tableWrapper.UpdateData();
                    dgv.Refresh();
                }
                catch { }
                finally
                {
                    enable_dtpDataEvent = true;
                }
            }
        }
        #endregion
    }
}
