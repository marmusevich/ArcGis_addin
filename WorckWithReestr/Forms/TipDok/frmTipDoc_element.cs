using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithReestr
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
            base.DB_to_FormElement(row);

            // взять из базы
            SetStringValueFromDBToTextBox(ref row, "Tip_Doc", txtTip_Doc);
            SetIntValueFromDBToTextBox(ref row, "Kod_Doc", txtKod_Doc);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            // положить в базы
            SaveStringValueFromTextBoxToDB(ref row, "Tip_Doc", txtTip_Doc);
            SaveIntValueFromTextBoxToDB(ref row, "Kod_Doc", txtKod_Doc);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsSmalInt_SetError(txtKod_Doc, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtTip_Doc, errorProvider) && ret;
            return ret;
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("Tip_Doc", txtTip_Doc);
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

            base.NameWorkspace = "reestr";
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
