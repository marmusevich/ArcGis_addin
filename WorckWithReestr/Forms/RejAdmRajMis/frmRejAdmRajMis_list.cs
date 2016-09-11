using System.Windows.Forms;
using SharedClasses;



namespace WorckWithReestr
{
    public partial class frmRejAdmRajMis_list : frmBaseSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            //всегда выбор, редактировать средствами арк мапа
            Form frm = new frmRejAdmRajMis_list(true, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmRejAdmRajMis_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmRejAdmRajMis_list() : base()
        {
            InitializeComponent();
        }

        public frmRejAdmRajMis_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Rej_Adm_Raj_Mis";
            base.NameSortFild = "ID_RAI";

            //this.Text = "";
        }

        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.Visible = false;

            dgv.Columns["ID_RAI"].Visible = true;
            dgv.Columns["NAZVA_UKR"].Visible = true;
            dgv.Columns["NAZVA_ROS"].Visible = true;
            dgv.Columns["NAZVA_LAT"].Visible = true;


            //редактировать средствами арк мапа
            cmsAddCopy.Visible = false;
            cmsEdit.Visible = false;
            cmsDelete.Visible = false;
            tsbAdd.Visible = false;
            tsbAddCopy.Visible = false;
            tsdEdit.Visible = false;
            tsdDelete.Visible = false;

        }

        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            MessageBox.Show("Функция не доступна.");

            return null;
        }

        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            dictionaryTableName = "";
            return false;
        }

        #endregion
    }
}
