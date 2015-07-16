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

        //получить форму элемента справочника
        protected virtual frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return null;
            // не мои таблицы, не редактирую
        }

        private void frmBaseKoord_list_Load(object sender, System.EventArgs e)
        {
            // не мои таблицы, не редактирую
            base.cmsAdd.Visible = false;
            base.tsbAdd.Visible = false;
            base.tsdEdit.Visible = false;
            base.tsdDelete.Visible = false;
            base.cmsEdit.Visible = false;
            base.cmsDelete.Visible = false;
            base.cmsMain.Visible = false;
            base.tsMain.Visible = false;
        }

    }
}
