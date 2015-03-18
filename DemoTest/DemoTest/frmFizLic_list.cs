using System;
using System.Windows.Forms;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace DemoTest
{
    public partial class frmFizLic_list : Form
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

        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        private bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                ITable table_fizLic = fws.OpenTable("reestr.DBO.fizichni_osoby");

                TableWraper tableWrapper = new TableWraper(table_fizLic, "П_І_Б");
                tableWrapper.AllowNew = false;
                tableWrapper.AllowRemove = false;

                BindingSource dsBinding = new BindingSource();
                dsBinding.DataSource = tableWrapper;

                dgv.AutoGenerateColumns = false;

                SharedClass.CreateColumIn(dgv, table_fizLic);

                dgv.DataSource = dsBinding;

                int width = 0;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    width += dgv.Columns[i].Width;
                }
                this.Width = (int)(width * 1.05);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            return ret;
        }

        private void AddRec()
        {

            frmFizLic_element frm = new frmFizLic_element(null, frmFizLic_element.EditMode.ADD);
            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }

        private void EditRec(object objectID)
        {

            frmFizLic_element frm = new frmFizLic_element(objectID, frmFizLic_element.EditMode.EDIT);
            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }

        private void DeleteRec(object objectID)
        {
            frmFizLic_element frm = new frmFizLic_element(objectID, frmFizLic_element.EditMode.DELETE);
            frm.Owner = this;
            frm.ShowDialog();

            //перечитать если лперация была закончена, или всегда???
            ReadData();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region form  events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmFizLic_list()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
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
            object id = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
            DeleteRec(id);
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            object id = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
            EditRec(id);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            AddRec();
        }

        private void tsdEdit_Click(object sender, EventArgs e)
        {
            object id = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
            EditRec(id);
        }

        private void tsdDelete_Click(object sender, EventArgs e)
        {
            object id = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value;
            DeleteRec(id);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                object id = dgv.Rows[e.RowIndex].Cells[0].Value;
                //dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[0];
                EditRec(id);
            }
        }

        #endregion
    }
}





