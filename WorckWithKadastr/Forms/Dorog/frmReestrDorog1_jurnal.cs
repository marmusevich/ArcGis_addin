using System;
using System.Windows.Forms;
using SharedClasses;

namespace WorckWithReestr
{
    public partial class frmReestrDorog1_list : frmBaseJurnal
    {

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView()
        {
            Form frm = new frmReestrDorog1_list();
            frm.Show();
            frm.Activate();
        }

        public frmReestrDorog1_list()
            : base()
        {
            InitializeComponent();

            base.NameWorkspace = "";
            base.NameTable = "";
            base.NameSortFild = "";
            base.NameDataFilteredFild = "";
        }

        protected override frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            return new frmReestrZayav_doc(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {

            //int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
            //                        };
            SharedClass.SetDisplayOrderByArray(dgv, displayIndicies);
        }

        protected override void OtherSetupDGV()
        {
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

