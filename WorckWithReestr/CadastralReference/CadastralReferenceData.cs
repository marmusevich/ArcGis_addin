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
        private string m_AllRTF = "";
        private string m_TitulRTF = " Титул ";
        private string m_Page1RTF = " начало динамической части";
        private string m_ConstRTF = " постоянная часть";
        private string m_RaspiskaRTF = " расписка";
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region публичные свойства
        public List<OnePageDescriptions> Pages { get { return m_Pages; } private set { m_Pages = value; } }
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
        public string AllRTF { get { return m_AllRTF; } set { m_AllRTF = value; } }
        public string TitulRTF { get { return m_TitulRTF; } set { m_TitulRTF = value; } }
        public string Page1RTF { get { return m_Page1RTF; } set { m_Page1RTF = value; } }
        public string ConstRTF { get { return m_ConstRTF; } set { m_ConstRTF = value; } }
        public string RaspiskaRTF { get { return m_RaspiskaRTF; } set { m_RaspiskaRTF = value; } }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region события
        // смена заявки
        public event EventHandler<EventArgs> ZayavkaID_Change;
        //смена изображения
        public event EventHandler<EventArgs> Image_Change;
        private void OnImage_Change(object sender, EventArgs e)
        {
            if (Image_Change != null)
                Image_Change(sender, EventArgs.Empty);
        }
        #endregion 
    }


    // Информация об одном листе
    public class OnePageDescriptions : IEquatable<OnePageDescriptions>
    {
        public OnePageDescriptions(string caption, bool enable = false)
        {
            Caption = caption;
            Enable = enable;
            PagesID = Caption.GetHashCode();
            Layers = new StringCollection();
            m_Image = null;
        }

        #region свойсва / поля
        // имя из базы Данных для поля
        public int PagesID { get; private set; }
        // Назвение листа
        public string Caption { get; set; }
        // включен ли лист
        public bool Enable { get; set; }
        // Слои этого листа

        public StringCollection Layers { get; set; }
        // макет
        private Image m_Image;
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

        #endregion

        #region события
        //смена изображения
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
