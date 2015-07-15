using System.Windows.Forms;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrZek_list : frmBaseAdrReestrSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmReestrZek_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmReestrZek_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmReestrZek_list() : base()
        {
            InitializeComponent();
        }
        
        public frmReestrZek_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "AdrReestr.DBO.ReestrZek_point";
            base.NameSortFild = "KodObject";        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmReestrZek_element(_objectID, _editMode);
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
