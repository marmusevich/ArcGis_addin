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
    public class GeneralDBWork
    {
        //---------------------------------------------------------------------------------------
        #region общее
        //хранит путь к деректории с настройками
        private static string m_applicationDataPath = null;

        //получить путь к коталогу с файлами настройкам програмы, при отсутствии, создать
        public static string GetAppDataPathAndCreateDirIfNeed()
        {
            if (m_applicationDataPath == null)
            {
                m_applicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESRI\\AddInns\\WorckWithKadastr");
            }
            if (!Directory.Exists(m_applicationDataPath))
                Directory.CreateDirectory(m_applicationDataPath); // Создаем директорию, если нужно

            return m_applicationDataPath;
        }
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
        //унифицированый вызов сообщения об ошибке
        public static void ShowErrorMessage(string errorText = "Произошла какая то ошибка!!", string errorCaption = "Ошибка в расширении")
        {
            MessageBox.Show(errorText, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // создать колонки в гриде по таблице ArcGIS
        public static void CreateColumIn(ref DataGridView dgv, ref ITable tableToWrap)
        {
            dgv.Columns.Clear();
            for (int fieldCount = 0; fieldCount < tableToWrap.Fields.FieldCount; fieldCount++)
            {
                IField f = tableToWrap.Fields.get_Field(fieldCount);

                DataGridViewColumn dGVC = null;
                //if ((f.Domain != null) && (string.Compare(f.Domain.Name, "bool", true) == 0))
                //    dGVC = new DataGridViewCheckBoxColumn();
                //else
                    dGVC = new DataGridViewTextBoxColumn();

                dGVC.Name = f.Name;
                dGVC.DataPropertyName = f.Name;
                dGVC.HeaderText = f.AliasName;
                dGVC.ReadOnly = true;
                dGVC.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns.Add(dGVC);
                f = null;
            }
        }
        //получить и сохранить в файле порядок колонок
        public static void GetDisplayOrder(ref DataGridView dgv, string tableName)
        {
            string filename = Path.Combine(GeneralDBWork.GetAppDataPathAndCreateDirIfNeed(), string.Format("{0}_gridColumOrder.config.xml", tableName));
            using (System.IO.FileStream isoStream = new System.IO.FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                int[] displayIndices = new int[dgv.ColumnCount];
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    //displayIndices[i] = dgv.Columns[i].DisplayIndex;
                    displayIndices[dgv.Columns[i].DisplayIndex] = i;
                }
                XmlSerializer ser = new XmlSerializer(typeof(int[]));
                ser.Serialize(isoStream, displayIndices);
            }
        }
        //устоновить порядок колонок из файла
        public static bool SetDisplayOrder(ref DataGridView dgv, string tableName)
        {
            bool ret = false;
            try
            {
                string filename = Path.Combine(GeneralDBWork.GetAppDataPathAndCreateDirIfNeed(), string.Format("{0}_gridColumOrder.config.xml", tableName));
                if(File.Exists(filename))
                {
                    using (System.IO.FileStream isoStream = new System.IO.FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(int[]));
                        int[] displayIndicies = (int[])ser.Deserialize(isoStream);
                        SetDisplayOrderByArray(ref dgv, displayIndicies);
                        ret = true;
                    }
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClass.SetDisplayOrder('{0}')", tableName));
            }
                return ret;
        }
        //устоновить порядок колонок по массиву
        public static void SetDisplayOrderByArray(ref DataGridView dgv, int[] displayIndices)
        {
            for (int i = 0; i < displayIndices.Length; i++)
            {
                //dgv.Columns[i].DisplayIndex = displayIndicies[i];
                dgv.Columns[displayIndices[i]].DisplayIndex = i;
            }
        }
        //получить дату первого дня месяца
        public static DateTime GetFirstMonthDayDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        //получить дату последнего дня месяца
        public static DateTime GetLastMonthDayDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region преобразование значений
        //преобразует значение из базы с предпологаемым домено BOOL  в  BOOL
        public static bool ConvertVolueToBool(object obj)
        {
            if ((obj != null) && !Convert.IsDBNull(obj) && Convert.ToBoolean(obj))
                return true;
            else
                return false;
        }
        //преобразует значение из базы в DateTime
        public static DateTime ConvertVolueToDateTime(object obj)
        {
            if ((obj != null) && !Convert.IsDBNull(obj))
                return Convert.ToDateTime(obj);
            else
                return DateTime.Now;
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

    //для форм списков котрые будут поддерживать поиск в таблице
    interface IListFormFilterMetods
    {
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName);
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