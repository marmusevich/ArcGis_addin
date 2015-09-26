using System;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Collections;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using ESRI.ArcGIS.Framework;


namespace SharedClasses
{
    //общие методы
    public static class GeneralDBWork
    {
        //---------------------------------------------------------------------------------------
        #region общее
        // получить рабочее пространство связоное с базой данных из сервера баз данных
        // dataBase - открываеммая база на сервере
        public static IWorkspace GetWorkspace(string dataBase)
        {
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("DB_CONNECTION_PROPERTIES", "KADASTER12_DATA1");
            propertySet.SetProperty("INSTANCE", @"sde:sqlserver:KADASTER12\DATA1");
            propertySet.SetProperty("DATABASE", dataBase);
            propertySet.SetProperty("VERSION", "DBO.DEFAULT");
            propertySet.SetProperty("AUTHENTICATION_MODE", "OSA"); // аунтификация средствами виндовса

            // добавить обработку ошибки

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);
        }
        
        #endregion

        //---------------------------------------------------------------------------------------
        #region методы проверок полей ввода
        // проверка текстового поля на содержание Smal Int (16 бит) и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsSmalInt_SetError(TextBox _chekedValue, ErrorProvider _errorProvider)
        {
            bool ret = true;
            try
            {
                short numVal = Convert.ToInt16(_chekedValue.Text);
                _errorProvider.SetError(_chekedValue, String.Empty);
            }
            catch (FormatException)
            {
                _errorProvider.SetError(_chekedValue, "Должно быть число.");
                ret = false;
            }
            catch (OverflowException)
            {
                _errorProvider.SetError(_chekedValue, "Слишком большое число.");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на содержание Int (32 бит) и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsInt_SetError(TextBox _chekedValue, ErrorProvider _errorProvider)
        {
            bool ret = true;
            try
            {
                int numVal = Convert.ToInt32(_chekedValue.Text);
                _errorProvider.SetError(_chekedValue, String.Empty);
            }
            catch (FormatException)
            {
                _errorProvider.SetError(_chekedValue, "Должно быть число.");
                ret = false;
            }
            catch (OverflowException)
            {
                _errorProvider.SetError(_chekedValue, "Слишком большое число.");
                ret = false;
            }
            return ret;
        }
        // проверка текстового поля на содержание Double и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsDouble_SetError(TextBox _chekedValue, ErrorProvider _errorProvider)
        {
            bool ret = true;
            try
            {
                double numVal = Convert.ToDouble(_chekedValue.Text);
                _errorProvider.SetError(_chekedValue, String.Empty);
            }
            catch (FormatException)
            {
                _errorProvider.SetError(_chekedValue, "Должно быть число.");
                ret = false;
            }
            catch (OverflowException)
            {
                _errorProvider.SetError(_chekedValue, "Слишком большое число.");
                ret = false;
            }
            return ret;
        }

        // остальные числовые значения

        // проверка текстового поля на не пуста при содержании текста и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueStringNotEmpty_SetError(TextBox _chekedValue, ErrorProvider _errorProvider)
        {
            bool ret = true;

            if (_chekedValue.Text == null || _chekedValue.Text == "")
            {
                _errorProvider.SetError(_chekedValue, "Строка должна быть не пустой.");
                ret = false;
            }
            else
            {
                _errorProvider.SetError(_chekedValue, String.Empty);
            }
            return ret;
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region механизм обращения к базе
        //универсальный метод получение значения из таблицы
        public static object GetValueByID(string workspaceName, int id, string tableName, string fildName)
        {
            object ret = null;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace( workspaceName ) as IFeatureWorkspace;
                ITable table = fws.OpenTable(workspaceName + ".DBO." + tableName);
                IRow row = table.GetRow(id);
                ret =  row.get_Value(table.FindField(fildName));
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClass.GetValueByID('{0}', '{1}', '{2}', '{3}')", workspaceName, id, tableName, fildName));
            }
            return ret;
        }
        //получение ID из таблицы по значению текстового поля
        public static int GetIDByTextValue(string workspaceName, string textValue, string tableName, string fildName, bool strongCompare = false)
        {
            int ret = -1;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(workspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = workspaceName + ".DBO." + tableName;
                queryDef2.SubFields = "DISTINCT TOP 1 OBJECTID";
                if (strongCompare)
                    queryDef2.WhereClause = fildName + " = '" + textValue + "'";
                else
                    queryDef2.WhereClause = fildName + " like '%" + textValue + "%'";
                //queryDef2.PostfixClause = "ORDER BY " + fildName;
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    ret = row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClass.GetIDByTextValue('{0}', '{1}', '{2}', '{3}', '{4}')", workspaceName, textValue, tableName, fildName, strongCompare));
            }
            return ret;
        }
        //получение ID из таблицы по значению числового поля
        public static int GetIDByIntValue(string workspaceName, int intValue, string tableName, string fildName)
        {
            int ret = -1;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(workspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = workspaceName + ".DBO." + tableName;
                queryDef2.SubFields = "DISTINCT TOP 1 OBJECTID";
                queryDef2.WhereClause = fildName + " = " + intValue.ToString();
                //queryDef2.PostfixClause = "ORDER BY " + fildName;
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    ret = row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClass.GetIDByIntValue('{0}', '{1}', '{2}', '{3}')", workspaceName, intValue, tableName, fildName));
            }
            return ret;
        }
        // возвращает максимальное значение цыфрового поля из таблицы например для автоинкриманта порядковых номеров
        public static int GetMaxNumerForAutoicrement(string workspaceName, string tableName, string fildName)
        {
            int ret = 0;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(workspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = workspaceName + ".DBO." + tableName;
                queryDef2.SubFields = "MAX( " + fildName + " )";
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    ret = row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClasses.GetMaxNumerForAutoicrement('{0}', '{1}', '{2}')", workspaceName, tableName, fildName));
            }
            return ret;
        }
        #endregion
        //---------------------------------------------------------------------------------------
        #region автозаполнение полей
        // получить лист для автозаполнения
        public static AutoCompleteStringCollection GenerateAutoCompleteStringCollection(string workspaceName, string tableName, string fildName)
        {
            AutoCompleteStringCollection ret = null;
            ArrayList data = new ArrayList();
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(workspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = workspaceName + ".DBO." + tableName;
                queryDef2.SubFields = "DISTINCT " + fildName;
                queryDef2.PostfixClause = "ORDER BY " + fildName;
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                while ((row = cursor.NextRow()) != null)
                {
                    data.Add(row.get_Value(0));
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClass.GenerateAutoCompleteStringCollection('{0}', '{1}')", tableName, fildName));
            }

            if (data != null && data.Count > 0)
            {
                ret = new AutoCompleteStringCollection();
                foreach (object o in data)
                {
                    ret.Add(o.ToString());
                }
            }
            return ret;
        }
        #endregion
        //---------------------------------------------------------------------------------------
    }

    //для форм элементов, работа с элементами управления и значениями из базы
    interface IElementFormWorckWithControlsAndDB
    {
        //создать адаптер домена, установить лист значений комбобокса, и установить значение по умолчанию
        void CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref ComboBox cb, ref DomeinDataAdapter dda, string fildName);
        //устоновить значение комбобокса по значению, если нет адаптера - создать, если нет значения устоновить по умолчанию
        void CheсkValueAndSetToComboBox(ref ComboBox cb, ref DomeinDataAdapter dda, string fildName, object value);
        
        //прочесть значение из базы
        object GetValueFromDB(ref IRow row, string fildName);
        //установить значение элемента управления тип текст
        void SetStringValueFromDBToTextBox(ref IRow row, string fildName, TextBox textBox);
        //установить значение элемента управления тип число
        void SetIntValueFromDBToTextBox(ref IRow row, string fildName, TextBox textBox);
        //установить значение элемента управления тип дата
        void SetDateValueFromDBToDateTimePicker(ref IRow row, string fildName, DateTimePicker dateTimePicker);

        //сохранить в базу значение элемента управления тип доменные значения
        void SaveDomeinDataValueFromComboBoxToDB(ref IRow row, string fildName, ref ComboBox cb);
        //сохранить в базу значение элемента управления тип текст
        void SaveStringValueFromTextBoxToDB(ref IRow row, string fildName, TextBox textBox);
        //сохранить в базу значение элемента управления тип число
        void SaveIntValueFromTextBoxToDB(ref IRow row, string fildName, TextBox textBox);
        //сохранить в базу значение элемента управления тип дата
        void SaveDateValueFromDateTimePickerToDB(ref IRow row, string fildName, DateTimePicker dateTimePicker);
    }
}