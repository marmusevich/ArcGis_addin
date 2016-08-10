using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr2016
{
    public partial class frmZtl_Bdn_list : frmBase_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmZtl_Bdn_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmZtl_Bdn_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmZtl_Bdn_list()
            : base()
        {
            InitializeComponent();
        }

        public frmZtl_Bdn_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Ztl_Bdn";
            base.NameSortFild = "ID_MSB_OBJ";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmZtl_Bdn_element(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                        base.table.FindField("KOD_KLS"),
                        base.table.FindField("KOD_STS"),

                        base.table.FindField("ID_MSB_OBJ"),
                        base.table.FindField("KOD_TYP_BDN"),
                        base.table.FindField("KLK_PVH"),
                        base.table.FindField("N_Kad"),
                        base.table.FindField("Pidcode"),

                        base.table.FindField("Prymitka"),
                        base.table.FindField("RuleID_2000"),
                        base.table.FindField("RuleID_5000"),

                        base.table.FindField("Override_2000"),
                        base.table.FindField("Override_5000"),
                        base.table.FindField("SHAPE"),
                        base.table.FindField("SHAPE.STArea()"),
                        base.table.FindField("SHAPE.STLength()")
                       };
            GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }
        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            dgv.Columns["SHAPE"].Visible = false;
            dgv.Columns["SHAPE.STLength()"].Visible = false;
            dgv.Columns["SHAPE.STArea()"].Visible = false;
            dgv.Columns["Override_2000"].Visible = false;
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
