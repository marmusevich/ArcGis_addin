using SharedClasses;
using System.Windows.Forms;

namespace WorckWithCadastr_V6
{
    public partial class frmVrb_Bdl_Spr_list : frmBase_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmVrb_Bdl_Spr_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmVrb_Bdl_Spr_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmVrb_Bdl_Spr_list()
            : base()
        {
            InitializeComponent();
        }

        public frmVrb_Bdl_Spr_list(bool isSelectMode, string filteredString)
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Vrb_Bdl_Spr";
            base.NameSortFild = "ID_MSB_OBJ";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmVrb_Bdl_Spr_element(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                        base.table.FindField("KOD_KLS"),
                        base.table.FindField("KOD_STS"),

                        base.table.FindField("ID_MSB_OBJ"),
                        base.table.FindField("KLK_PVH"),
                        base.table.FindField("N_Kad"),
                        base.table.FindField("Pidcode"),
                        base.table.FindField("RuleID_2000"),

                        base.table.FindField("Prymitka"),

                        base.table.FindField("SHAPE.STLength()"),
                        base.table.FindField("Override_5000"),
                        base.table.FindField("SHAPE"),
                        base.table.FindField("SHAPE.STArea()")};
            GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }
        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            dgv.Columns["SHAPE"].Visible = false;
            dgv.Columns["SHAPE.STLength()"].Visible = false;
            dgv.Columns["SHAPE.STArea()"].Visible = false;
            dgv.Columns["Override_5000"].Visible = false;

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
