using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;


namespace WorckWithReestr
{


    public partial class frmFizLic_element : frmBaseSpr_element
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
            // взять из базы
            txtFIO.Text = row.get_Value(base.table.FindField("П_І_Б")) as string;
            txtCategor.Text = row.get_Value(base.table.FindField("категорія")) as string;
            txtINN.Text = row.get_Value(base.table.FindField("ідент_код")) as string;

            cbIsWorker.Checked = SharedClass.ConvertVolueToBool(base.table.FindField("ЭтоСотрудник"));
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(base.table.FindField("П_І_Б"), txtFIO.Text);
            row.set_Value(base.table.FindField("категорія"), txtCategor.Text);
            row.set_Value(base.table.FindField("ідент_код"), txtINN.Text);

            if (cbIsWorker.Checked)
                row.set_Value(base.table.FindField("ЭтоСотрудник"), 1);
            else
                row.set_Value(base.table.FindField("ЭтоСотрудник"), 0);

        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = SharedClass.CheckValueStringNotEmpty_SetError(txtFIO, errorProvider) && ret;
            return ret;
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmFizLic_element() : base()
        {
            InitializeComponent();
        }

        public frmFizLic_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "fizichni_osoby";

        }


        private void frmFizLic_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового физического лица";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных физического лица";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление физического лица";
                    break;
                default:
                    this.Close();
                    return;
            }
        }


        private void txtFIO_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }
        private void txtFIO_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtINN_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
        }

        private void txtCategor_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
        }

        private void cbIsWorker_CheckedChanged(object sender, EventArgs e)
        {
            isModified = true;
        }
        #endregion
    }


}
