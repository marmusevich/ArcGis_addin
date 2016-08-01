using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;

namespace WorckWithCadastr_V6
{
    public partial class frmKvt_element : frmBaseElement
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
            CheсkValueAndSetToComboBox(ref cbKOD_KLS, ref ddaKOD_KLS, "KOD_KLS", GetValueFromDB(ref row, "KOD_KLS"));
            CheсkValueAndSetToComboBox(ref cbKOD_STS, ref ddaKOD_STS, "KOD_STS", GetValueFromDB(ref row, "KOD_STS"));
            CheсkValueAndSetToComboBox(ref cbRuleID, ref ddaRuleID, "RuleID", GetValueFromDB(ref row, "RuleID"));

            //числовые значения
            SetIntValueFromDBToTextBox(ref row, "ID_MSB_OBJ", txtID_MSB_OBJ);
            SetIntValueFromDBToTextBox(ref row, "KOD_TYP_KVT", txtKOD_TYP_KVT);
            SetIntValueFromDBToTextBox(ref row, "NMR_KVT", txtNMR_KVT);
            SetIntValueFromDBToTextBox(ref row, "KLK_DLK", txtKLK_DLK);
            SetIntValueFromDBToTextBox(ref row, "PLH_BLK", txtPLH_BLK);
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
            SaveIntValueFromTextBoxToDB(ref row, "KOD_TYP_KVT", txtKOD_TYP_KVT);
            SaveIntValueFromTextBoxToDB(ref row, "NMR_KVT", txtNMR_KVT);
            SaveIntValueFromTextBoxToDB(ref row, "KLK_DLK", txtKLK_DLK);
            SaveIntValueFromTextBoxToDB(ref row, "PLH_BLK", txtPLH_BLK);
            SaveIntValueFromTextBoxToDB(ref row, "Pidcode", txtPidcode);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtID_MSB_OBJ, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKOD_TYP_KVT, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKLK_DLK, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtNMR_KVT, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtPidcode, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtPLH_BLK, errorProvider) && ret;

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
        public frmKvt_element()
            : base()
        {
            InitializeComponent();
        }

        public frmKvt_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Kvt";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой Квартал багатоквартирноїї забудови";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных Квартал багатоквартирноїї забудови";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление Квартал багатоквартирноїї забудови";
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

//Name = Cadastr_V6.DBO.Kvt, Квартал багатоквартирноїї забудови
//Workspace Name = Cadastr_V6.DBO.BDL_SPR

//Field = OBJECTID, Type = esriFieldTypeOID, 


//--Field = KOD_STS, Type = esriFieldTypeInteger, Статус об&apos;єкту KOD_STS DefaultValue = 1, Domain = Dmn_Sts_Obj_Msb_Dln
//--Field = KOD_KLS, Type = esriFieldTypeDouble, Код за класифікатором Domain 
//--Field = RuleID, Type = esriFieldTypeInteger, Умовні знаки Domain = Kvt_Rep_Rules

//--Field = KOD_TYP_KVT, Type = esriFieldTypeInteger, DefaultValue = 0  KOD_TYP_KVT KOD_TYP_KVT
//--Field = ID_MSB_OBJ, Type = esriFieldTypeDouble, Ідентифікатор об’єкту
//--Field = NMR_KVT, Type = esriFieldTypeInteger, Номер кварталу
//--Field = KLK_DLK, Type = esriFieldTypeInteger, Кількість ділянок KLK_DLK
//--Field = PLH_BLK, Type = esriFieldTypeDouble, Площа ділянок
//--Field = N_Kad, Type = esriFieldTypeSmallInteger, Кадастровий № ДПТ
//--Field = Pidcode, Type = esriFieldTypeSmallInteger, Підкод типу документа

//--Field = Prymitka, Type = esriFieldTypeString, Примітка 

//Field = SHAPE.STArea(), Type = esriFieldTypeDouble, SHAPE.STArea()
//Field = SHAPE.STLength(), Type = esriFieldTypeDouble, SHAPE.STLength()
//Field = Override, Type = esriFieldTypeBlob, Заміщення 
//Field = SHAPE, Type = esriFieldTypeGeometry, SHAPE 

            //this..TextChanged += new System.EventHandler(this.main_TextChanged);
            //this..Validating += new System.ComponentModel.CancelEventHandler(this.main_Validating);

