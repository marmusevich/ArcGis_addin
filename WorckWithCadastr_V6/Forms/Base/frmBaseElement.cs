using System;
using SharedClasses;
using ESRI.ArcGIS.Geodatabase;


namespace WorckWithCadastr_V6
{
    public partial class frmBaseElement : frmBaseSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //название типа документа
        //protected string mKategorTablName = "";
        //адаптеры доменов
        //DomeinDataAdapter ddaStatusObject;
        //// коды значений справочников
        //int mAdminRajo = -1;

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        protected override void DB_to_FormElement(IRow row)
        {
            base.DB_to_FormElement(row);
            ////даты
            //dtpDataDocument.Value = GeneralDBWork.ConvertVolueToDateTime(row.get_Value(base.table.FindField("DataDocument")));

            ////простые тексты  
            //txtLinkDocument.Text = "" + row.get_Value(base.table.FindField("LinkDocument")) as string;
            //txtNazvaDocument.Text = "" + row.get_Value(base.table.FindField("NazvaDocument")) as string;
            //txtOpys.Text = "" + row.get_Value(base.table.FindField("Opys")) as string;
            //if (txtNazvaKorotkaLat.Visible)
            //{
            //    txtNazvaKorotkaLat.Text = "" + row.get_Value(base.table.FindField("NazvaKorotkaLat")) as string;
            //}
            //if (txtNazvaKorotkaRus.Visible)
            //{
            //    txtNazvaKorotkaRus.Text = "" + row.get_Value(base.table.FindField("NazvaKorotkaRus")) as string;
            //}
            //txtNazvaKorotkaUkr.Text = "" + row.get_Value(base.table.FindField("NazvaKorotkaUkr")) as string;
            //if (txtNazvaPovnaLat.Visible)
            //{
            //    txtNazvaPovnaLat.Text = "" + row.get_Value(base.table.FindField("NazvaPovnaLat")) as string;
            //}
            //if (txtNazvaPovnaRus.Visible)
            //{
            //    txtNazvaPovnaRus.Text = "" + row.get_Value(base.table.FindField("NazvaPovnaRus")) as string;
            //}
            //txtNazvaPovnaUkr.Text = "" + row.get_Value(base.table.FindField("NazvaPovnaUkr")) as string;
            //txtNomerDocument.Text = "" + row.get_Value(base.table.FindField("NomerDocument")) as string;

            ////доменные значения
            //object o = null;
            //o = row.get_Value(base.table.FindField("DzhereloKoord"));
            //if (o == null)
            //    o = row.Fields.get_Field(base.table.FindField("DzhereloKoord")).DefaultValue;
            //cbDzhereloKoord.SelectedIndex = ddaDzhereloKoord.GetIndexByValue(o);

            //o = row.get_Value(base.table.FindField("LocalType"));
            //if (o == null)
            //    o = row.Fields.get_Field(base.table.FindField("LocalType")).DefaultValue;
            //cbLocalType.SelectedIndex = ddaLocalType.GetIndexByValue(Convert.ToInt16(o));

            //o = row.get_Value(base.table.FindField("StatusObject"));
            //if (o == null)
            //    o = row.Fields.get_Field(base.table.FindField("StatusObject")).DefaultValue;
            //cbStatusObject.SelectedIndex = ddaStatusObject.GetIndexByValue(Convert.ToInt16(o));

            //// справочники
            //if (txtAdminRajon.Visible)
            //{
            //    mAdminRajo = row.get_Value(base.table.FindField("AdminRajon"));
            //    OnChangedAdminRajo();
            //}

            //mDocument = row.get_Value(base.table.FindField("Document"));
            //OnChangedDocument();

            //if (txtKodKategorii.Visible)
            //{
            //    mKodKategorii = row.get_Value(base.table.FindField("KodKategorii"));
            //    OnChangedKodKategorii();
            //}

            //txtKodObject.Text = "" + row.get_Value(base.table.FindField("KodObject")) as string;
        }

        protected override void FormElement_to_DB(IRow row)
        {
            base.FormElement_to_DB(row);
            ////даты
            //row.set_Value(base.table.FindField("DataDocument"), dtpDataDocument.Value);

            ////простые тексты  
            //row.set_Value(base.table.FindField("LinkDocument"), txtLinkDocument.Text);
            //row.set_Value(base.table.FindField("NazvaDocument"), txtNazvaDocument.Text);
            //row.set_Value(base.table.FindField("Opys"), txtOpys.Text);
            //if (txtNazvaKorotkaLat.Visible)
            //{
            //    row.set_Value(base.table.FindField("NazvaKorotkaLat"), txtNazvaKorotkaLat.Text);
            //}
            //if (txtNazvaKorotkaRus.Visible)
            //{
            //    row.set_Value(base.table.FindField("NazvaKorotkaRus"), txtNazvaKorotkaRus.Text);
            //}
            //row.set_Value(base.table.FindField("NazvaKorotkaUkr"), txtNazvaKorotkaUkr.Text);
            //if (txtNazvaPovnaLat.Visible)
            //{
            //    row.set_Value(base.table.FindField("NazvaPovnaLat"), txtNazvaPovnaLat.Text);
            //}
            //if (txtNazvaPovnaRus.Visible)
            //{
            //    row.set_Value(base.table.FindField("NazvaPovnaRus"), txtNazvaPovnaRus.Text);
            //}
            //row.set_Value(base.table.FindField("NazvaPovnaUkr"), txtNazvaPovnaUkr.Text);
            //row.set_Value(base.table.FindField("NomerDocument"), txtNomerDocument.Text);

            ////доменные значения
            //row.set_Value(base.table.FindField("DzhereloKoord"), ((DomeinDataAdapter.DomeinData)cbDzhereloKoord.SelectedItem).Value);
            //row.set_Value(base.table.FindField("LocalType"), ((DomeinDataAdapter.DomeinData)cbLocalType.SelectedItem).Value);
            //row.set_Value(base.table.FindField("StatusObject"), ((DomeinDataAdapter.DomeinData)cbStatusObject.SelectedItem).Value);

            //// справочники
            //if (txtAdminRajon.Visible)
            //{
            //    row.set_Value(base.table.FindField("AdminRajon"), mAdminRajo);
            //}

            //row.set_Value(base.table.FindField("Document"), mDocument);

            //if (txtKodKategorii.Visible)
            //{
            //    row.set_Value(base.table.FindField("KodKategorii"), mKodKategorii);
            //}

            //int KodObject = Convert.ToInt32(txtKodObject.Text);
            ////if (ReestrDocumentWork.IsNumerReestrZayavExist(KodObject) && (editMode != EditMode.EDIT))
            ////{
            ////    //if (MessageBox.Show(string.Format("Документ с номером [{0}] уже есть. \n Згенерировать следующий доступный? ", N_Z), "Не унекальный номер", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            ////    //    KodObject = ReestrDocumentWork.GetNextNumerToReestrZayav();
            ////}
            //row.set_Value(base.table.FindField("KodObject"), KodObject);
        }

        protected override bool ValidatingData()
        {
            bool ret = base.ValidatingData();
            //ret = GeneralDBWork.CheckValueIsInt_SetError(txtKodObject, errorProvider) && ret;

            //ret = AdresReestrWork.CheckValueIsContainsTip_Doc_SetError(txtDocument, errorProvider, ref mDocument) && ret;
            //if (txtKodKategorii.Visible)
            //{
            //    ret = AdresReestrWork.CheckValueIsContainsKategorObj_SetError(mKategorTablName, txtKodKategorii, errorProvider, ref mKodKategorii) && ret;
            //}
            return ret;
        }

        protected override void DB_SharedData_to_FormElement()
        {
            //base.DB_SharedData_to_FormElement();
            ////доменные значения
            //object o = null;
            //ddaDzhereloKoord = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("DzhereloKoord")).Domain);
            //cbDzhereloKoord.Items.AddRange(ddaDzhereloKoord.ToArray());
            //o = base.table.Fields.get_Field(base.table.FindField("DzhereloKoord")).DefaultValue;
            //if ((o != null) && !Convert.IsDBNull(o))
            //    cbDzhereloKoord.SelectedIndex = ddaDzhereloKoord.GetIndexByValue(Convert.ToInt16(o));

            //ddaLocalType = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("LocalType")).Domain);
            //cbLocalType.Items.AddRange(ddaLocalType.ToArray());
            //o = base.table.Fields.get_Field(base.table.FindField("LocalType")).DefaultValue;
            //if ((o != null) && !Convert.IsDBNull(o))
            //    cbLocalType.SelectedIndex = ddaLocalType.GetIndexByValue(Convert.ToInt16(o));

            //ddaStatusObject = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("StatusObject")).Domain);
            //cbStatusObject.Items.AddRange(ddaStatusObject.ToArray());
            //o = base.table.Fields.get_Field(base.table.FindField("StatusObject")).DefaultValue;
            //if ((o != null) && !Convert.IsDBNull(o))
            //    cbStatusObject.SelectedIndex = ddaStatusObject.GetIndexByValue(Convert.ToInt16(o));

            //// справочники
            //if (txtAdminRajon.Visible)
            //{
            //    AdresReestrWork.EnableAutoComlectToAdminRajon(txtAdminRajon);
            //}

            //AdresReestrWork.EnableAutoComlectToTip_Doc(txtDocument);

            //if (txtKodKategorii.Visible)
            //{
            //    AdresReestrWork.EnableAutoComlectToKategorObj(mKategorTablName, txtKodKategorii);
            //}
        }

        private void OnChangedAdminRajo()
        {
            //if (txtAdminRajon.Visible)
            //    txtAdminRajon.Text = AdresReestrWork.GetNazvaByIDFromAdminRajon(mAdminRajo, AdresReestrWork.eTypeNazvaFromReestr.NazvaPovnaUkr);
        }

        private void OnChangedDocument()
        {
            //txtDocument.Text = AdresReestrWork.GetNameByIDFromTip_Doc(mDocument);
        }

        private void OnChangedKodKategorii()
        {
            //if (txtKodKategorii.Visible)
            //    txtKodKategorii.Text = AdresReestrWork.GetKorotkaNazvaTypuByIDFromKategorObj(mKategorTablName, mKodKategorii);
        }



        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        public frmBaseElement()
            : base()
        {
            InitializeComponent();
        }
        public frmBaseElement(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

        }

        private void btnShowOnMap_Click(object sender, EventArgs e)
        {
            AdresReestrWork.ShowOnMap(NameTable);
        }

        private void frmBaseElement_Load(object sender, EventArgs e)
        {
            //if (editMode == EditMode.EDIT)
            //{
            //    btnShowOnMap.Enabled = true;
            //}
            //else
            //{
            //    btnShowOnMap.Enabled = false;
            //}
        }


        #endregion



        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  справочник
        //private void btnKodKategorii_Click(object sender, System.EventArgs e)
        //{
        //    string filteredString = "";
        //    if ("KategoriiDorog" == mKategorTablName)
        //        mKodKategorii = frmKategoriiDorog_list.ShowForSelect(filteredString);
        //    else if ("KategoriiMistzevist" == mKategorTablName)
        //        mKodKategorii = frmKategoriiMistzevist_list.ShowForSelect(filteredString);
        //    else if ("KategoriiPlanRecreatsija" == mKategorTablName)
        //        mKodKategorii = frmKategoriiPlanRecreatsija_list.ShowForSelect(filteredString);
        //    else if ("KategoriiVulyts" == mKategorTablName)
        //        mKodKategorii = frmKategoriiVulyts_list.ShowForSelect(filteredString);
        //    else
        //        return;

        //    OnChangedKodKategorii();
        //    errorProvider.SetError(txtKodKategorii, String.Empty);
        //}

        //private void btnAdminRajo_Click(object sender, System.EventArgs e)
        //{
        //    string filteredString = "";
        //    mAdminRajo = frmReestrRajon_list.ShowForSelect(filteredString);
        //    OnChangedAdminRajo();
        //    errorProvider.SetError(txtAdminRajon, String.Empty);
        //}

        //private void btnDocument_Click(object sender, System.EventArgs e)
        //{
        //    string filteredString = "";
        //    mDocument = frmTipDoc_list.ShowForSelect(filteredString);
        //    OnChangedDocument();
        //    errorProvider.SetError(txtDocument, String.Empty);
        //}
        #endregion

        //---------------------------------------------------------------------------------------
        #region  валидация
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //private void main_TextChanged(object sender, System.EventArgs e)
        //{
        //    isModified = true;
        //    ValidatingData();
        //}
        //private void main_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    isModified = true;
        //    e.Cancel = !ValidatingData();
        //}


        #endregion
    }
}
