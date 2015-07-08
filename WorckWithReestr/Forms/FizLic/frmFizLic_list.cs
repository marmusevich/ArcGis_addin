using System.Windows.Forms;
using SharedClasses;


namespace WorckWithReestr
{
    public partial class frmFizLic_list : frmBaseSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmFizLic_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmFizLic_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmFizLic_list() : base()
        {
            InitializeComponent();
        }
        
        public frmFizLic_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.fizichni_osoby";
            base.NameSortFild = "П_І_Б";

            //this.Text = "";
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
