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
        protected TableWraper tableWrapper = null;

        public int SelectID { get; protected set; }
        public string FilteredString { protected get; set; }
        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected virtual frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            return null;
        }

        protected virtual void AddRec()
        {
            frmBaseDocument frm = GetDocumentForm(-1, frmBaseDocument.EditMode.ADD);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }

        protected virtual void EditRec(int objectID)
        {
            frmBaseDocument frm = GetDocumentForm(objectID, frmBaseDocument.EditMode.EDIT);
            if (frm == null)
                return;

            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }

        protected virtual void DeleteRec(int objectID)
        {
            frmBaseDocument frm = GetDocumentForm(objectID, frmBaseDocument.EditMode.DELETE);
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


                tableWrapper = new TableWraper(table, NameSortFild);
                tableWrapper.AllowNew = false;
                tableWrapper.AllowRemove = false;

                BindingSource dsBinding = new BindingSource();
                dsBinding.DataSource = tableWrapper;

                dgv.AutoGenerateColumns = false;

                SharedClass.CreateColumIn(dgv, table);

                dgv.DataSource = dsBinding;

                int width = 0;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    width += dgv.Columns[i].Width;
                }
                if (Screen.PrimaryScreen.WorkingArea.Width < width)
                {
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width/2;

                 //   this.WindowState = FormWindowState.Maximized;
                }
                else
                    this.Width = width;

                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }

            if (ret)
                dgv.Columns[0].Visible = false;


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
        public frmBaseJurnal() : this(false, "") { }

        public frmBaseJurnal(bool isSelectMode = false, string filteredString = "")
        {
            InitializeComponent();
            FilteredString = filteredString;
            IsSelectMode = isSelectMode;

        }


        private void frmBaseJurnal_Load(object sender, System.EventArgs e)
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
            //if (e.RowIndex >= 0)
            //{
            //    int id = (int)dgv.Rows[e.RowIndex].Cells[0].Value;
            //    if (IsSelectMode)
            //        SelectRec(id);
            //    else
            //        EditRec(id);
            //}
        }
        #endregion

    }
}
