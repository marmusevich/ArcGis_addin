using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;

namespace CadastralReference
{
    //CadastralReference
    [Serializable]
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
                ////opd.Layers = new StringCollection();
                //for (int i = 0; i < 10; i++)
                //    opd.Layers.Add("_Layer_to_" + str + "_" + i.ToString());

                m_Pages.Add(opd);
            }
        }

        public CadastralReferenceData()
        {
            m_Pages = new List<OnePageDescriptions>();
            DinamicRTF_Template = new StringCollection();
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
        private string m_ObjectLayerName = "Кадастровая_справка.DBO.KS_OBJ_FOR_ALEX"; // проблема, это не имя слоя
        private Dictionary<string, object> m_ZayavkaData = null;
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region публичные свойства
        /// <summary>
        /// свойства графических листов
        /// </summary>
        [XmlArray("Pages"), XmlArrayItem("Page")]
        public List<OnePageDescriptions> Pages { get { return m_Pages; } private set { m_Pages = value; } }
        /// <summary>
        /// код заявления
        /// </summary>
        [XmlIgnore]
        public int ZayavkaID
        {
            get
            {
                return m_ZayavkaID;
            }
            set
            {
                if (m_ZayavkaID != value)
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
        [XmlIgnore]
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
        [XmlIgnore]
        public string AllRTF { get { return m_AllRTF; } set { m_AllRTF = value; } }
        /// <summary>
        /// титульный лист - шаблон
        /// </summary>
        [XmlElement("TitulRTF_Template")]
        public string TitulRTF_Template { get { return m_TitulRTF_Template; } set { m_TitulRTF_Template = value; } }
        /// <summary>
        /// надпись до динамической части - шаблон
        /// </summary>
        [XmlElement("Page1RTF_Template")]
        public string Page1RTF_Template { get { return m_Page1RTF_Template; } set { m_Page1RTF_Template = value; } }
        /// <summary>
        /// Динамическая часть - шаблон
        /// </summary>
        [XmlArray("DinamicRTF_Template")]
        public StringCollection DinamicRTF_Template { get { return m_DinamicRTF_Template; } set { m_DinamicRTF_Template = value; } }
        /// <summary>
        /// Динамическая часть из документа
        /// </summary>
        [XmlIgnore]
        public StringCollection DinamicRTF { get { return m_DinamicRTF; } set { m_DinamicRTF = value; } }
        /// <summary>
        /// окончание документа - шаблон
        /// </summary>
        [XmlElement("ConstRTF_Template")]
        public string ConstRTF_Template { get { return m_ConstRTF_Template; } set { m_ConstRTF_Template = value; } }
        /// <summary>
        /// расписка - шаблон
        /// </summary>
        [XmlElement("RaspiskaRTF_Template")]
        public string RaspiskaRTF_Template { get { return m_RaspiskaRTF_Template; } set { m_RaspiskaRTF_Template = value; } }
        /// <summary>
        /// имя слоя на котором размещены объекты
        /// </summary>
        [XmlElement("ObjectLayerName")]
        public string ObjectLayerName { get { return m_ObjectLayerName; } set { m_ObjectLayerName = value; } }
        /// <summary>
        /// справка закрыта для редактирования
        /// </summary>
        [XmlIgnore]
        public bool IsCloseToEdit { get; set; }
        /// <summary>
        /// Данные заявки
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, object> ZayavkaData { get { return m_ZayavkaData; } set { m_ZayavkaData = value; } }




        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
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



        static public string ToXMLSerializableString(CadastralReferenceData m)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CadastralReferenceData));
            StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, m);
            return stringWriter.ToString();
        }

        static public CadastralReferenceData FromSerializableXMLString(string xml)
        {

            var xmlSerializer = new XmlSerializer(typeof(CadastralReferenceData));
            var stringReader = new StringReader(xml);
            return (CadastralReferenceData)xmlSerializer.Deserialize(stringReader);
            //try
            //{

            //}
            //catch (Exception ex) // обработка ошибок
            //{
            //    System.Console.WriteLine(string.Format("{0}.{1}()] {2}\r\n", ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message));
            //}


        }
    }
}



