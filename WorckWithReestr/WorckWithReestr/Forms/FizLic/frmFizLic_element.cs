using ESRI.ArcGIS.Geodatabase;
using System;

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
            txtID.Text = "" + row.get_Value(0) as string;
            txtFIO.Text = row.get_Value(1) as string;
            txtCategor.Text = row.get_Value(2) as string;
            txtINN.Text = row.get_Value(3) as string;

            cbIsWorker.Checked = SharedClass.ConvertVolueToBool(row.get_Value(4));
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(1, txtFIO.Text);
            row.set_Value(2, txtCategor.Text);
            row.set_Value(3, txtINN.Text);

            if (cbIsWorker.Checked)
                row.set_Value(4, 1);
            else
                row.set_Value(4, 0);

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
            base.NameTable = "reestr.DBO.fizichni_osoby";

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
                    btnOk.Text = "Удалить";
                    txtID.Enabled = false;
                    txtFIO.Enabled = false;
                    txtINN.Enabled = false;
                    txtCategor.Enabled = false;
                    cbIsWorker.Enabled = false;

                    break;

                default:
                    this.Close();
                    return;
            }

            if (editMode != EditMode.ADD)
            {
                if (!this.ReadData()) // error
                {
                    SharedClass.ShowErrorMessage();
                    this.Close();
                }
            }
        }


        private void txtFIO_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
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
