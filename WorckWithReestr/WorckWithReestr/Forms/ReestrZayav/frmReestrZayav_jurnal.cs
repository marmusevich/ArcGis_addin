using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmReestrZayav_jurnal : frmBaseJurnal
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView()
        {
            Form frm = new frmReestrZayav_jurnal();
            frm.Show();
            frm.Activate();
        }

        //public static int ShovForSelect(string filteredString = "")
        //{
        //    frmReestrZayav_jurnal frm = new frmReestrZayav_jurnal();
        //    frm.ShowDialog();
        //    //return frm.SelectID;
        //}

        public frmReestrZayav_jurnal()
            : base()
        {
            InitializeComponent();


            //base.NameWorkspace = "reestr";
            //base.NameTable = "reestr.DBO.Kn_Reg_Zayv";
            //base.NameSortFild = "Data_Z";


            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.fizichni_osoby";
            base.NameSortFild = "П_І_Б";


        }

        protected override frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            //return new frmReestrZayav_doc(_objectID, _editMode);
            return new frmReestrZayav_doc();
        }


        #endregion
    }
}
