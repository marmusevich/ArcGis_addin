using System;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using ESRI.ArcGIS.Framework;
using System.Collections.Generic;

namespace SharedClasses
{
    //псевдоним для списка хранения значений параметров подключения
    using ListPropertySet = List<GeneralDBWork.DataItemForXmlSerialize_IPropertySet>;

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

  
    //работа с баззой
    public static class GeneralDBWork
    {
        //класс для хранения пары ключ-значение параметров подключен к БД
        public class DataItemForXmlSerialize_IPropertySet
        {
            public string Key = "";
            public string Value = "";
            public DataItemForXmlSerialize_IPropertySet(string key, string value)
            {
                Key = key;
                Value = value;
            }
            public DataItemForXmlSerialize_IPropertySet()
            {
                Key = "";
                Value = "";
            }
        }

        //---------------------------------------------------------------------------------------
        #region общее
        //параметры подключения к базе
        private static IPropertySet m_DBConnectPropertySet = null;

        // получить рабочее пространство связоное с базой данных из сервера баз данных
        // dataBase - открываеммая база на сервере
        public static IWorkspace GetWorkspace(string dataBase)
        {
            IWorkspace ret = null;

            // обертка для индикации пользователю
            AddInsAppInfo ai = GeneralApp.GetAddInsAppInfo();
            IMouseCursor appCursor = null;

            if (ai != null && ai.GetThisAddInnApp() != null)
            {
                appCursor = new MouseCursorClass();
                appCursor.SetCursor(2);
            }

            try
            {
                // подключится
                ret = GetWorkSpace_Implementation(dataBase);
            }
            catch (Exception ex) // обработка ошибок
            {
                m_DBConnectPropertySet = null;
                Logger.Write(ex, string.Format("GeneralDBWork.GetWorkspace({0})", dataBase));
                GeneralApp.ShowErrorMessage(string.Format("Подключение к базе не возможно!!\r\nПроверте параметры подключения в файле:\r\n{0}", GetFileName_DBConnectPropertySet()));
            }
            finally
            {
                if (appCursor != null)
                    appCursor.SetCursor(0);
            }

            return ret;
        }
        //собственно подключение здесь
        private static IWorkspace GetWorkSpace_Implementation(string dataBase)
        {
            IWorkspace ret = null;
            // параметры подключения есть?
            if (m_DBConnectPropertySet == null)
            {
                ListPropertySet ips = new ListPropertySet(5);
                //прочесть с диска
                if (!LoadDBConnectPropertySetFromDisk(ref ips) && ips.Count != 5)
                {
                    //устоновить и сохранить на диск
                    ips.Add(new DataItemForXmlSerialize_IPropertySet("DB_CONNECTION_PROPERTIES", "KADASTER12_DATA1"));
                    ips.Add(new DataItemForXmlSerialize_IPropertySet("INSTANCE", @"sde:sqlserver:KADASTER12\DATA1"));
                    ips.Add(new DataItemForXmlSerialize_IPropertySet("DATABASE", dataBase));
                    ips.Add(new DataItemForXmlSerialize_IPropertySet("VERSION", "DBO.DEFAULT"));
                    ips.Add(new DataItemForXmlSerialize_IPropertySet("AUTHENTICATION_MODE", "OSA")); // аунтификация средствами виндовса

                    SaveDBConnectPropertySetToDisk(ref ips);
                }

                if (ips.Count == 5)
                {
                    m_DBConnectPropertySet = new PropertySetClass();
                    foreach (DataItemForXmlSerialize_IPropertySet dps in ips)
                        m_DBConnectPropertySet.SetProperty(dps.Key, dps.Value);
                }
                else
                    throw new Exception("in loaded file not 5 parametrs...");
            }

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

            ret = workspaceFactory.Open(m_DBConnectPropertySet, 0);
            return ret;
        }
        //получить путь и имя к файлу параметров подключения
        private static string GetFileName_DBConnectPropertySet()
        {
            return Path.Combine(GeneralApp.GetAppDataPathAndCreateDirIfNeed(), string.Format("DB_ConnectPropertySet.config.xml"));
        }
        //сохранить на диск параметры подключения к базе
        private static void SaveDBConnectPropertySetToDisk(ref ListPropertySet ips)
        {
            string filename = GetFileName_DBConnectPropertySet();
            using (System.IO.FileStream isoStream = new System.IO.FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer ser = new XmlSerializer(typeof(ListPropertySet));
                ser.Serialize(isoStream, ips);
            }
        }
        //считать параметры подключения к базе с диска
        private static bool LoadDBConnectPropertySetFromDisk(ref ListPropertySet ips)
        {
            bool ret = false;
            try
            {
                string filename = GetFileName_DBConnectPropertySet();
                if (File.Exists(filename))
                {
                    using (System.IO.FileStream isoStream = new System.IO.FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(ListPropertySet));
                        ips = (ListPropertySet)ser.Deserialize(isoStream);
                        ret = true;
                    }
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("GeneralDBWork.LoadDBConnectPropertySetFromDisk"));
            }
            return ret;
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
                Logger.Write(ex, string.Format("GeneralDBWork.GetValueByID('{0}', '{1}', '{2}', '{3}')", workspaceName, id, tableName, fildName));
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
                    ret = (int)row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("GeneralDBWork.GetIDByTextValue('{0}', '{1}', '{2}', '{3}', '{4}')", workspaceName, textValue, tableName, fildName, strongCompare));
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
                    ret = (int)row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("GeneralDBWork.GetIDByIntValue('{0}', '{1}', '{2}', '{3}')", workspaceName, intValue, tableName, fildName));
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
                    ret = (int)row.get_Value(0);
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("GeneralDBWork.GetMaxNumerForAutoicrement('{0}', '{1}', '{2}')", workspaceName, tableName, fildName));
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
                Logger.Write(ex, string.Format("GeneralDBWork.GenerateAutoCompleteStringCollection('{0}', '{1}')", tableName, fildName));
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
}