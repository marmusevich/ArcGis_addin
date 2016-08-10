using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithKadastr2016
{
    public partial class frmRej_Adr_Poh_element : frmBaseElement
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
        DomeinDataAdapter ddaKOD_TYP_OBJ_ADR;
        DomeinDataAdapter ddaKOD_TYP_OBJ_POH_ADR;
        DomeinDataAdapter ddaKOD_TYP_ADR;
        DomeinDataAdapter ddaKOD_FUNC_PRYZN;
        DomeinDataAdapter ddaKOD_STAN_ADR;
        DomeinDataAdapter ddaRuleID;
        #endregion



        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            //даты

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "NAJM_OBJ", txtNAJM_OBJ);
            SetStringValueFromDBToTextBox(ref row, "SKOR_NAJM_OBJ", txtSKOR_NAJM_OBJ);
            SetStringValueFromDBToTextBox(ref row, "Korpus", txtKorpus);
            SetStringValueFromDBToTextBox(ref row, "NumerBud", txtNumerBud);
            SetStringValueFromDBToTextBox(ref row, "Address_Str", txtAddress_Str);

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row, "KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbKOD_TYP_OBJ_ADR, ref ddaKOD_TYP_OBJ_ADR, "KOD_TYP_OBJ_ADR", GetValueFromDB(ref row, "KOD_TYP_OBJ_ADR"));
            CheсkValueAndSetToComboBox(ref cbKOD_TYP_OBJ_POH_ADR, ref ddaKOD_TYP_OBJ_POH_ADR, "KOD_TYP_OBJ_POH_ADR", GetValueFromDB(ref row, "KOD_TYP_OBJ_POH_ADR"));
            CheсkValueAndSetToComboBox(ref cbKOD_TYP_ADR, ref ddaKOD_TYP_ADR, "KOD_TYP_ADR", GetValueFromDB(ref row, "KOD_TYP_ADR"));
            CheсkValueAndSetToComboBox(ref cbKOD_FUNC_PRYZN, ref ddaKOD_FUNC_PRYZN, "KOD_FUNC_PRYZN", GetValueFromDB(ref row, "KOD_FUNC_PRYZN"));
            CheсkValueAndSetToComboBox(ref cbKOD_STAN_ADR, ref ddaKOD_STAN_ADR, "KOD_STAN_ADR", GetValueFromDB(ref row, "KOD_STAN_ADR"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "ID_ADR_PERV", txtID_ADR_PERV);
            SetIntValueFromDBToTextBox(ref row, "ID_OBJECT_POH", txtID_OBJECT_POH);
            SetIntValueFromDBToTextBox(ref row, "ID_ADRESS", txtID_ADRESS);
            SetIntValueFromDBToTextBox(ref row, "ID_ELEMENT", txtID_ELEMENT);
            SetIntValueFromDBToTextBox(ref row, "ID_Adm_Rn", txtID_Adm_Rn);
            SetIntValueFromDBToTextBox(ref row, "ID_Obl", txtID_Obl);
            SetIntValueFromDBToTextBox(ref row, "ID_Nsl_Pnk", txtID_Nsl_Pnk);
            SetIntValueFromDBToTextBox(ref row, "ID_Rej_Vul", txtID_Rej_Vul);
            SetIntValueFromDBToTextBox(ref row, "INDEX_POSH_VID", txtINDEX_POSH_VID);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "NAJM_OBJ", txtNAJM_OBJ);
            SaveStringValueFromTextBoxToDB(ref row, "SKOR_NAJM_OBJ", txtSKOR_NAJM_OBJ);
            SaveStringValueFromTextBoxToDB(ref row, "Korpus", txtKorpus);
            SaveStringValueFromTextBoxToDB(ref row, "NumerBud", txtNumerBud);
            SaveStringValueFromTextBoxToDB(ref row, "Address_Str", txtAddress_Str);

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_TYP_OBJ_ADR", ref cbKOD_TYP_OBJ_ADR);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_TYP_OBJ_POH_ADR", ref cbKOD_TYP_OBJ_POH_ADR);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_TYP_ADR", ref cbKOD_TYP_ADR);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_FUNC_PRYZN", ref cbKOD_FUNC_PRYZN);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_STAN_ADR", ref cbKOD_STAN_ADR);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID", ref cbRuleID);

            //числовые значения
            SaveIntValueFromTextBoxToDB(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "ID_ADR_PERV", txtID_ADR_PERV);
            SaveIntValueFromTextBoxToDB(ref row, "ID_OBJECT_POH", txtID_OBJECT_POH);
            SaveIntValueFromTextBoxToDB(ref row, "ID_ADRESS", txtID_ADRESS);
            SaveIntValueFromTextBoxToDB(ref row, "ID_ELEMENT", txtID_ELEMENT);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Adm_Rn", txtID_Adm_Rn);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Obl", txtID_Obl);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Nsl_Pnk", txtID_Nsl_Pnk);
            SaveIntValueFromTextBoxToDB(ref row, "ID_Rej_Vul", txtID_Rej_Vul);
            SaveIntValueFromTextBoxToDB(ref row, "INDEX_POSH_VID", txtINDEX_POSH_VID);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();

            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_ADR_PERV, errorProvider) && ret;

            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNAJM_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNumerBud, errorProvider) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_TYP_OBJ_ADR, ref ddaKOD_TYP_OBJ_ADR, "KOD_TYP_OBJ_ADR");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_TYP_OBJ_POH_ADR, ref ddaKOD_TYP_OBJ_POH_ADR, "KOD_TYP_OBJ_POH_ADR");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_TYP_ADR, ref ddaKOD_TYP_ADR, "KOD_TYP_ADR");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_FUNC_PRYZN, ref ddaKOD_FUNC_PRYZN, "KOD_FUNC_PRYZN");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_STAN_ADR, ref ddaKOD_STAN_ADR, "KOD_STAN_ADR");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbRuleID, ref ddaRuleID, "RuleID");
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("NAJM_OBJ", txtNAJM_OBJ);
            SetMaxLengthStringValueToTextBox("SKOR_NAJM_OBJ", txtSKOR_NAJM_OBJ);
            SetMaxLengthStringValueToTextBox("Korpus", txtKorpus);
            SetMaxLengthStringValueToTextBox("NumerBud", txtNumerBud);
            SetMaxLengthStringValueToTextBox("Address_Str", txtAddress_Str);
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmRej_Adr_Poh_element()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Adr_Poh_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Rej_Adr_Poh";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Реєстр адрес будівель похідний";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Реєстр адрес будівель похідний";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление Реєстр адрес будівель похідний";
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

//Name = Cadastr_V6.DBO.Rej_Adr_Poh, Реєстр адрес будівель похідний 
//Workspace_Name = Cadastr_V6.DBO.OBJ_ADR_REJ_ZON

//Field = OBJECTID, Type = esriFieldTypeOID, 

//--Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором DefaultValue =21010300 Domain = Dmn_Kls_Rej_Adr_Poh
//--Field = KOD_TYP_OBJ_ADR, Type = esriFieldTypeSmallInteger, Тип об’єкта адресації Domain = Dmn_Rej_Adr_Poh_Kod_Typ_Obj_Adr
//--Field = KOD_TYP_OBJ_POH_ADR, Type = esriFieldTypeSmallInteger, Тип об’єкта похідної адреси Domain = Dmn_Rej_Adr_Poh_Kod_Typ_Obj_Poh_Adr
//--Field = KOD_TYP_ADR, Type = esriFieldTypeSmallInteger, Тип адреси Domain = Dmn_Rej_Adr_Poh_Kod_Typ_Obj_Poh_Adr
//--Field = KOD_FUNC_PRYZN, Type = esriFieldTypeSmallInteger, Тип переважного функціонального призначення об’єкта Domain =Dmn_Rej_Adr_Poh_Kod_Func_Pryzn
//--Field = KOD_STAN_ADR, Type = esriFieldTypeSmallInteger, Стан адреси DefaultValue=1, Domain =Dmn_Rej_Adr_Poh_Kod_Stan_Adr
//--Field = RuleID, Type = esriFieldTypeInteger, Умовні знаки Domain = Rej_Adr_Poh_Rep_Rules


//--Field = ID_MSB_OBJ, Type = esriFieldTypeDouble, Ідентифікатор об’єкту
//--Field = ID_ADRESS, Type = esriFieldTypeInteger, Ідентифікатор адреси
//--Field = ID_ELEMENT, Type = esriFieldTypeInteger, Ідентифікатор пойменованого елемента вулично-дорожньої мережі 
//--Field = ID_ADR_PERV, Type = esriFieldTypeInteger, Ідентифікатор адреси первинного об’єкта адресації 
//--Field = ID_OBJECT_POH, Type = esriFieldTypeInteger, Ідентифікатори об’єкта похідної адреси 
//--Field = ID_Adm_Rn, Type = esriFieldTypeInteger, Ідентифікатор адміністративного району
//--Field = INDEX_POSH_VID, Type = esriFieldTypeInteger, Індекс поштового відділення 
//--Field = ID_Obl, Type = esriFieldTypeInteger, область
//--Field = ID_Nsl_Pnk, Type = esriFieldTypeInteger, населений пукт 
//--Field = ID_Rej_Vul, Type = esriFieldTypeInteger, Посилання на вулицю 
//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ 

//--Field = NAJM_OBJ, Type = esriFieldTypeString, Найменування об’єкта 
//--Field = SKOR_NAJM_OBJ, Type = esriFieldTypeString, Скорочене найменування об’єкту 
//--Field = NumerBud, Type = esriFieldTypeString, Номер будинку 
//--Field = Korpus, Type = esriFieldTypeString, Корпус 
//--Field = Address_Str, Type = esriFieldTypeString, Address_Str 
//--Field = Prymitka, Type = esriFieldTypeString, Примітка 


//Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE
//Field = Override, Type = esriFieldTypeBlob, Заміщення 
