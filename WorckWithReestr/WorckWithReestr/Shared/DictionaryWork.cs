using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Windows.Forms;

namespace WorckWithReestr
{
    class DictionaryWork
    {
        //---------------------------------------------------------------------------------------
        #region получение значения из справочника
        //вернуть Ф.И.О. из справочника физлиц
        public static string GetFIOByIDFromFizLic(int id)
        {
            string ret = "";
            object o = SharedClass.GetValueByID(id, "fizichni_osoby", "П_І_Б");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть ИНН из справочника физлиц
        public static string GetINNByIDFromFizLic(int id)
        {
            string ret = "";
            object o = SharedClass.GetValueByID(id, "fizichni_osoby", "ідент_код");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть наименование из справочника юр лиц
        public static string GetNameByIDFromJurOsoby(int id)
        {
            string ret = "";
            object o = SharedClass.GetValueByID(id, "jur_osoby", "назва");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть ИНН из справочника юр лиц
        public static string GetINNByIDFromJurOsoby(int id)
        {
            string ret = "";
            object o = SharedClass.GetValueByID(id, "jur_osoby", "код_ЄДРПОУ");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть наименование из справочника типов документов
        public static string GetNameByIDFromTip_Doc(int id)
        {
            string ret = "";
            object o = SharedClass.GetValueByID(id, "Tip_Doc", "Tip_Doc");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть код из справочника типов документов
        public static string GetCodeByIDFromTip_Doc(int id)
        {
            string ret = "";
            object o = SharedClass.GetValueByID(id, "Tip_Doc", "Kod_Doc");
            if (o != null)
                ret = o.ToString();
            return ret;
        }

        #endregion
        //---------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------
        #region автозаполнение для справочников
        //включить автозаполнение поля по ФИО для физических лиц
        public static bool EnableAutoComlectToFizLic(TextBox fizLicTextBox)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse = SharedClass.GenerateAutoCompleteStringCollection("fizichni_osoby", "П_І_Б");
            if (sourse != null)
            {
                fizLicTextBox.AutoCompleteCustomSource = sourse;
                fizLicTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                fizLicTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ret = true;
            }
            fizLicTextBox.ReadOnly = !ret;
            return ret;
        }
        //включить автозаполнение поля по наименованию для юредических лиц
        public static bool EnableAutoComlectToJurLic(TextBox jurLicTextBox)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse = SharedClass.GenerateAutoCompleteStringCollection("jur_osoby", "назва");
            if (sourse != null)
            {
                jurLicTextBox.AutoCompleteCustomSource = sourse;
                jurLicTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                jurLicTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ret = true;
            }
            jurLicTextBox.ReadOnly = !ret;
            return ret;
        }
        //включить автозаполнение поля по наименованию для типов документов
        public static bool EnableAutoComlectToTip_Doc(TextBox tipDocTextBox)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse = SharedClass.GenerateAutoCompleteStringCollection("Tip_Doc", "Tip_Doc");
            if (sourse != null)
            {
                tipDocTextBox.AutoCompleteCustomSource = sourse;
                tipDocTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tipDocTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ret = true;
            }
            tipDocTextBox.ReadOnly = !ret;
            return ret;
        }
        
        #endregion
        //---------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------
        #region методы проверок полей ввода
        // проверка текстового поля на значения из справочника физ лиц и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsFizLic_SetError(TextBox _chekedValue, ErrorProvider _errorProvider, ref int id, TextBox _codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = SharedClass.GetIDByTextValue(_chekedValue.Text, "fizichni_osoby", "П_І_Б", true);

            if (id_temp != -1)
            {
                _errorProvider.SetError(_chekedValue, String.Empty);
                id = id_temp;
                _chekedValue.Text = GetFIOByIDFromFizLic(id);
                if( _codeValue != null)
                    _codeValue.Text = GetINNByIDFromFizLic(id);
            }
            else
            {
                _errorProvider.SetError(_chekedValue, "Должно быть значение из справочника физических лиц");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из справочника юр лиц и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsJurLic_SetError(TextBox _chekedValue, ErrorProvider _errorProvider, ref int id, TextBox _codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = SharedClass.GetIDByTextValue(_chekedValue.Text, "jur_osoby", "назва", true);

            if (id_temp != -1)
            {
                _errorProvider.SetError(_chekedValue, String.Empty);
                id = id_temp;
                _chekedValue.Text = GetNameByIDFromJurOsoby(id);
                if( _codeValue != null)
                    _codeValue.Text = GetINNByIDFromJurOsoby(id);
            }
            else
            {
                _errorProvider.SetError(_chekedValue, "Должно быть значение из справочника юридических лиц");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из справочника типы документов и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsTip_Doc_SetError(TextBox _chekedValue, ErrorProvider _errorProvider, ref int id, TextBox _codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = SharedClass.GetIDByTextValue(_chekedValue.Text, "Tip_Doc", "Tip_Doc", true);

            if (id_temp != -1)
            {
                _errorProvider.SetError(_chekedValue, String.Empty);
                id = id_temp;
                _chekedValue.Text = GetNameByIDFromTip_Doc(id);
                if( _codeValue != null)
                    _codeValue.Text = GetCodeByIDFromTip_Doc(id);
            }
            else
            {
                _errorProvider.SetError(_chekedValue, "Должно быть значение из справочника типов документов");
                ret = false;
            }
            return ret;
        }
        #endregion
        //---------------------------------------------------------------------------------------

    }
}

