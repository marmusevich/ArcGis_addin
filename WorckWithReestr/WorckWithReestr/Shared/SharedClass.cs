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
    class SharedClass
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
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForAssembly();
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(tableName + "_gridColumOrder", FileMode.Create, isoFile))
            {
                int[] displayIndices = new int[dgv.ColumnCount];
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    displayIndices[i] = dgv.Columns[i].DisplayIndex;
                }
                XmlSerializer ser = new XmlSerializer(typeof(int[]));
                ser.Serialize(isoStream, displayIndices);
            }
        }
        //устоновить порядок колонок из файла
        public static bool SetDisplayOrder(DataGridView dgv, string tableName)
        {
            bool ret = false;
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForAssembly();
            string[] fileNames = isoFile.GetFileNames("*");
            bool found = false;
            foreach (string fileName in fileNames)
            {
                if (fileName == tableName + "_gridColumOrder")
                    found = true;
            }
            if (!found)
                return ret;
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(tableName + "_gridColumOrder", FileMode.Open, isoFile))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(int[]));
                    int[] displayIndicies = (int[])ser.Deserialize(isoStream);
                    SetDisplayOrderByArray(dgv, displayIndicies);
                    ret = true;
                }
                catch { }
            }
            return ret;
        }
        //устоновить порядок колонок по массиву
        public static void SetDisplayOrderByArray(DataGridView dgv, int[] displayIndicies)
        {
            for (int i = 0; i < displayIndicies.Length; i++)
            {
                dgv.Columns[i].DisplayIndex = displayIndicies[i];
            }
        }

        #endregion
        //---------------------------------------------------------------------------------------

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
            catch (FormatException e)
            {
                _errorProvider.SetError(_chekedValue, "Должно быть число.");
                ret = false;
            }
            catch (OverflowException e)
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
            catch (FormatException e)
            {
                _errorProvider.SetError(_chekedValue, "Должно быть число.");
                ret = false;
            }
            catch (OverflowException e)
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

        //---------------------------------------------------------------------------------------
        #region механизм обращения к базе
        //универсальный метод получение значения из таблицы
        public static object GetValueByID(int id, string tableName, string fildName)
        {
            object ret = null;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                ITable table = fws.OpenTable("reestr.DBO." + tableName);
                IRow row = table.GetRow(id);
                ret =  row.get_Value(table.FindField(fildName));
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
            }
            return ret;
        }
        //получение ID из таблицы по значению текстового поля
        public static int GetIDByTextValue(string textValue, string tableName, string fildName, bool strongCompare = false)
        {
            int ret = -1;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = "reestr.DBO." + tableName;
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
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
            }
            return ret;
        }
        //получение ID из таблицы по значению числового поля
        public static int GetIDByIntValue(int intValue, string tableName, string fildName)
        {
            int ret = -1;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
                queryDef2.Tables = "reestr.DBO." + tableName;
                queryDef2.SubFields = "DISTINCT TOP 1 OBJECTID";
                queryDef2.WhereClause = fildName = " = " + intValue.ToString();
                //queryDef2.PostfixClause = "ORDER BY " + fildName;
                ICursor cursor = queryDef2.Evaluate2(true);
                IRow row = null;
                if ((row = cursor.NextRow()) != null)
                {
                    ret = row.get_Value(0);
                }
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
            }
            return ret;
        }

        #endregion
        //---------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------
        #region автозаполнение полей
        // получить лист для автозаполнения
        public static AutoCompleteStringCollection GenerateAutoCompleteStringCollection(string tableName, string fildName)
        {
            AutoCompleteStringCollection ret = null;
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
        //          !!!!!!!!!!!
        // подумать слишком непонятная работа
        // универсальный запрос а возвращает масив для одного поля
        //public static ArrayList GenerateList(string tableName, string fildName)
        //{
        //    ArrayList data = new ArrayList();
        //    try
        //    {
        //        IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
        //        IQueryDef2 queryDef2 = (IQueryDef2)fws.CreateQueryDef();
        //        queryDef2.Tables = "reestr.DBO." + tableName;
        //        queryDef2.SubFields = "DISTINCT " + fildName;
        //        //queryDef2.WhereClause = "1 = 1"; // условие
        //        queryDef2.PostfixClause = "ORDER BY "+fildName;
        //        ICursor cursor = queryDef2.Evaluate2(true);
        //        IRow row = null;
        //        while ((row = cursor.NextRow()) != null)
        //        {
        //            data.Add(row.get_Value(0));
        //        }
        //        return data;
        //    }
        //    catch (Exception e) // доработать блок ошибок на разные исключения
        //    {
        //        return null;
        //    }
        //}

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




