using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithCadastr_V6
{
    public partial class frmRej_Bud_Adr_element : frmBaseElement
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
        DomeinDataAdapter ddaKOD_STAN_ADR;
        DomeinDataAdapter ddaRuleID;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            //даты

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "NAJM_OBJ", txtNAJM_OBJ);
            SetStringValueFromDBToTextBox(ref row, "OPYS_ROZTASH", txtOPYS_ROZTASH);
            SetStringValueFromDBToTextBox(ref row, "Korpus", txtKorpus);
            SetStringValueFromDBToTextBox(ref row, "NumerBud", txtNumerBud);
            SetStringValueFromDBToTextBox(ref row, "Address_Str", txtAddress_Str);

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row, "KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbKOD_STAN_ADR, ref ddaKOD_STAN_ADR, "KOD_STAN_VUL", GetValueFromDB(ref row, "KOD_STAN_ADR"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "KOATUU", txtKOATUU);
            SetIntValueFromDBToTextBox(ref row, "ID_OBJECT", txtID_OBJECT);
            SetIntValueFromDBToTextBox(ref row, "KOD_KLS_OBJECT", txtKOD_KLS_OBJECT);
            SetIntValueFromDBToTextBox(ref row, "ID_ELEMENT", txtID_ELEMENT);
            SetIntValueFromDBToTextBox(ref row, "ID_Adm_Rn", txtID_Adm_Rn);
            SetIntValueFromDBToTextBox(ref row, "ID_Obl", txtID_Obl);
            SetIntValueFromDBToTextBox(ref row, "ID_Nsl_Pnk", txtID_Nsl_Pnk);
            SetIntValueFromDBToTextBox(ref row, "ID_Rej_Vul", txtID_Rej_Vul);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "NAJM_OBJ", txtNAJM_OBJ);
            SaveStringValueFromTextBoxToDB(ref row, "OPYS_ROZTASH", txtOPYS_ROZTASH);
            SaveStringValueFromTextBoxToDB(ref row, "Korpus", txtKorpus);
            SaveStringValueFromTextBoxToDB(ref row, "NumerBud", txtNumerBud);
            SaveStringValueFromTextBoxToDB(ref row, "Address_Str", txtAddress_Str);

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_STAN_ADR", ref cbKOD_STAN_ADR);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID", ref cbRuleID);

            //числовые значения
            SaveIntValueFromTextBoxToDB(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "KOATUU", txtKOATUU);
            SaveIntValueFromTextBoxToDB(ref row, "ID_OBJECT", txtID_OBJECT);
            SaveIntValueFromTextBoxToDB(ref row, "KOD_KLS_OBJECT", txtKOD_KLS_OBJECT);
            SaveIntValueFromTextBoxToDB(ref row, "ID_ELEMENT", txtID_ELEMENT);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Adm_Rn", txtID_Adm_Rn);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Obl", txtID_Obl);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Nsl_Pnk", txtID_Nsl_Pnk);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Rej_Vul", txtID_Rej_Vul);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();

            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_OBJECT, errorProvider) && ret;

            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNAJM_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNumerBud, errorProvider) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_STAN_ADR, ref ddaKOD_STAN_ADR, "KOD_STAN_ADR");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbRuleID, ref ddaRuleID, "RuleID");
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("NAJM_OBJ", txtNAJM_OBJ);
            SetMaxLengthStringValueToTextBox("OPYS_ROZTASH", txtOPYS_ROZTASH);
            SetMaxLengthStringValueToTextBox("Korpus", txtKorpus);
            SetMaxLengthStringValueToTextBox("NumerBud", txtNumerBud);
            SetMaxLengthStringValueToTextBox("Address_Str", txtAddress_Str);
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmRej_Bud_Adr_element()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Bud_Adr_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Rej_Bud_Adr";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Реєстр будівельних адрес";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Реєстр будівельних адрес";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление Реєстр будівельних адрес";
                    break;
                default:
                    Close();
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

        }

        private void main_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            isModified = true;
        }

        #endregion

    }
}


//Name = Cadastr_V6.DBO.Rej_Bud_Adr Реєстр будівельних адрес
//Workspace_Name = Cadastr_V6.DBO.OBJ_ADR_REJ_ZON

//Field = OBJECTID, Type = esriFieldTypeOID, 

//--Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором DefaultValue = 21010200 Domain =Dmn_Kls_Rej_Bud_Adr
//--Field = KOD_STAN_ADR, Type = esriFieldTypeSmallInteger, Стан адреси DefaultValue = 1 Domain = Dmn_Rej_Bud_Adr_Kod_Stan_Adr
//--Field = RuleID, Type = esriFieldTypeInteger, Умовні знаки RuleID, Domain =Rej_Bud_Adr_Rep_Rules

//--Field = ID_MSB_OBJ, Type = esriFieldTypeInteger, Ідентифікатор об’єкту 
//--Field = KOATUU, Type = esriFieldTypeInteger, Коатуу 
//--Field = ID_OBJECT, Type = esriFieldTypeInteger,  Ідентифікатор об’єкту будівництва, що адресується
//--Field = ID_ELEMENT, Type = esriFieldTypeInteger, Ідентифікатор пойменованого елемента вулично-дорожньої мережі
//--Field = ID_Adm_Rn, Type = esriFieldTypeInteger, Адміністративний район 
//--Field = ID_Obl, Type = esriFieldTypeInteger, Область
//--Field = ID_Nsl_Pnk, Type = esriFieldTypeInteger, Населений пункт 
//--Field = ID_Rej_Vul, Type = esriFieldTypeInteger, Вулиця 
//--Field = KOD_KLS_OBJECT, Type = esriFieldTypeDouble, Код класу обєкту, що адресується
//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ 

//--Field = NAJM_OBJ, Type = esriFieldTypeString, Найменування об’єкта 
//--Field = OPYS_ROZTASH, Type = esriFieldTypeString, Опис місцерозташування об’єкту
//--Field = NumerBud, Type = esriFieldTypeString, Номер будинку
//--Field = Korpus, Type = esriFieldTypeString, Корпус 
//--Field = Address_Str, Type = esriFieldTypeString, Адреса (для підписів)

//Field = Override, Type = esriFieldTypeBlob, Заміщення Override

//--Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE
//--Field = Prymitka, Type = esriFieldTypeString, Примітка 
