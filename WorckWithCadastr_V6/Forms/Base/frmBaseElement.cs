using System;
using SharedClasses;
using ESRI.ArcGIS.Geodatabase;


namespace WorckWithCadastr_V6
{
    public partial class frmBaseElement : frmBaseSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);

            SetStringValueFromDBToTextBox(ref row, "Prymitka", txtPrymitka);
            SetIntValueFromDBToTextBox(ref row, "N_Kad", txtN_Kad);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);

            SaveStringValueFromTextBoxToDB(ref row, "Prymitka", txtPrymitka);
            SaveIntValueFromTextBoxToDB(ref row, "N_Kad", txtN_Kad);


        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            if (  txtN_Kad.Text.Trim().Length != 0)
            {
                ret = GeneralDBWork.CheckValueIsSmalInt_SetError(txtN_Kad, errorProvider) && ret;
            }
            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        public frmBaseElement()
            : base()
        {
            InitializeComponent();
        }
        public frmBaseElement(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

        }

        private void btnShowOnMap_Click(object sender, EventArgs e)
        {
            GeneralMapWork.ShowOnMap(NameTable);
        }

        private void frmBaseElement_Load(object sender, EventArgs e)
        {
            //if (editMode == EditMode.EDIT)
            //{
            //    btnShowOnMap.Enabled = true;
            //}
            //else
            //{
            //    btnShowOnMap.Enabled = false;
            //}
        }
        #endregion


        //---------------------------------------------------------------------------------------
        #region  валидация
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void txtPrymitka_TextChanged(object sender, EventArgs e)
        {
                isModified = true;
                ValidatingData();
        }

        private void txtPrymitka_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
                isModified = true;
                e.Cancel = !ValidatingData();
        }

        private void txtN_Kad_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtN_Kad_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }
        #endregion

    }
}
