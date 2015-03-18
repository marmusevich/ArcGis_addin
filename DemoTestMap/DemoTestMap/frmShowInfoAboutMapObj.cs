using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
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
    public partial class frmShowInfoAboutMapObj : Form
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
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
                txtInfo.Text = "";

                IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
                IMap map = mxDoc.FocusMap as IMap;

                IEnumFeature enumFeature = map.FeatureSelection as IEnumFeature;

                IFeature f = enumFeature.Next();
                while (f != null)
                {

                    IObjectClass cc = f.Class;
                    txtInfo.Text += "AliasName = " + cc.AliasName + Environment.NewLine;
                    txtInfo.Text += "ID = " + f.OID + Environment.NewLine;


                    txtInfo.Text += "Fields (count = " + f.Fields.FieldCount + "):" + Environment.NewLine;
                    for (int i = 0; i < f.Fields.FieldCount; i++)
                    {
                        txtInfo.Text += "\t" + f.Fields.get_Field(i).Name + " - ";
                        txtInfo.Text += f.Value[i].ToString() as string;
                        
                        txtInfo.Text += Environment.NewLine;
                    }
                    txtInfo.Text += Environment.NewLine;

                    f = enumFeature.Next();
                }
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
        public frmShowInfoAboutMapObj()
        {
            InitializeComponent();
        }

        private void frmShowInfoAboutMapObj_Load(object sender, EventArgs e)
        {
            ReadData();
        }


        #endregion

    }
}


