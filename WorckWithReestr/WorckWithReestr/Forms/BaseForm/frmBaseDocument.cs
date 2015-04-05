//#define CONSTRUCT_FORM

using ESRI.ArcGIS.Geodatabase;
using System;
using System.Windows.Forms;


namespace WorckWithReestr
{
    public partial class frmBaseDocument : Form
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

        protected ITable table;
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
        //присвоение значений по умалчанию для полей при создании нового
        protected virtual void DB_DefaultValue_to_FormElement()
        {
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - base
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected virtual bool SetDefaultValueToNew()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);

                DB_DefaultValue_to_FormElement();
                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Установка значений по умолчанию в документе  '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема установки значений по умолчанию в документе  '{0}' id {1}", NameTable, objectID));

                ret = false;
            }
            return ret;
        }

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
                Logger.Write(ex, string.Format("Получения общих данных в документе  '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема получения общих данных в документе  '{0}' id {1}", NameTable, objectID));
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
                Logger.Write(ex, string.Format("Чтиение данных документа  '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема при чтиение данных документа  '{0}' id {1}", NameTable, objectID));
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
                Logger.Write(ex, string.Format("Сохранение данных документа '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема при сохранение данных документа '{0}' id {1}", NameTable, objectID));
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
                Logger.Write(ex, string.Format("Удаление документа '{0}' id {1}", NameTable, objectID));
                SharedClass.ShowErrorMessage(string.Format("Проблема при удаление документа '{0}' id {1}", NameTable, objectID));
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
        public frmBaseDocument()
        {
            InitializeComponent();

            objectID = -1;
            editMode = EditMode.UNKNOW;
        }

        public frmBaseDocument(int _objectID, EditMode _editMode)
        {
            InitializeComponent();

            objectID = _objectID;
            editMode = _editMode;
        }

        private void frmBaseDocument_Load(object sender, EventArgs e)
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
            else
            {
                if (!this.SetDefaultValueToNew()) // error
                {
                    this.Close();
                }
            }

            //отключить контролы для режыма удаления
            if (editMode == EditMode.DELETE)
            {
                foreach (System.Windows.Forms.Control c in Controls)
                {
                    c.Enabled = false;
                }
                btnOk.Text = "Удалить";
                btnOk.Enabled = true;
                btnCancel.Enabled = true;
            }
#endif
        }

        protected void frmBaseDocument_FormClosing(object sender, FormClosingEventArgs e)
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
