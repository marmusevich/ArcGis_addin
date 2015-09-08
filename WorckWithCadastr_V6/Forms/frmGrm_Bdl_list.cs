using SharedClasses;

namespace WorckWithCadastr_V6
{
    public partial class frmGrm_Bdl_list : frmBase_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmGrm_Bdl_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmGrm_Bdl_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmGrm_Bdl_list() : base()
        {
            InitializeComponent();
        }
        
        public frmGrm_Bdl_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Cadastr_V6";
            base.NameTable = "Grm_Bdl";
            base.NameSortFild = "ID_MSB_OBJ";        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmGrm_Bdl_element(_objectID, _editMode);
        }

        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            dictionaryTableName = "";
            return false;
        }
        #endregion
    }
}
