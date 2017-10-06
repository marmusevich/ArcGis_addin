using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SharedClasses;
using ESRI.ArcGIS.Display;
using PdfSharp.Pdf;


namespace CadastralReference
{
    //CadastralReference
    [Serializable]
    [XmlRootAttribute("CadastralReferenceData", IsNullable = true)]
    public class CadastralReferenceData
    {
        [XmlIgnore]
        //слой и таблица для кадастровой справки
        public static readonly OneLayerDescriptions ObjectLayer_KS = new OneLayerDescriptions(@"Об""єкт", OneLayerDescriptions.LayerType.Feature, @"Кадастровая_справка.DBO.KS_OBJ");
        [XmlIgnore]
        //слой и таблица для архетектурно исторической справки
        public static readonly OneLayerDescriptions ObjectLayer_IAS = new OneLayerDescriptions(@"Об""єкт", OneLayerDescriptions.LayerType.Feature, @"Историко_архитектурная_справка.DBO.IAS_OBJ");
        [XmlIgnore]
        public static OneLayerDescriptions ObjectLayer = ObjectLayer_KS;

        [XmlIgnore]
        public const string DB_NameWorkspace = @"Kadastr2016";
        [XmlIgnore]
        public const string ReestrZayav_NameTable = @"Kn_Reg_Zayv";



        [XmlIgnore]
        public const string CadastralReferenceData_NameTable = @"CadastralReferenceData";

        #region по умолчанию
        public void InitDefaultSetting()
        {
            try
            {
                //GeneralApp.ShowErrorMessage("InitDefaultSetting()");
                LoadSettingFromXMLString(getDeffaultSettingXMl());
            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "_InitDefaultSetting_");
            }
        }
        #endregion // по умолчанию

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region внутренние переменные

        List<OnePageDescriptions> m_Pages = null;

        private int m_ZayavkaID = -1;
        private int m_MapObjectID = -1;
        private bool m_IsReferenceClose = false;
        private Dictionary<string, object> m_ZayavkaData = null;

        private string m_BodyText = "";
        private PdfDocument m_AllDocumentPdf = null;
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region публичные свойства
        /// <summary>/// код заявления/// </summary>
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
        /// <summary>/// код объекта/// </summary>
        [XmlIgnore]
        public int MapObjectID
        {
            get
            {
                return m_MapObjectID;
            }
            set
            {
                if (m_MapObjectID != value)
                {
                    m_MapObjectID = value;
                    if (ObjektInMapID_Change != null)
                        ObjektInMapID_Change(this, EventArgs.Empty);
                }
            }
        }
        [XmlIgnore]
        public string MapObjectID_Discription = "";

        /// <summary>/// справка закрыта для редактирования/// </summary>
        [XmlIgnore]
        public bool IsReferenceClose
        {
            get
            {
                return m_IsReferenceClose;
            }
            set
            {
                if (m_IsReferenceClose != value)
                {
                    m_IsReferenceClose = value;
                    if (IsReferenceClose_Change != null)
                        IsReferenceClose_Change(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>/// Данные заявки/// </summary>
        [XmlIgnore]
        public Dictionary<string, object> ZayavkaData { get { return m_ZayavkaData; } set { m_ZayavkaData = value; } }

        public string RukovoditelDoljnost = @"Начальник служби м\б кадастру";
        public string RukovoditelFIO = "Греков О.С.";

        /// <summary> свойства графических листов        /// </summary>
        [XmlArray("Pages"), XmlArrayItem("Page")]
        public List<OnePageDescriptions> Pages { get { return m_Pages; } set { m_Pages = value; } }

        // отступы от краев в текстовой части PDF
        public double PDFTextMarningUp = 0;
        public double PDFTextMarningDown = 0;
        public double PDFTextMarningRight = 0;
        public double PDFTextMarningLeft = 0;


        //титульный лист
        public string Titul_Template = "";
        //основная часть - начало
        public string Body_Begin_Template = "";
        //шаблоны изменяемой части
        private StringCollection m_Body_Template = null;
        public StringCollection Body_Template { get { return m_Body_Template; } set { m_Body_Template = value; } }

        // основная часть - итоговый документ
        [XmlIgnore]
        public string BodyText
        {
            get
            {
                return m_BodyText;
            }
            set
            {
                if (m_BodyText != value)
                {
                    m_BodyText = value;
                    if (BodyText_Change != null)
                        BodyText_Change(this, EventArgs.Empty);
                }
            }
        }



        //основная часть - окончание
        public string Body_End_Template = "";
        //расписка
        public string Raspiska_Template = "";

        // итоговый документ
        [XmlIgnore]
        public PdfDocument AllDocumentPdf
        {
            get
            {
                return m_AllDocumentPdf;
            }
            set
            {
                if (m_AllDocumentPdf != value)
                {
                    m_AllDocumentPdf = value;
                    if (AllDocumentPdf_Change != null)
                        AllDocumentPdf_Change(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region функции
        public CadastralReferenceData()
        {
            m_Pages = new List<OnePageDescriptions>();
        }

        public CadastralReferenceData(CadastralReferenceData crd):this()
        {
            CopySetingFrom(crd);
        }

        //скопировать настройки
        public void CopySetingFrom(CadastralReferenceData crd)
        {
            if (crd == null) return;

            if (crd.Pages != null)
            {
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
                    opd.Image_Change += new EventHandler<EventArgs>(OnImage_Change);
                }
            }

            foreach (OnePageDescriptions opd in this.Pages)
            {
                opd.Image_Change += new EventHandler<EventArgs>(OnImage_Change);
            }

            this.Titul_Template = crd.Titul_Template;
            this.Body_Begin_Template = crd.Body_Begin_Template;
            if (crd.Body_Template != null)
            {
                this.Body_Template = new StringCollection();
                foreach (string s in crd.Body_Template)
                    this.Body_Template.Add(s);
            }
            this.Body_End_Template = crd.Body_End_Template;
            this.Raspiska_Template = crd.Raspiska_Template;

            this.PDFTextMarningUp = crd.PDFTextMarningUp;
            this.PDFTextMarningDown = crd.PDFTextMarningDown;
            this.PDFTextMarningRight = crd.PDFTextMarningRight;
            this.PDFTextMarningLeft = crd.PDFTextMarningLeft;

        }

        public void ClearSeting()
        {
            m_Pages = new List<OnePageDescriptions>();
            
            foreach (OnePageDescriptions opd in this.Pages)
            {
                opd.Image_Change += new EventHandler<EventArgs>(OnImage_Change);
            }

            Titul_Template = "";
            Body_Begin_Template = "";
            Body_Template = new StringCollection();

            Body_End_Template = "";
            Raspiska_Template = "";

            PDFTextMarningUp = 0;
            PDFTextMarningDown = 0;
            PDFTextMarningRight = 0;
            PDFTextMarningLeft = 0;
        }

        public void ClearData()
        {
            ZayavkaID = -1;
            MapObjectID = -1;
            IsReferenceClose = false;
            ZayavkaData = null;

            BodyText = "";
            AllDocumentPdf = null;
            

            if (Pages != null)
                foreach (OnePageDescriptions opd in Pages)
                    opd.Image = null;
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
                Logger.Write(ex, "SaveSettingToXMLString Error");
            }
            return ret;
        }

        //загрузить настройки, при неудаче установить по умалчанию
        public void LoadSettingFromXMLString(string xml)
        {
            CadastralReferenceData tmp = null;
            XmlSerializer xmlSerializer = null;
            StringReader stringReader = null;

            xmlSerializer = new XmlSerializer(typeof(CadastralReferenceData));
            stringReader = new StringReader(xml);
            tmp = (CadastralReferenceData)xmlSerializer.Deserialize(stringReader);

            if (tmp != null)
            {
                this.CopySetingFrom(tmp);
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region события
        /// <summary>/// смена заявления/// </summary>
        public event EventHandler<EventArgs> TypeReference_Change;
        public void OnTypeReference_Change()
        {
            if (TypeReference_Change != null)
                TypeReference_Change(this, EventArgs.Empty);
        }

        /// <summary>/// смена заявления/// </summary>
        public event EventHandler<EventArgs> ZayavkaID_Change;

        /// <summary>/// смена объекта/// </summary>
        public event EventHandler<EventArgs> ObjektInMapID_Change;
        
        /// <summary>/// запрет редактирования/// </summary>
        public event EventHandler<EventArgs> IsReferenceClose_Change;
        
        /// <summary>///смена изображения/// </summary>
        public event EventHandler<EventArgs> Image_Change;
        private void OnImage_Change(object sender, EventArgs e)
        {
            if (Image_Change != null)
                Image_Change(sender, EventArgs.Empty);
        }

        /// <summary>///смена итоговый документ/// </summary>
        public event EventHandler<EventArgs> AllDocumentPdf_Change;
        /// <summary>///смена основная часть/// </summary>
        public event EventHandler<EventArgs> BodyText_Change;
        #endregion



        string getDeffaultSettingXMl()
        {
            return @"<?xml version=""1.0"" encoding=""utf-16""?>     <CadastralReferenceData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <RukovoditelDoljnost>Начальник служби м\б кадастру</RukovoditelDoljnost>
  <RukovoditelFIO>Греков О.С.</RukovoditelFIO>
  <PDFTextMarningUp>1</PDFTextMarningUp>
  <PDFTextMarningDown>1</PDFTextMarningDown>
  <PDFTextMarningRight>1</PDFTextMarningRight>
  <PDFTextMarningLeft>2</PDFTextMarningLeft>
  <Titul_Template />
  <Body_Begin_Template />
  <Body_End_Template />
  <Raspiska_Template />
  <Pages />
  <Body_Template />
  </CadastralReferenceData>";

        }
    }
}



