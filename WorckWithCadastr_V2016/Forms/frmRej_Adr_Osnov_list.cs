using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr2016
{
    public partial class frmRej_Adr_Osnov_list : frmBase_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRej_Adr_Osnov_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRej_Adr_Osnov_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmRej_Adr_Osnov_list()
            : base()
        {
            InitializeComponent();
        }

        public frmRej_Adr_Osnov_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Rej_Adr_Osnov";
            base.NameSortFild = "ID_MSB_OBJ";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmRej_Adr_Osnov_element(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                        base.table.FindField("KOD_KLS"),
                        base.table.FindField("NAJM_OBJ"),
                        base.table.FindField("SKOR_NAJM_OBJ"),
                        base.table.FindField("Korpus"),
                        base.table.FindField("NumerBud"),

                        base.table.FindField("ID_MSB_OBJ"),
                        base.table.FindField("KOATUU"),
                        base.table.FindField("ID_ADRESS"),
                        base.table.FindField("ID_ELEMENT"),
                        base.table.FindField("ID_Adm_Rn"),
                        base.table.FindField("ID_Obl"),
                        base.table.FindField("ID_Nsl_Pnk"),
                        base.table.FindField("ID_Rej_Vul"),
                        base.table.FindField("INDEX_POSH_VID"),
                        base.table.FindField("N_Kad"),

                        base.table.FindField("Prymitka"),
                        base.table.FindField("KOD_TYP_OBJ_ADR"),
                        base.table.FindField("KOD_TYP_ADR"),
                        base.table.FindField("KOD_FUNC_PRYZN"),
                        base.table.FindField("KOD_STAN_ADR"),
                        base.table.FindField("RuleID"),

                        base.table.FindField("Override"),
                        base.table.FindField("SHAPE")
                       };
            GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }

        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            dgv.Columns["SHAPE"].Visible = false;
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
