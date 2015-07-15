using SharedClasses;

using System.Windows.Forms;

namespace WorckWithKadastr
{
    public partial class frmBaseAdrReestrSpr_list : frmBaseSpr_list
    {
        public frmBaseAdrReestrSpr_list()
            : base()
        {
            InitializeComponent();
        }

        public frmBaseAdrReestrSpr_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();
        }


    }
}
