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
            base.DB_to_FormElement(row);

            // взять из базы
            SetStringValueFromDBToTextBox(ref row, "П_І_Б", txtFIO);
            SetStringValueFromDBToTextBox(ref row, "категорія", txtCategor);
            SetStringValueFromDBToTextBox(ref row, "ідент_код", txtINN);

            cbIsWorker.Checked = GeneralApp.ConvertVolueToBool(base.table.FindField("ЭтоСотрудник"));
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            // положить в базы
            SaveStringValueFromTextBoxToDB(ref row, "П_І_Б", txtFIO);
            SaveStringValueFromTextBoxToDB(ref row, "категорія", txtCategor);
            SaveStringValueFromTextBoxToDB(ref row, "ідент_код", txtINN);

            if (cbIsWorker.Checked)
                row.set_Value(base.table.FindField("ЭтоСотрудник"), 1);
            else
                row.set_Value(base.table.FindField("ЭтоСотрудник"), 0);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtFIO, errorProvider) && ret;
            return ret;
        }

        protected override void SetMaxLengthStringValueToTextBoxFromDB()
        {
            base.SetMaxLengthStringValueToTextBoxFromDB();
            //простые тексты  
            SetMaxLengthStringValueToTextBox("П_І_Б", txtFIO);
            SetMaxLengthStringValueToTextBox("категорія", txtCategor);
            SetMaxLengthStringValueToTextBox("ідент_код", txtINN);
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
                    Close();
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
