using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmTipDoc_element : frmBaseSpr_element
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
            // взять из базы
            txtTip_Doc.Text = "" + row.get_Value(base.table.FindField("Tip_Doc")) as string;
            txtKod_Doc.Text = "" + row.get_Value(base.table.FindField("Kod_Doc")).ToString();
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(base.table.FindField("Tip_Doc"), txtTip_Doc.Text);
            row.set_Value(base.table.FindField("Kod_Doc"), Convert.ToInt16(txtKod_Doc.Text));
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsSmalInt_SetError(txtKod_Doc, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtTip_Doc, errorProvider) && ret;
            return ret;
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmTipDoc_element()
            : base()
        {
            InitializeComponent();
        }

        public frmTipDoc_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "Tip_Doc";
        }

        private void frmTipDoc_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового типа документа";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных типа документа";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление типа документа";
                    break;
                default:
                    this.Close();
                    return;
            }
        }

        private void txtTip_Doc_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKod_Doc_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKod_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtTip_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }
        #endregion
    }
}
