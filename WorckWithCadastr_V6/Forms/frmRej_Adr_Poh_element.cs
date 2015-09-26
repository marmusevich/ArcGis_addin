using ESRI.ArcGIS.Geodatabase;
using System;

namespace WorckWithCadastr_V6
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
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            return ret;
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

            base.NameWorkspace = "Cadastr_V6";
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
