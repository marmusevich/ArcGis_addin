using System;

using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace SharedClasses
{

    //для форм списков котрые будут поддерживать поиск в таблице
    public interface IListFormFilterMetods
    {
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName);
    }

    public abstract class AddInsAppInfo
    {
        public abstract IApplication GetThisAddInnApp();
        public abstract IMxDocument GetDocument();
        public abstract IMxApplication GetHostApplication();
        public abstract IDockableWindowManager GetDockableWindowManager();
        public abstract IDocumentEvents_Event GetEvents();

        public abstract string GetNameApp();
    }
    
    public static class GeneralApp
    {
        //---------------------------------------------------------------------------------------
        #region общее

        //хранит путь к деректории с настройками
        private static string m_ApplicationDataPath = null;
        private static AddInsAppInfo m_AddInsAppInfo = null;

        //установить ссылку на класс информации об текущем приложении
        public static void SetAddInsAppInfo(AddInsAppInfo addInsAppInfo)
        {
            m_AddInsAppInfo = addInsAppInfo;
        }
        // вернуть ссылку на класс информации об текущем приложении
        public static AddInsAppInfo GetAddInsAppInfo()
        {
            return m_AddInsAppInfo;
        }
        //получить путь к коталогу с файлами настройкам програмы, при отсутствии, создать
        public static string GetAppDataPathAndCreateDirIfNeed()
        {
            if (m_ApplicationDataPath == null)
            {
                string thisAppName = "defaultName";
                if (m_AddInsAppInfo != null && GetAddInsAppInfo().GetNameApp() != "")
                    thisAppName = GetAddInsAppInfo().GetNameApp();

                m_ApplicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ESRI","AddInns", thisAppName);
            }
            if (!Directory.Exists(m_ApplicationDataPath))
                Directory.CreateDirectory(m_ApplicationDataPath); // Создаем директорию, если нужно

            return m_ApplicationDataPath;
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
        //получить путь и имя к файлу порядок колонок в гриде
        private static string GetFileName_GridColumDisplayOrder(string tableName)
        {
            return Path.Combine(GeneralApp.GetAppDataPathAndCreateDirIfNeed(), string.Format("{0}_gridColumOrder.config.xml", tableName));
        }
        //получить и сохранить в файле порядок колонок
        public static void SaveDisplayOrderToDisk(ref DataGridView dgv, string tableName)
        {
            string filename = GetFileName_GridColumDisplayOrder(tableName);
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
        public static bool LoadDisplayOrderFromDisk_AndSetToDGV(ref DataGridView dgv, string tableName)
        {
            bool ret = false;
            try
            {
                string filename = GetFileName_GridColumDisplayOrder(tableName);
                if (File.Exists(filename))
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
            try
            {
                for (int i = 0; i < displayIndices.Length; i++)
                {
                    //dgv.Columns[i].DisplayIndex = displayIndicies[i];
                    dgv.Columns[displayIndices[i]].DisplayIndex = i;
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, " _SetDisplayOrderByArray_ ");
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

    }
}
