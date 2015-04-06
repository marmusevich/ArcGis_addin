using System;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmReestrVedomostey_jurnal : frmBaseJurnal
    {
        int indexFIO_Kad;

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

            //int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
            //                          base.table.FindField("N_Z"),// 1
            //                          base.table.FindField("Data_Z"),// 2
            //                          base.table.FindField("Kod_Z"),// 3
            //                          base.table.FindField("N_Ish_Z"),// 4
            //                          base.table.FindField("Data_Ish"),// 5
            //                          base.table.FindField("Sodergan"),// 6
            //                          base.table.FindField("Fio_Z"),// 7
            //                          base.table.FindField("Otkaz"),// 8
            //                          base.table.FindField("Pr_Otkaz"),// 9
            //                          base.table.FindField("Tip_Doc"),// 10
            //                          base.table.FindField("Status"),// 11
            //                          base.table.FindField("Tel_Z"),// 12
            //                          base.table.FindField("Dodatok"),// 13
            //                          base.table.FindField("Cane"),// 14
            //                          base.table.FindField("Oplata"),// 15
            //                          base.table.FindField("Data_Oplata"),// 16
            //                          base.table.FindField("Doc_Oplata"),// 17
            //                          base.table.FindField("Data_Ved"),// 18
            //                          base.table.FindField("Opisan_Ved"),// 19
            //                          base.table.FindField("Forma_Ved"),// 20
            //                          base.table.FindField("Fio_Ved_Vid"),// 21
            //                          base.table.FindField("Fio_Ved_Prin"),// 22
            //                          base.table.FindField("Prim") // 23
            //                        };
            //SharedClass.SetDisplayOrderByArray(dgv, displayIndicies);
        }

        protected override void OtherSetupDGV()
        {
            indexFIO_Kad = dgv.Columns["FIO_Kad"].Index;
            dgv.CellFormatting += OnCellFormatting;
        }

        //вернуть строку доаолнительных условий
        protected override string GetStringAddetConditions()
        {
            string ret = base.GetStringAddetConditions();
            return ret;
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == indexFIO_Kad)
            {
                try
                {
                    e.FormattingApplied = true;
                    e.Value = DictionaryWork.GetFIOByIDFromFizLic(Convert.ToInt32(e.Value));
                }
                catch (Exception ex) // обработка ошибок
                {
                    Logger.Write(ex, string.Format("frmReestrVedomostey_jurnal.OnCellFormatting FIO_Kad ={0}", e.Value));
                }
            }

        }
        #endregion


    }
}
