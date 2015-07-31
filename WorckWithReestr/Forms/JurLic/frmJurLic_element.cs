using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using SharedClasses;

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
            cbFV.SelectedIndex = dda.GetIndexByValue(Convert.ToInt16(o)); 
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(base.table.FindField("назва"), txtName.Text);
            row.set_Value(base.table.FindField("код_ЄДРПОУ"), txtOKPO.Text);
            row.set_Value(base.table.FindField("форма_власності"), ((DomeinDataAdapter.DomeinData)cbFV.SelectedItem).Value);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = SharedClass.CheckValueStringNotEmpty_SetError(txtName, errorProvider) && ret;
            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            dda = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("форма_власності")).Domain);
            cbFV.Items.AddRange(dda.ToArray());
            object o = base.table.Fields.get_Field(base.table.FindField("форма_власності")).DefaultValue;
            if ((o != null) && !Convert.IsDBNull(o))
                cbFV.SelectedIndex = dda.GetIndexByValue(Convert.ToInt16(o));
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
            base.NameTable = "jur_osoby";

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
                    break;
                default:
                    this.Close();
                    return;
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
