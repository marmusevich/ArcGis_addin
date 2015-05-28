using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmJurLic_list : frmBaseSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmJurLic_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmJurLic_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmJurLic_list() : base()
        {
            InitializeComponent();
        }

        public frmJurLic_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.jur_osoby";
            base.NameSortFild = "назва";

            //this.Text = "";
        }


        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmJurLic_element(_objectID, _editMode);
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
