using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace WorckWithReestr
{
    public partial class frmBaseSpr_element : Form
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
        protected int objectID;
        protected EditMode editMode = EditMode.UNKNOW;
        protected bool isModified = false;

        protected string NameWorkspace = "";
        protected string NameTable = "";

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected virtual void DB_to_FormElement(IRow row)
        {
        }

        protected virtual void FormElement_to_DB(IRow row)
        {
        }






        protected virtual bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                ITable table_fizLic = fws.OpenTable(NameTable);

                IRow row = table_fizLic.GetRow(objectID);
                DB_to_FormElement(row);

                ret = true;
            }
            catch (Exception e) // доработать блок ошибок на разные исключения
            {
                ret = false;
            }
            isModified = false;
            return ret;
        }

        protected virtual bool SaveData()
        {
            bool ret = false;
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                ITable table_fizLic = fws.OpenTable(NameTable);
                IRow row = null;

                if (editMode == EditMode.ADD)
                    row = table_fizLic.CreateRow();
                else if (editMode == EditMode.EDIT)
                    row = table_fizLic.GetRow(objectID);
                else
                    return false;

                FormElement_to_DB(row);
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

        protected virtual bool DeleteData()
        {
            bool ret = false;
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                // начать транзакцию
                wse = fws as IWorkspaceEdit;
                wse.StartEditing(false);
                wse.StartEditOperation();

                ITable table_fizLic = fws.OpenTable(NameTable);

                IRow row = table_fizLic.GetRow(objectID);
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
        public frmBaseSpr_element()
        {
            InitializeComponent();

            objectID = -1;
            editMode = EditMode.UNKNOW;
        }

        public frmBaseSpr_element(int _objectID, EditMode _editMode)
        {
            InitializeComponent();

            objectID = _objectID;
            editMode = _editMode;
        }

        private void frmBaseSpr_element_Load(object sender, EventArgs e)
        {

        }

        protected void frmBaseSpr_element_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified && editMode != EditMode.DELETE)
            {
                if (MessageBox.Show("Сохранить внесенные изменения?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveData();
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            bool ret = false;

            if (editMode == EditMode.DELETE)
            {
                if (MessageBox.Show("Удалить выбранную запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        #endregion
    }
}
