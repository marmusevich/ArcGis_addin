using System;
using System.Windows.Forms;


namespace SharedClasses
{
    //общие функции для работы со справочниками
    public static class ReestrDictionaryWork
    {
        public enum AdmRajFieldSufix
        {
            UKR, ROS, LAT
        }


        //---------------------------------------------------------------------------------------
        #region получение значения из справочника
        //вернуть Ф.И.О. из справочника физлиц
        public static string GetFIOByIDFromFizLic(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "fizichni_osoby", "P_I_B");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть ИНН из справочника физлиц
        public static string GetINNByIDFromFizLic(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "fizichni_osoby", "ident_kod");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть наименование из справочника юр лиц
        public static string GetNameByIDFromJurOsoby(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "jur_osoby", "nazva");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть ИНН из справочника юр лиц
        public static string GetINNByIDFromJurOsoby(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "jur_osoby", "kod_EDRPOY");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть наименование из справочника типов документов
        public static string GetNameByIDFromTip_Doc(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "Tip_Doc", "Tip_Doc");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть код из справочника типов документов
        public static string GetCodeByIDFromTip_Doc(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "Tip_Doc", "Kod_Doc");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть название района из справочника районов
        public static string GetNazvaByIDFromAdmRaj(int id, AdmRajFieldSufix arfs = AdmRajFieldSufix.UKR)
        {
            string fildname = "NAZVA_UKR";
            if(arfs ==AdmRajFieldSufix.ROS)
                fildname = "NAZVA_ROS";
            if (arfs == AdmRajFieldSufix.LAT)
                fildname = "NAZVA_LAT";

            string ret = "";
            object o = GeneralDBWork.GetValueByID("Kadastr2016", id, "Rej_Adm_Raj_Mis", fildname);
            if (o != null)
                ret = o.ToString();
            return ret;
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region автозаполнение для справочников
        //включить автозаполнение поля по ФИО для физических лиц
        public static bool EnableAutoComlectToFizLic(TextBox fizLicTextBox)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("Kadastr2016", "fizichni_osoby", "P_I_B");
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
            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("Kadastr2016", "jur_osoby", "nazva");
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
            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("Kadastr2016", "Tip_Doc", "Tip_Doc");
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
        //включить автозаполнение поля по  название района для справочника районов
        public static bool EnableAutoComlectToAdmRaj(TextBox fizLicTextBox, AdmRajFieldSufix arfs = AdmRajFieldSufix.UKR)
        {
            string fildname = "NAZVA_UKR";
            if (arfs == AdmRajFieldSufix.ROS)
                fildname = "NAZVA_ROS";
            if (arfs == AdmRajFieldSufix.LAT)
                fildname = "NAZVA_LAT";

            bool ret = false;
            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("Kadastr2016", "Rej_Adm_Raj_Mis", fildname);
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

        #endregion
        //---------------------------------------------------------------------------------------
        #region методы проверок полей ввода
        // проверка текстового поля на значения из справочника физ лиц и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsFizLic_SetError(TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = GeneralDBWork.GetIDByTextValue("Kadastr2016", chekedValue.Text, "fizichni_osoby", "P_I_B", true);

            if (id_temp != -1)
            {
                errorProvider.SetError(chekedValue, String.Empty);
                id = id_temp;
                chekedValue.Text = GetFIOByIDFromFizLic(id);
                if( codeValue != null)
                    codeValue.Text = GetINNByIDFromFizLic(id);
            }
            else
            {
                errorProvider.SetError(chekedValue, "Должно быть значение из справочника физических лиц");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из справочника юр лиц и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsJurLic_SetError(TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = GeneralDBWork.GetIDByTextValue("Kadastr2016", chekedValue.Text, "jur_osoby", "nazva", true);

            if (id_temp != -1)
            {
                errorProvider.SetError(chekedValue, String.Empty);
                id = id_temp;
                chekedValue.Text = GetNameByIDFromJurOsoby(id);
                if( codeValue != null)
                    codeValue.Text = GetINNByIDFromJurOsoby(id);
            }
            else
            {
                errorProvider.SetError(chekedValue, "Должно быть значение из справочника юридических лиц");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из справочника типы документов и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsTip_Doc_SetError(TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = GeneralDBWork.GetIDByTextValue("Kadastr2016", chekedValue.Text, "Tip_Doc", "Tip_Doc", true);

            if (id_temp != -1)
            {
                errorProvider.SetError(chekedValue, String.Empty);
                id = id_temp;
                chekedValue.Text = GetNameByIDFromTip_Doc(id);
                if( codeValue != null)
                    codeValue.Text = GetCodeByIDFromTip_Doc(id);
            }
            else
            {
                errorProvider.SetError(chekedValue, "Должно быть значение из справочника типов документов");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из справочника районов и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsAdmRaj_SetError(TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null, AdmRajFieldSufix arfs = AdmRajFieldSufix.UKR)
        {
            string fildname = "NAZVA_UKR";
            if (arfs == AdmRajFieldSufix.ROS)
                fildname = "NAZVA_ROS";
            if (arfs == AdmRajFieldSufix.LAT)
                fildname = "NAZVA_LAT";

            bool ret = true;
            int id_temp;

            id_temp = GeneralDBWork.GetIDByTextValue("Kadastr2016", chekedValue.Text, "Rej_Adm_Raj_Mis", fildname, true);

            if (id_temp != -1)
            {
                errorProvider.SetError(chekedValue, String.Empty);
                id = id_temp;
                chekedValue.Text = GetNazvaByIDFromAdmRaj(id);
                //if (codeValue != null)
                //    codeValue.Text = (id);
            }
            else
            {
                errorProvider.SetError(chekedValue, "Должно быть значение из справочника административных районов города");
                ret = false;
            }
            return ret;
        }
        #endregion
    }
}

