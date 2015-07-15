using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr
{
    public partial class frmBaseAdrReestrSpr_element : frmBaseSpr_element
    {

        public frmBaseAdrReestrSpr_element()
            : base()
        {
            InitializeComponent();
        }
        public frmBaseAdrReestrSpr_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

        }

    }
}
