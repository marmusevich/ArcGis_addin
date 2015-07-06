using System;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Collections;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;


namespace WorckWithReestr
{
    //общие методы
    class SharedClass
    {
        //---------------------------------------------------------------------------------------
        #region общее
        //хранит путь к деректории с настройками
        private static string m_applicationDataPath = null;
        // хранит имя осномного пространства (имя базы данных)
        public static string WorkspaceName = "reestr";

        //получить путь к коталогу с файлами настройкам програмы, при отсутствии, создать
        public static string GetAppDataPathAndCreateDirIfNeed()
        {
            if (m_applicationDataPath == null)
            {
                m_applicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESRI\\AddInns\\WorkWithReestr");
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
        public static void CreateColumIn(DataGridView dgv, ITable tableToWrap)
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
        public static void GetDisplayOrder(DataGridView dgv, string tableName)
        {
            string filename = Path.Combine(SharedClass.GetAppDataPathAndCreateDirIfNeed(), string.Format("{0}_gridColumOrder.config.xml", tableName));
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
        public static bool SetDisplayOrder(DataGridView dgv, string tableName)
        {
            bool ret = false;
            try
            {
                string filename = Path.Combine(SharedClass.GetAppDataPathAndCreateDirIfNeed(), string.Format("{0}_gridColumOrder.config.xml", tableName));
                if(File.Exists(filename))
                {
                    using (System.IO.FileStream isoStream = new System.IO.FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(int[]));
                        int[] displayIndicies = (int[])ser.Deserialize(isoStream);
                        SetDisplayOrderByArray(dgv, displayIndicies);
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
        public static void SetDisplayOrderByArray(DataGridView dgv, int[] displayIndices)
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
        public static object GetValueByID(int id, string tableName, string fildName)
        {
            object ret = null;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(SharedClass.WorkspaceName) as IFeatureWorkspace;
                ITable table = fws.OpenTable(SharedClass.WorkspaceName + ".DBO." + tableName);
                IRow row = table.GetRow(id);
                ret =  row.get_Value(table.FindField(fildName));
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("SharedClass.GetValueByID('{0}', '{1}', '{2}')", id, tableName, fildName));
            }
            return ret;
        }
        //получение ID из таблицы по значению текстового поля
        public static int GetIDByTextValue(string textValue, string tableName, string fildName, bool strongCompare = false)
        {
            int ret = -1;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(SharedClass.WorkspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = SharedClass.WorkspaceName + ".DBO." + tableName;
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
                Logger.Write(ex, string.Format("SharedClass.GetIDByTextValue('{0}', '{1}', '{2}', '{3}')", textValue, tableName, fildName, strongCompare));
            }
            return ret;
        }
        //получение ID из таблицы по значению числового поля
        public static int GetIDByIntValue(int intValue, string tableName, string fildName)
        {
            int ret = -1;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(SharedClass.WorkspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = SharedClass.WorkspaceName + ".DBO." + tableName;
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
                Logger.Write(ex, string.Format("SharedClass.GetIDByIntValue('{0}', '{1}', '{2}')", intValue, tableName, fildName));
            }
            return ret;
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region автозаполнение полей
        // получить лист для автозаполнения
        public static AutoCompleteStringCollection GenerateAutoCompleteStringCollection(string tableName, string fildName)
        {
            AutoCompleteStringCollection ret = null;
            ArrayList data = new ArrayList();
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(SharedClass.WorkspaceName) as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = SharedClass.WorkspaceName  + ".DBO." + tableName;
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


    interface IFormFilterMetods
    {
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName);
    }

}

// примеры

// текущий документ / карта
//IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
//IMap map = mxDoc.FocusMap as IMap;


// выбрать по клику мышы видемый объект
//IMxDocument mxDoc = ArcMap.Document;
//IActiveView  m_focusMap = mxDoc.FocusMap as IActiveView;
//IPoint point = m_focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y) as IPoint;
//ArcMap.Document.FocusMap.SelectByShape(point, null, false);
//m_focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);


// обратится к катологу 
//IWorkspace ws = SharedClass.GetWorkspace("reestr") as IWorkspace;
//IFeatureWorkspace fws = ws as IFeatureWorkspace;
//ITable tt = fws.OpenTable("reestr.DBO.fizichni_osoby");
//IDataset ids = tt as IDataset;
//IObjectClass ob = tt as IObjectClass;




