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

namespace DemoTest
{


    public partial class frmFizLic_element : Form
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------


        public enum EditMode
        {
            UNKNOW,
            ADD,
            EDIT,
            DELETE
        };

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------


        private object objectID = null;
        private EditMode editMode = EditMode.UNKNOW;
        private bool isModified = false;

        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        private bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                ITable table_fizLic = fws.OpenTable("reestr.DBO.fizichni_osoby");

                IRow row = table_fizLic.GetRow((int)objectID);

                // взять из базы
                txtID.Text = "" + row.get_Value(0) as string;
                txtFIO.Text = row.get_Value(1) as string;
                txtCategor.Text = row.get_Value(2) as string;
                txtINN.Text = row.get_Value(3) as string;

                if ((int)row.get_Value(4) == 1)
                    cbIsWorker.Checked = true;
                else
                    cbIsWorker.Checked = false;

                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            isModified = false;
            return ret;
        }

        private bool SaveData()
        {
            bool ret = false;
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as  IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                ITable table_fizLic = fws.OpenTable("reestr.DBO.fizichni_osoby");
                IRow row = null;

                if (editMode == EditMode.ADD)
                    row = table_fizLic.CreateRow();
                else if (editMode == EditMode.EDIT)
                    row = table_fizLic.GetRow((int)objectID);
                else
                    return false;

                // положить в базы
                row.set_Value(1, txtFIO.Text);
                row.set_Value(2, txtCategor.Text);
                row.set_Value(3, txtINN.Text);

                if (cbIsWorker.Checked)
                    row.set_Value(4, 1);
                else
                    row.set_Value(4, 0);

                row.Store();

                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);

                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            finally
            {
                    if ((wse != null) && wse.IsBeingEdited())
                    {
                        wse.StopEditing(false);
                    }
            }

            return ret;
        }

        private bool DeleteData()
        {
            bool ret = false;
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace("reestr") as IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                ITable table_fizLic = fws.OpenTable("reestr.DBO.fizichni_osoby");

                IRow row = table_fizLic.GetRow((int)objectID);
                row.Delete();

                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);


                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            finally
            {
                if ((wse != null) && wse.IsBeingEdited())
                {
                    wse.StopEditing(false);
                }
            }            
            
            return ret;
        }


        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public frmFizLic_element(object _objectID, EditMode _editMode)
        {
            InitializeComponent();

            objectID = _objectID;
            editMode = _editMode;
        }


        private void frmFizLic_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового физического лица";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных физического лица";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление физического лица";
                    btnOk.Text = "Удалить";

                    txtID.Enabled = false;
                    txtFIO.Enabled = false;
                    txtINN.Enabled = false;
                    txtCategor.Enabled = false;
                    cbIsWorker.Enabled = false;

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

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool ret = false;

            if (editMode == EditMode.DELETE)
            {
                if (MessageBox.Show("Удалить выбранное физическое лицо?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ret = DeleteData();
                }
            }
            else
            {
                ret = SaveData();
                isModified = false;
            }

            //-------------------
            if (!ret) // error
            {
                SharedClass.ShowErrorMessage();
            }

            this.Close();
        }

        private void frmFizLic_element_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified && editMode != EditMode.DELETE)
            {
                if (MessageBox.Show("Сохранить внесенные изменения?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveData();
                }
            }
        }

        private void txtFIO_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
        }

        private void txtINN_TextChanged(object sender, EventArgs e)
        {
            isModified = true;

        }

        private void txtCategor_TextChanged(object sender, EventArgs e)
        {
            isModified = true;

        }

        private void cbIsWorker_CheckedChanged(object sender, EventArgs e)
        {
            isModified = true;

        }


        #endregion

    }
}
