//using ESRI.ArcGIS.Display;
//using ESRI.ArcGIS.DisplayUI;
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
    public partial class frmNorthArrowtSetting : Form
    {
    //    private class TextAlignment
    //    {
    //        public esriTextHorizontalAlignment ha;
    //        public esriTextVerticalAlignment va;

    //        public TextAlignment(esriTextHorizontalAlignment _ha, esriTextVerticalAlignment _va)
    //        {
    //            ha = _ha;
    //            va = _va;
    //        }
    //    }

        public OnePageDescriptions m_opd = null;

        private void init()
        {
            if (m_opd == null) return;

            //this.rbAncorPoint_BL.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            //this.rbAncorPoint_BC.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            //this.rbAncorPoint_BR.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            //this.rbAncorPoint_CL.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVACenter);
            //this.rbAncorPoint_CC.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVACenter);
            //this.rbAncorPoint_CR.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVACenter);
            //this.rbAncorPoint_TL.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            //this.rbAncorPoint_TC.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
            //this.rbAncorPoint_TR.Tag = new TextAlignment(esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
            //foreach (Control c in gbAncorPoint.Controls)
            //{
            //    RadioButton rb = c as RadioButton;
            //    if (rb != null)
            //    {
            //        TextAlignment ap = (TextAlignment)rb.Tag;
            //        if (ap != null)
            //            rb.Checked = (ap.ha == m_oted.AncorHorizontal) && (ap.va == m_oted.AncorVertical);
            //    }
            //}


            this.rbPosVer_Botton.Tag = esriTextVerticalAlignment.esriTVABottom;
            this.rbPosVer_Centr.Tag = esriTextVerticalAlignment.esriTVACenter;
            this.rbPosVer_Top.Tag = esriTextVerticalAlignment.esriTVATop;

            this.rbPosHor_Right.Tag = esriTextHorizontalAlignment.esriTHARight;
            this.rbPosHor_Centr.Tag = esriTextHorizontalAlignment.esriTHACenter;
            this.rbPosHor_Left.Tag = esriTextHorizontalAlignment.esriTHALeft;


            foreach (Control c in gbPositionHorizontal.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null)
                    rb.Checked = ((esriTextHorizontalAlignment)rb.Tag) == m_opd.NorthArrow_PagePosHorizontal;
            }
            foreach (Control c in gbPositionVertical.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null)
                    rb.Checked = ((esriTextVerticalAlignment)rb.Tag) == m_opd.NorthArrow_PagePosVertical;
            }

            nudPosVer.Value = (decimal)m_opd.NorthArrow_PosY;
            nudPosHor.Value = (decimal)m_opd.NorthArrow_PosX;
        }

        private void save()
        {
            //foreach (Control c in gbAncorPoint.Controls)
            //{
            //    RadioButton rb = c as RadioButton;
            //    if (rb != null && rb.Checked)
            //    {
            //        TextAlignment ap = (TextAlignment)rb.Tag;
            //        if (ap != null)
            //        {
            //            m_oted.AncorVertical = ap.va;
            //            m_oted.AncorHorizontal = ap.ha;
            //        }
            //    }
            //}

            foreach (Control c in gbPositionHorizontal.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    m_opd.NorthArrow_PagePosHorizontal = (esriTextHorizontalAlignment)rb.Tag;
            }
            foreach (Control c in gbPositionVertical.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Checked)
                    m_opd.NorthArrow_PagePosVertical = (esriTextVerticalAlignment)rb.Tag;
            }

            m_opd.NorthArrow_PosY = (double)nudPosVer.Value;
            m_opd.NorthArrow_PosX = (double)nudPosHor.Value;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////

        private void btnNorthArrowSetting_Click(object sender, EventArgs e)
        {
            // здесь выбор
            ESRI.ArcGIS.Framework.IStyleSelector StyleSelector = new ESRI.ArcGIS.CartoUI.NorthArrowSelector();
            StyleSelector.AddStyle(m_opd.NorthArrow);
            if (StyleSelector.DoModal(WorckWithReestr.ArcMap.Application.hWnd))//;
                m_opd.NorthArrow = StyleSelector.GetStyle(0) as ESRI.ArcGIS.Carto.MarkerNorthArrow;
            //else
            //    MessageBox.Show("false"); // кнопка отмена
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public frmNorthArrowtSetting(OnePageDescriptions opd)
        {
            InitializeComponent();
            m_opd = new OnePageDescriptions();
            m_opd.CopySetingFrom(opd);

        }

        private void frmNorthArrowtSetting_Load(object sender, EventArgs e)
        {
            init();

        }
    }
}
