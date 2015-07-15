using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmKategoriiPlanRecreatsija_element : frmBaseAdrKategor_element
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
        public frmKategoriiPlanRecreatsija_element() : base()
        {
            InitializeComponent();
        }

        public frmKategoriiPlanRecreatsija_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "KategoriiPlanRecreatsija";
        }

        private void frmKategoriiPlanRecreatsija_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового элемента категорий реакрационных объектов";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных элемента категории реакрационных объектов";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление элемента категорий реакрационных объектов";
                    break;
                default:
                    this.Close();
                    return;
            }
        }
        #endregion
    }
}
