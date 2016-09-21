//using ESRI.ArcGIS.Display;
//using ESRI.ArcGIS.DisplayUI;
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
        private class TextAlignment
        {
            public esriTextHorizontalAlignment ha;
            public esriTextVerticalAlignment va;

            public TextAlignment(esriTextHorizontalAlignment _ha, esriTextVerticalAlignment _va)
            {
                ha = _ha;
                va = _va;
            }
        }

        public OneTextElementDescription m_oted = null;

        private void init()
        {
            if (m_oted == null) return;

            this.rbAncorPoint_BL.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            this.rbAncorPoint_BC.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            this.rbAncorPoint_BR.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            this.rbAncorPoint_CL.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVACenter);
            this.rbAncorPoint_CC.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVACenter);
            this.rbAncorPoint_CR.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVACenter);
            this.rbAncorPoint_TL.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            this.rbAncorPoint_TC.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
            this.rbAncorPoint_TR.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);

            this.rbPosVer_Botton.Tag = esriTextVerticalAlignment.esriTVABottom;
            this.rbPosVer_Centr.Tag = esriTextVerticalAlignment.esriTVACenter;
            this.rbPosVer_Top.Tag = esriTextVerticalAlignment.esriTVATop;

            this.rbPosHor_Right.Tag = esriTextHorizontalAlignment.esriTHARight;
            this.rbPosHor_Centr.Tag = esriTextHorizontalAlignment.esriTHACenter;
            this.rbPosHor_Left.Tag = esriTextHorizontalAlignment.esriTHALeft;

            foreach (Control c in gbAncorPoint.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null)
                {
                    TextAlignment ap = (TextAlignment)rb.Tag;
                    if (ap != null)
                        rb.Checked = (ap.ha == m_oted.AncorHorizontal) && (ap.va == m_oted.AncorVertical);
                }
            }
            foreach (Control c in gbPositionHorizontal.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null )
                    rb.Checked = ((esriTextHorizontalAlignment)rb.Tag) == m_oted.PagePosHorizontal;
            }
            foreach (Control c in gbPositionVertical.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null)
                    rb.Checked = ((esriTextVerticalAlignment)rb.Tag) == m_oted.PagePosVertical;
            }

            nudPosVer.Value = (decimal)m_oted.PosY;
            nudPosHor.Value = (decimal)m_oted.PosX;
            txtText.Text = m_oted.Text;
        }

        private void save()
        {
            foreach (Control c in gbAncorPoint.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                {
                    TextAlignment ap = (TextAlignment)rb.Tag;
                    if (ap != null)
                    {
                        m_oted.AncorVertical = ap.va;
                        m_oted.AncorHorizontal = ap.ha;
                    }
                }
            }
            foreach (Control c in gbPositionHorizontal.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    m_oted.PagePosHorizontal = (esriTextHorizontalAlignment)rb.Tag;
            }
            foreach (Control c in gbPositionVertical.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    m_oted.PagePosVertical = (esriTextVerticalAlignment)rb.Tag;
            }

            m_oted.PosY = (double)nudPosVer.Value;
            m_oted.PosX = (double)nudPosHor.Value;
            m_oted.Text = txtText.Text;
        }

        //private static void OpenSymbolEditor(TextSymbolClass symbol)
        //{
        //    ISymbolEditor symbolEditor = new SymbolEditor();
        //    symbolEditor.Title = "Edit My Marker";
        //    if (!symbolEditor.EditSymbol(symbol as ISymbol, 0))
        //    {
        //        //Return a message here.
        //        MessageBox.Show("!!!");
        //    }
        //    else
        //    {
        //        //Do something with the edited symbol.
        //        //A multi-layer symbol will be returned.
        //        IMarkerSymbol newSymbol = null;
        //        IMultiLayerMarkerSymbol multiMarker = null;
        //        multiMarker = symbol as IMultiLayerMarkerSymbol;
        //        newSymbol = multiMarker.get_Layer(0);
        //        symbol = multiMarker.get_Layer(0) as ITextSymbol;
        //    }
        //}
        ////textElement.Symbol
        private void OpenSymbolSelecter(TextSymbolClass symbol)
        {
        //    ISymbolSelector symbolSelector = new SymbolSelector();
        //    if ((symbolSelector.AddSymbol((ISymbol)symbol)))
        //    {
        //        if (symbolSelector.SelectSymbol(0))
        //        {
        //            symbol = symbolSelector.GetSymbolAt(0) as ITextSymbol;
        //        }
        //    }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        private void btnFontSetting_Click(object sender, EventArgs e)
        {
            OpenSymbolSelecter(m_oted.TextSymbolClass);
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

        public frmTextSetting(OneTextElementDescription oted)
        {
            InitializeComponent();
            m_oted = new CadastralReference.OneTextElementDescription();
            m_oted.CopySetingFrom(oted);
            init();
        }
    }
}
