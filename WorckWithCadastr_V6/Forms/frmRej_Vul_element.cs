using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithCadastr_V6
{
    public partial class frmRej_Vul_element : frmBaseElement
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //адаптеры доменов
        DomeinDataAdapter ddaKOD_KLS;
        DomeinDataAdapter ddaKOD_STAN_VUL;
        DomeinDataAdapter ddaKOD_KAT;
        DomeinDataAdapter ddaRuleID;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            //даты
            SetDateValueFromDBToDateTimePicker(ref row, "DataDocument", dtpDataDocument);

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "NAZVA_UKR", txtNAZVA_UKR);
            SetStringValueFromDBToTextBox(ref row, "NAZVA_ROS", txtNAZVA_ROS);
            SetStringValueFromDBToTextBox(ref row, "NAZVA_LAT", txtNAZVA_LAT);
            SetStringValueFromDBToTextBox(ref row, "IST_DOV", txtIST_DOV);
            SetStringValueFromDBToTextBox(ref row, "NZV_MSB_OBJ", txtNZV_MSB_OBJ);
            SetStringValueFromDBToTextBox(ref row, "NomerDocument", txtNomerDocument);
            //Field = Назва_вули, Type = esriFieldTypeString, Назва_вули Назва_вули

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row,"KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbKOD_STAN_VUL, ref ddaKOD_STAN_VUL, "KOD_STAN_VUL", GetValueFromDB(ref row,"KOD_STAN_VUL"));
            CheсkValueAndSetToComboBox(ref cbKOD_KAT, ref ddaKOD_KAT, "KOD_KAT", GetValueFromDB(ref row, "KOD_KAT"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "KOD_VUL", txtKOD_VUL);
            SetIntValueFromDBToTextBox(ref row, "KOATUU", txtKOATUU);
            SetIntValueFromDBToTextBox(ref row, "KodObject", txtKodObject);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты
            SaveDateValueFromDateTimePickerToDB(ref row, "DataDocument", dtpDataDocument);

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "NAZVA_UKR", txtNAZVA_UKR);
            SaveStringValueFromTextBoxToDB(ref row, "NAZVA_ROS", txtNAZVA_ROS);
            SaveStringValueFromTextBoxToDB(ref row, "NAZVA_LAT", txtNAZVA_LAT);
            SaveStringValueFromTextBoxToDB(ref row, "IST_DOV", txtIST_DOV);
            SaveStringValueFromTextBoxToDB(ref row, "NZV_MSB_OBJ", txtNZV_MSB_OBJ);
            SaveStringValueFromTextBoxToDB(ref row, "NomerDocument", txtNomerDocument);
            //Field = Назва_вули, Type = esriFieldTypeString, Назва_вули Назва_вули

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_STAN_VUL", ref cbKOD_STAN_VUL);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KAT", ref cbKOD_KAT);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID", ref cbRuleID);

            //числовые значения
            SaveIntValueFromTextBoxToDB(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "KOD_VUL", txtKOD_VUL);
            SaveIntValueFromTextBoxToDB(ref row, "KOATUU", txtKOATUU);
            SaveIntValueFromTextBoxToDB(ref row, "KodObject", txtKodObject);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKOD_VUL, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKOATUU, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKodObject, errorProvider) && ret;

            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNZV_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNAZVA_UKR, errorProvider) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_STAN_VUL, ref ddaKOD_STAN_VUL, "KOD_STAN_VUL");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KAT, ref ddaKOD_KAT, "KOD_KAT");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbRuleID, ref ddaRuleID, "RuleID");

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmRej_Vul_element()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Vul_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Rej_Vul";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Реєстр вулиць";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Реєстр вулиць";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление записи о Реєстр вулиць";
                    break;
                default:
                    this.Close();
                    return;
            }
        }

        private void main_TextChanged(object sender, System.EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }
        
        private void main_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void main_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            isModified = true;
        }

        private void dtpDataDocument_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }
        
        #endregion
    }
}
