using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace CadastralReference
{
    //CadastralReference
    [Serializable]
    [XmlRootAttribute("CadastralReferenceData", IsNullable = true)]
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

        #endregion // для теста

        // XML строка с настройками по умалчанию
        private const string defaultSettingXML = null;

        ////////////////////////////////////////////////////////////////////////////////////////////
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


        /// <summary>
        /// свойства графических листов
        /// </summary>
        [XmlArray("Pages"), XmlArrayItem("Page")]
        public List<OnePageDescriptions> Pages { get { return m_Pages; } set { m_Pages = value; } }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region функции
        public CadastralReferenceData()
        {
            m_Pages = new List<OnePageDescriptions>();
            DinamicRTF_Template = new StringCollection();
        }

        //скопировать настройки
        public void CopySetingFrom(CadastralReferenceData crd)
        {
            this.TitulRTF_Template = crd.TitulRTF_Template;
            this.Page1RTF_Template = crd.Page1RTF_Template; 
            this.ConstRTF_Template = crd.ConstRTF_Template; 
            this.RaspiskaRTF_Template = crd.RaspiskaRTF_Template; 
            this.ObjectLayerName = crd.ObjectLayerName;

            this.DinamicRTF_Template = new StringCollection();
            foreach (string s in crd.DinamicRTF_Template)
                this.DinamicRTF_Template.Add(s);

            foreach (OnePageDescriptions opd in crd.Pages)
            {
                // не портить текущие значения, новые добавить
                int index = this.Pages.IndexOf(opd);
                if (index == -1)
                {
                    OnePageDescriptions tmp = new OnePageDescriptions();
                    tmp.CopySetingFrom(opd);
                    this.Pages.Add(tmp);
                }
                else
                {
                    this.Pages[index].CopySetingFrom(opd);
                }
            }
        }


        //выгрузить настройки
        public string SaveSettingToXMLString()
        {
            string ret = "";
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CadastralReferenceData));
                StringWriter stringWriter = new StringWriter();
                xmlSerializer.Serialize(stringWriter, this);
                ret = stringWriter.ToString();
            }
            catch (Exception ex) // обработка ошибок
            {
                System.Console.WriteLine(string.Format("SaveSettingToXMLString Error = {0}\r\n",  ex.Message));
            }
            return ret;
        }

        //загрузить настройки, при неудаче установить по умалчанию
        public void LoadSettingFromXMLString(string xml)
        {
            CadastralReferenceData tmp = null;
            XmlSerializer xmlSerializer = null;
            StringReader stringReader = null;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(CadastralReferenceData));
                try
                {
                    xmlSerializer = new XmlSerializer(typeof(CadastralReferenceData));
                    stringReader = new StringReader(xml);
                    tmp = (CadastralReferenceData)xmlSerializer.Deserialize(stringReader);
                }
                catch (Exception ex) // попробывать загрузить установки по умолчанию
                {
                    System.Console.WriteLine(string.Format("LoadSettingFromXMLString load Error = {0}\r\n", ex.Message));
                    System.Console.WriteLine(string.Format("LoadSettingFromXMLString try load default\r\n"));
                    stringReader = new StringReader(defaultSettingXML);
                    tmp = (CadastralReferenceData)xmlSerializer.Deserialize(stringReader);
                }

                if (tmp != null)
                {
                    this.CopySetingFrom(tmp);
                }

            }
            catch (Exception ex) // обработка ошибок
            {
                System.Console.WriteLine(string.Format("LoadSettingFromXMLString Error = {0}\r\n", ex.Message));
            }

        }
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
}



