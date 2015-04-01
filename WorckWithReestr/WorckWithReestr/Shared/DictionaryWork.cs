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
        // проверка текстового поля на содержание Smal Int (16 бит) и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsSmalInt_SetError(TextBox _chekedValue, ErrorProvider _errorProvider)
        {
            bool ret = true;
            //try
            //{
            //    short numVal = Convert.ToInt16(_chekedValue.Text);
            //    _errorProvider.SetError(_chekedValue, String.Empty);
            //}
            //catch (FormatException e)
            //{
            //    _errorProvider.SetError(_chekedValue, "Должно быть число.");
            //    ret = false;
            //}
            //catch (OverflowException e)
            //{
            //    _errorProvider.SetError(_chekedValue, "Слишком большое число.");
            //    ret = false;
            //}
            return ret;
        }

        #endregion
        //---------------------------------------------------------------------------------------

    }
}
