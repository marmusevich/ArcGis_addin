using System;
using SharedClasses;
using System.Windows.Forms;


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
            MessageBox.Show("Rename?", "Функция заблокирована", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


