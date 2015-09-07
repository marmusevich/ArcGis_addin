using SharedClasses;
using System.Windows.Forms;


namespace WorckWithCadastr_V6
{
    public partial class frmRej_Vul_list : frmBase_list
    {
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

        public frmRej_Vul_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "";
            base.NameTable = "";
            base.NameSortFild = "";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmRej_Vul_element(_objectID, _editMode);
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
