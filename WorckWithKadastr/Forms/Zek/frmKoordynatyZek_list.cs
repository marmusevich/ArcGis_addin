using System.Windows.Forms;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmKoordynatyZek_list : frmBaseKoord_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmKoordynatyZek_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmKoordynatyZek_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmKoordynatyZek_list() : base()
        {
            InitializeComponent();
        }
        
        public frmKoordynatyZek_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "KoordynatyZek";
            base.NameSortFild = "KodObject";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmKoordynatyZek_element(_objectID, _editMode);
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
