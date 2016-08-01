using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithCadastr_V6
{
    public partial class frmVrb_Bdl_Spr_element : frmBaseElement
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //адаптеры доменов
        DomeinDataAdapter ddaRuleID_2000;
        DomeinDataAdapter ddaKOD_KLS;
        DomeinDataAdapter ddaKOD_STS;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            //даты

            //простые тексты  

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row, "KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbKOD_STS, ref ddaKOD_STS, "KOD_STS", GetValueFromDB(ref row, "KOD_STS"));
            CheсkValueAndSetToComboBox(ref cbRuleID_2000, ref ddaRuleID_2000, "RuleID_2000", GetValueFromDB(ref row, "RuleID_2000"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "Pidcode", txtPidcode);
            SetIntValueFromDBToTextBox(ref row, "KOD_TYP_BDN", txtKOD_TYP_BDN);
            SetIntValueFromDBToTextBox(ref row, "KLK_PVH", txtKLK_PVH);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты

            //простые тексты  

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_KLS", ref cbKOD_KLS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "KOD_STS", ref cbKOD_STS);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "RuleID_2000", ref cbRuleID_2000);

            //числовые значения
            SaveIntValueFromTextBoxToDB(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SaveIntValueFromTextBoxToDB(ref row, "Pidcode", txtPidcode);
            SaveIntValueFromTextBoxToDB(ref row, "KOD_TYP_BDN", txtKOD_TYP_BDN);
            SaveIntValueFromTextBoxToDB(ref row, "KLK_PVH", txtKLK_PVH);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKOD_TYP_BDN, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtPidcode, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKLK_PVH, errorProvider) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbKOD_STS, ref ddaKOD_STS, "KOD_STS");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbRuleID_2000, ref ddaRuleID_2000, "RuleID_2000");

        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmVrb_Bdl_Spr_element()
            : base()
        {
            InitializeComponent();
        }

        public frmVrb_Bdl_Spr_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Vrb_Bdl_Spr";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Виробничі будівлі  і споруди";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Виробничі будівлі  і споруди";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление Виробничі будівлі  і споруди";
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

//Name = Cadastr_V6.DBO.Vrb_Bdl_Spr Виробничі будівлі  і споруди 
//Workspace Name = Cadastr_V6.DBO.BDL_SPR

//Field = OBJECTID, Type = esriFieldTypeOID, 

//Field = KOD_STS, Type = esriFieldTypeInteger, Статус об&apos;єкту  DefaultValue = 1, Domain = Dmn_Sts_Obj_Msb_Dln
//Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором DefaultValue = 17010200, Domain = Dmn_Vrb_Bdl_Spr
//Field = RuleID_2000, Type = esriFieldTypeInteger, Умовні знаки (1:2 000 і кр.) Domain = Vrb_Bdl_Spr_Rep_2000_Rules

//Field = ID_MSB_OBJ, Type = esriFieldTypeInteger, Ідентифікатор об’єкту
//Field = KLK_PVH, Type = esriFieldTypeInteger, Кількість поверхів 
//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ 
//Field = Pidcode, Type = esriFieldTypeSmallInteger, Підкод типу документа 

//--Field = Prymitka, Type = esriFieldTypeString, Примітка 

//Field = SHAPE.STLength(), Type = esriFieldTypeDouble, SHAPE.STLength()
//Field = Override_5000, Type = esriFieldTypeBlob, Заміщення (1:5 000) Override_5000
//Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE
//Field = SHAPE.STArea(), Type = esriFieldTypeDouble, SHAPE.STArea()