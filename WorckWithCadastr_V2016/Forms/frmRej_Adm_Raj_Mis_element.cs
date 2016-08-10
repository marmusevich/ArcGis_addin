using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithKadastr2016
{
    public partial class frmRej_Adm_Raj_Mis_element : frmBaseElement
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
        DomeinDataAdapter ddaRuleID;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            //даты

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "NAZVA_UKR", txtNAZVA_UKR);
            SetStringValueFromDBToTextBox(ref row, "NAZVA_ROS", txtNAZVA_ROS);
            SetStringValueFromDBToTextBox(ref row, "NAZVA_LAT", txtNAZVA_LAT);

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row, "KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "ID_RAI", txtID_RAI);
            SetIntValueFromDBToTextBox(ref row, "KOD_KOATUU_RAI", txtKOD_KOATUU_RAI);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "NAZVA_UKR", txtNAZVA_UKR);
            SaveStringValueFromTextBoxToDB(ref row, "NAZVA_ROS", txtNAZVA_ROS);
            SaveStringValueFromTextBoxToDB(ref row, "NAZVA_LAT", txtNAZVA_LAT);

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID", ref cbRuleID);

            //числовые значения
            SaveIntValueFromTextBoxToDB(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "ID_RAI", txtID_RAI);
            SaveIntValueFromTextBoxToDB(ref row, "KOD_KOATUU_RAI", txtKOD_KOATUU_RAI);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_RAI, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKOD_KOATUU_RAI, errorProvider) && ret;

            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNAZVA_UKR, errorProvider) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbRuleID, ref ddaRuleID, "RuleID");

        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("NAZVA_UKR", txtNAZVA_UKR);
            SetMaxLengthStringValueToTextBox("NAZVA_ROS", txtNAZVA_ROS);
            SetMaxLengthStringValueToTextBox("NAZVA_LAT", txtNAZVA_LAT);
        }
        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmRej_Adm_Raj_Mis_element()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Adm_Raj_Mis_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Rej_Adm_Raj_Mis";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Реєстр адміністративних районів міста";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Реєстр адміністративних районів міста";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление Реєстр адміністративних районів міста";
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
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void main_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            isModified = true;
        }
        #endregion
    }
}

//Name = Cadastr_V6.DBO.Rej_Adm_Raj_Mis Реєстр адміністративних районів міста
//Workspace_Name = Cadastr_V6.DBO.OBJ_ADR_REJ_ZON

//Field = OBJECTID, Type = esriFieldTypeOID, 

//--Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором DefaultValue = 21010500 Domain = Dmn_Kls_Rej_Adm_Rai_Mis
//--Field = RuleID, Type = esriFieldTypeInteger, Умовні знаки Domain = Rej_Adm_Raj_Mis_Rep_Rules

//--Field = ID_MSB_OBJ, Type = esriFieldTypeInteger, Ідентифікатор об’єкту 
//--Field = ID_RAI, Type = esriFieldTypeInteger, Ідентифікатор району 
//--Field = KOD_KOATUU_RAI, Type = esriFieldTypeInteger, Код КОАТУУ адміністративного району міста
//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ 

//--Field = Prymitka, Type = esriFieldTypeString, Примітка 
//--Field = NAZVA_UKR, Type = esriFieldTypeString, Назва району українською мовою
//--Field = NAZVA_ROS, Type = esriFieldTypeString, Назва району російською мовою
//--Field = NAZVA_LAT, Type = esriFieldTypeString, Назва району латиницею

//Field = SHAPE.STLength() Type = esriFieldTypeDouble, SHAPE.STLength()
//Field = SHAPE.STArea(), Type = esriFieldTypeDouble, SHAPE.STArea()
//Field = Override, Type = esriFieldTypeBlob, Заміщення
//Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE
