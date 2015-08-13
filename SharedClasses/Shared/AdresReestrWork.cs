using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Windows.Forms;
namespace SharedClasses
{
    public class AdresReestrWork
    {
        //---------------------------------------------------------------------------------------
        #region работа с картой
        //
        public static void ShowOnMap(string tablName)
        {

        }
        #endregion

        //---------------------------------------------------------------------------------------
        #region работа с таблицами реестров (общее)
        public enum eTypeNazvaFromReestr
        {
            All,
            NazvaKorotkaLat,
            NazvaKorotkaRus,
            NazvaKorotkaUkr,
            NazvaPovnaLat,
            NazvaPovnaRus,
            NazvaPovnaUkr
        }

        //вернуть наименование из реестра 
        public static string GetNazvaByIDFromReestr(string tablName, int id, eTypeNazvaFromReestr type)
        {
            string ret = "";
            if (type != eTypeNazvaFromReestr.All)
            {
                object o = GeneralDBWork.GetValueByID("AdrReestr", id, tablName, type.ToString("F"));
                if (o != null)
                    ret = o.ToString();
            }
            return ret;
        }
        //включить автозаполнение поля по наименованию для реестра 
        public static bool EnableAutoComlectToReestr(string tablName, TextBox reestrValueTextBox, eTypeNazvaFromReestr type)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse;
            if (type == eTypeNazvaFromReestr.All)
            {
                sourse = new AutoCompleteStringCollection();

                foreach (eTypeNazvaFromReestr t in Enum.GetValues(typeof(eTypeNazvaFromReestr)))
                {
                    if (type == eTypeNazvaFromReestr.All) 
                        continue;
                    AutoCompleteStringCollection tmp = GeneralDBWork.GenerateAutoCompleteStringCollection("AdrReestr", tablName, t.ToString("F"));
                    if (tmp != null)
                    {
                        string[] arr = new string[0];
                        tmp.CopyTo(arr, 0);
                        sourse.AddRange(arr);
                    }
                }
            }
            else
            {
                sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("AdrReestr", tablName, type.ToString("F"));
            }
            if (sourse != null)
            {
                reestrValueTextBox.AutoCompleteCustomSource = sourse;
                reestrValueTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                reestrValueTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ret = true;
            }
            reestrValueTextBox.ReadOnly = !ret;
           
            return ret;
        }
        // проверка текстового поля на значения из реестра и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsReestr_SetError(string tablName, TextBox reestrValurChekedValue, ErrorProvider errorProvider, ref int id, eTypeNazvaFromReestr type, TextBox codeValue = null)
        {
            bool ret = true;
            int id_temp = -1;

            if (type == eTypeNazvaFromReestr.All)
            {
                foreach (eTypeNazvaFromReestr t in Enum.GetValues(typeof(eTypeNazvaFromReestr)))
                {
                    if (type == eTypeNazvaFromReestr.All)
                        continue;
                    id_temp = GeneralDBWork.GetIDByTextValue("AdrReestr", reestrValurChekedValue.Text, tablName, t.ToString("F"), true);
                    if (id_temp != -1)
                        break;
                }
            }
            else
                id_temp = GeneralDBWork.GetIDByTextValue("AdrReestr", reestrValurChekedValue.Text, tablName, type.ToString("F"), true);

            if (id_temp != -1)
            {
                errorProvider.SetError(reestrValurChekedValue, String.Empty);
                id = id_temp;
                reestrValurChekedValue.Text = GetNameByIDFromTip_Doc(id);
                if (codeValue != null)
                    codeValue.Text = GetCodeByIDFromTip_Doc(id);
            }
            else
            {
                errorProvider.SetError(reestrValurChekedValue, "Должно быть значение из справочника типов документов");
                ret = false;
            }
            return ret;
        }
        #endregion
        
        //---------------------------------------------------------------------------------------
        #region нумерация в категориях объектов
        //проверить существование кода в справочнике категорий
        public static bool IsCodeKategorObjExist(string tablName, int numer)
        {
            return GeneralDBWork.GetIDByIntValue("AdrReestr", numer, tablName, "KodKategorii") != -1;
        }
        //вернуть следующий код в справочнике категорий
        public static int GetNextCodeKategorObj(string tablName)
        {
            return GeneralDBWork.GetMaxNumerForAutoicrement("AdrReestr", tablName, "KodKategorii") + 1;
        }
        #endregion

        //---------------------------------------------------------------------------------------
        #region получение значения из справочника
        //вернуть код из справочника категорий
        public static string GetKodKategoriiByIDFromKategorObj(string tablName, int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("AdrReestr", id, tablName, "KodKategorii");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть наименование из справочника категорий
        public static string GetNazvaTypuByIDFromKategorObj(string tablName, int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("AdrReestr", id, tablName, "NazvaTypu");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть сокращенное наименование из справочника категорий
        public static string GetKorotkaNazvaTypuByIDFromKategorObj(string tablName, int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("AdrReestr", id, tablName, "KorotkaNazvaTypu");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        
        //вернуть наименование из справочника типов документов
        public static string GetNameByIDFromTip_Doc(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("AdrReestr", id, "Tip_Doc", "Tip_Doc");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть код из справочника типов документов
        public static string GetCodeByIDFromTip_Doc(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("AdrReestr", id, "Tip_Doc", "Kod_Doc");
            if (o != null)
                ret = o.ToString();
            return ret;
        }

        //вернуть код из реестра районов
        public static string GetKodObjectByIDFromAdminRajon(int id)
        {
            string ret = "";
            object o = GeneralDBWork.GetValueByID("AdrReestr", id, "ReestrRajon_polygon", "KodObject");
            if (o != null)
                ret = o.ToString();
            return ret;
        }
        //вернуть наименование из реестра районов
        public static string GetNazvaByIDFromAdminRajon(int id, eTypeNazvaFromReestr type)
        {
            return GetNazvaByIDFromReestr("ReestrRajon_polygon", id, type);
        }
        #endregion

        //---------------------------------------------------------------------------------------
        #region автозаполнение для справочников
        //включить автозаполнение поля по полному наименованию для категории объектов
        public static bool EnableAutoComlectToKategorObj(string tablName, TextBox KategorObjTextBox)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("AdrReestr", tablName, "NazvaTypu");
            if (sourse != null)
            {
                KategorObjTextBox.AutoCompleteCustomSource = sourse;
                KategorObjTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                KategorObjTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                ret = true;
            }
            KategorObjTextBox.ReadOnly = !ret;
            return ret;
        }
        //включить автозаполнение поля по наименованию для типов документов
        public static bool EnableAutoComlectToTip_Doc(TextBox tipDocTextBox)
        {
            bool ret = false;
            AutoCompleteStringCollection sourse = GeneralDBWork.GenerateAutoCompleteStringCollection("AdrReestr", "Tip_Doc", "Tip_Doc");
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
        //включить автозаполнение поля по наименованию для реестра районов
        public static bool EnableAutoComlectToAdminRajon(TextBox adminRajonTextBox)
        {
            bool ret = EnableAutoComlectToReestr("ReestrRajon_polygon", adminRajonTextBox, eTypeNazvaFromReestr.All);
            adminRajonTextBox.ReadOnly = !ret;
            return ret;
        }
        #endregion
        
        //---------------------------------------------------------------------------------------
        #region методы проверок полей ввода
        // проверка текстового поля на значения из справочника категориий объектов и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsKategorObj_SetError(string tablName, TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null)
        {
            bool ret = true;
            int id_temp;
            id_temp = GeneralDBWork.GetIDByTextValue("AdrReestr", chekedValue.Text, tablName, "NazvaTypu", true);
            if (id_temp != -1)
            {
                errorProvider.SetError(chekedValue, String.Empty);
                id = id_temp;
                chekedValue.Text = GetNazvaTypuByIDFromKategorObj(tablName, id);
                if (codeValue != null)
                    codeValue.Text = GetKorotkaNazvaTypuByIDFromKategorObj(tablName, id);
            }
            else
            {
                errorProvider.SetError(chekedValue, "Должно быть значение из справочника Категории объектов");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из справочника типы документов и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsTip_Doc_SetError(TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null)
        {
            bool ret = true;
            int id_temp;

            id_temp = GeneralDBWork.GetIDByTextValue("AdrReestr", chekedValue.Text, "Tip_Doc", "Tip_Doc", true);

            if (id_temp != -1)
            {
                errorProvider.SetError(chekedValue, String.Empty);
                id = id_temp;
                chekedValue.Text = GetNameByIDFromTip_Doc(id);
                if (codeValue != null)
                    codeValue.Text = GetCodeByIDFromTip_Doc(id);
            }
            else
            {
                errorProvider.SetError(chekedValue, "Должно быть значение из справочника типов документов");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на значения из реестра районов и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsContainsAdminRajon_SetError(TextBox chekedValue, ErrorProvider errorProvider, ref int id, TextBox codeValue = null)
        {
            bool ret = CheckValueIsContainsReestr_SetError("ReestrRajon_polygon", chekedValue, errorProvider, ref id, eTypeNazvaFromReestr.All, codeValue);
            return ret;
        }
        #endregion

    }
}
