using System;
using System.Windows.Forms;
using SharedClasses;

namespace WorckWithReestr
{
    public partial class frmReestrVedomostey_jurnal : frmBaseJurnal
    {
        int indexFIO_Kad;
        int indexTip_Doc;

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView()
        {
            Form frm = new frmReestrVedomostey_jurnal();
            frm.Show();
            frm.Activate();
        }

        public frmReestrVedomostey_jurnal()
            : base()
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.Kn_Reg_Ved";
            base.NameSortFild = "Data_Vh";
            base.NameDataFilteredFild = "Data_Vh";
        }

        protected override frmBaseDocument GetDocumentForm(int _objectID, frmBaseDocument.EditMode _editMode)
        {
            return new frmReestrVedomostey_doc(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {

            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                                      base.table.FindField("N_Vh"),
                                      base.table.FindField("Data_Vh"),
                                      base.table.FindField("Ist_Ved"),
                                      base.table.FindField("N_Sop_List"),
                                      base.table.FindField("Data_Otp"),
                                      base.table.FindField("N_GD"),
                                      base.table.FindField("N_Doc_GD"),
                                      base.table.FindField("Name_GD"),
                                      base.table.FindField("Kol_Str_GD"),
                                      base.table.FindField("N_Kad"),
                                      base.table.FindField("Data_Kad"),
                                      base.table.FindField("Tip_Doc"),
                                      base.table.FindField("El_Format_GD"),
                                      base.table.FindField("FIO_Kad"),
                                      base.table.FindField("Prim")

                                    };
            SharedClass.SetDisplayOrderByArray(dgv, displayIndicies);
        }

        protected override void OtherSetupDGV()
        {
            indexFIO_Kad = dgv.Columns["FIO_Kad"].Index;
            indexTip_Doc = dgv.Columns["Tip_Doc"].Index;

            dgv.CellFormatting += OnCellFormatting;
        }

        //вернуть строку доаолнительных условий
        protected override string GetStringAddetConditions()
        {
            string ret = base.GetStringAddetConditions();
            return ret;
        }
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {

            if(fildName == "FIO_Kad")
            {
                dictionaryTableName = "fizichni_osoby";
                return true;
            }
            else if(fildName == "Tip_Doc")
            {
                dictionaryTableName = "Tip_Doc";
                return true;
            }
            return false;
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == indexFIO_Kad)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = ReestrDictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrVedomostey_jurnal.OnCellFormatting FIO_Kad ={0}", e.Value));
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


    }
}
