using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CadastralReference
{
    //CadastralReference
    public class CadastralReferenceData
    {
        #region // для теста
        private static string[] m_ArrayDBNamesPages = new string[6]
                  {
                    "Выкопировка с топологии",
                    "Генеральный план",
                    "Генеральный план: планировочные ограничения",
                    "Детальный план: схема зонирования територий",
                    "Детальный план: схема планировочные ограничения",
                    "Граници участков смежных землепользователей"
                  };
        #endregion // для теста


        public void InitPagesDescription()
        {
            m_Pages = new List<OnePageDescriptions>();
            foreach (string str in m_ArrayDBNamesPages)
            {
                OnePageDescriptions opd = new OnePageDescriptions(str, true);
                opd.Image_Change += new EventHandler<EventArgs>(OnImage_Change);
                m_Pages.Add(opd);
            }
            m_Pages[0].Enable = false;

        }


        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region внутренние переменные
        List<OnePageDescriptions> m_Pages = null;

        private int m_ZayavkaID = -1;
        private int m_ObjektInMapID = -1;
        private string m_AllRTF = "";
        private string m_TitulRTF_Template = " Титул ";
        private string m_Page1RTF_Template = " начало динамической части";
        private StringCollection m_DinamicRTF_Template = null;
        private StringCollection m_DinamicRTF = null;
        private string m_ConstRTF_Template = " постоянная часть";
        private string m_RaspiskaRTF_Template = " расписка";
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region публичные свойства
        /// <summary>
        /// свойства графических листов
        /// </summary>
        public List<OnePageDescriptions> Pages { get { return m_Pages; } private set { m_Pages = value; } }
        /// <summary>
        /// код заявления
        /// </summary>
        public int ZayavkaID 
        { 
            get 
            { 
                return m_ZayavkaID; 
            } 
            set 
            {
                if(m_ZayavkaID != value)
                { 
                m_ZayavkaID = value;
                if (ZayavkaID_Change != null)
                    ZayavkaID_Change(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// код объекта
        /// </summary>
        public int ObjektInMapID
        {
            get
            {
                return m_ObjektInMapID;
            }
            set
            {
                if (m_ObjektInMapID != value)
                {
                    m_ObjektInMapID = value;
                    if (ObjektInMapID_Change != null)
                        ObjektInMapID_Change(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// весь текст итогового документа
        /// </summary>
        public string AllRTF { get { return m_AllRTF; } set { m_AllRTF = value; } }
        /// <summary>
        /// титульный лист - шаблон
        /// </summary>
        public string TitulRTF_Template { get { return m_TitulRTF_Template; } set { m_TitulRTF_Template = value; } }
        /// <summary>
        /// надпись до динамической части - шаблон
        /// </summary>
        public string Page1RTF_Template { get { return m_Page1RTF_Template; } set { m_Page1RTF_Template = value; } }
        /// <summary>
        /// Динамическая часть - шаблон
        /// </summary>
        public StringCollection DinamicRTF_Template { get { return m_DinamicRTF_Template; } set { m_DinamicRTF_Template = value; } }
        /// <summary>
        /// Динамическая часть из документа
        /// </summary>
        public StringCollection DinamicRTF { get { return m_DinamicRTF; } set { m_DinamicRTF = value; } }
        /// <summary>
        /// окончание документа - шаблон
        /// </summary>
        public string ConstRTF_Template { get { return m_ConstRTF_Template; } set { m_ConstRTF_Template = value; } }
        /// <summary>
        /// расписка - шаблон
        /// </summary>
        public string RaspiskaRTF_Template { get { return m_RaspiskaRTF_Template; } set { m_RaspiskaRTF_Template = value; } }
        /// <summary>
        /// справка закрыта для редактирования
        /// </summary>
        public bool IsCloseToEdit { get; set; }

        #endregion

        #region функции

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region события
        /// <summary>
        /// смена заявления
        /// </summary>
        public event EventHandler<EventArgs> ZayavkaID_Change;
        /// <summary>
        /// смена объекта
        /// </summary>
        public event EventHandler<EventArgs> ObjektInMapID_Change;
        /// <summary>
        ///смена изображения
        /// </summary>
        public event EventHandler<EventArgs> Image_Change;
        private void OnImage_Change(object sender, EventArgs e)
        {
            if (Image_Change != null)
                Image_Change(sender, EventArgs.Empty);
        }
        #endregion
    }

    /// <summary>
    /// Информация об одном листе
    /// </summary>
    public class OnePageDescriptions : IEquatable<OnePageDescriptions>
    {
        /// <summary>
        /// иницилизация 
        /// </summary>
        /// <param name="caption"> название </param>
        /// <param name="enable"> включен ли?</param>
        public OnePageDescriptions(string caption, bool enable = false)
        {
            Caption = caption;
            Enable = enable;
            PagesID = Caption.GetHashCode();
            Layers = new StringCollection();
            m_Image = null;
        }

        #region свойсва / поля
        /// <summary>
        ///  имя из базы Данных для поля 
        /// </summary>
        public int PagesID { get; private set; }
        /// <summary>
        /// Назвение листа
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// включен ли лист
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        ///  Слои этого листа
        /// </summary>
        public StringCollection Layers { get; set; }
        /// <summary>
        /// макет 
        /// </summary>
        public Image Image
        {
            get
            {
                return m_Image;
            }
            set
            {
                if (m_Image != value)
                {
                    m_Image = value;
                    if (Image_Change != null)
                        Image_Change(this, EventArgs.Empty);
                }
            }
        }
        private Image m_Image;


        #endregion

        #region события
        /// <summary>
        /// смена изображения
        /// </summary>
        public event EventHandler<EventArgs> Image_Change;
        #endregion

        #region перегрузка стандартных функций
        public override string ToString()
        {
            return Caption;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            OnePageDescriptions objAsPart = obj as OnePageDescriptions;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return PagesID;
        }
        public bool Equals(OnePageDescriptions other)
        {
            if (other == null) return false;
            return (this.PagesID.Equals(other.PagesID));
        }
        #endregion
    }

}
