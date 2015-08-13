using ESRI.ArcGIS.Geodatabase;
using SharedClasses;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WorckWithKadastr
{
    public partial class frmBaseAdrKategor_element :frmBaseSpr_element
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
            txtKodKategorii.Text = "" + row.get_Value(base.table.FindField("KodKategorii")) as string;
            txtNazvaTypu.Text = row.get_Value(base.table.FindField("NazvaTypu")) as string;
            txtKorotkaNazvaTypu.Text = row.get_Value(base.table.FindField("KorotkaNazvaTypu")) as string;
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            int KodKategorii = Convert.ToInt32(txtKodKategorii.Text);
            if (AdresReestrWork.IsCodeKategorObjExist(NameTable, KodKategorii) && (editMode != EditMode.EDIT))
            {
                MessageBox.Show(string.Format("Элемент категории с номером [{0}] уже есть.", KodKategorii), "Не унекальный код");
                return;
            }
            row.set_Value(base.table.FindField("KodKategorii"), KodKategorii);

            row.set_Value(base.table.FindField("NazvaTypu"), txtNazvaTypu.Text);
            row.set_Value(base.table.FindField("KorotkaNazvaTypu"), txtKorotkaNazvaTypu.Text);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKodKategorii, errorProvider) && ret;
            ret = GeneralDBWork.CheckValueStringNotEmpty_SetError(txtNazvaTypu, errorProvider) && ret;
            return ret;
        }

        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public frmBaseAdrKategor_element()
           : base()
        {
            InitializeComponent();
        }
        public frmBaseAdrKategor_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

        }

        private void txtNazvaTypu_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKorotkaNazvaTypu_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
        }

        private void txtKodKategorii_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }

        private void txtKodKategorii_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }

        private void txtNazvaTypu_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidatingData();
        }
        #endregion
    }
}
