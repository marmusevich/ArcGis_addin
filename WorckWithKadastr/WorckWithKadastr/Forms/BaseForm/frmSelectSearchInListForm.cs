using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmSelectSearchInListForm : Form
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------

        //типы:
        enum TypeOfType
        {
            UNCNOW = 0, // неопределено
            TEXT,// - текст
            NUMBER,// - число
            DATE,// - дата
            DOMAIN,// - домен
            DICTIONARY,// - справочник        
            ID // ключевое поле
        }
        //типы сравнения
        enum TypesOfComparisons
        {
            MORE,//больше (>)
            MORE_OR_EQUAL,//больше или равно (>+)
            EQUAL,//равно (==, =)
            LESS_OR_EQUAL,//меньше или равно (<=)
            LESS,//меньше (<)
            STARTING_WITH,//начинается с (like "%str")
            CONTAINS,//содержит (like "%str%")
            ENDS_WITH//оканчивается на (like "str%")
        }
        // для выбора на форме видов сравнения
        class ComparisonsData
        {
            //значение
            public readonly TypesOfComparisons Value;
            //наименование
            public readonly string Text;

            public ComparisonsData(TypesOfComparisons Value, string Text)
            {
                this.Value = Value;
                this.Text = Text;
            }
            public override string ToString()
            {
                return this.Text;
            }
        }
        // информация об одном поле
        class sFildsDescription
        {
            public TypeOfType mTOT; // тип поля
            public string mDictionareTableName; // если справочник, то таблица справочника
            //public string mDictionareKeyFildName = ""; //для справочников ключевое поле
            public int mDictionareValue;

            public DomeinDataAdapter mDomeinDataAdapter;
            public string mAliasName; // представление для пользователя
            public string mName; // внутренее имя поля
            public ArrayList mControls;//ссылки на связаные элементы управления

            public sFildsDescription()
            {
                mTOT = TypeOfType.UNCNOW;
                mDictionareTableName = "";
                //mDictionareKeyFildName = "";
                mDictionareValue = -1;
                mAliasName = "";
                mName = "";
                mDomeinDataAdapter = null;
                mControls = new ArrayList(4);
            }
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        ////имя пространства данных
        //protected string NameWorkspace = "";
        ////имя таблицы
        //protected string NameTable = "";

        //собственно таблица
        protected ITable table;
        private ArrayList allFilds = null;
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public static void ShowForView(Form _owner, ITable _table)
        {
            Form frm = new frmSelectSearchInListForm(_table);

            frm.ShowDialog(_owner);
            frm.Activate();
        }

        // получить тип поля
        private void GetTypeOfType(ref sFildsDescription sfd, IField f)
        {
            if (Owner is IFormFilterMetods)
            {
                if ((Owner as IFormFilterMetods).ChekFildIsDictionary(f.Name, ref sfd.mDictionareTableName))
                {
                    sfd.mTOT = TypeOfType.DICTIONARY;
                    return; // это ссылка на справочник
                }
            }

            if (f.Domain != null)
            {
                sfd.mTOT = TypeOfType.DOMAIN;
                return; // это домен
            }

            if (f.Type == esriFieldType.esriFieldTypeDate)
            {
                sfd.mTOT = TypeOfType.DATE;
                return;
            }
            else if (f.Type == esriFieldType.esriFieldTypeOID)
            {
                sfd.mTOT = TypeOfType.ID;
                return;
            }
            else if (f.Type == esriFieldType.esriFieldTypeString)
            {
                sfd.mTOT = TypeOfType.TEXT;
                return;
            }
            else if (f.Type == esriFieldType.esriFieldTypeSingle ||
                        f.Type == esriFieldType.esriFieldTypeDouble ||
                        f.Type == esriFieldType.esriFieldTypeInteger ||
                        f.Type == esriFieldType.esriFieldTypeSmallInteger)
            {
                sfd.mTOT = TypeOfType.NUMBER;
                return;
            }

            //определить не удалось
            sfd.mTOT = TypeOfType.UNCNOW;
        }

        //доступные типы сравнения
        private object[] ConstructComparisons(TypeOfType tot)
        {
            ArrayList tmp = new ArrayList();

            //должны быть условия для
            switch (tot)
            {
                // - текст (=, like "%str", like "%str%", like "str%")
                case TypeOfType.TEXT:
                    tmp.Add(new ComparisonsData(TypesOfComparisons.EQUAL, "равно (==, =)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.STARTING_WITH, "начинается с (like '%str')"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.CONTAINS, "содержит (like '%str%')"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.ENDS_WITH, "оканчивается на (like 'str%')"));
                    break;
                // - число (=, >=, >, <, <=)
                case TypeOfType.NUMBER:
                    tmp.Add(new ComparisonsData(TypesOfComparisons.MORE, "больше (>)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.MORE_OR_EQUAL, "больше или равно (>+)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.EQUAL, "равно (==, =)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.LESS_OR_EQUAL, "меньше или равно (<=)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.LESS, "меньше (<)"));
                    break;
                // - дата (=, >=, >, <, <=, beetwen)
                case TypeOfType.DATE:
                    tmp.Add(new ComparisonsData(TypesOfComparisons.MORE, "больше (>)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.MORE_OR_EQUAL, "больше или равно (>+)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.EQUAL, "равно (==, =)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.LESS_OR_EQUAL, "меньше или равно (<=)"));
                    tmp.Add(new ComparisonsData(TypesOfComparisons.LESS, "меньше (<)"));

                    //tmp.Add(new ComparisonsData(TypesOfComparisons., "beetwen"));
                    break;
                // - домен (=)
                case TypeOfType.DOMAIN:
                    tmp.Add(new ComparisonsData(TypesOfComparisons.EQUAL, "равно (==, =)"));
                    break;
                // - справочник (=), для основного представления как для текста ??
                case TypeOfType.DICTIONARY:
                    tmp.Add(new ComparisonsData(TypesOfComparisons.EQUAL, "равно (==, =)"));
                    break;
                case TypeOfType.ID:
                    tmp.Add(new ComparisonsData(TypesOfComparisons.EQUAL, "равно (==, =)"));
                    break;
                // TypeOfType.UNCNOW
                default:
                    //tmp.Add(new ComparisonsData(TypesOfComparisons., ""));
                    break;
            }
            return tmp.ToArray();
        }

        // построить форму 
        private void ConstructForm()
        {
            this.Text = "Фильтр для: " + (table as IObjectClass).AliasName;

            int y = 0;
            allFilds = new ArrayList(table.Fields.FieldCount);
            //из таблици получить поля
            for (int fieldCount = 0; fieldCount < table.Fields.FieldCount; fieldCount++)
            {
                //получить от владельца список полей справочников, с указанием таблици справочника
                //функции в зависимости от типа получить масив допустимых операций сравнений

                IField f = table.Fields.get_Field(fieldCount);
                sFildsDescription sfd = new sFildsDescription();
                sfd.mAliasName = f.AliasName;
                sfd.mName = f.Name;

                GetTypeOfType(ref sfd, f);

                // специальное поведение 
                //  -для домена БУЛ
                //  -для реестра заявлений поле "Kod_Z" может быть ссылкой на таблици"fizichni_osoby, jur_osoby";

                //сгенерировать контролы с учетом типа
                //высота ряда
                y = 25 * fieldCount;

                // -чек бокс - использование условия
                if (sfd.mTOT != TypeOfType.UNCNOW)
                    sfd.mControls.Add(CreateCheckBox(ref sfd, y, 10, 15));
                //- надпись - имя поля
                sfd.mControls.Add(CreateLabel(ref sfd, y, 10 + 15 + 10, 150));
                // - надпись тип поля + хинт полное указание типа
                //- комбобокс с типами сравнений
                if (sfd.mTOT != TypeOfType.UNCNOW)
                    sfd.mControls.Add(CreateComparisonsComboBox(ref sfd, y, 10 + 15 + 10 + 150 + 10, 100));

                //- поле ввода (текст, число, справочник), комбобокс (домен), выбор даты (даты)
                //- для справочника кнопка выбора из справочника
                //- заполнить масивы автозаполнения где надо
                //- назначить обработчики

                //должны быть условия для
                switch (sfd.mTOT)
                {
                    // - текст (=, like "%str", like "%str%", like "str%")
                    case TypeOfType.TEXT:
                        sfd.mControls.Add(CreateValueTextBox(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150));
                        break;
                    // - число (=, >=, >, <, <=)
                    case TypeOfType.NUMBER:
                        sfd.mControls.Add(CreateValueTextBox(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150));
                        break;
                    // - дата (=, >=, >, <, <=, beetwen)
                    case TypeOfType.DATE:
                        sfd.mControls.Add(CreateValueDateTimePicker(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150));
                        break;
                    // - домен (=)
                    case TypeOfType.DOMAIN:
                        sfd.mControls.Add(CreateValueDomainComboBox(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150));
                        break;
                    // - справочник (=), для основного представления как для текста ??
                    case TypeOfType.DICTIONARY:
                        sfd.mControls.Add(CreateValueTextBox(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150));
                        sfd.mControls.Add(CreateSelectDictionaryButton(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10 + 150 + 10, 20));
                        break;
                    case TypeOfType.ID:
                        sfd.mControls.Add(CreateValueTextBox(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150));
                        break;
                    // TypeOfType.UNCNOW
                    default:
                        {
                            Label temp = CreateLabel(ref sfd, y, 10 + 15 + 10 + 150 + 10 + 100 + 10, 150, "lb_uncnow_") as Label;
                            temp.Text = "Неизвестный тип...";
                            sfd.mControls.Add(temp);
                        }
                        break;
                }
                allFilds.Add(sfd);
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region form dinamic element creation functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        // -чек бокс - использование условия
        private Control CreateCheckBox(ref sFildsDescription sfd, int y, int x, int w = 15, string prefix = "chb_")
        {
            CheckBox temp = new CheckBox();
            temp.AutoSize = true;
            temp.Location = new System.Drawing.Point(x, y);
            temp.Size = new System.Drawing.Size(w, 14);
            temp.Name = prefix + sfd.mName;
            temp.Text = "";
            temp.Tag = sfd;
            //temp.CheckedChanged += new System.EventHandler(this.cbIsWorker_CheckedChanged);
            pnMain.Controls.Add(temp);
            return temp;
        }
        //- надпись - имя поля
        private Control CreateLabel(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "lb_")
        {
            Label temp = new Label();
            temp.AutoSize = false;
            temp.Location = new System.Drawing.Point(x, y);
            temp.Size = new System.Drawing.Size(w, 14);
            temp.Name = prefix + sfd.mName;
            temp.Text = sfd.mAliasName;
            temp.Tag = sfd;
            pnMain.Controls.Add(temp);
            // - надпись тип поля + хинт полное указание типа
            return temp;
        }
        //- комбобокс с типами сравнений
        private Control CreateComparisonsComboBox(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "cb_Comparisons_")
        {
            ComboBox temp = new ComboBox();
            temp.FormattingEnabled = true;
            temp.Items.AddRange(ConstructComparisons(sfd.mTOT));
            temp.SelectedIndex = 0;
            temp.Location = new System.Drawing.Point(x, y);
            temp.Name = prefix + sfd.mName;
            temp.Size = new System.Drawing.Size(w, 14);
            temp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            temp.SelectedIndexChanged += new System.EventHandler(this.cb_Comparisons_SelectedIndexChanged);

            temp.Tag = sfd;
            pnMain.Controls.Add(temp);
            return temp;
        }
        //- комбобокс с доменных значений
        private Control CreateValueDomainComboBox(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "cb_vd_")
        {
            ComboBox temp = new ComboBox();
            temp.FormattingEnabled = true;
            //вставить диапозон для доменных значений
            sfd.mDomeinDataAdapter = new DomeinDataAdapter(table.Fields.get_Field(table.FindField(sfd.mName)).Domain);
            temp.Items.AddRange(sfd.mDomeinDataAdapter.ToArray());

            temp.SelectedIndex = 0;
            temp.Location = new System.Drawing.Point(x, y);
            temp.Name = prefix + sfd.mName;
            temp.Size = new System.Drawing.Size(w, 14);
            temp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            temp.SelectedIndexChanged += new System.EventHandler(this.cb_Comparisons_SelectedIndexChanged);
            temp.Tag = sfd;
            pnMain.Controls.Add(temp);
            return temp;
        }
        //- поле ввода даты
        private Control CreateValueDateTimePicker(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "dtp_v_")
        {
            DateTimePicker temp = new DateTimePicker();
            temp.Location = new System.Drawing.Point(x, y);
            temp.Name = prefix + sfd.mName;
            temp.Size = new System.Drawing.Size(w, 14);
            temp.CustomFormat = "dd.MMM.yyyy HH.mm";

            temp.Tag = sfd;
            temp.Validating += new System.ComponentModel.CancelEventHandler(this.dtp_v_Validating);
            pnMain.Controls.Add(temp);
            return temp;
        }
        //- текстовое поле ввода
        private Control CreateValueTextBox(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "txt_v_")
        {
            TextBox temp = new TextBox();
            temp.Location = new System.Drawing.Point(x, y);
            temp.Name = prefix + sfd.mName;
            temp.Size = new System.Drawing.Size(w, 14);
            temp.Tag = sfd;
            temp.Validating += new System.ComponentModel.CancelEventHandler(this.txt_v_Validating);
            temp.TextChanged += new System.EventHandler(this.txt_v_TextChanged);

            pnMain.Controls.Add(temp);

            if (sfd.mTOT == TypeOfType.DICTIONARY)
            {
                if (sfd.mDictionareTableName == "fizichni_osoby")
                {
                    DictionaryWork.EnableAutoComlectToFizLic(temp);
                }
                else if (sfd.mDictionareTableName == "jur_osoby")
                {
                    DictionaryWork.EnableAutoComlectToJurLic(temp);
                }
                else if (sfd.mDictionareTableName == "Tip_Doc")
                {
                    DictionaryWork.EnableAutoComlectToTip_Doc(temp);
                }
                else
                {
                    temp.Enabled = false;
                }
            }

            return temp;
        }
        //- кнопка выбора из справочника
        private Control CreateSelectDictionaryButton(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "but_sd_")
        {
            Button temp = new Button();
            temp.Location = new System.Drawing.Point(x, y);
            temp.Name = prefix + sfd.mName;
            temp.Size = new System.Drawing.Size(w, 23);
            temp.Click += new System.EventHandler(this.but_sd_Click);
            temp.UseVisualStyleBackColor = true;
            temp.Tag = sfd;
            temp.Text = "...";
            pnMain.Controls.Add(temp);
            return temp;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form dinamic element events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        // при изменении типа сравнения
        private void cb_Comparisons_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ctr = sender as ComboBox;
            if (ctr != null && ctr.Tag != null)
            {
                sFildsDescription sfd = ctr.Tag as sFildsDescription;
                if (sfd != null)
                {
                    CheckBox cb = sfd.mControls[0] as CheckBox;
                    if (cb != null)
                    {
                        if (!cb.Checked) cb.Checked = true;
                    }
                }
            }
        }

        private void txt_v_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && txt.Tag != null)
            {
                sFildsDescription sfd = txt.Tag as sFildsDescription;
                if (sfd != null)
                {
                    CheckBox cb = sfd.mControls[0] as CheckBox;
                    if (cb != null)
                    {
                        if (!cb.Checked) cb.Checked = true;
                    }

                    //
                    if (sfd.mTOT == TypeOfType.NUMBER)
                    {
                        SharedClass.CheckValueIsInt_SetError(txt, errorProvider);
                    }
                    else if (sfd.mTOT == TypeOfType.ID)
                    {
                        SharedClass.CheckValueIsInt_SetError(txt, errorProvider);
                    }
                    else if (sfd.mTOT == TypeOfType.DICTIONARY)
                    {
                        if (sfd.mDictionareTableName == "fizichni_osoby")
                        {
                            DictionaryWork.CheckValueIsContainsFizLic_SetError(txt, errorProvider, ref sfd.mDictionareValue);
                        }
                        else if (sfd.mDictionareTableName == "jur_osoby")
                        {
                            DictionaryWork.CheckValueIsContainsJurLic_SetError(txt, errorProvider, ref sfd.mDictionareValue);
                        }
                        else if (sfd.mDictionareTableName == "Tip_Doc")
                        {
                            DictionaryWork.CheckValueIsContainsTip_Doc_SetError(txt, errorProvider, ref sfd.mDictionareValue);
                        }
                        else
                        {
                        }
                    }

                }
            }
        }

        // кнопка выбора из справочника
        private void but_sd_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                sFildsDescription sfd = btn.Tag as sFildsDescription;
                if (sfd != null)
                {
                    if (sfd.mDictionareTableName == "fizichni_osoby")
                    {
                        string filteredString = "";
                        sfd.mDictionareValue = frmFizLic_list.ShowForSelect(filteredString);

                        TextBox txt = sfd.mControls[3] as TextBox;
                        if (txt != null)
                        {
                            txt.Text = DictionaryWork.GetFIOByIDFromFizLic(sfd.mDictionareValue);
                            errorProvider.SetError(txt, String.Empty);
                        }
                    }
                    else if (sfd.mDictionareTableName == "jur_osoby")
                    {
                        string filteredString = "";
                        sfd.mDictionareValue = frmJurLic_list.ShowForSelect(filteredString);
                        TextBox txt = sfd.mControls[4] as TextBox;
                        if (txt != null)
                        {
                            txt.Text = DictionaryWork.GetNameByIDFromJurOsoby(sfd.mDictionareValue);
                            errorProvider.SetError(txt, String.Empty);
                        }
                    }
                    else if (sfd.mDictionareTableName == "Tip_Doc")
                    {
                        string filteredString = "";
                        sfd.mDictionareValue = frmTipDoc_list.ShowForSelect(filteredString);
                        TextBox txt = sfd.mControls[4] as TextBox;
                        if (txt != null)
                        {
                            txt.Text = DictionaryWork.GetNameByIDFromTip_Doc(sfd.mDictionareValue);
                            errorProvider.SetError(txt, String.Empty);
                        }
                    }
                    else
                    {
                    }
                }
            }
        }
        // валидация даты времени
        private void dtp_v_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //isModified = true;
            //e.Cancel = !ValidatingData();
        }
        // валидация текстового поля
        private void txt_v_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null && txt.Tag != null)
            {
                sFildsDescription sfd = txt.Tag as sFildsDescription;
                if (sfd != null)
                {
                    if (sfd.mTOT == TypeOfType.NUMBER)
                    {
                        e.Cancel = SharedClass.CheckValueIsInt_SetError(txt, errorProvider);
                    }
                    else if (sfd.mTOT == TypeOfType.ID)
                    {
                        e.Cancel = SharedClass.CheckValueIsInt_SetError(txt, errorProvider);
                    }
                    else if (sfd.mTOT == TypeOfType.DICTIONARY)
                    {
                        if (sfd.mDictionareTableName == "fizichni_osoby")
                        {
                            e.Cancel = DictionaryWork.CheckValueIsContainsFizLic_SetError(txt, errorProvider, ref sfd.mDictionareValue);
                        }
                        else if (sfd.mDictionareTableName == "jur_osoby")
                        {
                            e.Cancel = DictionaryWork.CheckValueIsContainsJurLic_SetError(txt, errorProvider, ref sfd.mDictionareValue);
                        }
                        else if (sfd.mDictionareTableName == "Tip_Doc")
                        {
                            e.Cancel = DictionaryWork.CheckValueIsContainsTip_Doc_SetError(txt, errorProvider, ref sfd.mDictionareValue);
                        }
                        else
                        {
                        }
                    }
                }
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmSelectSearchInListForm(ITable _table = null)
        {
            InitializeComponent();

            table = _table;
        }

        private void frmSelectSearchInListForm_Load(object sender, EventArgs e)
        {
            if (table != null)
            {
                ConstructForm();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            MessageBox.Show("btnOk_Click");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("btnCancel_Click");
        }
        
        #endregion
    }
}

// описание последовательности действий

//получить таблицу
//из таблици получить поля
//получить от владельца список полей справочников, с указанием таблици справочника

//имеем список всех полей с описанием типов

//функции в зависимости от типа получить масив допустимых операций сравнений

//типы:
// - текст
// - число
// - дата
// - домен
// - справочник

//сгенерировать контролы с учетом типа
//- надпись - имя поля
// - надпись тип поля + хинт полное указание типа
//- комбобокс с типами сравнений
//- поле ввода (текст, число, справочник), комбобокс (домен), выбор даты (даты)
//- для справочника кнопка выбора из справочника

//- заполнить масивы автозаполнения где надо
//- назначить обработчики
//нажатие, изменение, валидация

//сгенерировать текст условия по значения полей ввода

//хранить все значения в объекте для сохранения / востановления

//для журналов при наличии этого отбора не учитывать отборы из журнала (по дате или еще что то)

//кнопки в журнале отбор, сброс отбора, добавить отбор по значению этого поля




