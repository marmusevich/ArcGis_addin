
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace SharedClasses
{
    public partial class frmBaseSpr_element : Form, IElementFormWorckWithControlsAndDB
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //виды действий
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
        //ID - текущей записи
        protected int objectID;
        //режим формы
        protected EditMode editMode = EditMode.UNKNOW;
        //данные изменены
        protected bool isModified = false;
        //имя пространства данных
        protected string NameWorkspace = "";
        //имя таблицы
        protected string NameTable = "";
        //сама таблица
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
        //установить максимальную длину элементов управления для значения типа текст
        protected virtual void SetMaxLengthStringValueToTextBoxFromDB()
        {
        }
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions - base
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //получить общие данные
        protected virtual bool GetSharedData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);
                DB_SharedData_to_FormElement();
                SetMaxLengthStringValueToTextBoxFromDB();
                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Проблема получения общих данных в справочнике  '{0}' id {1}", NameTable, objectID));
                GeneralApp.ShowErrorMessage(string.Format("Проблема получения общих данных в справочнике  '{0}' id {1}", NameTable, objectID));
                ret = false;
            }
            return ret;
        }
        //читать данные
        protected virtual bool ReadData()
        {
            bool ret = false;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                table = fws.OpenTable(NameTable);

                IRow row = table.GetRow(objectID);
                DB_to_FormElement(row);

                ret = true;
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, string.Format("Чтиение данных справочника  '{0}' id {1}", NameTable, objectID));
                GeneralApp.ShowErrorMessage(string.Format("Проблема при чтиение данных справочника  '{0}' id {1}", NameTable, objectID));
                ret = false;
            }
            isModified = false;
            return ret;
        }
        //сохранить данные
        protected virtual bool SaveData()
        {
            bool ret = false;
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
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
                GeneralApp.ShowErrorMessage(string.Format("Проблема при сохранение данных справочника '{0}' id {1}", NameTable, objectID));
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
        //удалить запись
        protected virtual bool DeleteData()
        {
            bool ret = false;
            IWorkspaceEdit wse = null;
            try
            {
                IFeatureWorkspace fws = GeneralDBWork.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
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
                GeneralApp.ShowErrorMessage(string.Format("Проблема при удаление справочника '{0}' id {1}", NameTable, objectID));
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
        //действие для кнопки ОК
        private void OnOkClick()
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
        
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  работа с элементами управления
        //создать адаптер домена, установить лист значений комбобокса, и установить значение по умолчанию
        public void CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref ComboBox cb, ref DomeinDataAdapter dda, string fildName)
        {
            DomeinDataAdapter.CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref cb, ref dda, ref table, fildName);
        }
        //устоновить значение комбобокса по значению, если нет адаптера - создать, если нет значения устоновить по умолчанию
        public void CheсkValueAndSetToComboBox(ref ComboBox cb, ref DomeinDataAdapter dda, string fildName, object value)
        {
            DomeinDataAdapter.CheсkValueAndSetToComboBox(ref cb, ref dda, ref table, fildName, value);
        }

        //установить максимальную длину элемента управления для значения типа текст
        public void SetMaxLengthStringValueToTextBox(string fildName, TextBox textBox)
        {
            textBox.MaxLength = table.Fields.get_Field(table.Fields.FindField(fildName)).Length;
        }


        //прочесть значение из базы
        public object GetValueFromDB(ref IRow row, string fildName)
        {
            return row.get_Value(table.FindField(fildName));
        }
        //установить значение элемента управления тип текст
        public void SetStringValueFromDBToTextBox(ref IRow row, string fildName, TextBox textBox)
        {
            SetMaxLengthStringValueToTextBox(fildName, textBox);
            textBox.Text = "" + GetValueFromDB(ref row, fildName) as string;
        }
        //установить значение элемента управления тип число
        public void SetIntValueFromDBToTextBox(ref IRow row, string fildName, TextBox textBox)
        {
            textBox.Text = "" + GetValueFromDB(ref row, fildName) as string;
        }
        //установить значение элемента управления тип дата
        public void SetDateValueFromDBToDateTimePicker(ref IRow row, string fildName, DateTimePicker dateTimePicker)
        {
            dateTimePicker.Value = GeneralApp.ConvertVolueToDateTime(GetValueFromDB(ref row, fildName));
        }

        //сохранить в базу значение элемента управления тип доменные значения
        public void SaveDomeinDataValueFromComboBoxToDB(ref IRow row, string fildName, ref ComboBox cb)
        {
            row.set_Value(table.FindField(fildName), ((DomeinDataAdapter.DomeinData)cb.SelectedItem).Value);
        }
        //сохранить в базу значение элемента управления тип текст
        public void SaveStringValueFromTextBoxToDB(ref IRow row, string fildName, TextBox textBox)
        {
            int MaxLength = row.Fields.get_Field(row.Fields.FindField(fildName)).Length;
            if (textBox.Text.Length > MaxLength)
            {
                string s = textBox.Text.Trim().Substring(0, MaxLength - 1);
                textBox.Text = s;
            } 
            row.set_Value(table.FindField(fildName), textBox.Text);
        }
        //сохранить в базу значение элемента управления тип число
        public void SaveIntValueFromTextBoxToDB(ref IRow row, string fildName, TextBox textBox)
        {
            int tmp = Convert.ToInt32(textBox.Text);
            row.set_Value(table.FindField(fildName), tmp);
        }
        //сохранить в базу значение элемента управления тип дата
        public void SaveDateValueFromDateTimePickerToDB(ref IRow row, string fildName, DateTimePicker dateTimePicker)
        {
            row.set_Value(table.FindField(fildName), dateTimePicker.Value);
        }

        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
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
            if (editMode == EditMode.UNKNOW) // для конструктора форм
                return;

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

            ToolTip toolTipOk = new ToolTip();
            toolTipOk.SetToolTip(btnOk, "Ctrl+Enter");
            ToolTip toolTipCancel = new ToolTip();
            toolTipCancel.SetToolTip(btnCancel, "Esc");
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
            OnOkClick();
        }
        private void frmBaseSpr_element_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Control)
            {
                e.Handled = true;
                OnOkClick();
            }

        }
        #endregion
    }
}
