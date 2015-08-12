using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrDorogy_point_element : frmBaseAdrReestrSpr_element
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
			base.DB_to_FormElement(row);
        }

        protected override void FormElement_to_DB(IRow row)
        {
			base.FormElement_to_DB(row);
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
        public frmReestrDorogy_point_element() : base()
        {
            InitializeComponent();
        }

        public frmReestrDorogy_point_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "ReestrDorogy_point";
            base.mKategorTablName = "KategoriiDorog";
        }

        private void frmReestrDorogy_point_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового транспортного объекта (точька)";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных транспортного объекта (точька)";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление транспортного объекта (точька)";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
