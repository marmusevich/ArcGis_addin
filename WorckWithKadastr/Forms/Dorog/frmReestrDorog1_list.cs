using System.Windows.Forms;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrDorog1_list : frmBaseSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmReestrDorog1_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmReestrDorog1_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmReestrDorog1_list() : base()
        {
            InitializeComponent();
        }
        
        public frmReestrDorog1_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "";
            base.NameTable = "";
            base.NameSortFild = "";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmFizLic_element(_objectID, _editMode);
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
