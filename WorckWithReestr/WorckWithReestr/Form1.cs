using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;


namespace WorckWithReestr
{
    public partial class Form1 : Form
    {
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
        public static void ShowForView()
        {
            Form frm = new Form1();
            frm.Show();
            frm.Activate();
        }

        protected  frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            //return new frmReestrZayav_doc(_objectID, _editMode);
            return new frmReestrZayav_doc();
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

        protected virtual void SelectRec(int objectID)
        {
            SelectID = objectID;
            Close();
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

        private void Form1_Load(object sender, System.EventArgs e)
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
#endregion

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public Form1()
        {
            InitializeComponent();

            FilteredString = "";
            IsSelectMode = false;


            //NameWorkspace = "reestr";
            //NameTable = "reestr.DBO.Kn_Reg_Zayv";
            //NameSortFild = "Data_Z";


            NameWorkspace = "reestr";
            NameTable = "reestr.DBO.fizichni_osoby";
            NameSortFild = "П_І_Б";


            VirtualModeDGV();

        }

        public void VirtualModeDGV()
        {

            ////((System.ComponentModel.ISupportInitialize)(dgv)).BeginInit();
            //dgv.VirtualMode = true;
            //dgv.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgv_CellValueNeeded);
            //dgv.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgv_CellValuePushed);

            ////dgv.Dock = DockStyle.Fill;

            //for (int i = 0; i < 'Z' - 'A'; i++)
            //{
            //    string name = ((char)('A' + i)).ToString();
            //    dgv.Columns.Add(name, name);
            //}

            ////dgv.RowCount = ushort.MaxValue;

            //dgv.RowCount = 10;
            ////((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();
        }

        private void dgv_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            int ColumnIndex = e.ColumnIndex;
            int RowIndex = e.RowIndex;
            object Value = null;

            Value = "C " + ColumnIndex.ToString() + "; R " + RowIndex.ToString();

            e.Value = Value;
        }

        private void dgv_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {

        }        

        protected virtual bool ReadData()
        {
            IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
            ITable table = fws.OpenTable(NameTable);
////-----
            //TableWraper tableWrapper = new TableWraper(table, NameSortFild);
            //tableWrapper.AllowNew = false;
            //tableWrapper.AllowRemove = false;

            //BindingSource dsBinding = new BindingSource();
            //dsBinding.DataSource = tableWrapper;

            //dgv.AutoGenerateColumns = false;

            //SharedClass.CreateColumIn(dgv, table);

            //dgv.DataSource = dsBinding;
////-----






//Dim pLayer As FeatureLayer = AxMapControl1.Map.Layer(0) 
//Dim attributeTable As IAttributeTable = TryCast(pLayer, IAttributeTable) 
//Dim dt As New System.Data.DataTable 
//Dim bs As New System.Windows.Forms.BindingSource 
//dt.Load(attributeTable.AttributeTable) 
//bs.DataSource = dt 
//Me.DataGridView1.DataSource = bs 
//Me.DataGridView1.Refresh() 

//ESRI.ArcGIS.Carto.IAttributeTable

//System.Data.DataTable dt = new System.Data.DataTable;
//dt.Load(table as System.Data.IDataReader);


 
//IRecordSetInit recordSetInit= new ESRI.ArcGIS.Geodatabase.RecordSetClass();  
//recordSetInit.SetSourceTable(table, new QueryFilterClass());  
//IRecordSet recordSet= recordSetInit as IRecordSet;  

//System.Data.DataSet netDS = ESRI.ArcGIS.Utility.Converter.ToDataSet(recordSet);  

                //dgv.DataSource = table;

            IDataset ds = table as IDataset;
            BindingSource dsBinding = new BindingSource();

            dsBinding.DataSource = ds;

            dgv.AutoGenerateColumns = true;
            dgv.Refresh();
            dgv.DataSource = dsBinding;


            return true;
        }
    }
}

