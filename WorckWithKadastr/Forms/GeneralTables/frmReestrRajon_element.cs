using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrRajon_element : frmBaseAdrReestrSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            txtKodKOATU.Text = "" + row.get_Value(base.table.FindField("KodKOATU")) as string;
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            row.set_Value(base.table.FindField("KodKOATU"), txtKodKOATU.Text);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = SharedClass.CheckValueIsInt_SetError(txtKodKOATU, errorProvider) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmReestrRajon_element() : base()
        {
            InitializeComponent();
        }

        public frmReestrRajon_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "ReestrRajon_polygon";
        }

        private void frmReestrRajon_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового района города";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных района города";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление района города";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        
        
        private void txtKodKOATU_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKodKOATU_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        
        #endregion

    }
}
