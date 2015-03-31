using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorckWithReestr
{
    class DictionaryWork
    {

        #region получение значения из справочника


        //вернуть Ф.И.О. из справочника физлиц
        public static string GetFIOByIDFromFizLic(int id)
        {
            object o = SharedClass.GetValueByID(id, "fizichni_osoby", "П_І_Б");
            if (o == null)
                return "";
            else
                return o.ToString();
        }
        //вернуть ИНН из справочника физлиц
        public static string GetINNByIDFromFizLic(int id)
        {
            object o = SharedClass.GetValueByID(id, "fizichni_osoby", "ідент_код");
            if (o == null)
                return "";
            else
                return o.ToString();
        }

        //вернуть наименование из справочника юр лиц
        public static string GetNameByIDFromJurOsoby(int id)
        {
            object o = SharedClass.GetValueByID(id, "jur_osoby", "назва");
            if (o == null)
                return "";
            else
                return o.ToString();
        }
        //вернуть ИНН из справочника юр лиц
        public static string GetINNByIDFromJurOsoby(int id)
        {
            object o = SharedClass.GetValueByID(id, "jur_osoby", "код_ЄДРПОУ");
            if (o == null)
                return "";
            else
                return o.ToString();
        }

        //вернуть наименование из справочника типов документов
        public static string GetNameByIDFromTip_Doc(int id)
        {
            object o = SharedClass.GetValueByID(id, "Tip_Doc", "Tip_Doc");
            if (o == null)
                return "";
            else
                return o.ToString();
        }
        //вернуть код из справочника типов документов
        public static string GetCodeByIDFromTip_Doc(int id)
        {
            object o = SharedClass.GetValueByID(id, "Tip_Doc", "Kod_Doc");
            if (o == null)
                return "";
            else
                return o.ToString();
        }
        #endregion


        #region автозаполнение для справочников
        // получить лист для автозаполнения
        public static System.Windows.Forms.AutoCompleteStringCollection GenerateAutoCompleteStringCollection(string tableName, string fildName)
        {
            ArrayList data = new ArrayList();
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = "reestr.DBO." + tableName;
                queryDef2.SubFields = "DISTINCT " + fildName;
                queryDef2.PostfixClause = "ORDER BY " + fildName;
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                while ((row = cursor.NextRow()) != null)
                {
                    data.Add(row.get_Value(0));
                }
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                return null;
            }
            
            System.Windows.Forms.AutoCompleteStringCollection source = new System.Windows.Forms.AutoCompleteStringCollection();
            foreach (object o in data)
            {
                source.Add(o.ToString());
            }
            return source;
        }

        public static bool EnableAutoComlectToFizLic(System.Windows.Forms.TextBox fizLicTextBox)
        {
            System.Windows.Forms.AutoCompleteStringCollection sourse = GenerateAutoCompleteStringCollection("fizichni_osoby", "П_І_Б");
            if (sourse != null)
            {
                fizLicTextBox.ReadOnly = false;
                fizLicTextBox.AutoCompleteCustomSource = sourse;
                fizLicTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                fizLicTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                return true;
            }
            else
            {
                fizLicTextBox.ReadOnly = true;
                return false;
            }
        }

        public static bool EnableAutoComlectToJurLic(System.Windows.Forms.TextBox jurLicTextBox)
        {
            System.Windows.Forms.AutoCompleteStringCollection sourse = GenerateAutoCompleteStringCollection("jur_osoby", "назва");
            if (sourse != null)
            {
                jurLicTextBox.ReadOnly = false;
                jurLicTextBox.AutoCompleteCustomSource = sourse;
                jurLicTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                jurLicTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                return true;
            }
            else
            {
                jurLicTextBox.ReadOnly = true;
                return false;
            }
        }

        public static bool EnableAutoComlectToTip_Doc(System.Windows.Forms.TextBox tipDocTextBox)
        {
            System.Windows.Forms.AutoCompleteStringCollection sourse = GenerateAutoCompleteStringCollection("Tip_Doc", "Tip_Doc");
            if (sourse != null)
            {
                tipDocTextBox.ReadOnly = false;
                tipDocTextBox.AutoCompleteCustomSource = sourse;
                tipDocTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                tipDocTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                return true;
            }
            else
            {
                tipDocTextBox.ReadOnly = true;
                return false;
            }
        }
        #endregion



    }
}
