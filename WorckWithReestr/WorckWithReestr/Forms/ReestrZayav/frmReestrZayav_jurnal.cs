using System;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmReestrZayav_jurnal : frmBaseJurnal
    {
        int indexFio_Ved_Vid;
        int indexFio_Ved_Prin;
        int indexFio_Z;

        int indexStatus;
        int indexKod_Z;
        //int indexKod_Z_code;

        int indexTip_Doc;
        //int indexTip_Doc_code;

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView()
        {
            Form frm = new frmReestrZayav_jurnal();
            frm.Show();
            frm.Activate();
        }

        public frmReestrZayav_jurnal()
            : base()
        {
            InitializeComponent();


            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.Kn_Reg_Zayv";
            base.NameSortFild = "Data_Z";
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
                                      base.table.FindField("N_Ish_Z"),// 4
                                      base.table.FindField("Data_Ish"),// 5
                                      base.table.FindField("Sodergan"),// 6
                                      base.table.FindField("Fio_Z"),// 7
                                      base.table.FindField("Otkaz"),// 8
                                      base.table.FindField("Pr_Otkaz"),// 9
                                      base.table.FindField("Tip_Doc"),// 10
                                      base.table.FindField("Status"),// 11
                                      base.table.FindField("Tel_Z"),// 12
                                      base.table.FindField("Dodatok"),// 13
                                      base.table.FindField("Cane"),// 14
                                      base.table.FindField("Oplata"),// 15
                                      base.table.FindField("Data_Oplata"),// 16
                                      base.table.FindField("Doc_Oplata"),// 17
                                      base.table.FindField("Data_Ved"),// 18
                                      base.table.FindField("Opisan_Ved"),// 19
                                      base.table.FindField("Forma_Ved"),// 20
                                      base.table.FindField("Fio_Ved_Vid"),// 21
                                      base.table.FindField("Fio_Ved_Prin"),// 22
                                      base.table.FindField("Prim") // 23
                                    };
            SharedClass.SetDisplayOrderByArray(dgv, displayIndicies);
        }

        protected override void OtherSetupDGV()
        {
            indexFio_Ved_Vid = dgv.Columns["Fio_Ved_Vid"].Index;
            indexFio_Ved_Prin = dgv.Columns["Fio_Ved_Prin"].Index;
            indexFio_Z = dgv.Columns["Fio_Z"].Index;

            indexStatus = dgv.Columns["Status"].Index;
            indexKod_Z = dgv.Columns["Kod_Z"].Index;
            //indexKod_Z_code = dgv.Columns.Add("Kod_Z_code", "Код заявника");

            indexTip_Doc = dgv.Columns["Tip_Doc"].Index;
            //indexTip_code = dgv.Columns.Add("Tip_code", "Код типа документа");

            dgv.CellFormatting += OnCellFormatting;
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == indexFio_Ved_Vid)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Fio_Ved_Vid ={0}", e.Value));
                }
            }

            if (e.ColumnIndex == indexFio_Ved_Prin)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Fio_Ved_Prin ={0}", e.Value));
                }
            }

            if (e.ColumnIndex == indexFio_Z)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Fio_Z ={0}", e.Value));
                }
            }

            //
            if (e.ColumnIndex == indexKod_Z)
            {
                try
                {
                    e.FormattingApplied = true;
                    DataGridViewRow row = dgv.Rows[e.RowIndex];
                    string s = row.Cells[indexStatus].Value.ToString();
                    if(s[0] == 'Ю')
                        e.Value = DictionaryWork.GetNameByIDFromJurOsoby(Convert.ToInt32(e.Value));
                    else
                        e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Kod_Z ={0}", e.Value));
                }
            }



            //if (e.ColumnIndex == indexKod_Z_code)
            //{
            //    try
            //    {
            //        e.FormattingApplied = true;
            //        DataGridViewRow row = dgv.Rows[e.RowIndex];
            //        object o = row.Cells[indexStatus].Value;
            //        if (Convert.ToInt32(o) == 0)
            //            e.Value = DictionaryWork.GetINNByIDFromJurOsoby(Convert.ToInt32(e.Value));
            //        else
            //            e.Value = DictionaryWork.GetINNByIDFromFizLic(Convert.ToInt32(e.Value));
            //    }
            //    catch (FormatException ee)
            //    { }
            //}

            if (e.ColumnIndex == indexTip_Doc)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetNameByIDFromTip_Doc(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrZayav_jurnal.OnCellFormatting Tip_Doc ={0}", e.Value));
                }
            }

            //if (e.ColumnIndex == indexTip_code)
            //{
            //    try
            //    {
            //        e.FormattingApplied = true;
            //        e.Value = DictionaryWork.GetCodeByIDFromTip_Doc(Convert.ToInt32(e.Value));
            //    }
            //    catch (FormatException ee)
            //    { }
            //}
        }
        #endregion
    }
}
