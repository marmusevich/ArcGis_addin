using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoTestMap
{

    public partial class frmFindAndSelect_MapObject : Form
    {
         //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types

        //вспомогательный для выбора значений
        class SelectData
        {
            public readonly object Value;
            public readonly string Text;

            public SelectData(object Value, string Text)
            {
                this.Value = Value;
                this.Text = Text;
            }

            public override string ToString()
            {
                return this.Text;
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------


        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        private bool ReadData()
        {
            bool ret = false;
            try
            {
/*
                txtInfo.Text = "";

                IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
                IMap map = mxDoc.FocusMap as IMap;

                  
                IEnumLayer enumLayer = map.Layers;
                ILayer layer = enumLayer.Next();
                while (layer != null)
                {
                    txtInfo.Text += "Fayer name :" + Environment.NewLine; //(count = "+enumLayer.+")
                    if (layer is IFeatureLayer2)
                    {
                        txtInfo.Text += layer.Name + Environment.NewLine;
                        IFeatureLayer2 statesLayer = layer as IFeatureLayer2;
                        if (statesLayer != null)
                        {
                            IFeatureCursor statesFCursor = statesLayer.FeatureClass.Search(null, true);
                            txtInfo.Text += "Fields (count = " + statesFCursor.Fields.FieldCount + "):" + Environment.NewLine;
                            for (int i = 0; i < statesFCursor.Fields.FieldCount; i++)
                            {
                                txtInfo.Text += "\t" + statesFCursor.Fields.get_Field(i).Name + " - ";
                                IFeature state = statesFCursor.NextFeature();
                                if (state != null)
                                {
                                    txtInfo.Text += state.Value[i].ToString();
                                }
                                txtInfo.Text += Environment.NewLine;
                            }
                        }
                    }
                    txtInfo.Text += Environment.NewLine;
                    layer = enumLayer.Next();
                }
 */
                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            return ret;
        }


        private bool FillIn_SelTab()
        {
            bool ret = false;
            try
            {
                cbSelTab.Items.Clear();

                IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
                IMap map = mxDoc.FocusMap as IMap;

                IEnumLayer enumLayer = map.Layers;
                ILayer layer = enumLayer.Next();
                while (layer != null)
                {
                    if (layer is IFeatureLayer2)
                    {
                        //txtInfo.Text += layer.Name + Environment.NewLine;
                        IFeatureLayer2 featureLayer = layer as IFeatureLayer2;

                        cbSelTab.Items.Add( new SelectData( layer, layer.Name ) );

                    }
                    layer = enumLayer.Next();
                }


                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            return ret;
        }

        private bool FillIn_SelFild()
        {
            bool ret = false;
            try
            {
                SelectData selLayer = cbSelTab.SelectedItem as SelectData;
                IFeatureLayer2 layer = selLayer.Value as IFeatureLayer2;

                if (layer != null)
                {
                    IFeatureCursor statesFCursor = layer.FeatureClass.Search(null, true);
                    for (int i = 0; i < statesFCursor.Fields.FieldCount; i++)
                    {
                        cblSelFild.Items.Add(new SelectData(statesFCursor.Fields.get_Field(i), statesFCursor.Fields.get_Field(i).Name));
                    }
                }
                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            return ret;
        }

        private bool FillIn_SelVal()
        {
            bool ret = false;
            try
            {

                string Text = "";


                SelectData selLayer = cbSelTab.SelectedItem as SelectData;
                IFeatureLayer2 layer = selLayer.Value as IFeatureLayer2;
                SelectData selFild = cblSelFild.SelectedItem as SelectData;

                IFeatureCursor featureCursor = layer.FeatureClass.Search(null, true);

                int fieldIndex = featureCursor.Fields.FindField(selFild.Text);

                IFeature feature = featureCursor.NextFeature();
                while (feature != null)
                {
                    cblSelFild.Items.Add(new SelectData(feature.get_Value(fieldIndex), "" + (feature.get_Value(fieldIndex).ToString() as string) ));
                    //cblSelFild.Items.Add(feature.Value[fieldIndex]);
                    //Text += feature.get_Value(fieldIndex);
                    //Text += feature.ToString();
                    feature = featureCursor.NextFeature();
                }

                System.Windows.Forms.MessageBox.Show(Text);

                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            return ret;
        }


        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------

        // -ctor
        public frmFindAndSelect_MapObject()
        {
            InitializeComponent();
        }

        private void frmFindAndSelect_MapObject_Load(object sender, EventArgs e)
        {
            cbSelTab.Items.Clear();
            cblSelFild.Items.Clear();
            cbSelVal.Items.Clear();
            FillIn_SelTab();

            cblSelFild.Enabled = false;
            cbSelVal.Enabled = false;
        }

        private void cbSelTab_SelectedValueChanged(object sender, EventArgs e)
        {
            cblSelFild.Items.Clear();
            cbSelVal.Items.Clear();
            FillIn_SelFild();
            cblSelFild.Enabled = true;
            cbSelVal.Enabled = false;
        }

        private void cblSelFild_SelectedValueChanged(object sender, EventArgs e)
        {
            cbSelVal.Items.Clear();
            FillIn_SelVal();
            cbSelVal.Enabled = true;
        }

        private void cbSelVal_SelectedValueChanged(object sender, EventArgs e)
        {
            //btnOk.Enabled = true;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}



