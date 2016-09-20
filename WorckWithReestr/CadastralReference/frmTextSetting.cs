using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DisplayUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmTextSetting : Form
    {
        public frmTextSetting()
        {
            InitializeComponent();

            //инить перечислениями выравнивания
            this.rbAncorPoint_BL.Tag = new int[] { -1, -1 };
            this.rbAncorPoint_BC.Tag = new int[] { -1, 00 };
            this.rbAncorPoint_BR.Tag = new int[] { -1, 01 };
            this.rbAncorPoint_CL.Tag = new int[] { 00, -1 };
            this.rbAncorPoint_CC.Tag = new int[] { 00, 00 };
            this.rbAncorPoint_CR.Tag = new int[] { 00, 01 };
            this.rbAncorPoint_TL.Tag = new int[] { 01, -1 };
            this.rbAncorPoint_TC.Tag = new int[] { 01, 00 };
            this.rbAncorPoint_TR.Tag = new int[] { 01, 01 };

            //HorizontalAlignment = ESRI.ArcGIS.Display.esriTextHorizontalAlignment.esriTHARight;
            //VerticalAlignment = ESRI.ArcGIS.Display.esriTextVerticalAlignment.esriTVATop;



            this.rbPosVer_Botton.Tag = "низ";
            this.rbPosVer_Centr.Tag = "центр";
            this.rbPosVer_Top.Tag = "верх";

            this.rbPosGor_Right.Tag = "право";
            this.rbPosGor_Centr.Tag = "центр";
            this.rbPosGor_Left.Tag = "лево"; 
        }

        private void save()
        {
            int[] AncorPoint = null;

            string PosVer ="";
            string PosGor = "";

            foreach (Control c in gbAncorPoint.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    AncorPoint = (int[])rb.Tag;
            }
            foreach (Control c in gbPositionGorizontal.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    PosGor = (string)rb.Tag;
            }
            foreach (Control c in gbPositionVertical.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    PosVer = (string)rb.Tag;
            }
            MessageBox.Show(string.Format("AncorPoint({0} - {1}) \n PosVer - {2} = {3} \n PosGor - {4} = {5}", AncorPoint[0], AncorPoint[1], PosVer, nudPosVer.Value, PosGor, nudPosGor.Value));
        }

        //textElement.Symbol
        private static void OpenSymbolSelecter(ITextSymbol symbol)
        {
            ISymbolSelector symbolSelector = new SymbolSelector();
            if ((symbolSelector.AddSymbol((ISymbol)symbol)))
            {
                if (symbolSelector.SelectSymbol(0))
                {
                    symbol = symbolSelector.GetSymbolAt(0) as ITextSymbol;
                }
            }
        }

        private static void OpenSymbolEditor(ITextSymbol symbol)
        {
            ISymbolEditor symbolEditor = new SymbolEditor();
            symbolEditor.Title = "Edit My Marker";
            if (!symbolEditor.EditSymbol(symbol as ISymbol, 0))
            {
                //Return a message here.
                MessageBox.Show("!!!");
            }
            else
            {
                //Do something with the edited symbol.
                //A multi-layer symbol will be returned.
                IMarkerSymbol newSymbol = null;
                IMultiLayerMarkerSymbol multiMarker = null;
                multiMarker = symbol as IMultiLayerMarkerSymbol;
                newSymbol = multiMarker.get_Layer(0);
                symbol = multiMarker.get_Layer(0) as ITextSymbol;
            }
        }












        private void btnFontSetting_Click(object sender, EventArgs e)
        {


        }

        private void btnHelpTemplate_Click(object sender, EventArgs e)
        {
            Form frm = new frmHelpTemplateView();
            frm.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            save();

            this.Close();
        }

        private void rbAncorPoint_change(object sender, EventArgs e)
        {
        }
    }
}
