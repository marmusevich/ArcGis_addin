using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmReestrZek_element : frmBaseAdrReestrSpr_element
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
        public frmReestrZek_element() : base()
        {
            InitializeComponent();
        }

        public frmReestrZek_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "ReestrZek_point";
        }

        private void frmReestrZek_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление новой жилишно-эксплатоционной организации";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных жилишно-эксплатоционной организации";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление жилишно-эксплатоционной организации";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
