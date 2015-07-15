using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr
{
    public partial class frmBaseKoord_element : frmBaseSpr_element
    {
        public frmBaseKoord_element()
           : base()
        {
            InitializeComponent();
        }
        public frmBaseKoord_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

        }
    }
}
