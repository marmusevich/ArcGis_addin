using System;
using System.Windows.Forms;
using SharedClasses;

namespace WorckWithReestr
{
    public partial class frmReestrZayav_jurnal : frmBaseJurnal
    {
        DomeinDataAdapter ddaStatus;

        int indexFio_Z;
        int indexStatus;
        int indexKod_Z;
        int indexTip_Doc;

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmReestrZayav_jurnal(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmReestrZayav_jurnal frm = new frmReestrZayav_jurnal(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }


        public frmReestrZayav_jurnal()
            : base()
        {
            InitializeComponent();

            cbHaveReferense.SelectedIndex = 0;
            cbReferenceClose.SelectedIndex = 0;

        }

        public frmReestrZayav_jurnal(bool isSelectMode, string filteredString)
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();
            
            cbHaveReferense.SelectedIndex = 0;
            cbReferenceClose.SelectedIndex = 0;

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Kn_Reg_Zayv";
            base.NameSortFild = "Data_Z";
            base.NameDataFilteredFild = "Data_Z";
        }

        protected override frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            return new frmReestrZayav_doc(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                                      base.table.FindField("N_Z"),// 1
                                      base.table.FindField("Data_Z"),// 2
                                      base.table.FindField("Kod_Z"),// 3
                                      base.table.FindField("Sodergan"),// 6
                                      base.table.FindField("Adress_Text"),
                                      base.table.FindField("Fio_Z"),// 7
                                      base.table.FindField("Tip_Doc"),// 10
                                      base.table.FindField("Status"),// 11
                                      base.table.FindField("Cane"),// 14
                                      base.table.FindField("Cane_Date"),
                                      base.table.FindField("qr_kod"),
                                      base.table.FindField("Prim"),
                                      base.table.FindField("IsHaveReferense"),
                                      base.table.FindField("IsReferenceClose"),
                                      base.table.FindField("MapObjectID")
                                    };
            GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }

        protected override void OtherSetupDGV()
        {
            //доменные значения
            ddaStatus = new DomeinDataAdapter(base.table.Fields.get_Field(base.table.FindField("Status")).Domain);

            indexFio_Z = dgv.Columns["Fio_Z"].Index;
            indexStatus = dgv.Columns["Status"].Index;
            indexKod_Z = dgv.Columns["Kod_Z"].Index;

            indexTip_Doc = dgv.Columns["Tip_Doc"].Index;

            dgv.Columns["GlobalID"].Visible = false;
            dgv.Columns["MapObjectID"].Visible = false;

            //2022-04-03 hide some filds by customer willing
            dgv.Columns["Oplata"].Visible = false;
            dgv.Columns["Data_Oplata"].Visible = false;
            dgv.Columns["Doc_Oplata"].Visible = false;
            dgv.Columns["Data_Ved"].Visible = false;
            dgv.Columns["Opisan_Ved"].Visible = false;
            dgv.Columns["Forma_Ved"].Visible = false;
            dgv.Columns["Fio_Ved_Vid"].Visible = false;
            dgv.Columns["Fio_Ved_Prin"].Visible = false;
            //dgv.Columns["Prim"].Visible = false; // 2022-04-10 - enable
            dgv.Columns["Rajon"].Visible = false;
            dgv.Columns["Tel_Z"].Visible = false;
            dgv.Columns["Dodatok"].Visible = false;
            dgv.Columns["Otkaz"].Visible = false;
            dgv.Columns["Pr_Otkaz"].Visible = false;
            dgv.Columns["N_Ish_Z"].Visible = false;
            dgv.Columns["Data_Ish"].Visible = false;
            //END  2022-04-03 hide some filds by customer willing

            dgv.CellFormatting += OnCellFormatting;
        }

        //вернуть строку доаолнительных условий
        protected override string GetStringAddetConditions()
        {
            string ret = base.GetStringAddetConditions();

            if (cbHaveReferense.SelectedIndex == 1)
                ret += " IsHaveReferense = 1 "; // "Имеющие кадастровую справку"
            else if (cbHaveReferense.SelectedIndex == 2)
                ret += " IsHaveReferense = 0 "; // "Не имеющие кадастровую справку"

            if (cbReferenceClose.SelectedIndex == 1)
                ret += " IsReferenceClose = 1 "; // "Закрыта справка"
            else if (cbReferenceClose.SelectedIndex == 2)
                ret += " IsReferenceClose = 0 "; // "Открыта справка"

            return ret;
        }
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            if (fildName == "Fio_Z")
            {
                dictionaryTableName = "fizichni_osoby";
                return true;
            }
            else if (fildName == "Kod_Z")
            {
                dictionaryTableName = "fizichni_osoby, jur_osoby";
                return true;
            }
            else if (fildName == "Tip_Doc")
            {
                dictionaryTableName = "Tip_Doc";
                return true;
            }

            return false;
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == indexFio_Z)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = ReestrDictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Fio_Z ={0}", e.Value));
                }
            }
            if (e.ColumnIndex == indexKod_Z)
            {
                try
                {
                    e.FormattingApplied = true;
                    DataGridViewRow row = dgv.Rows[e.RowIndex];
                    if (ddaStatus.GetIndexByText(row.Cells[indexStatus].Value.ToString()) == 0)
                       e.Value = ReestrDictionaryWork.GetNameByIDFromJurOsoby(Convert.ToInt32(e.Value));
                    else
                        e.Value = ReestrDictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Kod_Z ={0}", e.Value));
                }
            }
            if (e.ColumnIndex == indexTip_Doc)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = ReestrDictionaryWork.GetNameByIDFromTip_Doc(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Tip_Doc ={0}", e.Value));
                }
            }
        }
        #endregion

        private void cbHaveReferense_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableWrapper != null)
            {
                tableWrapper.QueryFilter = BuildConditions();
                Reflesh();
            }
        }

        private void cbReferenceClose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tableWrapper != null)
            {
                tableWrapper.QueryFilter = BuildConditions();
                Reflesh();
            }
        }
    }
}

