﻿using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;
using SharedClasses;

//base.table.FindField("Cane_Date"), 
//base.table.FindField("Adress_Text"), 

//base.table.FindField("Rajon"), 
//base.table.FindField("IsHaveReferense"),
//base.table.FindField("IsReferenceClose") 

namespace WorckWithReestr
{
    public partial class frmReestrZayav_doc : frmBaseDocument
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //адаптеры доменов
        DomeinDataAdapter ddaStatus;
        DomeinDataAdapter ddaOtkaz;
        DomeinDataAdapter ddaOplata;
 
        // коды значений справочников
        int mTip_Doc = -1;
        int mKod_Z = -1;
        int mFio_Ved_Vid = -1;
        int mFio_Ved_Prin = -1;
        int mFio_Z = -1;
        int mRajon = -1;

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
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Ish", dtpData_Ish);
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Oplata", dtpData_Oplata);
            SetDateValueFromDBToDateTimePicker(ref row, "Data_Ved", dtpData_Ved);
            SetDateValueFromDBToDateTimePicker(ref row, "Cane_Date", dtpCane_Date);

            //простые тексты  
            SetStringValueFromDBToTextBox(ref row, "Tel_Z", txtTel_Z);
            SetStringValueFromDBToTextBox(ref row, "Prim", txtPrim);
            SetStringValueFromDBToTextBox(ref row, "Opisan_Ved", txtOpisan_Ved);
            SetStringValueFromDBToTextBox(ref row, "Forma_Ved", txtForma_Ved);
            SetStringValueFromDBToTextBox(ref row, "N_Ish_Z", txtN_Ish_Z);
            SetStringValueFromDBToTextBox(ref row, "Sodergan", txtSodergan);
            SetStringValueFromDBToTextBox(ref row, "Doc_Oplata", txtDoc_Oplata);
            SetStringValueFromDBToTextBox(ref row, "Pr_Otkaz", txtPr_Otkaz);
            SetStringValueFromDBToTextBox(ref row, "Dodatok", txtDodatok);
            SetStringValueFromDBToTextBox(ref row, "Cane", txtCane);
            SetStringValueFromDBToTextBox(ref row, "Adress_Text", txtAdress_Text);

            //доменные значения
            CheсkValueAndSetToComboBox(ref cbStatus, ref ddaStatus, "Status", GetValueFromDB(ref row, "Status"));
            CheсkValueAndSetToComboBox(ref cbOtkaz, ref ddaOtkaz, "Otkaz", GetValueFromDB(ref row, "Otkaz"));
            CheсkValueAndSetToComboBox(ref cbOplata, ref ddaOplata, "Oplata", GetValueFromDB(ref row, "Oplata"));

            // справочники
            mTip_Doc = (int)GetValueFromDB(ref row, "Tip_Doc");
            mKod_Z = (int)GetValueFromDB(ref row, "Kod_Z");
            mFio_Ved_Vid = (int)GetValueFromDB(ref row, "Fio_Ved_Vid");
            mFio_Ved_Prin = (int)GetValueFromDB(ref row, "Fio_Ved_Prin");
            mFio_Z = (int)GetValueFromDB(ref row, "Fio_Z");
            mRajon = (int)GetValueFromDB(ref row, "Rajon");

            OnChangedFio_Z();
            OnChangedKod_Z();
            OnChangedFio_Ved_Vid();
            OnChangedFio_Ved_Prin();
            OnChangedTipDoc();
            OnChangedRajon();

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
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Ish", dtpData_Ish);
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Oplata", dtpData_Oplata);
            SaveDateValueFromDateTimePickerToDB(ref row, "Data_Ved", dtpData_Ved);
            SaveDateValueFromDateTimePickerToDB(ref row, "Cane_Date", dtpCane_Date);

            //простые тексты  
            SaveStringValueFromTextBoxToDB(ref row, "Tel_Z", txtTel_Z);
            SaveStringValueFromTextBoxToDB(ref row, "Prim", txtPrim);
            SaveStringValueFromTextBoxToDB(ref row, "Opisan_Ved", txtOpisan_Ved);
            SaveStringValueFromTextBoxToDB(ref row, "Forma_Ved", txtForma_Ved);
            SaveStringValueFromTextBoxToDB(ref row, "N_Ish_Z", txtN_Ish_Z);
            SaveStringValueFromTextBoxToDB(ref row, "Sodergan", txtSodergan);
            SaveStringValueFromTextBoxToDB(ref row, "Doc_Oplata", txtDoc_Oplata);
            SaveStringValueFromTextBoxToDB(ref row, "Pr_Otkaz", txtPr_Otkaz);
            SaveStringValueFromTextBoxToDB(ref row, "Dodatok", txtDodatok);
            SaveStringValueFromTextBoxToDB(ref row, "Cane", txtCane);
            SaveStringValueFromTextBoxToDB(ref row, "Adress_Text", txtAdress_Text);

            //доменные значения
            SaveDomeinDataValueFromComboBoxToDB(ref row, "Status", ref cbStatus);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "Otkaz", ref cbOtkaz);
            SaveDomeinDataValueFromComboBoxToDB(ref row, "Oplata", ref cbOplata);

            // справочники
            row.set_Value(base.table.FindField("Tip_Doc"), mTip_Doc);
            row.set_Value(base.table.FindField("Kod_Z"), mKod_Z);
            row.set_Value(base.table.FindField("Fio_Ved_Vid"), mFio_Ved_Vid);
            row.set_Value(base.table.FindField("Fio_Ved_Prin"), mFio_Ved_Prin);
            row.set_Value(base.table.FindField("Fio_Z"), mFio_Z);
            row.set_Value(base.table.FindField("Rajon"), mRajon);

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
            //ret = ReestrDictionaryWork.CheckValueIsContainsFizLic_SetError(txtFio_Ved_Vid, errorProvider, ref mFio_Ved_Vid) && ret;
            //ret = ReestrDictionaryWork.CheckValueIsContainsFizLic_SetError(txtFio_Ved_Prin, errorProvider, ref mFio_Ved_Prin) && ret;
            if (cbStatus.SelectedIndex == 0)
            {
                ret = ReestrDictionaryWork.CheckValueIsContainsJurLic_SetError(txtKod_Z, errorProvider, ref mKod_Z, null) && ret;
            }
            else
            {
                ret = ReestrDictionaryWork.CheckValueIsContainsFizLic_SetError(txtKod_Z, errorProvider, ref mKod_Z, null) && ret;
            }
            ret = ReestrDictionaryWork.CheckValueIsContainsTip_Doc_SetError(txtTip_Doc, errorProvider, ref mTip_Doc, null) && ret;
            //ret = ReestrDictionaryWork.CheckValueIsContainsAdmRaj_SetError(txtRajon, errorProvider, ref mRajon, null) && ret;


            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            //доменные значения
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbStatus, ref ddaStatus, "Status");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbOtkaz, ref ddaOtkaz, "Otkaz");
            CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cbOplata, ref ddaOplata, "Oplata");


            // справочники
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtFio_Z);
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtFio_Ved_Prin);
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtFio_Ved_Vid);
            ReestrDictionaryWork.EnableAutoComlectToFizLic(txtTip_Doc);

            EnableAutoComlectToAdress();

            if (cbStatus.SelectedIndex == 0)
                ReestrDictionaryWork.EnableAutoComlectToJurLic(txtKod_Z);
            else
                ReestrDictionaryWork.EnableAutoComlectToFizLic(txtKod_Z);

            ReestrDictionaryWork.EnableAutoComlectToAdmRaj(txtRajon);

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
            SetMaxLengthStringValueToTextBox("Tel_Z", txtTel_Z);
            SetMaxLengthStringValueToTextBox("Prim", txtPrim);
            SetMaxLengthStringValueToTextBox("Opisan_Ved", txtOpisan_Ved);
            SetMaxLengthStringValueToTextBox("Forma_Ved", txtForma_Ved);
            SetMaxLengthStringValueToTextBox("N_Ish_Z", txtN_Ish_Z);
            SetMaxLengthStringValueToTextBox("Sodergan", txtSodergan);
            SetMaxLengthStringValueToTextBox("Doc_Oplata", txtDoc_Oplata);
            SetMaxLengthStringValueToTextBox("Pr_Otkaz", txtPr_Otkaz);
            SetMaxLengthStringValueToTextBox("Dodatok", txtDodatok);
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

        private void OnChangedFio_Ved_Prin()
        {
            txtFio_Ved_Prin.Text = ReestrDictionaryWork.GetFIOByIDFromFizLic(mFio_Ved_Prin);
        }

        private void OnChangedFio_Ved_Vid()
        {
            txtFio_Ved_Vid.Text = ReestrDictionaryWork.GetFIOByIDFromFizLic(mFio_Ved_Vid);
        }

        private void OnChangedKod_Z()
        {
            if (cbStatus.SelectedIndex == 0)
            {
                txtKod_Z.Text = ReestrDictionaryWork.GetNameByIDFromJurOsoby(mKod_Z);
                txtTel_Z.Text = ReestrDictionaryWork.GetTelByIDFromJurOsoby(mKod_Z);
                
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

        private void OnChangedRajon()
        {
            txtRajon.Text = ReestrDictionaryWork.GetNazvaByIDFromAdmRaj(mRajon);
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

        private void btnFio_Ved_Prin_Click(object sender, EventArgs e)
        {
            string filteredString = " EtoSotrydnik = 1 ";
            mFio_Ved_Prin = frmFizLic_list.ShowForSelect(filteredString);
            OnChangedFio_Ved_Prin();
            errorProvider.SetError(txtFio_Ved_Prin, String.Empty);
            //ValidatingData();
        }

        private void btnFio_Ved_Vid_Click(object sender, EventArgs e)
        {
            string filteredString = " EtoSotrydnik = 1 ";
            mFio_Ved_Vid = frmFizLic_list.ShowForSelect(filteredString);
            OnChangedFio_Ved_Vid();
            errorProvider.SetError(txtFio_Ved_Vid, String.Empty);
            //ValidatingData();
        }
        private void btnRajon_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mRajon = frmRejAdmRajMis_list.ShowForSelect(filteredString);
            OnChangedRajon();
            errorProvider.SetError(txtRajon, String.Empty);
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

        private void cbOplata_SelectedIndexChanged(object sender, EventArgs e)
        {
            isModified = true;
            if (cbOplata.SelectedIndex == 0)
            {
                dtpData_Oplata.Enabled = true;
                txtDoc_Oplata.Enabled = true;
            }
            else
            {
                dtpData_Oplata.Enabled = false;
                txtDoc_Oplata.Enabled = false;
                //dtpData_Oplata.Value = DateTime.Now;
                //txtDoc_Oplata.Text = "";
            }
        }

        private void cbOtkaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            isModified = true;
            if (cbOtkaz.SelectedIndex == 0)
            {
                txtPr_Otkaz.Enabled = false;
                //txtPr_Otkaz.Text = "";

                dtpData_Ved.Enabled = true;
                txtOpisan_Ved.Enabled = true;
                txtForma_Ved.Enabled = true;

                txtFio_Ved_Vid.Enabled = true;
                btnFio_Ved_Vid.Enabled = true;
                txtFio_Ved_Prin.Enabled = true;
                btnFio_Ved_Prin.Enabled = true;
            }
            else
            {
                txtPr_Otkaz.Enabled = true;

                dtpData_Ved.Enabled = false;
                //dtpData_Ved.Value = DateTime.Now;
                txtOpisan_Ved.Enabled = false;
                //txtOpisan_Ved.Text = "";
                txtForma_Ved.Enabled = false;
                //txtForma_Ved.Text = "";

                txtFio_Ved_Vid.Enabled = false;
                //mFio_Ved_Vid = -1;
                //txtFio_Ved_Vid.Text = "";
                btnFio_Ved_Vid.Enabled = false;
                txtFio_Ved_Prin.Enabled = false;
                //mFio_Ved_Prin = -1;
                //txtFio_Ved_Prin.Text = "";
                btnFio_Ved_Prin.Enabled = false;
            }
        }

        private void txtKod_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtFio_Ved_Vid_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtFio_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtFio_Ved_Prin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtTip_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        private void txtRajon_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }

        //---
        private void txtTel_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtN_Ish_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtSodergan_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtDodatok_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtPrim_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtCane_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtDoc_Oplata_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtPr_Otkaz_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtOpisan_Ved_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void txtForma_Ved_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Ish_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Oplata_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
        }

        private void dtpData_Ved_Validating(object sender, System.ComponentModel.CancelEventArgs e)
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
