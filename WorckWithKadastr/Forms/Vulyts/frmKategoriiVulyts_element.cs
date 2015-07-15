using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmKategoriiVulyts_element : frmBaseAdrKategor_element
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

        //protected override void DB_to_FormElement(IRow row)
        //{
        //    base.DB_to_FormElement(row);
        //}

        //protected override void FormElement_to_DB(IRow row)
        //{
        //    base.FormElement_to_DB(row);
        //}

        //protected override bool ValidatingData()
        //{
        //    bool ret = base.ValidatingData();
        //    return ret;
        //}

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmKategoriiVulyts_element() : base()
        {
            InitializeComponent();
        }

        public frmKategoriiVulyts_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "AdrReestr.DBO.KategoriiVulyts";
        }

        private void frmKategoriiVulyts_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового элемента категорий улиц";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных элемента категории улиц";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление элемента категорий улиц";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
