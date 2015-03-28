
using ESRI.ArcGIS.Geodatabase;
namespace WorckWithReestr
{
    public partial class frmReestrVedomostey_doc : frmBaseDocument
    {
        protected override void DB_to_FormElement(IRow row)
        {
            //OBJECTID
            //N_Vh
            //Data_Vh
            //Ist_Ved
            //N_Sop_List
            //Data_Otp
            //N_GD
            //N_Doc_GD
            //Name_GD
            //Kol_Str_GD
            //El_Format_GD
            //Data_Kad
            //N_Kad
            //FIO_Kad

        }

        protected override void FormElement_to_DB(IRow row)
        {


        }




        public frmReestrVedomostey_doc()
        {
            InitializeComponent();
        }


    }
}
