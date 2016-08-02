using ESRI.ArcGIS.Geodatabase;
using System;
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
            base.DB_to_FormElement(row);

            // взять из базы
            SetStringValueFromDBToTextBox(ref row, "назва", txtName);
            SetStringValueFromDBToTextBox(ref row, "код_ЄДРПОУ", txtOKPO);

            CheсkValueAndSetToComboBox(ref cbFV, ref dda, "форма_власності", GetValueFromDB(ref row, "форма_власності"));
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            // положить в базы
            SaveStringValueFromTextBoxToDB(ref row, "назва", txtName);
            SaveStringValueFromTextBoxToDB(ref row, "код_ЄДРПОУ", txtOKPO);

            SaveDomeinDataValueFromComboBoxToDB(ref row, "форма_власності", ref cbFV);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtName, errorProvider) && ret;
            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();

            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbFV, ref dda, "форма_власності");
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("назва", txtName);
            SetMaxLengthStringValueToTextBox("код_ЄДРПОУ", txtOKPO);
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
