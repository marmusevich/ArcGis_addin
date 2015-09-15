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

            //простые тексты  

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row,"KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbKOD_STAN_VUL, ref ddaKOD_STAN_VUL, "KOD_STAN_VUL", GetValueFromDB(ref row,"KOD_STAN_VUL"));
            CheсkValueAndSetToComboBox(ref cbKOD_KAT, ref ddaKOD_KAT, "KOD_KAT", GetValueFromDB(ref row, "KOD_KAT"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты

            //простые тексты  

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_STAN_VUL", ref cbKOD_STAN_VUL);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KAT", ref cbKOD_KAT);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID", ref cbRuleID);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            //ret = GeneralDBWork.CheckValueIsInt_SetError(txtKodObject, errorProvider) && ret;

            //ret = AdresReestrWork.CheckValueIsContainsTip_Doc_SetError(txtDocument, errorProvider, ref mDocument) && ret;
            //if (txtKodKategorii.Visible)
            //{
            //    ret = AdresReestrWork.CheckValueIsContainsKategorObj_SetError(mKategorTablName, txtKodKategorii, errorProvider, ref mKodKategorii) && ret;
            //}
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

        private void main__SelectedIndexChanged(object sender, System.EventArgs e)
        {
            isModified = true;
        }
        #endregion
    }
}



//Name = Cadastr_V6.DBO.Rej_Vul Реєстр вулиць
//Workspace_Name = Cadastr_V6.DBO.OBJ_ADR_REJ_ZON
//----------------------------------------------------------------------------------------------------------------------------------------------
//Field = OBJECTID Type = esriFieldTypeOID, 

//-Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором, DefaultValue = 21010400, Domain = Dmn_Kls_Rej_Vul
//-Field = KOD_STAN_VUL, Type = esriFieldTypeSmallInteger, AliasName>Стан вулиці, Domain = Dmn_Rej_Vul_Kod_Stan_Vul
//-Field = KOD_KAT, Type = esriFieldTypeSmallInteger, Код категорії вулиці, Domain = Dmn_Rej_Vul_Kod_Kat
//-Field = RuleID, Type = esriFieldTypeInteger, Умовні знаки RuleID Domain = Rej_Vul_Rep_Rules

//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ 
//Field = ID_MSB_OBJ, Type = esriFieldTypeInteger, Ідентифікатор об’єкту
//Field = KOD_VUL, Type = esriFieldTypeInteger, Реестраційний код вулиці
//Field = KOATUU, Type = esriFieldTypeInteger, KOATUU
//Field = KodObject, Type = esriFieldTypeInteger, KodObject 

//--Field = Prymitka, Type = esriFieldTypeString, Примітка Prymitka
//Field = NAZVA_UKR, Type = esriFieldTypeString, Назва об’єкта українською мовою
//Field = NAZVA_ROS, Type = esriFieldTypeString, Назва об’єкта російською мовою
//Field = NAZVA_LAT, Type = esriFieldTypeString, Назва об’єкта латиницею 
//Field = IST_DOV, Type = esriFieldTypeString, Історична довідка
//Field = NZV_MSB_OBJ, Type = esriFieldTypeString, Загальна назва вулиці 
//Field = Назва_вули, Type = esriFieldTypeString, Назва_вули Назва_вули
//Field = NomerDocument, Type = esriFieldTypeString, NomerDocument 

//Field = DataDocument, Type = esriFieldTypeDate, Дата документа 

//Field = Override, Type = esriFieldTypeBlob, Заміщення 

//----------------------------------------------------------------------------------------------------------------------------------------------
//Field = SHAPE.STLength(), Type = esriFieldTypeDouble, SHAPE.STLength
//Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE 