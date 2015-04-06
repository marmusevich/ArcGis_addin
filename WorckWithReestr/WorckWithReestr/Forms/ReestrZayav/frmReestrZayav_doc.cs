using ESRI.ArcGIS.Geodatabase;
using System;

// получить умолчательные значения для справочников
// привязать к справочнику
//по заполнять автозаполнение

//        private void OnFormLoad() - вынести в базовый


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

        #endregion
        
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected override void DB_to_FormElement(IRow row)
        {
            //даты
            dtpData_Z.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Z")));
            dtpData_Ish.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Ish")));
            dtpData_Oplata.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Oplata")));
            dtpData_Ved.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Ved")));

            ////простые тексты  
            txtTel_Z.Text = "" + row.get_Value(base.table.FindField("Tel_Z")) as string;
            txtPrim.Text = "" + row.get_Value(base.table.FindField("Prim")) as string;
            txtOpisan_Ved.Text = "" + row.get_Value(base.table.FindField("Opisan_Ved")) as string;
            txtForma_Ved.Text = "" + row.get_Value(base.table.FindField("Forma_Ved")) as string;
            txtN_Ish_Z.Text = "" + row.get_Value(base.table.FindField("N_Ish_Z")) as string;
            txtSodergan.Text = "" + row.get_Value(base.table.FindField("Sodergan")) as string;
            txtDoc_Oplata.Text = "" + row.get_Value(base.table.FindField("Doc_Oplata")) as string;
            txtPr_Otkaz.Text = "" + row.get_Value(base.table.FindField("Pr_Otkaz")) as string;
            txtDodatok.Text = "" + row.get_Value(base.table.FindField("Dodatok")) as string;
            txtCane.Text = "" + row.get_Value(base.table.FindField("Cane")) as string;

            //доменные значения
            object o = null;
            o = row.get_Value(base.table.FindField("Status"));
            if (o == null)
                o = row.Fields.get_Field(base.table.FindField("Status")).DefaultValue;
            cbStatus.SelectedIndex = ddaStatus.GetIndexByValue(o);

            o = row.get_Value(base.table.FindField("Otkaz"));
            if (o == null)
                o = row.Fields.get_Field(base.table.FindField("Otkaz")).DefaultValue;
            cbOtkaz.SelectedIndex = ddaOtkaz.GetIndexByValue(Convert.ToInt16(o));

            o = row.get_Value(base.table.FindField("Oplata"));
            if (o == null)
                o = row.Fields.get_Field(base.table.FindField("Oplata")).DefaultValue;
            cbOplata.SelectedIndex = ddaOplata.GetIndexByValue(Convert.ToInt16(o)); 

            // справочники
            mTip_Doc = row.get_Value(base.table.FindField("Tip_Doc"));
            mKod_Z = row.get_Value(base.table.FindField("Kod_Z"));
            mFio_Ved_Vid = row.get_Value(base.table.FindField("Fio_Ved_Vid"));
            mFio_Ved_Prin = row.get_Value(base.table.FindField("Fio_Ved_Prin"));
            mFio_Z = row.get_Value(base.table.FindField("Fio_Z"));

            OnChangedFio_Z();
            OnChangedKod_Z();
            OnChangedFio_Ved_Vid();
            OnChangedFio_Ved_Prin();
            OnChangedTipDoc();

            // порядковый номер ??
            //N_Z, Type = esriFieldTypeInteger, AliasName = № пп 
            txtN_Z.Text = "" + row.get_Value(base.table.FindField("N_Z")) as string;

        }

        protected override void FormElement_to_DB(IRow row)
        {
            //даты
            row.set_Value(base.table.FindField("Data_Z"), dtpData_Z.Value);
            row.set_Value(base.table.FindField("Data_Ish"), dtpData_Ish.Value);
            row.set_Value(base.table.FindField("Data_Oplata"), dtpData_Oplata.Value);
            row.set_Value(base.table.FindField("Data_Ved"), dtpData_Ved.Value);

            //простые тексты  
            row.set_Value(base.table.FindField("Tel_Z"), txtTel_Z.Text);
            row.set_Value(base.table.FindField("Prim"), txtPrim.Text);
            row.set_Value(base.table.FindField("Opisan_Ved"), txtOpisan_Ved.Text);
            row.set_Value(base.table.FindField("Forma_Ved"), txtForma_Ved.Text);
            row.set_Value(base.table.FindField("N_Ish_Z"), txtN_Ish_Z.Text);
            row.set_Value(base.table.FindField("Sodergan"), txtSodergan.Text);
            row.set_Value(base.table.FindField("Doc_Oplata"), txtDoc_Oplata.Text);
            row.set_Value(base.table.FindField("Pr_Otkaz"), txtPr_Otkaz.Text);
            row.set_Value(base.table.FindField("Dodatok"), txtDodatok.Text);
            row.set_Value(base.table.FindField("Cane"), txtCane.Text);

            //доменные значения
            row.set_Value(base.table.FindField("Status"), ((DomeinData)cbStatus.SelectedItem).Value);
            row.set_Value(base.table.FindField("Otkaz"), ((DomeinData)cbOtkaz.SelectedItem).Value);
            row.set_Value(base.table.FindField("Oplata"), ((DomeinData)cbOplata.SelectedItem).Value);

            // справочники
            row.set_Value(base.table.FindField("Tip_Doc"), mTip_Doc);
            row.set_Value(base.table.FindField("Kod_Z"), mKod_Z);
            row.set_Value(base.table.FindField("Fio_Ved_Vid"), mFio_Ved_Vid);
            row.set_Value(base.table.FindField("Fio_Ved_Prin"), mFio_Ved_Prin);
            row.set_Value(base.table.FindField("Fio_Z"), mFio_Z);

            // порядковый номер ??
            //N_Z, Type = esriFieldTypeInteger, AliasName = № пп 
            int N_Z = Convert.ToInt32(txtN_Z.Text);
            row.set_Value(base.table.FindField("N_Z"), N_Z);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = SharedClass.CheckValueIsInt_SetError(txtN_Z, errorProvider) && ret;
            ret = DictionaryWork.CheckValueIsContainsFizLic_SetError(txtFio_Z, errorProvider, ref mFio_Z) && ret;
            ret = DictionaryWork.CheckValueIsContainsFizLic_SetError(txtFio_Ved_Vid, errorProvider, ref mFio_Ved_Vid) && ret;
            ret = DictionaryWork.CheckValueIsContainsFizLic_SetError(txtFio_Ved_Prin, errorProvider, ref mFio_Ved_Prin) && ret;
            if (cbStatus.SelectedIndex == 0)
            {
                ret = DictionaryWork.CheckValueIsContainsJurLic_SetError(txtKod_Z, errorProvider, ref mKod_Z, txtKod_Z_code) && ret;
            }
            else
            {
                ret = DictionaryWork.CheckValueIsContainsFizLic_SetError(txtKod_Z, errorProvider, ref mKod_Z, txtKod_Z_code) && ret;
            }
            ret = DictionaryWork.CheckValueIsContainsTip_Doc_SetError(txtTip_Doc, errorProvider, ref mTip_Doc, txtTip_Doc_code) && ret;


            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            //доменные значения
            object o = null;
            ddaStatus = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("Status")).Domain);
            cbStatus.Items.AddRange(ddaStatus.ToArray());
            o = base.table.Fields.get_Field(base.table.FindField("Status")).DefaultValue;
            cbStatus.SelectedIndex = ddaStatus.GetIndexByValue(Convert.ToInt16(o));

            ddaOtkaz = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("Otkaz")).Domain);
            cbOtkaz.Items.AddRange(ddaOtkaz.ToArray());
            o = base.table.Fields.get_Field(base.table.FindField("Otkaz")).DefaultValue;
            cbOtkaz.SelectedIndex = ddaOtkaz.GetIndexByValue(Convert.ToInt16(o));

            ddaOplata = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("Oplata")).Domain);
            cbOplata.Items.AddRange(ddaOplata.ToArray());
            o = base.table.Fields.get_Field(base.table.FindField("Oplata")).DefaultValue;
            cbOplata.SelectedIndex = ddaOplata.GetIndexByValue(Convert.ToInt16(o));


            // справочники
            DictionaryWork.EnableAutoComlectToFizLic(txtFio_Z);
            DictionaryWork.EnableAutoComlectToFizLic(txtFio_Ved_Prin);
            DictionaryWork.EnableAutoComlectToFizLic(txtFio_Ved_Vid);
            if (cbStatus.SelectedIndex == 0)
                DictionaryWork.EnableAutoComlectToJurLic(txtKod_Z);
            else
                DictionaryWork.EnableAutoComlectToFizLic(txtKod_Z);

            DictionaryWork.EnableAutoComlectToFizLic(txtTip_Doc);

        }

        protected override void DB_DefaultValue_to_FormElement()
        {
            //алгоритм генерации номера, запрос большего из базы или последнего
            txtN_Z.Text = DocumentWork.GetNextNumerToReestrZayav().ToString();
        }

        private void OnChangedTipDoc()
        {
            txtTip_Doc.Text = DictionaryWork.GetNameByIDFromTip_Doc(mTip_Doc);
            txtTip_Doc_code.Text = DictionaryWork.GetCodeByIDFromTip_Doc(mTip_Doc);
        }

        private void OnChangedFio_Ved_Prin()
        {
            txtFio_Ved_Prin.Text = DictionaryWork.GetFIOByIDFromFizLic(mFio_Ved_Prin);
        }

        private void OnChangedFio_Ved_Vid()
        {
            txtFio_Ved_Vid.Text = DictionaryWork.GetFIOByIDFromFizLic(mFio_Ved_Vid);
        }

        private void OnChangedKod_Z()
        {
            if (cbStatus.SelectedIndex == 0)
            {
                txtKod_Z.Text = DictionaryWork.GetNameByIDFromJurOsoby(mKod_Z);
                txtKod_Z_code.Text = DictionaryWork.GetINNByIDFromJurOsoby(mKod_Z);
            }
            else
            {
                txtKod_Z.Text = DictionaryWork.GetFIOByIDFromFizLic(mKod_Z);
                txtKod_Z_code.Text = DictionaryWork.GetINNByIDFromFizLic(mKod_Z);
            }
        }

        private void OnChangedFio_Z()
        {
            txtFio_Z.Text = DictionaryWork.GetFIOByIDFromFizLic(mFio_Z);
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
            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.Kn_Reg_Zayv";
        }

        private void frmReestrZayav_doc_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового заявления / обращения";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных заявления / обращения";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление заявления / обращения";
                    break;
                default:
                    this.Close();
                    return;
            }
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  справочник
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void btnFio_Z_Click(object sender, EventArgs e)
        {
            string filteredString = "";
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
                string filteredString = "";
                mKod_Z = frmFizLic_list.ShowForSelect(filteredString);
            }
            OnChangedKod_Z();
            errorProvider.SetError(txtKod_Z, String.Empty);
            //ValidatingData();
        }

        private void btnFio_Ved_Prin_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mFio_Ved_Prin = frmFizLic_list.ShowForSelect(filteredString);
            OnChangedFio_Ved_Prin();
            errorProvider.SetError(txtFio_Ved_Prin, String.Empty);
            //ValidatingData();
        }

        private void btnFio_Ved_Vid_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mFio_Ved_Vid = frmFizLic_list.ShowForSelect(filteredString);
            OnChangedFio_Ved_Vid();
            errorProvider.SetError(txtFio_Ved_Vid, String.Empty);
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

        private void txtN_Z_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            isModified = true;
            mKod_Z = -1;
            txtKod_Z.Text = "";
            txtKod_Z_code.Text = "";

            if (cbStatus.SelectedIndex == 0)
                DictionaryWork.EnableAutoComlectToJurLic(txtKod_Z);
            else
                DictionaryWork.EnableAutoComlectToFizLic(txtKod_Z);
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

        #endregion

        private void frmReestrZayav_doc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }


    }
}

#region  описание полей


////ключь
//OBJECTID, Type = esriFieldTypeOID 

////даты
//Data_Z, Type = esriFieldTypeDate, AliasName = Дата звернення 
//Data_Ish, Type = esriFieldTypeDate, AliasName = Дата листа 
//Data_Oplata, Type = esriFieldTypeDate, AliasName = Дата оплати 
//Data_Ved, Type = esriFieldTypeDate, AliasName = Дата видачі 

////простые тексты  
//N_Ish_Z, Type = esriFieldTypeString, AliasName = Вихідний № листа 
//Sodergan, Type = esriFieldTypeString, AliasName = Стислий зміст 
//Pr_Otkaz, Type = esriFieldTypeString, AliasName = Причина відмови 
//Tel_Z, Type = esriFieldTypeString, AliasName = Телефон / e-mail заявника 
//Doc_Oplata, Type = esriFieldTypeString, AliasName = Документ підтвердження оплати 
//Opisan_Ved, Type = esriFieldTypeString, AliasName = Опис наданних документів 
//Forma_Ved, Type = esriFieldTypeString, AliasName = Форма передачі документа 
//Prim, Type = esriFieldTypeString, AliasName = Примечание 
//Dodatok, Type = esriFieldTypeString, AliasName = Перелік доданніх матеріалів 
//Cane, Type = esriFieldTypeString, AliasName = Канцелярскій № входящій





////доменные значения
//Status, Type = esriFieldTypeString, AliasName = Статус заявника 
//Domain = StatusZayavitelya xsi:type="esri:CodedValue"
//Code xsi:type="xs:string" (Ю = Юредическое лицо )
//Code xsi:type="xs:string" (Ф = Физическое лицо )
////--
//Otkaz, Type = esriFieldTypeSmallInteger, AliasName = Відмітка про відмову 
//Domain = OtmetkaProOtkaz xsi:type="esri:CodedValueDomain"
//Code xsi:type="xs:short" (0 = Прийнято )
//Code xsi:type="xs:short" (1 = Відмова )
////--
//Oplata, Type = esriFieldTypeSmallInteger, AliasName = Статус надання послуги 
//Domain = StatusNadannaPjslugi xsi:type="esri:CodedValueDomain"  
//Code xsi:type="xs:short" (0 = Платна)
//Code xsi:type="xs:short" (1 = Безоплатна)

// // справочники
//Tip_Doc, Type = esriFieldTypeInteger, AliasName = Тип документа 
////--
//Fio_Z, Type = esriFieldTypeInteger, AliasName = ПІБ особи, що прийняла звернення 
//Fio_Ved_Vid, Type = esriFieldTypeInteger, AliasName = ФІО особи , що видала документ 
//Fio_Ved_Prin, Type = esriFieldTypeInteger, AliasName = ФІО особи , що прийняла документ 
//Kod_Z, Type = esriFieldTypeInteger, AliasName = Код заявника 

//// порядковый номер ??
//N_Z, Type = esriFieldTypeInteger, AliasName = № пп 

#endregion

