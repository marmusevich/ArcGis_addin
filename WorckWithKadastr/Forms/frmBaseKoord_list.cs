using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr
{
    public partial class frmBaseKoord_list : frmBaseSpr_list
    {
        public frmBaseKoord_list()
            : base()
        {
            InitializeComponent();
        }

        public frmBaseKoord_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();
        }
    }
}
