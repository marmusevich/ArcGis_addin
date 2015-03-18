using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmReestrVedomostey_jurnal : frmBaseJurnal
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView()
        {
            Form frm = new frmReestrVedomostey_jurnal();
            frm.Show();
            frm.Activate();
        }

        //public static int ShovForSelect(string filteredString = "")
        //{
        //    frmReestrVedomostey_jurnal frm = new frmReestrVedomostey_jurnal();
        //    frm.ShowDialog();
        //    //return frm.SelectID;
        //}

       
        
        
        public frmReestrVedomostey_jurnal()
            : base()
        {
            InitializeComponent();
        }
        #endregion
    }
}
