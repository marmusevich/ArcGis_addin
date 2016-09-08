using System;
using System.Drawing;

namespace CadastralReference
{
    //CadastralReference
    public class CadastralReferenceData
    {
        #region // для теста
        private static string captionsPrefex = "Лист: ";
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
            m_Pages = new OnePageDescriptions[m_ArrayDBNamesPages.Length];
            for (int index = 0; index < m_ArrayDBNamesPages.Length; ++index)
            {
                string str = m_ArrayDBNamesPages[index];
                m_Pages[index] = new OnePageDescriptions();
                m_Pages[index].NameFromDB = str;
                m_Pages[index].Caption = captionsPrefex + str;
                m_Pages[index].Image = (Image)null;
                m_Pages[index].index = index;
                m_Pages[index].Enable = true;
                m_Pages[index].Image_Change += new EventHandler<EventArgs>(OnImage_Change);
            }
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region внутренние переменные
        private OnePageDescriptions[] m_Pages = null;
        private int m_ZayavkaID = -1;
        private int m_CadastralReferenceID = -1;
        private string m_AllRTF = "";
        private string m_TitulRTF = " Титул ";
        private string m_Page1RTF = " начало динамической части";
        private string m_ConstRTF = " постоянная часть";
        private string m_RaspiskaRTF = " расписка";
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region публичные свойства
        public OnePageDescriptions[] Pages { get { return m_Pages; } set { m_Pages = value; } }
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
        public int CadastralReferenceID
        {
            get
            {
                return m_CadastralReferenceID;
            }
            set
            {
                if(m_CadastralReferenceID != value)
                { 
                m_CadastralReferenceID = value;
                if (CadastralReferenceID_Change != null)
                    CadastralReferenceID_Change(this, EventArgs.Empty);
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
        // смена справки
        public event EventHandler<EventArgs> CadastralReferenceID_Change;
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
    public class OnePageDescriptions
    {
        // имя из базы Данных для поля
        public string NameFromDB { get; set; }
        // Назвение листа
        public string Caption { get; set; }
        // Слои этого листа
        public string[] Layers { get; set; }
        // макет
        private Image m_Image = null;
        public Image Image
        {
            get
            {
                return m_Image;
            }
            set
            {
                if(m_Image != value)
                {
                    m_Image = value;
                    if (Image_Change != null)
                        Image_Change(this, EventArgs.Empty);
                }
            }
        }
        // индекс в масиве
        public int index { get; set; }
        // включен ли лист
        public bool Enable { get; set; }
        #region события
        //смена изображения
        public event EventHandler<EventArgs> Image_Change;
        #endregion
        public override string ToString()
        {
            return Caption;
        }
    }
}
