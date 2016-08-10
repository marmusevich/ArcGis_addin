using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr2016
{
    public partial class frmRej_Adm_Raj_Mis_list : frmBase_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRej_Adm_Raj_Mis_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRej_Adm_Raj_Mis_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmRej_Adm_Raj_Mis_list()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Adm_Raj_Mis_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Rej_Adm_Raj_Mis";
            base.NameSortFild = "ID_MSB_OBJ";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmRej_Adm_Raj_Mis_element(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                        base.table.FindField("KOD_KLS"),
                        base.table.FindField("NAZVA_UKR"),
                        base.table.FindField("NAZVA_ROS"),
                        base.table.FindField("NAZVA_LAT"),
                        base.table.FindField("ID_MSB_OBJ"),
                        base.table.FindField("ID_RAI"),
                        base.table.FindField("KOD_KOATUU_RAI"),
                        base.table.FindField("N_Kad"),
                        base.table.FindField("Prymitka"),
                        base.table.FindField("RuleID"),

                        base.table.FindField("SHAPE.STLength()"),
                        base.table.FindField("SHAPE.STArea()"),
                        base.table.FindField("Override"),
                        base.table.FindField("SHAPE")
                       };
            GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }
        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            dgv.Columns["SHAPE"].Visible = false;
            dgv.Columns["SHAPE.STLength()"].Visible = false;
            dgv.Columns["SHAPE.STArea()"].Visible = false;
            dgv.Columns["Override"].Visible = false;

            dgv.CellFormatting += OnCellFormatting;
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
    }
}
