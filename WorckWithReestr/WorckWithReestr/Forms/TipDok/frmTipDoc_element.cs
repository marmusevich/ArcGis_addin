using ESRI.ArcGIS.Geodatabase;
using System;

namespace WorckWithReestr
{
    public partial class frmTipDoc_element : frmBaseSpr_element
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
            txtTip_Doc.Text = "" + row.get_Value(base.table.FindField("Tip_Doc")) as string;
            txtKod_Doc.Text = "" + row.get_Value(base.table.FindField("Kod_Doc")).ToString();
        }
        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(base.table.FindField("Tip_Doc"), txtTip_Doc.Text);
            row.set_Value(base.table.FindField("Kod_Doc"), Convert.ToInt16(txtKod_Doc.Text));
        }



        protected override bool ValidatingData()
        {
            bool ret = false;

            try
            {
                short numVal = Convert.ToInt16(txtKod_Doc.Text);
                errorProvider.SetError(txtKod_Doc, String.Empty);
                ret = true;
            }
            catch (FormatException e)
            {
                errorProvider.SetError(txtKod_Doc, "Должно быть число.");
                ret = false;
            }
            catch (OverflowException e)
            {
                errorProvider.SetError(txtKod_Doc, "Слишком большое число.");
                ret = false;
            }

            if (txtTip_Doc.Text == null || txtTip_Doc.Text == "")
            {
                errorProvider.SetError(txtTip_Doc, "Тип документа пустой.");
                ret = false;
            }
            else
            {
                errorProvider.SetError(txtTip_Doc, String.Empty);
                ret = true;
            }



            return ret;
        }




        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmTipDoc_element()
            : base()
        {
            InitializeComponent();
        }

        public frmTipDoc_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.Tip_Doc";
        }


        private void frmTipDoc_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового типа документа";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных типа документа";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление типа документа";
                    btnOk.Text = "Удалить";
                    txtTip_Doc.Enabled = false;
                    txtKod_Doc.Enabled = false;
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
        #endregion

        private void txtTip_Doc_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKod_Doc_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKod_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtTip_Doc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }
    }
}
