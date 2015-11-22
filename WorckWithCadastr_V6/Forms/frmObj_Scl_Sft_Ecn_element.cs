using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithCadastr_V6
{
    public partial class frmObj_Scl_Sft_Ecn_element : frmBaseElement
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
        DomeinDataAdapter ddaKOD_STS;
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
            CheсkValueAndSetToComboBox(ref cbKOD_STS, ref ddaKOD_STS, "KOD_STS", GetValueFromDB(ref row, "KOD_STS"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "KOD_TYP_OBJ", txtKOD_TYP_OBJ);
            SetIntValueFromDBToTextBox(ref row, "Pidcode", txtPidcode);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты

            //простые тексты  

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_STS", ref cbKOD_STS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID", ref cbRuleID);

            //числовые значения
            SaveIntValueFromTextBoxToDB(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "KOD_TYP_OBJ", txtKOD_TYP_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "Pidcode", txtPidcode);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKOD_TYP_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtPidcode, errorProvider) && ret;


            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_STS, ref ddaKOD_STS, "KOD_STS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbRuleID, ref ddaRuleID, "RuleID");
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmObj_Scl_Sft_Ecn_element()
            : base()
        {
            InitializeComponent();
        }

        public frmObj_Scl_Sft_Ecn_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Obj_Scl_Sft_Ecn";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Об’єкти соціальної сфери та економіки";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Об’єкти соціальної сфери та економіки";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление Об’єкти соціальної сфери та економіки";
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
        #endregion
    }
}

//Name = Cadastr_V6.DBO.Obj_Scl_Sft_Ecn Об’єкти соціальної сфери та економіки 
//Workspace Name = Cadastr_V6.DBO.BDL_SPR

//Field = OBJECTID, Type = esriFieldTypeOID, 

//--Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором DefaultValue = 17010500 Domain =Dmn_Obj_Scl_Sft_Ecn
//--Field = KOD_STS, Type = esriFieldTypeInteger, Статус об&apos;єкту  DefaultValue = 1, Domain = Dmn_Sts_Obj_Msb_Dln
//--Field = RuleID, Type = esriFieldTypeInteger, Умовні знаки  Domain = Obj_Scl_Sft_Ecn_Rep_Rules

//--Field = ID_MSB_OBJ, Type = esriFieldTypeInteger, Ідентифікатор об’єкту
//--Field = KOD_TYP_OBJ, Type = esriFieldTypeInteger, Код типу Об’єкту DefaultValue = 0
//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ 
//--Field = Pidcode, Type = esriFieldTypeSmallInteger, Підкод типу документа 

//--Field = Prymitka, Type = esriFieldTypeString, Примітка 

//Field = Override, Type = esriFieldTypeBlob, Заміщення 
//Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE 
