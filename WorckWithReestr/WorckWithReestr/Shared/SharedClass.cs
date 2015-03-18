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
                if ((f.Domain != null) && (string.Compare(f.Domain.Name, "bool", true) == 0))
                    dGVC = new System.Windows.Forms.DataGridViewCheckBoxColumn();
                else
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

        //преобразует значение из базы с предпологаемым домено BOOL  в  BOOL
        public static bool ConvertVolueToBool(object obj)
        {
            if ((obj != null) && !Convert.IsDBNull(obj) && Convert.ToBoolean(obj))
                return true;
            else
                return false;
        }
    }

        //public static object[] GetListOfDamaibValue(IDomain domain)
        //{
        //    // Use the ICodedValueDomain interface.
        //    ICodedValueDomain codedValueDomain = (ICodedValueDomain)domain;
        //    if (codedValueDomain == null)
        //        return null;

        //    ArrayList ret = new ArrayList(codedValueDomain.CodeCount);

        //    // Iterate through the code/value pairs.
        //    for (int i = 0; i < codedValueDomain.CodeCount; i++)
        //    {
        //        ret.Add(new SelectData(codedValueDomain.get_Value(i), codedValueDomain.get_Name(i)));
        //    }
        //    return ret.ToArray();
        //}




    ////вспомогательный для выбора значений
    //class SelectData
    //{
    //    public readonly object Value;
    //    public readonly string Text;

    //    public SelectData(object Value, string Text)
    //    {
    //        this.Value = Value;
    //        this.Text = Text;
    //    }

    //    public override string ToString()
    //    {
    //        return this.Text;
    //    }
    //}

    //class CompareSelectDataByValue : IComparer
    //{
    //    int Compare(object obj1, object obj2)
    //    {
    //        object v1 = ((SelectData)obj1).Value;
    //        object v2 = ((SelectData)obj2).Value;

    //        int ret = 0;
    //        //if (v1 == v2)
    //        //    ret = 0;
    //        //else if (v1 > v2)
    //        //    ret = +1;
    //        //else 
    //        //    ret = -1;

    //        return ret;
    //    }
    //}
    //class CompareSelectDataByText : IComparer
    //{
    //    int Compare(object obj1, object obj2)
    //    {
    //        string str1 = ((SelectData)obj1).ToString();
    //        string str2 = ((SelectData)obj2).ToString();
    //        return str1.CompareTo(str2);
    //    }
    //}


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
