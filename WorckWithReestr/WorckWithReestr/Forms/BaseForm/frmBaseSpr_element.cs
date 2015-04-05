//#define CONSTRUCT_FORM

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
        protected ITable table = null;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - call back
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //получение данных из базы и иницилизация значений элементов управлений
        protected virtual void DB_to_FormElement(IRow row)
        {
        }
        //сохранение значений элементов управления в базу данных
        protected virtual void FormElement_to_DB(IRow row)
        {
        }
        //проверка полей ввода на коректность ввода
        protected virtual bool ValidatingData()
        {
            return true;
        }
        //получение обобщеных данных для элементов данных (например: списки доменных значений, данные для автодополнения)
        protected virtual void DB_SharedData_to_FormElement()
        {
        }


        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - base
        //---------------------------------------------------------------------------------------------------------------------------------------------


        protected virtual bool GetSharedData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);
                DB_SharedData_to_FormElement();
                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("олучения общих данных в справочника  '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема получения общих данных в справочнике  '{0}' id {1}", NameTable, objectID));
                ret = false;
            }
            return ret;
        }

        protected virtual bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);

                IRow row = table.GetRow(objectID);
                DB_to_FormElement(row);

                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Чтиение данных справочника  '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема при чтиение данных справочника  '{0}' id {1}", NameTable, objectID));
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

                table = fws.OpenTable(NameTable);
                IRow row = null;

                if (editMode == EditMode.ADD)
                    row = table.CreateRow();
                else if (editMode == EditMode.EDIT)
                    row = table.GetRow(objectID);
                else
                    return false;

                FormElement_to_DB(row);
                row.Store();

                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);

                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Сохранение данных справочника '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема при сохранение данных справочника '{0}' id {1}", NameTable, objectID));
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

                table = fws.OpenTable(NameTable);

                IRow row = table.GetRow(objectID);
                row.Delete();

                // закончить транзакцию
                wse.StopEditOperation();
                wse.StopEditing(true);


                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Удаление справочника '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема при удаление справочника '{0}' id {1}", NameTable, objectID));
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
#if (!CONSTRUCT_FORM)
            if (!this.GetSharedData()) // error
            {
                this.Close();
            }

            if (editMode != EditMode.ADD)
            {
                if (!this.ReadData()) // error
                {
                    this.Close();
                }
            }
#endif
        }

        protected void frmBaseSpr_element_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified && editMode != EditMode.DELETE)
            {
                if (!ValidatingData())
                    return;

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
                if (!ValidatingData())
                    return;

                ret = SaveData();
                isModified = false;
            }

            this.Close();
        }

        #endregion
    }
}
