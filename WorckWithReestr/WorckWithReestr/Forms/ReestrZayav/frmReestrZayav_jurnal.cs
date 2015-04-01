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
            //0-23
            int[] displayIndicies = { 0, 2, 4, 5, 6, 9, 11, 12, 16, 17, 18, 19, 20, 23, 7, 8, 10, 15, 21, 22, 3, 1, 13, 14 };
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
                catch (FormatException ee)
                {}
            }

            if (e.ColumnIndex == indexFio_Ved_Prin)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (FormatException ee)
                { }
            }

            if (e.ColumnIndex == indexFio_Z)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (FormatException ee)
                { }
            }

            //
            if (e.ColumnIndex == indexKod_Z)
            {
                try
                {
                    e.FormattingApplied = true;
                    DataGridViewRow row = dgv.Rows[e.RowIndex];
                    if(Convert.ToInt32(row.Cells[indexStatus].Value) == 0)
                        e.Value = DictionaryWork.GetNameByIDFromJurOsoby(Convert.ToInt32(e.Value));
                    else
                        e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (FormatException ee)
                { }
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
                catch (FormatException ee)
                { }
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
