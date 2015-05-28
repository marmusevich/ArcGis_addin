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
            public string mAliasName; // представление для пользователя
            public string mName; // внутренее имя поля
            public ArrayList mControls;//ссылки на связаные элементы управления

            public sFildsDescription()
            {
                mTOT = TypeOfType.UNCNOW;
                mDictionareTableName = "";
                //mDictionareKeyFildName = "";
                mAliasName = "";
                mName = "";
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
            Form frm = new frmSelectSearchInListForm( _table );

            frm.ShowDialog(_owner);
            frm.Activate();
        }


        // получить тип поля
        private void GetTypeOfType(ref sFildsDescription sfd, IField f)
        {
            if (Owner is IFormFilterMetods )
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
                        f.Type == esriFieldType.esriFieldTypeSmallInteger  )
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
                sfd.mControls.Add(CreateCheckBox(ref sfd, y, 10, 15));
                //- надпись - имя поля
                sfd.mControls.Add(CreateLabel(ref sfd, y, 10 + 15 + 10, 150));
                // - надпись тип поля + хинт полное указание типа
                //- комбобокс с типами сравнений
                sfd.mControls.Add(CreateComboBox(ref sfd, y, 10 + 15 + 10 + 150 + 10, 100));


                
                //- поле ввода (текст, число, справочник), комбобокс (домен), выбор даты (даты)
                //- для справочника кнопка выбора из справочника

                                    //    // dateTimePicker1
                                    //    // 
                                    //    this.dateTimePicker1.Location = new System.Drawing.Point(198, 84);
                                    //    this.dateTimePicker1.Name = "dateTimePicker1";
                                    //    this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
                                    //    this.dateTimePicker1.TabIndex = 1;




                //- заполнить масивы автозаполнения где надо
                //- назначить обработчики




                allFilds.Add(sfd);
            }            

        }

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
        private Control CreateComboBox(ref sFildsDescription sfd, int y, int x, int w = 50, string prefix = "cb_")
        {
            ComboBox temp = new ComboBox();
            temp.FormattingEnabled = true;
            temp.Items.AddRange(ConstructComparisons(sfd.mTOT));
            temp.SelectedIndex = 0;
            temp.Location = new System.Drawing.Point(x, y);
            temp.Name = prefix + sfd.mName;
            temp.Size = new System.Drawing.Size(w, 14);
            temp.Tag = sfd;
            pnMain.Controls.Add(temp);
            return temp;
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
        #endregion

        private void frmSelectSearchInListForm_Load(object sender, EventArgs e)
        {
            if (table != null)
            {
                ConstructForm();
            }
        }
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




