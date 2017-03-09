using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;

//using DgvFilterPopup;


namespace SharedClasses
{
    public partial class frmBaseSpr_list : Form, IListFormFilterMetods
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
        //строка фильтрации
        public string FilteredString { protected get; set; }
        //собственно таблица
        protected ITable table;
        //обертка над таблицей
        protected TableWraper tableWrapper = null;
        //результат выбраного значения
        public int SelectID { get; protected set; }
        // признак того что не надо считывать данные, конструируется форма
        private bool IsNotReadData_FormIsConstruct = false;


        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - call back
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //получить форму элемента справочника
        protected virtual frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return null;
        }
        //устоновить порядок колонок по умолчанию
        protected virtual void SetDefaultDisplayOrder()
        {
            
        }
        //доп настройка грида
        protected virtual void OtherSetupDGV()
        {

        }
        //вернуть строку дополнительных условий
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
            frmBaseSpr_element frm = GetElementForm(-1, frmBaseSpr_element.EditMode.ADD);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();
            frm.Dispose();

            Reflesh();
        }

        //добавить запись копированием
        protected virtual void AddCopyRec(int objectID)
        {
            frmBaseSpr_element frm = GetElementForm(objectID, frmBaseSpr_element.EditMode.ADD_COPY);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();
            frm.Dispose();

            Reflesh();
        }
        //редактировать запись
        protected virtual void EditRec(int objectID)
        {
            frmBaseSpr_element frm = GetElementForm(objectID, frmBaseSpr_element.EditMode.EDIT);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            Reflesh();
        }
        //удалить запись
        protected virtual void DeleteRec(int objectID)
        {
            frmBaseSpr_element frm = GetElementForm(objectID, frmBaseSpr_element.EditMode.DELETE);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();
            frm.Dispose();

            Reflesh();
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


                this.Text = (table as IObjectClass).AliasName;
                tableWrapper = new TableWraper(table, NameSortFild, BuildConditions());

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
                Logger.Write(ex);
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
        protected IQueryFilter BuildConditions()
        {
            IQueryFilter ret = null;
            string dopUsl = GetStringAddetConditions();
            string usl = "";

            if (FilteredString != "")
            {
                usl = FilteredString;

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
            if (width < this.Width)
                width = this.Width;
            else
            {
                if (Screen.PrimaryScreen.WorkingArea.Width < width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
                else
                    this.Width = width;
            }
            this.StartPosition = FormStartPosition.CenterScreen;

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

        protected void Reflesh()
        {
            tableWrapper.UpdateData();
            dgv.Refresh();
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region form  events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmBaseSpr_list() 
        {
            InitializeComponent();
            IsNotReadData_FormIsConstruct = true;
        }
        public frmBaseSpr_list(bool isSelectMode, string filteredString)
        {
            InitializeComponent();
            FilteredString = filteredString;
            IsSelectMode = isSelectMode;

            SelectID = -1;
        }
        private void frmBaseSpr_list_Load(object sender, EventArgs e)
        {
            if (!IsNotReadData_FormIsConstruct)
            {
                if(!this.ReadData()) // -ok
                {
                    this.Close();
                }
            }

            //new DgvFilterManager(dgv);
        }
        private void frmBaseSpr_list_FormClosing(object sender, FormClosingEventArgs e)
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
        private void frmBaseSpr_list_KeyDown(object sender, KeyEventArgs e)
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
		
        private void tsbReflesh_Click(object sender, EventArgs e)
        {
            Reflesh();
        }
        private void cmsReflesh_Click(object sender, EventArgs e)
        {
            Reflesh();
        }
        private void cmsAddCopy_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                AddCopyRec(id);
            }

        }
        private void tsbAddCopy_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex > -1)
            {
                int id = (int)dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
                AddCopyRec(id);
            }

        }

        #endregion
    }
}





