
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using SharedClasses;


namespace WorckWithReestr
{
    public partial class frmReestrVedomostey_doc : frmBaseDocument
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        // коды значений справочников
        int mFIO_Kad = -1;
        int mTip_Doc = -1;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //получение данных из базы и иницилизация значений элементов управлений
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);

            //даты
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Vh", dtpData_Vh);
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Otp", dtpData_Otp);
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Kad", dtpData_Kad);

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "Ist_Ved", txtIst_Ved);
            SetStringValueFromDBToTextBox(ref row, "N_Sop_List", txtN_Sop_List);
            SetStringValueFromDBToTextBox(ref row, "N_GD", txtN_GD);
            SetStringValueFromDBToTextBox(ref row, "N_Doc_GD", txtN_Doc_GD);
            SetStringValueFromDBToTextBox(ref row, "Name_GD", txtName_GD);
            SetStringValueFromDBToTextBox(ref row, "El_Format_GD", txtEl_Format_GD);
            SetStringValueFromDBToTextBox(ref row, "N_Kad", txtN_Kad);
            SetStringValueFromDBToTextBox(ref row, "Prim", txtPrim);

            // справочники
            //mFIO_Kad = row.get_Value(base.table.FindField("FIO_Kad"));
            mFIO_Kad = (int)GetValueFromDB(ref row, "FIO_Kad");
            mTip_Doc = (int)GetValueFromDB(ref row, "Tip_Doc");
            OnChangedFIO_Kad();
            OnChangedTipDoc();

            SetIntValueFromDBToTextBox(ref row, "Kol_Str_GD", txtKol_Str_GD);
            SetIntValueFromDBToTextBox(ref row, "N_Vh", txtN_Vh);
        }
        //сохранение значений элементов управления в базу данных
        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Vh", dtpData_Vh);
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Otp", dtpData_Otp);
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Kad", dtpData_Kad);

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "Ist_Ved", txtIst_Ved);
            SaveStringValueFromTextBoxToDB(ref row, "N_Sop_List", txtN_Sop_List);
            SaveStringValueFromTextBoxToDB(ref row, "N_GD", txtN_GD);
            SaveStringValueFromTextBoxToDB(ref row, "N_Doc_GD", txtN_Doc_GD);
            SaveStringValueFromTextBoxToDB(ref row, "Name_GD", txtName_GD);
            SaveStringValueFromTextBoxToDB(ref row, "El_Format_GD", txtEl_Format_GD);
            SaveStringValueFromTextBoxToDB(ref row, "N_Kad", txtN_Kad);
            SaveStringValueFromTextBoxToDB(ref row, "Prim", txtPrim);
            
            // справочники
            row.set_Value(base.table.FindField("FIO_Kad"), mFIO_Kad);
            row.set_Value(base.table.FindField("Tip_Doc"), mTip_Doc);

            SaveIntValueFromTextBoxToDB(ref row, "Kol_Str_GD", txtKol_Str_GD);

            //N_Vh, Type = esriFieldTypeInteger, AliasName = Вхідний №
            int N_Vh = Convert.ToInt32(txtN_Vh.Text);
            if (ReestrDocumentWork.IsNumerReestrVedomosteyExist(N_Vh) && (editMode != EditMode.EDIT))
            {
                if (MessageBox.Show(string.Format("Документ с номером [{0}] уже есть. \n Згенерировать следующий доступный? ", N_Vh), "Не унекальный номер", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    N_Vh = ReestrDocumentWork.GetNextNumerToReestrZayav();
            }
            row.set_Value(base.table.FindField("N_Vh"), N_Vh);
        }
        //проверка полей ввода на коректность ввода
        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtN_Vh, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKol_Str_GD, errorProvider) && ret;
            ret = ReestrDictionaryWork.CheckValueIsContainsFizLic_SetError(txtFIO_Kad, errorProvider, ref mFIO_Kad) && ret;
            ret = ReestrDictionaryWork.CheckValueIsContainsTip_Doc_SetError(txtTip_Doc, errorProvider, ref mTip_Doc, txtTip_Doc_code) && ret;

            return ret;
        }
        //получение обобщеных данных для элементов данных (например: списки доменных значений, данные для автодополнения)
        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            // справочники
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtFIO_Kad);
        }
        //присвоение значений по умалчанию для полей при создании нового
        protected override void DB_DefaultValue_to_FormElement()
        {
            base.DB_DefaultValue_to_FormElement();
            //алгоритм генерации номера, запрос большего из базы или последнего
            txtN_Vh.Text = ReestrDocumentWork.GetNextNumerToReestrVedomostey().ToString();
            txtKol_Str_GD.Text = "0";
        }
        //установить максимальную длину элементов управления для значения типа текст
        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("Ist_Ved", txtIst_Ved);
            SetMaxLengthStringValueToTextBox("N_Sop_List", txtN_Sop_List);
            SetMaxLengthStringValueToTextBox("N_GD", txtN_GD);
            SetMaxLengthStringValueToTextBox("N_Doc_GD", txtN_Doc_GD);
            SetMaxLengthStringValueToTextBox("Name_GD", txtName_GD);
            SetMaxLengthStringValueToTextBox("El_Format_GD", txtEl_Format_GD);
            SetMaxLengthStringValueToTextBox("N_Kad", txtN_Kad);
            SetMaxLengthStringValueToTextBox("Prim", txtPrim);
        }

        private void OnChangedFIO_Kad()
        {
            txtFIO_Kad.Text = ReestrDictionaryWork.GetFIOByIDFromFizLic(mFIO_Kad);
        }


        private void OnChangedTipDoc()
        {
            txtTip_Doc.Text = ReestrDictionaryWork.GetNameByIDFromTip_Doc(mTip_Doc);
            txtTip_Doc_code.Text = ReestrDictionaryWork.GetCodeByIDFromTip_Doc(mTip_Doc);
        }



        #endregion
        
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmReestrVedomostey_doc() : base()
        {
            InitializeComponent();
        }

        public frmReestrVedomostey_doc(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();
            base.NameWorkspace = "reestr";
            base.NameTable = "Kn_Reg_Ved";
        }

        private void frmReestrVedomostey_doc_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового ведомости";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных ведомости";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление ведомости";
                    break;
                default:
                    Close();
                    return;
            }
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  справочник
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void btnFIO_Kad_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mFIO_Kad = frmFizLic_list.ShowForSelect(filteredString);
            OnChangedFIO_Kad();
            errorProvider.SetError(txtFIO_Kad, String.Empty);
        }

        private void btnTip_Doc_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mTip_Doc = frmTipDoc_list.ShowForSelect(filteredString);
            OnChangedTipDoc();
            errorProvider.SetError(txtTip_Doc, String.Empty);
            //ValidatingData();
        }

        #endregion

        //---------------------------------------------------------------------------------------
        #region  валидация
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void txtN_Vh_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtN_Vh_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtFIO_Kad_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtKol_Str_GD_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKol_Str_GD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        //---
        private void txtIst_Ved_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Vh_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtN_Sop_List_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Otp_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtN_GD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtN_Doc_GD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtName_GD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtEl_Format_GD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtN_Kad_Validating(object sender, EventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Kad_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtPrim_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }
        
        private void txtN_Kad_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void txtTip_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }
       
        #endregion
    }
}

#region  описание полей

////ключь
//OBJECTID, Type = esriFieldTypeOID

////даты
//Data_Vh, Type = esriFieldTypeDate, AliasName = Дата внесення документа
//Data_Otp, Type = esriFieldTypeDate, AliasName = Дата відправлення
//Data_Kad, Type = esriFieldTypeDate, AliasName = Дата розміщення в кадастрі

//////простые тексты 
//Ist_Ved, Type = esriFieldTypeString, AliasName = Джерело відомостей
//N_Sop_List, Type = esriFieldTypeString, AliasName = Вихідий № супровідного листа
//N_GD, Type = esriFieldTypeString, AliasName = № п/п
//N_Doc_GD, Type = esriFieldTypeString, AliasName = Номер документу МД
//Name_GD, Type = esriFieldTypeString, AliasName = Назва документу МД
//El_Format_GD, Type = esriFieldTypeString, AliasName = Електронна форма подання
//N_Kad, Type = esriFieldTypeString, AliasName = Реєстраційний № в кадастрі
//Prim, Type = esriFieldTypeString, AliasName = Примечание 

//// справочники
//FIO_Kad, Type = esriFieldTypeInteger, AliasName = ПІБ особи, що розмістила
//Tip_Doc, Type = esriFieldTypeInteger, AliasName = Тип документа 

////числа
//Kol_Str_GD, Type = esriFieldTypeSmallInteger, AliasName = Кількість аркушів
//N_Vh, Type = esriFieldTypeInteger, AliasName = Вхідний №


#endregion
