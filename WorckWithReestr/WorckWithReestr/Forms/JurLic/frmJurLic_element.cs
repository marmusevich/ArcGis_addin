using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmJurLic_element : frmBaseSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------



        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        DomeinDataAdapter dda;
        
        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------


        protected override void DB_to_FormElement(IRow row)
        {
            // взять из базы
            txtName.Text = "" + row.get_Value(base.table.FindField("назва")) as string;
            txtOKPO.Text = "" + row.get_Value(base.table.FindField("код_ЄДРПОУ")) as string;
            
            object o = row.get_Value(base.table.FindField("форма_власності"));
            if(o == null )
                o = row.Fields.get_Field(base.table.FindField("форма_власності")).DefaultValue;
            cbFV.SelectedIndex = dda.GetIndexByValue(o); 
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(base.table.FindField("назва"), txtName.Text);
            row.set_Value(base.table.FindField("код_ЄДРПОУ"), txtOKPO.Text);
            row.set_Value(base.table.FindField("форма_власності"), ((DomeinData)cbFV.SelectedItem).Value);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = SharedClass.CheckValueStringNotEmpty_SetError(txtName, errorProvider) && ret;
            return ret;
        }


        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmJurLic_element() : base()
        {
            InitializeComponent();
        }

        public frmJurLic_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();
            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.jur_osoby";

        }

        private void frmJurLic_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового юридического лица";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных юридического лица";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление юридического лица";
                    btnOk.Text = "Удалить";
                    txtName.Enabled = false;
                    txtOKPO.Enabled = false;
                    cbFV.Enabled = false;

                    break;

                default:
                    this.Close();
                    return;
            }

            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                ITable table = fws.OpenTable(NameTable);


                dda = new DomeinDataAdapter(table.Fields.get_Field(3).Domain);
                cbFV.Items.AddRange(dda.ToArray());
                object o = table.Fields.get_Field(3).DefaultValue;
                cbFV.SelectedIndex = dda.GetIndexByValue(o);
            }
            catch (Exception ee) // доработать блок ошибок на разные исключения
            {
                SharedClass.ShowErrorMessage();
                this.Close();
            }


            if (editMode != EditMode.ADD)
            {
                if (!this.ReadData()) // error
                {
                    SharedClass.ShowErrorMessage();
                    this.Close();
                }
            }
        }
        #endregion

        private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }
    }
}
