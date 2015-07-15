using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr
{
    public partial class frmKategoriiDorog_list : frmBaseAdrKategor_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmKategoriiDorog_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmKategoriiDorog_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmKategoriiDorog_list() : base()
        {
            InitializeComponent();
        }
        
        public frmKategoriiDorog_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "KategoriiDorogy";
            base.NameSortFild = "KodKategorii";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmKategoriiDorog_element(_objectID, _editMode);
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
