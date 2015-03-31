using System;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using System.Collections;


namespace WorckWithReestr
{
    class SharedClass
    {
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
            System.Windows.Forms.MessageBox.Show(errorText, errorCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        // создать колонки в гриде по таблице ArcGIS
        public static void CreateColumIn(System.Windows.Forms.DataGridView dgv, ITable tableToWrap)
        {
            dgv.Columns.Clear();
            for (int fieldCount = 0; fieldCount < tableToWrap.Fields.FieldCount; fieldCount++)
            {
                IField f = tableToWrap.Fields.get_Field(fieldCount);

                System.Windows.Forms.DataGridViewColumn dGVC = null;
                //if ((f.Domain != null) && (string.Compare(f.Domain.Name, "bool", true) == 0))
                //    dGVC = new System.Windows.Forms.DataGridViewCheckBoxColumn();
                //else
                    dGVC = new System.Windows.Forms.DataGridViewTextBoxColumn();

                dGVC.Name = f.Name;
                dGVC.DataPropertyName = f.Name;
                dGVC.HeaderText = f.AliasName;
                dGVC.ReadOnly = true;
                dGVC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;

                dgv.Columns.Add(dGVC);
                f = null;
            }
        }

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



        #region методы проверок полей ввода
        // проверка текстового поля на содержание Smal Int (16 бит) и выстовить ошибку в провайдер ошыбок
        public static bool CheckValueIsSmalInt_SetError(System.Windows.Forms.TextBox _chekedValue, System.Windows.Forms.ErrorProvider _errorProvider)
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
        public static bool CheckValueIsInt_SetError(System.Windows.Forms.TextBox _chekedValue, System.Windows.Forms.ErrorProvider _errorProvider)
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
        public static bool CheckValueStringNotEmpty_SetError(System.Windows.Forms.TextBox _chekedValue, System.Windows.Forms.ErrorProvider _errorProvider)
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
