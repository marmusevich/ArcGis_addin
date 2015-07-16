using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrVulytsDorig_element : frmBaseAdrReestrSpr_element
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
        public frmReestrVulytsDorig_element() : base()
        {
            InitializeComponent();
        }

        public frmReestrVulytsDorig_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "reestrvulytsdorig_polyline";
        }

        private void frmReestrVulytsDorig_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового объекта дорожней сети";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных объекта дорожней сети";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление объекта дорожней сети";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
