using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;

namespace WorckWithReestr
{
    public partial class frmBaseSpr_list : Form
    {
        // режим выбора из списка
        // перечитывать данные и установка текущих строки/колонки когда и как?

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

        public int SelectID { get; protected set; }
        public string FilteredString { protected get; set; }
        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected virtual frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return null;
        }

        protected virtual void AddRec()
        {
            frmBaseSpr_element frm = GetElementForm(-1, frmBaseSpr_element.EditMode.ADD);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }

        protected virtual void EditRec(int objectID)
        {
            frmBaseSpr_element frm = GetElementForm(objectID, frmBaseSpr_element.EditMode.EDIT);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }

        protected virtual void DeleteRec(int objectID)
        {
            frmBaseSpr_element frm = GetElementForm(objectID, frmBaseSpr_element.EditMode.DELETE);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();

        }

        protected virtual bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                ITable table = fws.OpenTable(NameTable);

                this.Text = (table as IObjectClass).AliasName;


                TableWraper tableWrapper = new TableWraper(table, NameSortFild);
                //TableWraper tableWrapper = new TableWraper(table);
                //(tableWrapper as System.ComponentModel.IBindingList).ApplySort(tableWrapper.GetPropertyDescriptorByName(NameSortFild), System.ComponentModel.ListSortDirection.Ascending);

                //tableWrapper.AllowNew = false;
                //tableWrapper.AllowRemove = false;
                //tableWrapper.UseCVDomains = true;



                BindingSource dsBinding = new BindingSource();
                dsBinding.DataSource = tableWrapper;

                dgv.AutoGenerateColumns = false;
                SharedClass.CreateColumIn(dgv, table);
                dgv.DataSource = dsBinding;
                dgv.Refresh();

                int width = 0;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    width += dgv.Columns[i].Width;
                }
                if (Screen.PrimaryScreen.WorkingArea.Width < width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width/2;
                else
                    this.Width = width;

                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }

            //if (ret)
            //    dgv.Columns[0].Visible = false;


            return ret;
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
        public frmBaseSpr_list() : this(false, "") { }

        public frmBaseSpr_list(bool isSelectMode = false, string filteredString = "")
        {
            InitializeComponent();
            FilteredString = filteredString;
            IsSelectMode = isSelectMode;
        }

        private void frmBaseSpr_list_Load(object sender, EventArgs e)
        {
            if (this.ReadData()) // -ok
            {

            }
            else // error
            {
                SharedClass.ShowErrorMessage();
                this.Close();
            }
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
        #endregion
    }
}





