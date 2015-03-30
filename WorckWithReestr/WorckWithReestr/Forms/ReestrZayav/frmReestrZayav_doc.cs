using ESRI.ArcGIS.Geodatabase;
using System;

namespace WorckWithReestr
{
    public partial class frmReestrZayav_doc : frmBaseDocument
    {

        protected override void DB_to_FormElement(IRow row)
        {
            ////OBJECTID
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("N_Z")) as string;
            //dtpDataObr.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Z")));
            
            //cbZamovnik.Text = "" + row.get_Value(base.table.FindField("Name_Z")) as string;
            
            //txtNomerVhod.Text = "" + row.get_Value(base.table.FindField("N_Ish_Z")) as string;
            //dtpDataVhod.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Ish")) );
            //txtZmist.Text = "" + row.get_Value(base.table.FindField("Sodergan")) as string;

            //cblblTipDok.Text = "" + row.get_Value(base.table.FindField("Tip_Doc")) as string;
            //cbOtmetkaOtkaz.Text = "" + row.get_Value(base.table.FindField("Otkaz")) as string;
            //cbPrichinaOtkaz.Text = "" + row.get_Value(base.table.FindField("Pr_Otkaz")) as string;
            //cbStatusZayavnika.Text = "" + row.get_Value(base.table.FindField("Status")) as string;
            
            //txtKodZayavnika.Text = "" + row.get_Value(base.table.FindField("Kod_Z")) as string;
            //txtEMael.Text = "" + row.get_Value(base.table.FindField("Tel_Z")) as string;
            
            ////cb.Text = "" + row.get_Value(base.table.FindField("fio_z_code")) as string;
            
            //cbStatusNaddanyaPoslugi.Text = "" + row.get_Value(base.table.FindField("Oplata")) as string;

            //dtpDataSplati.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Oplata")));
            //txtDokProSplatu.Text = "" + row.get_Value(base.table.FindField("Doc_Oplata")) as string;
            //dtpDataNadannyaPoslugi.Value = SharedClass.ConvertVolueToDateTime(row.get_Value(base.table.FindField("Data_Ved")));
            
            //txtOpisNadannoiPoslugi.Text = "" + row.get_Value(base.table.FindField("Opisan_Ved")) as string;
            //txtFormaPeredachiPoslugi.Text = "" + row.get_Value(base.table.FindField("Forma_Ved")) as string;
            
            //cbFIOVikonovucha.Text = "" + row.get_Value(base.table.FindField("Fio_Ved_Vid")) as string;
            //cbFIOPoluchil.Text = "" + row.get_Value(base.table.FindField("Fio_Ved_Prin")) as string;
            
            ////txt.Text = "" + row.get_Value(base.table.FindField("Prim")) as string;
            
            
            
            ////cbFIOPoluchil.Text = "" + row.get_Value(base.table.FindField("Fio_Z")) as string;
        }

        protected override void FormElement_to_DB(IRow row)
        {
            //row.set_Value(base.table.FindField("N_Z"), short.Parse( txtNomerZvernennya.Text ));
            //row.set_Value(base.table.FindField("Data_Z"), dtpDataObr.Value);
            
            ////row.set_Value(base.table.FindField("Name_Z"), );
            
            //row.set_Value(base.table.FindField("N_Ish_Z"), txtNomerVhod.Text);
            //row.set_Value(base.table.FindField("Data_Ish"), dtpDataVhod.Value);
            //row.set_Value(base.table.FindField("Sodergan"), txtZmist.Text);
            
            //row.set_Value(base.table.FindField("Tip_Doc"), cblblTipDok.Text);
            //row.set_Value(base.table.FindField("Otkaz"), cbOtmetkaOtkaz.Text);
            //row.set_Value(base.table.FindField("Pr_Otkaz"), cbPrichinaOtkaz.Text);
            //row.set_Value(base.table.FindField("Status"), cbStatusZayavnika.Text);
            
            //row.set_Value(base.table.FindField("Kod_Z"), txtKodZayavnika.Text);
            //row.set_Value(base.table.FindField("Tel_Z"), txtEMael.Text);
            
            ////row.set_Value(base.table.FindField("fio_z_code"), );

            //row.set_Value(base.table.FindField("Oplata"), cbStatusNaddanyaPoslugi.Text);
            
            //row.set_Value(base.table.FindField("Data_Oplata"), dtpDataSplati.Value);
            //row.set_Value(base.table.FindField("Doc_Oplata"), txtDokProSplatu.Text);
            //row.set_Value(base.table.FindField("Data_Ved"), dtpDataNadannyaPoslugi.Value);
            //row.set_Value(base.table.FindField("Opisan_Ved"), txtOpisNadannoiPoslugi.Text);
            //row.set_Value(base.table.FindField("Forma_Ved"), txtFormaPeredachiPoslugi.Text);

            //row.set_Value(base.table.FindField("Fio_Ved_Vid"), cbFIOVikonovucha.Text);
            //row.set_Value(base.table.FindField("Fio_Ved_Prin"), cbFIOPoluchil.Text);
            
            ////row.set_Value(base.table.FindField("Prim"), );
            
            ////row.set_Value(base.table.FindField("Fio_Z"), );
        }


        public frmReestrZayav_doc() : base()
        {
            InitializeComponent();
        }

        public frmReestrZayav_doc(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();
            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.Kn_Reg_Zayv";

        }

        private void frmReestrZayav_doc_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    //Text = "Добавление нового физического лица";
                    break;
                case EditMode.EDIT:
                    //Text = "Корректировка данных физического лица";
                    break;
                case EditMode.DELETE:
                    //Text = "Удаление физического лица";
                    btnOk.Text = "Удалить";
                    //txtID.Enabled = false;
                    //txtFIO.Enabled = false;
                    //txtINN.Enabled = false;
                    //txtCategor.Enabled = false;
                    //cbIsWorker.Enabled = false;

                    break;

                default:
                    this.Close();
                    return;
            }

            if (editMode != EditMode.ADD)
            {
                if (!this.ReadData()) // error
                {
                    SharedClass.ShowErrorMessage();
                    this.Close();
                }
            }
        }

        private void cbFIOPoluchil_DropDown(object sender, EventArgs e)
        {
            frmFizLic_list.ShowForSelect();
        }



    }
}
