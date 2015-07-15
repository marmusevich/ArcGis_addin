using System.Windows.Forms;
using SharedClasses;


namespace WorckWithKadastr
{
    public partial class frmBaseAdrKategor_list : frmBaseSpr_list
    {
        public frmBaseAdrKategor_list()
            : base()
        {
            InitializeComponent();
        }

        public frmBaseAdrKategor_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();
        }

    }
}
