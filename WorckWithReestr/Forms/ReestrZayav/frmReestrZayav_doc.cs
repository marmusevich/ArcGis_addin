using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using SharedClasses;

namespace WorckWithReestr
{
    public partial class frmReestrZayav_doc : frmBaseDocument
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //адаптеры доменов
        DomeinDataAdapter ddaStatus;
 
        // коды значений справочников
        int mTip_Doc = -1;
        int mKod_Z = -1;
        int mFio_Z = -1;

        bool IsHaveReferense = false;
        bool IsReferenceClose = false;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);

            //даты
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Z", dtpData_Z);
            SetDateValueFromDBToDateTimePicker(ref row, "Cane_Date", dtpCane_Date);

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "Sodergan", txtSodergan);
            SetStringValueFromDBToTextBox(ref row, "Cane", txtCane);
            SetStringValueFromDBToTextBox(ref row, "Adress_Text", txtAdress_Text);

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbStatus, ref ddaStatus, "Status", GetValueFromDB(ref row, "Status"));

            // справочники
            mTip_Doc = (int)GetValueFromDB(ref row, "Tip_Doc");
            mKod_Z = (int)GetValueFromDB(ref row, "Kod_Z");
            mFio_Z = (int)GetValueFromDB(ref row, "Fio_Z");

            OnChangedFio_Z();
            OnChangedKod_Z();
            OnChangedTipDoc();

            //N_Z, Type = esriFieldTypeInteger, AliasName = № пп 
            SetIntValueFromDBToTextBox(ref row, "N_Z", txtN_Z);

            IsHaveReferense = GeneralApp.ConvertVolueToBool(GetValueFromDB(ref row, "IsHaveReferense"));
            IsReferenceClose = GeneralApp.ConvertVolueToBool(GetValueFromDB(ref row, "IsReferenceClose"));
            string tmp = "";
            if (IsHaveReferense)
            {
                tmp += "Есть кадастровая справка.";
                if (IsReferenceClose)
                    tmp += " Заблокирована для редоктирования.";
                else
                    tmp += " Не заблокирована.";
            }
            else
            {
                tmp += "Кадастровой справки нет.";
            }
            llblHaveReferense.Text = tmp;

            // справка есть и она закрыта - заблкировать изменение
            if (IsHaveReferense && IsReferenceClose)
            {
                foreach (System.Windows.Forms.Control c in Controls)
                {
                    c.Enabled = false;
                }
                btnOk.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Z", dtpData_Z);
            SaveDateValueFromDateTimePickerToDB(ref row, "Cane_Date", dtpCane_Date);

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "Sodergan", txtSodergan);
            SaveStringValueFromTextBoxToDB(ref row, "Cane", txtCane);
            SaveStringValueFromTextBoxToDB(ref row, "Adress_Text", txtAdress_Text);

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "Status", ref cbStatus);

            // справочники
            row.set_Value(base.table.FindField("Tip_Doc"), mTip_Doc);
            row.set_Value(base.table.FindField("Kod_Z"), mKod_Z);
            row.set_Value(base.table.FindField("Fio_Z"), mFio_Z);

            //N_Z, Type = esriFieldTypeInteger, AliasName = № пп 
            int N_Z = Convert.ToInt32(txtN_Z.Text);
            if (ReestrDocumentWork.IsNumerReestrZayavExist(N_Z) && (editMode != EditMode.EDIT))
            {
                if (MessageBox.Show(string.Format("Документ с номером [{0}] уже есть. \n Згенерировать следующий доступный? ",N_Z), "Не унекальный номер", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    N_Z = ReestrDocumentWork.GetNextNumerToReestrZayav("Tip_Doc = " + mTip_Doc.ToString());
            }
            row.set_Value(base.table.FindField("N_Z"), N_Z);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtN_Z, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtCane, errorProvider) && ret;
            ret = ReestrDictionaryWork.CheckValueIsContainsFizLic_SetError(txtFio_Z, errorProvider, ref mFio_Z) && ret;
            if (cbStatus.SelectedIndex == 0)
            {
                ret = ReestrDictionaryWork.CheckValueIsContainsJurLic_SetError(txtKod_Z, errorProvider, ref mKod_Z, null) && ret;
            }
            else
            {
                ret = ReestrDictionaryWork.CheckValueIsContainsFizLic_SetError(txtKod_Z, errorProvider, ref mKod_Z, null) && ret;
            }
            ret = ReestrDictionaryWork.CheckValueIsContainsTip_Doc_SetError(txtTip_Doc, errorProvider, ref mTip_Doc, null) && ret;

            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbStatus, ref ddaStatus, "Status");

            // справочники
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtFio_Z);
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtTip_Doc);

            EnableAutoComlectToAdress();

            if (cbStatus.SelectedIndex == 0)
                ReestrDictionaryWork.EnableAutoComlectToJurLic(txtKod_Z);
            else
                ReestrDictionaryWork.EnableAutoComlectToFizLic(txtKod_Z);
        }

        protected override void DB_DefaultValue_to_FormElement()
        {
            base.DB_DefaultValue_to_FormElement();
            ////алгоритм генерации номера, запрос большего из базы или последнего
            //txtN_Z.Text = ReestrDocumentWork.GetNextNumerToReestrZayav().ToString();
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("Sodergan", txtSodergan);
            SetMaxLengthStringValueToTextBox("Cane", txtCane);
            SetMaxLengthStringValueToTextBox("Adress_Text", txtAdress_Text);
        }

        protected override bool DeleteData()
        {
            if (CadastralReference.WorkCadastralReference.CheckReferenceToExistPages(objectID))
            {
                return base.DeleteData();
            }
            return false;
        }

        //включить автозаполнение поля по описательного адресса
        public bool EnableAutoComlectToAdress()
        {
            bool ret = false;

            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("Kadastr2016", "Kn_Reg_Zayv", "Adress_Text");
            if (sourse != null)
            {
                txtAdress_Text.AutoCompleteCustomSource = sourse;
                txtAdress_Text.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtAdress_Text.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ret = true;
            }
            //txtAdress_Text.ReadOnly = !ret;
            return ret;
        }



        private void OnChangedTipDoc()
        {
            txtTip_Doc.Text = ReestrDictionaryWork.GetNameByIDFromTip_Doc(mTip_Doc);
            //алгоритм генерации номера, запрос большего из базы или последнего
            txtN_Z.Text = ReestrDocumentWork.GetNextNumerToReestrZayav("Tip_Doc = " + mTip_Doc.ToString()).ToString();

        }

        private void OnChangedKod_Z()
        {
            if (cbStatus.SelectedIndex == 0)
            {
                txtKod_Z.Text = ReestrDictionaryWork.GetNameByIDFromJurOsoby(mKod_Z);
                
            }
            else
            {
                txtKod_Z.Text = ReestrDictionaryWork.GetFIOByIDFromFizLic(mKod_Z);
            }
        }

        private void OnChangedFio_Z()
        {
            txtFio_Z.Text = ReestrDictionaryWork.GetFIOByIDFromFizLic(mFio_Z);
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmReestrZayav_doc() : base()
        {
            InitializeComponent();
        }

        public frmReestrZayav_doc(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();
            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Kn_Reg_Zayv";
        }

        private void frmReestrZayav_doc_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового заявления / обращения";
                    break;
                case EditMode.ADD_COPY:
                    Text = "Добавление нового заявления / обращения копированием";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных заявления / обращения";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление заявления / обращения";
                    break;
                default:
                    Close();
                    return;
            }
        }

        private void llblHaveReferense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //arcBtn_Open_dwCadastralReference.Show(objectID, true);
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  справочник
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void btnFio_Z_Click(object sender, EventArgs e)
        {
            string filteredString = " EtoSotrydnik = 1 ";
            mFio_Z = frmFizLic_list.ShowForSelect(filteredString);
            OnChangedFio_Z();
            errorProvider.SetError(txtFio_Z, String.Empty);
            //ValidatingData();
        }

        private void btnTip_Doc_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mTip_Doc = frmTipDoc_list.ShowForSelect(filteredString);
            OnChangedTipDoc();
            errorProvider.SetError(txtTip_Doc, String.Empty);
            //ValidatingData();
        }

        private void btnKod_Z_Click(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex == 0)
            {
                string filteredString = "";
                mKod_Z = frmJurLic_list.ShowForSelect(filteredString);
            }
            else
            {
                string filteredString = " EtoSotrydnik = 0 ";
                mKod_Z = frmFizLic_list.ShowForSelect(filteredString);
            }
            OnChangedKod_Z();
            errorProvider.SetError(txtKod_Z, String.Empty);
            //ValidatingData();
        }
        #endregion

        //---------------------------------------------------------------------------------------
        #region  валидация
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void txtN_Z_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }
        private void txtCane_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }


        private void txtN_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            isModified = true;
            mKod_Z = -1;
            txtKod_Z.Text = "";

            if (cbStatus.SelectedIndex == 0)
                ReestrDictionaryWork.EnableAutoComlectToJurLic(txtKod_Z);
            else
                ReestrDictionaryWork.EnableAutoComlectToFizLic(txtKod_Z);
        }

        private void txtKod_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtFio_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtTip_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtSodergan_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtCane_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpCane_Date_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtAdress_Text_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }
        #endregion

    }
}
