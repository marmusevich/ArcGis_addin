using ESRI.ArcGIS.Geodatabase;
using System;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmArhivDocument_element : frmBaseSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //// коды значений справочников
        int mDocument = -1;

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            //даты
            dtpDataDocument.Value = GeneralDBWork.ConvertVolueToDateTime(row.get_Value(base.table.FindField("DataDocument")));

            //простые тексты  
            txtNazvaDocument.Text = "" + row.get_Value(base.table.FindField("NazvaDocument")) as string;
            txtNomerDocument.Text = "" + row.get_Value(base.table.FindField("NomerDocument")) as string;
            txtOpysDocument.Text = "" + row.get_Value(base.table.FindField("OpysDocument")) as string;
            txtLinkDocument.Text = "" + row.get_Value(base.table.FindField("LinkDocument")) as string;

            mDocument = row.get_Value(base.table.FindField("Document"));
            OnChangedDocument();

            txtKodObject.Text = "" + row.get_Value(base.table.FindField("KodObject")) as string;        
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            //даты
            row.set_Value(base.table.FindField("DataDocument"), dtpDataDocument.Value);

            row.set_Value(base.table.FindField("LinkDocument"), txtLinkDocument.Text);
            row.set_Value(base.table.FindField("NazvaDocument"), txtNazvaDocument.Text);
            row.set_Value(base.table.FindField("NomerDocument"), txtNomerDocument.Text);
            row.set_Value(base.table.FindField("OpysDocument"), txtOpysDocument.Text);

            row.set_Value(base.table.FindField("Document"), mDocument);

            int KodObject = Convert.ToInt32(txtKodObject.Text);
            //if (ReestrDocumentWork.IsNumerReestrZayavExist(KodObject) && (editMode != EditMode.EDIT))
            //{
            //    //if (MessageBox.Show(string.Format("Документ с номером [{0}] уже есть. \n Згенерировать следующий доступный? ", N_Z), "Не унекальный номер", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    //    KodObject = ReestrDocumentWork.GetNextNumerToReestrZayav();
            //}
            row.set_Value(base.table.FindField("KodObject"), KodObject);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();

            ret = GeneralDBWork.CheckValueIsInt_SetError(txtKodObject, errorProvider) && ret;
            ret = AdresReestrWork.CheckValueIsContainsTip_Doc_SetError(txtDocument, errorProvider, ref mDocument) && ret;


            return ret;
        }


        protected override void DB_SharedData_to_FormElement()
        {
            base.DB_SharedData_to_FormElement();
            AdresReestrWork.EnableAutoComlectToTip_Doc(txtDocument);
        }

        
        private void OnChangedDocument()
        {
            txtDocument.Text = AdresReestrWork.GetNameByIDFromTip_Doc(mDocument);
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmArhivDocument_element() : base()
        {
            InitializeComponent();
        }

        public frmArhivDocument_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "ArhivDocument";

        }

        private void frmArhivDocument_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового ";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных ";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление ";
                    break;
                default:
                    this.Close();
                    return;
            }
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            string filteredString = "";
            mDocument = frmTipDoc_list.ShowForSelect(filteredString);
            OnChangedDocument();
            errorProvider.SetError(txtDocument, String.Empty);
        }


        private void main_TextChanged(object sender, System.EventArgs e)
        {
            isModified = true;
            ValidatingData();
        }
        private void main_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isModified = true;
            e.Cancel = !ValidatingData();
        }        
        
        #endregion


    }
}
