using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrMistzevist_polyline_element : frmBaseAdrReestrSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        protected override void DB_to_FormElement(IRow row)
        {
        }

        protected override void FormElement_to_DB(IRow row)
        {
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            return ret;
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmReestrMistzevist_polyline_element() : base()
        {
            InitializeComponent();
        }

        public frmReestrMistzevist_polyline_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "ReestrMistzevist_polyline";
        }

        private void frmReestrMistzevist_polyline_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового природного объекта (кривая)";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных природного объекта (кривая)";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление природного объекта (кривая)";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
