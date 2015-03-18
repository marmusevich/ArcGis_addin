using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmJurLic_list : frmBaseSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmJurLic_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmJurLic_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmJurLic_list() : base()
        {
            InitializeComponent();
        }

        public frmJurLic_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.jur_osoby";
            base.NameSortFild = "назва";

            //this.Text = "";
        }

        protected override bool ReadData()
        {
            bool ret = base.ReadData();


            //try
            //{
            //    IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
            //    ITable table = fws.OpenTable(NameTable);
            //    DomeinDataAdapter dda = new DomeinDataAdapter(table.Fields.get_Field(3).Domain);

            //    //dgv.Columns.RemoveAt(3);
            //    dgv.Columns[3].Visible = false;
            //    System.Windows.Forms.DataGridViewComboBoxColumn dGVC = new System.Windows.Forms.DataGridViewComboBoxColumn();
            //    dGVC.Name = table.Fields.get_Field(3).Name;
            //    dGVC.DataPropertyName = table.Fields.get_Field(3).Name;
            //    dGVC.HeaderText = table.Fields.get_Field(3).AliasName;
            //    dGVC.ReadOnly = true;
            //    dGVC.Items.AddRange(dda.ToArray());
            //    dGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;

            //    dgv.Columns.Add(dGVC);
            //}
            //catch (Exception ee) // доработать блок ошибок на разные исключения
            //{
            //    SharedClass.ShowErrorMessage();
            //    this.Close();
            //}


            return ret;
        }

        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmJurLic_element(_objectID, _editMode);
        }

        #endregion
    }
}
