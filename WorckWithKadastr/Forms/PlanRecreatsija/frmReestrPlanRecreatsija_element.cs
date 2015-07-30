using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrPlanRecreatsija_element : frmBaseAdrReestrSpr_element
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
        public frmReestrPlanRecreatsija_element() : base()
        {
            InitializeComponent();
        }

        public frmReestrPlanRecreatsija_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "ReestrPlanRecreatsija_polygon";

            base.mKategorTablName = "KategoriiPlanRecreatsija";
        }

        private void frmReestrPlanRecreatsija_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового реакреационного объекта";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных реакреационного объекта";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление реакреационного объекта";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
