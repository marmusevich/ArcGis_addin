using System;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using SharedClasses;

namespace WorckWithCadastr_V6
{
    public partial class frmRej_Vul_list : frmBase_list
    {
        protected System.Windows.Forms.ToolStripButton tsdTranslitName;

        
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRej_Vul_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRej_Vul_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmRej_Vul_list()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Vul_list(bool isSelectMode, string filteredString)
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            //Kadastr2016.DBO.Rej_Vul_1
            //base.NameWorkspace = "Cadastr_V6";
            //base.NameTable = "Rej_Vul";
            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Rej_Vul_1";
            base.NameSortFild = "ID_MSB_OBJ";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmRej_Vul_element(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                        base.table.FindField("KOD_KLS"),
                        base.table.FindField("ID_MSB_OBJ"),
                        base.table.FindField("KOD_VUL"),
                        base.table.FindField("KOD_STAN_VUL"),
                        base.table.FindField("NAZVA_UKR"),
                        base.table.FindField("NAZVA_ROS"),
                        base.table.FindField("NAZVA_LAT"),
                        base.table.FindField("KOD_KAT"),
                        base.table.FindField("IST_DOV"),
                        base.table.FindField("NZV_MSB_OBJ"),
                        base.table.FindField("KOATUU"),
                        base.table.FindField("RuleID"),
                        base.table.FindField("Override"),
                        base.table.FindField("N_Kad"),
                        base.table.FindField("Prymitka"),
                        base.table.FindField("SHAPE"),
                        base.table.FindField("KodObject"),
                        base.table.FindField("Назва_вули"),
                        base.table.FindField("NomerDocument"),
                        base.table.FindField("DataDocument"),
                        base.table.FindField("SHAPE.STLength()")
                       };
            //GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }
        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            //dgv.Columns["SHAPE"].Visible = false;
            //dgv.Columns["SHAPE.STLength()"].Visible = false;
            //dgv.Columns["Назва_вули"].Visible = false;
            //dgv.Columns["Override"].Visible = false;

            //dgv.CellFormatting += OnCellFormatting;
        }
        //вернуть строку доаолнительных условий
        protected override string GetStringAddetConditions()
        {
            string ret = base.GetStringAddetConditions();
            return ret;
        }
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            return false;
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        #endregion

        private void frmRej_Vul_list_Load(object sender, System.EventArgs e)
        {
            // 
            // dgv
            // 
            //dgv.Size = new System.Drawing.Size(396, 261);

            // 
            // tsdShowOnMap
            // 
            tsdTranslitName = new System.Windows.Forms.ToolStripButton();
            tsdTranslitName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            tsdTranslitName.ImageTransparentColor = System.Drawing.Color.Magenta;
            tsdTranslitName.Name = "tsdTranslitName";
            tsdTranslitName.Size = new System.Drawing.Size(110, 22);
            tsdTranslitName.Text = "Транслитерация названий";
            tsdTranslitName.Click += new System.EventHandler(tsbtsdTranslitName_Click);
            tsMain.Items.Add(tsdTranslitName);
        }

        private void tsbtsdTranslitName_Click(object sender, EventArgs e)
        {
            ///-------------------------------
            ///
            
            // какието делегаты или еще както универсализировать
            // обертка для индикации пользователю
            AddInsAppInfo ai = GeneralApp.GetAddInsAppInfo();
            IStatusBar statusBar = null;

            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                table = fws.OpenTable(NameTable);

                ICursor cursor = table.Search(null, false);

                // количество строк
                int rowCount = table.RowCount(null);

                // статус бар, иницилизация
                if (ai != null && ai.GetThisAddInnApp() != null)
                {
                    statusBar = ai.GetThisAddInnApp().StatusBar;
                    statusBar.ShowProgressBar("", 0, rowCount, 1, false);
                    statusBar.ProgressBar.Position = 0;
                    statusBar.StepProgressBar();
                }

                //индексы полей
                int fieldIndex_NAZVA_UKR = table.FindField("NAZVA_UKR");
                int fieldIndex_NAZVA_LAT = table.FindField("NAZVA_LAT");

                // здесь перебор в цикле и менять поля
                IRow row = null;
                int i = 0;//счетчик
                while ((row = cursor.NextRow()) != null)
                {
                    //получить
                    string txtNAZVA_UKR = "" + row.get_Value(fieldIndex_NAZVA_UKR) as string;
                    //транслитировать
                    string txtNAZVA_LAT = SharedClasses.TranslitUaToLat.Convert(txtNAZVA_UKR);
                    //изменить
                    row.set_Value(fieldIndex_NAZVA_LAT, txtNAZVA_LAT);
                    //сохранить                
                    row.Store();

                    //счетчик
                    i++;

                    // статус бар
                    statusBar.ProgressBar.Message = "Транслитерация " + i.ToString() + " из " + rowCount.ToString();
                    statusBar.StepProgressBar();
                }

                MessageBox.Show("Транслитерация " + i.ToString() + " из " + rowCount.ToString(), "Выполнено транслитерация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Сохранение данных справочника '{0}' id {1}", NameTable, ""));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при сохранение данных справочника '{0}' id {1}", NameTable, "objectID"));
            }
            finally
            {
                if ((wse != null) && wse.IsBeingEdited())
                {
                    wse.StopEditing(false);
                }


                if (statusBar != null)
                {
                    statusBar.HideProgressBar();
                    statusBar = null;
                }
            }

            ///---------------------------------
            ///

            tableWrapper.UpdateData();
            dgv.Refresh();
        }
    }
}


