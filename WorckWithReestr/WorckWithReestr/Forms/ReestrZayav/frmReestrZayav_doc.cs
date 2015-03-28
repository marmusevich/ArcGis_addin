using ESRI.ArcGIS.Geodatabase;
using System;

namespace WorckWithReestr
{
    public partial class frmReestrZayav_doc : frmBaseDocument
    {

        protected override void DB_to_FormElement(IRow row)
        {
            //OBJECTID
            txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("N_Z")) as string;
            dtpDataObr.Value = (DateTime)row.get_Value(base.table.FindField("Data_Z"));
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Name_Z")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("N_Ish_Z")) as string;
            //dtpDataVhod.Value = row.get_Value(base.table.FindField("Data_Ish")) as DateTime;
            //txtZmist.Text = "" + row.get_Value(base.table.FindField("Sodergan")) as string;
            //txt.Text = "" + row.get_Value(base.table.FindField("Tip_Doc")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Otkaz")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Pr_Otkaz")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Status")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Kod_Z")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Tel_Z")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("fio_z_code")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Oplata")) as string;
            //dtpDataSplati = row.get_Value(base.table.FindField("Data_Oplata")) as DateTime;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Doc_Oplata")) as string;
            //dtpDataNadannyaPoslugi = row.get_Value(base.table.FindField("Data_Ved")) as DateTime;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Opisan_Ved")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Forma_Ved")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Fio_Ved_Vid")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Fio_Ved_Prin")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Prim")) as string;
            //txtNomerZvernennya.Text = "" + row.get_Value(base.table.FindField("Fio_Z")) as string;
        }

        protected override void FormElement_to_DB(IRow row)
        {
            row.set_Value(base.table.FindField("N_Z"), txtNomerZvernennya.Text);
            row.set_Value(base.table.FindField("Data_Z"), dtpDataObr.Value);
            //row.set_Value(base.table.FindField("Name_Z"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("N_Ish_Z"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Data_Ish"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Sodergan"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Tip_Doc"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Otkaz"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Pr_Otkaz"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Status"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Kod_Z"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Tel_Z"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("fio_z_code"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Oplata"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Data_Oplata"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Doc_Oplata"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Data_Ved"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Opisan_Ved"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Forma_Ved"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Fio_Ved_Vid"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Fio_Ved_Prin"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Prim"), txtNomerZvernennya.Text);
            //row.set_Value(base.table.FindField("Fio_Z"), txtNomerZvernennya.Text);
        }





        public frmReestrZayav_doc()
        {
            InitializeComponent();
        }
    }
}
