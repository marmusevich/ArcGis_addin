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
        //public const string ObjectLayerName = @"Зона розміщення об’єкту ";
        public const string ObjectLayerName = @"Об""єкт";
        
        [XmlIgnore]
        public const string ObjectWorkspaceAndTableName = @"Кадастровая_справка.DBO.KS_OBJ";

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
  <Titul_Template>&lt;P class=MsoNormal style=""MARGIN: 0cm 0cm 0pt; tab - stops: 163.8pt""&gt;&lt;SPAN lang=EN-US style=""mso - ansi - language: EN - US""&gt;&lt;?xml:namespace prefix = ""o"" ns = ""urn: schemas - microsoft - com:office: office"" /&gt;&lt;o:p&gt;&lt;FONT size=3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; tab-stops: 163.8pt"" & gt;&lt;SPAN lang = EN - US style=""mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; tab-stops: 163.8pt"" & gt;&lt;SPAN lang = UK & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;B&gt;&lt;SPAN lang = UK & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;B&gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;B&gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;ОДЕСЬКА МІСЬКА РАДА&lt;/SPAN&gt;&lt;/B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;B&gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;УПРАВЛІННЯ АРХІТЕКТУРИ ТА МІСТОБУДУВАННЯ&lt;/SPAN&gt;&lt;/B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;СЛУЖБА МІСТОБУДІВНОГО КАДАСТРУ&lt;/SPAN&gt;&lt;/B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 9pt; COLOR: #333333""&gt;65082, м.Одеса, вул.Гоголя, 10, телефони: (048) 723-07-35, (048) 723-01-88, е-mail: &lt;U&gt;&lt;A href = ""mailto:odemmk@odessa.gov.ua"" & gt; odemmk @odessa.gov.ua&lt;/A&gt;&lt;/U&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 9pt; COLOR: #333333""&gt;&lt;U&gt;&lt;/U&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT size = 3 & gt;&lt;A href = ""http://www.ombk.odessa.ua/"" & gt; www.ombk.odessa.ua&lt;/A&gt; &lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 15pt; COLOR: #333333""&gt;КАДАСТРОВА&lt;SPAN style = ""mso-tab-count: 1"" & gt; &lt;/SPAN&gt;ДОВІДКА&lt;/SPAN&gt;&lt;/B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;U&gt;&lt;SPAN lang = UK & gt;&lt;A href = ""http://ombk.ymaps.net.ua/#{NAME}"" & gt;&lt;B&gt;&lt;SPAN style = ""COLOR: black"" & gt;&lt;FONT size = 3 face=""Times New Roman""&gt;№&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/A&gt;&lt;B&gt;&lt;SPAN style = ""COLOR: black"" & gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/SPAN&gt;&lt;/U&gt;&lt;B&gt;&lt;U&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt; &lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/U&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" align=center&gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;B&gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;«___»&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;«____________»&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;20____ року&lt;/SPAN&gt;&lt;/B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;BLOCKQUOTE style = ""MARGIN-RIGHT: 0px"" dir=ltr&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt; Код _ _____&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;&lt;STRONG&gt;Адреса об’єкта:&lt;/STRONG&gt; &lt;/SPAN&gt;&lt;SPAN style = ""COLOR: #333333; mso-ansi-language: RU"" & gt;{_ОписательныйАдрес_
    }&lt;U&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/U&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN style = ""COLOR: #333333; mso-ansi-language: RU"" & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;STRONG&gt;Район&lt;/STRONG&gt;:&lt;SPAN style = ""mso-tab-count: 2"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;FONT size = 4 & gt; Суворівський&lt;/FONT&gt; &lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;&lt;FONT size = 3 & gt;&lt;STRONG&gt;Замовник&lt;/STRONG&gt;: ____&lt;/FONT&gt;&lt;/SPAN&gt;&lt;FONT size = 4 & gt;&lt;U&gt;&lt;SPAN style = ""COLOR: #333333; mso-ansi-language: RU"" & gt; ТОВ ""Меркурій+""&lt;/SPAN&gt;&lt;/U&gt;&lt;U&gt;&lt;SPAN style = ""COLOR: #333333"" & gt;&amp;nbsp;&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;_&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;&lt;/BLOCKQUOTE&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;BLOCKQUOTE style = ""MARGIN-RIGHT: 0px"" dir=ltr&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;Начальник служби містобудівного кадастру &lt;SPAN style = ""mso-tab-count: 4"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;О.С.&lt;/SPAN&gt;&lt;SPAN lang = UK style=""COLOR: #333333; mso-ansi-language: RU""&gt; &lt;/SPAN&gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;Греков &lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;&lt;/BLOCKQUOTE&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;BLOCKQUOTE style = ""MARGIN-RIGHT: 0px"" dir=ltr&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;Вх. № &lt;U&gt;01-&lt;/U&gt;&lt;/SPAN&gt;&lt;U&gt;&lt;SPAN style = ""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: RU"" & gt;11&lt;/SPAN&gt;&lt;/U&gt;&lt;U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;/&lt;/SPAN&gt;&lt;/U&gt;&lt;U&gt;&lt;SPAN style = ""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: RU"" & gt;5181&lt;/SPAN&gt;&lt;/U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&amp;nbsp;&lt;/SPAN&gt;&lt;SPAN style = ""mso-tab-count: 2"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;Дата&amp;nbsp;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333; mso-ansi-language: RU""&gt;&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;21&lt;/SPAN&gt;&lt;/U&gt;&lt;U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;.10&lt;/SPAN&gt;&lt;/U&gt;&lt;U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;. 2016 &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;року&lt;SPAN style = ""mso-tab-count: 1"" & gt;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/U&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;SPAN style = ""mso-tab-count: 5"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;&lt;/BLOCKQUOTE&gt;</Titul_Template>
  <Body_Begin_Template>&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 4 & gt; Кадастрова інформація про ділянку(об’єкт):&lt;? xml:namespace prefix = ""o"" ns = ""urn:schemas-microsoft-com:office:office"" /&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;I style = ""mso-bidi-font-style: normal"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/I&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;I style = ""mso-bidi-font-style: normal"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT size = 4 face=""Times New Roman""&gt;Загальна інформація&lt;/FONT&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/I&gt;&lt;/B&gt;&lt;/P&gt;</Body_Begin_Template>
  <Body_End_Template>&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;I style = ""mso-bidi-font-style: normal"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 4 & gt; Довідкова інформація:&lt;? xml:namespace prefix = ""o"" ns = ""urn:schemas-microsoft-com:office:office"" /&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/I&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt 36pt; LINE-HEIGHT: 15.6pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; FONT-FAMILY: Symbol; COLOR: #333333; mso-fareast-font-family: Symbol; mso-bidi-font-family: Symbol""&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;·&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;FONT size = 3 face=""Georgia, Times New Roman, Times, serif""&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/FONT&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Georgia, Times New Roman, Times, serif"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;Згідно&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt""&gt;Порядку надання містобудівних умов та обмежень(МУ і О) забудови земельної ділянки, затвердженого Наказом&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;Міністерства регіонального розвитку, будівництва та житлово-комунального господарства України № 109 від 07.07.2011 р., кадастрова довідка з містобудівного кадастру є документом, що використовується під час підготовки МУ і О забудови земельної ділянки.Інформація, що міститься в містобудівному кадастрі,&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;повинна обов’язково враховуватись&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;в МУ і О.&lt; SPAN style = ""COLOR: #333333"" & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt 36pt; LINE-HEIGHT: 15.6pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Georgia, Times New Roman, Times, serif"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; FONT-FAMILY: Symbol; COLOR: #333333; mso-fareast-font-family: Symbol; mso-bidi-font-family: Symbol""&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;·&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt""&gt;Наявність підземних та наземних інженерних комунікацій на ділянці, які слід врахувати під час розробки містобудівного розрахунку та подальшого проектування, можливо встановити&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;при наявності відкорегованої топо-геоподоснови М 1:500. Ведення, корегування та оновлення топооснови &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;М 1:500 м.Одеси, згідно &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;рішення виконкому ОМР від 30.10.2014 р. № 289 «Про затвердження Положення про інформаційні ресурси єдиної цифрової топографічної основи(ЄЦТО) території міста Одеси як складової частини системи баз даних&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;містобудівного кадастру» виконує КП «Одеспроект» (вул.Канатна, 26-Б).&lt;SPAN style = ""COLOR: #333333"" & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt 36pt; LINE-HEIGHT: 15.6pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Georgia, Times New Roman, Times, serif"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; FONT-FAMILY: Symbol; COLOR: #333333; mso-fareast-font-family: Symbol; mso-bidi-font-family: Symbol""&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;·&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt""&gt;Кадастрова довідка разом з генеральним планом забудови земельної ділянки(М 1:500), розробленим в складі містобудівного розрахунку(або ДПТ), &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;є графічною складовою МУ і О.&lt; SPAN style = ""COLOR: #333333"" & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt 36pt; LINE-HEIGHT: 15.6pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Georgia, Times New Roman, Times, serif"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; FONT-FAMILY: Symbol; COLOR: #333333; mso-fareast-font-family: Symbol; mso-bidi-font-family: Symbol""&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;·&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;Після завершення будівельних робот замовник(забудовник земельної ділянки) повинен замовити виконавчу зйомку завершеного будівництвом об’єкту, що приймається &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;в експлуатацію і,&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;відповідно до &lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt""&gt;Положення про інформаційні ресурси ЕЦТО території міста Одеси як складової частини системи баз даних&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;містобудівного кадастру, надати в КП «Одеспроект» для внесення в ЕЦТО на бумажному та цифровому носіях.&lt;SPAN style = ""COLOR: #333333"" & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;I style = ""mso-bidi-font-style: normal"" & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/I&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;I style = ""mso-bidi-font-style: normal"" & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/I&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt; LINE-HEIGHT: 15.6pt"" & gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;B style = ""mso-bidi-font-weight: normal"" & gt;&lt;I style = ""mso-bidi-font-style: normal"" & gt;&lt;SPAN lang = UK style=""COLOR: #333333""&gt;Примітки:&lt;/SPAN&gt;&lt;/I&gt;&lt;/B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 10pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN lang = EN - US style=""FONT-SIZE: 14pt; COLOR: #333333; mso-ansi-language: EN-US""&gt;R&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt; – радіус;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt; ДПТ – детальний план території&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt; СЗЗ - Санітарно-захисна зона;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt; УАМ – управління архітектури&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;та містобудування ОМР&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt; УЗР ДКВ ОМР – управління земельних ресурсів департаменту комунальної власності &lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt; ОМР - Одеська міська рада&lt;/FONT&gt; &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-SIZE: 11pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: #333333; mso-fareast-font-family: ""Times New Roman""; mso-ansi-language: UK; mso-fareast-language: RU; mso-bidi-language: AR-SA'&gt;Служба містобудівного кадастру УАМ&lt;BR style = ""PAGE-BREAK-BEFORE: always; mso-special-character: line-break"" clear=all&gt;&lt;/SPAN&gt;&lt;/P&gt;</Body_End_Template>
  <Raspiska_Template>&lt;P class=MsoNormal style = ""TEXT-ALIGN: right; MARGIN: 0cm 0cm 0pt"" align=right&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 11pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt; кадастрову довідку отримав:&lt;? xml:namespace prefix = ""o"" ns = ""urn:schemas-microsoft-com:office:office"" /&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;DIV style = ""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: windowtext 1.5pt solid; PADDING-BOTTOM: 1pt; PADDING-TOP: 0cm; PADDING-LEFT: 0cm; BORDER-LEFT: medium none; PADDING-RIGHT: 0cm; mso-element: para-border-div"" & gt;
&lt;P class=MsoNormal style = ""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 0cm; TEXT-ALIGN: right; PADDING-TOP: 0cm; PADDING-LEFT: 0cm; MARGIN: 0cm 0cm 0pt; BORDER-LEFT: medium none; PADDING-RIGHT: 0cm; mso-border-bottom-alt: solid windowtext 1.5pt; mso-padding-alt: 0cm 0cm 1.0pt 0cm"" align=right&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 11pt; COLOR: #333333""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;&lt;/DIV&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt"" align=center&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 11pt; COLOR: #333333""&gt;&lt;FONT face = ""Times New Roman"" & gt; Ф.І.Б (дата, підпис) &lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 18pt"" & gt;&lt;SPAN style = ""mso-ansi-language: RU"" & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt"" align=center&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt""&gt;&lt;FONT face = ""Times New Roman"" & gt; Інформація моніторингу містобудівної діяльності &lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt"" align=center&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 14pt""&gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;з намірів забудови ділянки за адресою:&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt"" align=center&gt;&lt;S&gt;&lt;SUB&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 8pt""&gt;&lt;o:p&gt;&lt;SPAN style = ""TEXT-DECORATION: none"" & gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/SUB&gt;&lt;/S&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: center; MARGIN: 0cm 0cm 0pt"" align=center&gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;U&gt;__________________{_ОписательныйАдрес_
    }
    _____________________&lt;o:p&gt;&lt;/o:p&gt;&lt;/U&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraph style = ""MARGIN: 0cm 0cm 10pt 36pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;&lt;FONT size = 3 & gt;1.&lt;/FONT&gt;&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;FONT size = 3 & gt; Містобудівні умови та обмеження:&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = ""mso-tab-count: 2"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;Надано &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;/SPAN&gt;«____» _______________ 20___ року&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;№ ________.&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpFirst style = ""MARGIN: 0cm 0cm 0pt 21.3pt; TEXT-INDENT: 49.6pt; mso-list: l0 level2 lfo1; mso-add-space: auto"" & gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;&lt;FONT size = 3 & gt;1.1.&lt;/FONT&gt;&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;FONT size = 3 & gt; Внесення змін __________________&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;(підпис уповноваженої особи служби містобудівного кадастру у складі управління архітектури та містобудування Одеської міської ради).&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpMiddle style = ""MARGIN: 0cm 0cm 0pt 88.5pt; TEXT-INDENT: -18pt; mso-list: l0 level2 lfo1; mso-add-space: auto"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;1.2.&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;_______________________________________________________________________.&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpMiddle style = ""MARGIN: 0cm 0cm 0pt 88.5pt; TEXT-INDENT: -18pt; mso-list: l0 level2 lfo1; mso-add-space: auto"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;1.3.&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;_______________________________________________________________________.&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpMiddle style = ""MARGIN: 0cm 0cm 0pt 36pt"" & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;o:p&gt;&lt;FONT size = 3 & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpLast style = ""MARGIN: 0cm 0cm 10pt 36pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;&lt;FONT size = 3 & gt;2.&lt;/FONT&gt;&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;FONT size = 3 & gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;Е&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; кспертиза проектно&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;ї&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; документац&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;ії.Паспорт оздоблення фасаду.&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = ""mso-tab-count: 2"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;Внесено до бази даних &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;містобудівного &lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;кадастру&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = ""mso-tab-count: 1"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;«____» _______________ 20 ___ року № ________(підпис уповноваженої особи служби містобудівного кадастру у складі управління архітектури та містобудування Одеської міської ради).&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = ""mso-tab-count: 1"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraph style = ""MARGIN: 0cm 0cm 10pt 36pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;&lt;FONT size = 3 & gt;3.&lt;/FONT&gt;&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;FONT size = 3 & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; Декларац&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;і&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; я &lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;пр&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; о &lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;початок&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""'&gt; &lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;будівельних &lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;(&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;підготовчих) &lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; р&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;о&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; б&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;і&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; т&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;.&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt 18pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = ""mso-tab-count: 1"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;Внесено до бази даних&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;містобудівного&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;кадастру&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;«____» _______________ 20___ року № ________(підпис уповноваженої особи служби містобудівного кадастру у складі управління архітектури та містобудування Одеської міської ради).&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt 18pt"" & gt;&lt;SPAN lang = UK & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraph style = ""MARGIN: 0cm 0cm 10pt 36pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;&lt;FONT size = 3 & gt;4.&lt;/FONT&gt;&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;FONT size = 3 & gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; Декларац&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;і&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; я &lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;(сертифікат) про&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; вв&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;е&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; де&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;ння &lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp;&lt;/SPAN&gt;в &lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;е&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; ксплуатац&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;і&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt; ю&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;.&lt;/SPAN&gt;&lt;SPAN style = 'FONT-FAMILY: ""Times New Roman"",""serif""' & gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/FONT&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt 18pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;&lt;SPAN style = ""mso-tab-count: 1"" & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;Внесено до бази даних&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;містобудівного&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;кадастру&lt;SPAN style = ""mso-spacerun: yes"" & gt;&amp;nbsp; &lt;/SPAN&gt;&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT face = ""Times New Roman"" & gt;&lt;FONT size = 3 & gt;«____» _______________ 20___ року № ________(підпис уповноваженої особи служби містобудівного кадастру у складі управління архітектури та містобудування Одеської міської ради).&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;o:p&gt;&lt;FONT size = 3 face=""Times New Roman""&gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpFirst style = ""MARGIN: 0cm 0cm 0pt 36pt; TEXT-INDENT: -18pt; mso-list: l0 level1 lfo1"" & gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;SPAN style = ""mso-list: Ignore"" & gt;&lt;FONT size = 3 & gt;5.&lt;/FONT&gt;&lt;SPAN style = 'FONT: 7pt ""Times New Roman""' & gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/SPAN&gt;&lt;/SPAN&gt;&lt;/SPAN&gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;FONT size = 3 & gt; Присвоєння коду об’єкта для внесення до адресного реєстру&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoListParagraphCxSpLast style = ""MARGIN: 0cm 0cm 10pt 36pt"" & gt;&lt;SPAN lang = UK style='FONT-FAMILY: ""Times New Roman"",""serif""; mso-ansi-language: UK'&gt;&lt;o:p&gt;&lt;FONT size = 3 & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt"" & gt;&lt;SPAN lang = UK & gt;&lt;FONT size = 3 & gt;&lt;FONT face = ""Times New Roman"" & gt;«____» _______________ 20___ року № ________(підпис уповноваженої особи служби містобудівного кадастру у складі управління архітектури та містобудування Одеської міської ради).&lt;o:p&gt;&lt;/o:p&gt;&lt;/FONT&gt;&lt;/FONT&gt;&lt;/SPAN&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""MARGIN: 0cm 0cm 0pt"" & gt;&lt;B&gt;&lt;SPAN lang = UK style=""FONT-SIZE: 13pt""&gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt; TEXT-INDENT: 35.45pt"" & gt;&lt;B&gt;&lt;SPAN style = ""FONT-SIZE: 13pt; mso-ansi-language: RU"" & gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;
&lt;P class=MsoNormal style = ""TEXT-ALIGN: justify; MARGIN: 0cm 0cm 0pt; TEXT-INDENT: 35.45pt"" & gt;&lt;B&gt;&lt;SPAN style = ""FONT-SIZE: 13pt; mso-ansi-language: RU"" & gt;&lt;o:p&gt;&lt;FONT face = ""Times New Roman"" & gt;&amp;nbsp;&lt;/FONT&gt;&lt;/o:p&gt;&lt;/SPAN&gt;&lt;/B&gt;&lt;/P&gt;</Raspiska_Template>
  <Pages>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>2.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Double Alternating Scale Bar</TypeScaleBarName>
      <Caption>Выкопировка с топологии</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Реєстр вулиць</Layer>
        <Layer>Annotation</Layer>
        <Layer>Характеристика</Layer>
        <Layer>Номер_буд</Layer>
        <Layer>Пояснення</Layer>
        <Layer>Знаки</Layer>
        <Layer>Берегові навігаційні знаки</Layer>
        <Layer>Чагарники колючі окремі групи знак</Layer>
        <Layer>Чагарники колючі зарості знак</Layer>
        <Layer>Чагарники колючі зарості знак</Layer>
        <Layer>Ділянки, покриті відходами пром-підпр-знак</Layer>
        <Layer>Ділянки зі зритою поверхнею(знак)</Layer>
        <Layer>Фруктові і цитрусові сади знак</Layer>
        <Layer>Газони знак</Layer>
        <Layer>Скульптури стели- тури знак</Layer>
        <Layer>Характеристика глибини обриву</Layer>
        <Layer>Характеристики каналів, канав</Layer>
        <Layer>Характеристики сухих канав</Layer>
        <Layer>Хрести і знаки з релігійним зображенням</Layer>
        <Layer>Інші знаки дорожні</Layer>
        <Layer>Кабельні стовпчики - сторожки</Layer>
        <Layer>Камені-орієнтири окремі</Layer>
        <Layer>Кладовища знак</Layer>
        <Layer>Клумби знак</Layer>
        <Layer>Ліс високостовб- знак</Layer>
        <Layer>Ліси низькор- знак</Layer>
        <Layer>Пам'ятники Вічний вогонь знак</Layer>
        <Layer>Памятники знак</Layer>
        <Layer>Рідка поросль знак</Layer>
        <Layer>Рідколісся низькоросле знак</Layer>
        <Layer>Рідколісся знак</Layer>
        <Layer>Росл трав степу знак</Layer>
        <Layer>Рослин- високот- знак</Layer>
        <Layer>Рослин- волог- знак</Layer>
        <Layer>Синагоги знак</Layer>
        <Layer>Сквери, парки, бульвари, знак</Layer>
        <Layer>Телевіз- знак</Layer>
        <Layer>Трав росл лук знак</Layer>
        <Layer>Виноградники знак</Layer>
        <Layer>Заболочені землі знак</Layer>
        <Layer>Зарості очерету знак</Layer>
        <Layer>Землі засол знак</Layer>
        <Layer>Знаки нівелірні репери стінні</Layer>
        <Layer>Кран знак</Layer>
        <Layer>Могили братські знак</Layer>
        <Layer>Карта</Layer>
        <Layer>2 000 </Layer>
        <Layer>Заводські і фабричні труби, що є орієнтирами</Layer>
        <Layer>Ями позамасштабні</Layer>
        <Layer>Ями і траншеї позамасштабні</Layer>
        <Layer>Висота від землі до низу труби (арочн- перех)</Layer>
        <Layer>Вишки легкого типу позамасштабні</Layer>
        <Layer>Входи в підземні частини будівель (напрямок)</Layer>
        <Layer>Трансформатори на стовпах позамасштабні</Layer>
        <Layer>Світлофори на стовпах</Layer>
        <Layer>Світлофори карликові</Layer>
        <Layer>Свердловини з механічним підйомом води</Layer>
        <Layer>Свердловини глибокого буріння</Layer>
        <Layer>Свердловини артезіанські</Layer>
        <Layer>Стовп ЗБ квадратний</Layer>
        <Layer>Стовп мет квадратний</Layer>
        <Layer>Споруди баштового типу капітальні</Layer>
        <Layer>Скупчення каміння позамасштабний</Layer>
        <Layer>Скупчення каміння(окремий камінь)</Layer>
        <Layer>Скелі останці позамасштабні</Layer>
        <Layer>Скелі надводні</Layer>
        <Layer>Семафори</Layer>
        <Layer>Пункти геодезичних мереж згущення</Layer>
        <Layer>Пункти геодез- мереж згущення у стінах будівель</Layer>
        <Layer>Пункти державної геодезичної мережі</Layer>
        <Layer>Прожектори карликові постійні</Layer>
        <Layer>Прожектори</Layer>
        <Layer>Позначки висот підпірної стінки</Layer>
        <Layer>Позначки урізів води</Layer>
        <Layer>Покажчик напрямку схилів (бергштрихи)</Layer>
        <Layer>Покажчик напрямку схилу(бергштрихи ізобат)</Layer>
        <Layer>Пляжі обладнані</Layer>
        <Layer>Підпис</Layer>
        <Layer>Павільйони, альтанки позамасштабні</Layer>
        <Layer>Окремі кущі уздовж доріг річок</Layer>
        <Layer>Напрям течії</Layer>
        <Layer>Могили окремі</Layer>
        <Layer>Мочарі з очеретом і тростиною</Layer>
        <Layer>Місця переходу від повітряних ліній зв’язку до кабельних підзем</Layer>
        <Layer>Місця переходу від повітряних ЛЕП до кабельних підземних ЛЕП</Layer>
        <Layer>Межі аркових переходів трубопроводів</Layer>
        <Layer>Маяки</Layer>
        <Layer>ЛЕП направлення вис напруги</Layer>
        <Layer>ЛЕП направлення низ напруги</Layer>
        <Layer>Кущі що ростуть окремо</Layer>
        <Layer>Кургани позамасштабні</Layer>
        <Layer>Крани підйомні( кран-балки)</Layer>
        <Layer>Колонки водозбірні</Layer>
        <Layer>Колодязі з механічним підйомом води(колодязі)</Layer>
        <Layer>Колодязі з ручним насосом</Layer>
        <Layer>Колодязі з корбою на стовпах</Layer>
        <Layer>Колодязі артезіанські</Layer>
        <Layer>Кінці колій з упорами</Layer>
        <Layer>Кінці колій без упорів</Layer>
        <Layer>Камені у водоймах обсихаючі окремі</Layer>
        <Layer>Камені у водоймах надводні розташ- групами</Layer>
        <Layer>Камені у водоймах надводні окремі</Layer>
        <Layer>Горби позамасштабні</Layer>
        <Layer>Гідранти пожежні полівальні</Layer>
        <Layer>Факели газові</Layer>
        <Layer>Джерела необладнані</Layer>
        <Layer>Чагарники окремі групи</Layer>
        <Layer>Будки трансформаторні позамасштабні</Layer>
        <Layer>Блискавковідводи</Layer>
        <Layer>Камені у водоймах надводні окремі</Layer>
        <Layer>Бензоколонки, колонки дизельного пального</Layer>
        <Layer>Зсуви недіючі</Layer>
        <Layer>Зсуви діючі</Layer>
        <Layer>Землі засолені виходами солі на поверхню</Layer>
        <Layer>Заводські і фабричні труби, їх основа</Layer>
        <Layer>Залізобетонна стіна</Layer>
        <Layer>Залізниці лінія контактної мережі</Layer>
        <Layer>Заболоченість</Layer>
        <Layer>Яри без знаку</Layer>
        <Layer>Ями в масштабі плану</Layer>
        <Layer>Ями і траншеї бетоновані</Layer>
        <Layer>Ями і траншеї</Layer>
        <Layer>Ями без знаку</Layer>
        <Layer>В'їзди на 2-й поверх</Layer>
        <Layer>Вузькі смуги дерев висотою до 4 м</Layer>
        <Layer>Вузькі смуги дерев висотою 4 м і більше</Layer>
        <Layer>Вулиці</Layer>
        <Layer>Ворота габаритні</Layer>
        <Layer>Водовипуски із заслонками</Layer>
        <Layer>Водосховища підземні</Layer>
        <Layer>Водна рослинність</Layer>
        <Layer>Виїмки без знаку</Layer>
        <Layer>Вишки легкого типу в масштабі плану</Layer>
        <Layer>Вирви карстові і псевдокарстові в масштабі плану</Layer>
        <Layer>Входи закриті в підземні частини будівель</Layer>
        <Layer>Входи відкриті в підземні частини будівель</Layer>
        <Layer>Веранди та тераси</Layer>
        <Layer>Вентилятори наземні</Layer>
        <Layer>Трубопроводи на опорах</Layer>
        <Layer>Трубопроводи на грунті</Layer>
        <Layer>Трубопроводи без розподілу за призначенням</Layer>
        <Layer>Труби під дорогами (вихід)</Layer>
        <Layer>Труби під дорогами</Layer>
        <Layer>Труби на річках каналах(вихід)</Layer>
        <Layer>Труби на річках, каналах</Layer>
        <Layer>Терикони без знаку</Layer>
        <Layer>Тераси полів закріплені</Layer>
        <Layer>Тераси без знаку</Layer>
        <Layer>Телевізійні та радіощогли, ретранслятори</Layer>
        <Layer>Струмки з пересихаючою береговою лінією</Layer>
        <Layer>Струмки з невизначеною береговою лінією</Layer>
        <Layer>Струмки(лінійний об'єкт)</Layer>
        <Layer>Стенди, меморіальні дошки</Layer>
        <Layer>Стародавні і історичні стіни</Layer>
        <Layer>Станційні колії</Layer>
        <Layer>Спуски та сходи на набережних</Layer>
        <Layer>Споруди башт- типу капітальні в м-бі плану</Layer>
        <Layer>Смуги деревних насаджень шир - 3мм і висотою до 4м</Layer>
        <Layer>Смуги деревних насаджень шир - 3мм і висотою 4м та більше</Layer>
        <Layer>Смуги чагарників шириною від 3 до 8мм</Layer>
        <Layer>Смуги чагарників шириною - 3мм (живоплоти)</Layer>
        <Layer>Скелі останці її основа</Layer>
        <Layer>Скелі останці в масштабі плану</Layer>
        <Layer>Шляхопроводи автодорожні над залізницею</Layer>
        <Layer>Селища сільського типу</Layer>
        <Layer>Ряди окремих дерев уздовж доріг, річок</Layer>
        <Layer>Розподільний металевий блок</Layer>
        <Layer>Розподільний бетонний блок</Layer>
        <Layer>Рейки підйомних кранів</Layer>
        <Layer>Промоїни</Layer>
        <Layer>Приямки</Layer>
        <Layer>Порти, пристані з обладнаними причалами</Layer>
        <Layer>Польові та лісові дороги (край пунктир)</Layer>
        <Layer>Польові та лісові дороги</Layer>
        <Layer>Покращені грунтові дороги 0,3</Layer>
        <Layer>Покращені грунтові дороги 0,1</Layer>
        <Layer>Погреби і овочесховища(лінія)</Layer>
        <Layer>Площадки</Layer>
        <Layer>Пішохідні стежки</Layer>
        <Layer>Підпірно-регулюючі споруди</Layer>
        <Layer>Підпірні стінки прямовисні</Layer>
        <Layer>Підпірні стінки похилі в масштабі плану</Layer>
        <Layer>Підпірні стінки похилі</Layer>
        <Layer>Переходи підземні</Layer>
        <Layer>Парники</Layer>
        <Layer>Парапети в масштабі плану</Layer>
        <Layer>Парапети</Layer>
        <Layer>Опорні стовпи та ферми</Layer>
        <Layer>Огорожі, трельяжі</Layer>
        <Layer>Огорожі шиферні на фундаменті</Layer>
        <Layer>Огорожі металеві вис- менше 1 м</Layer>
        <Layer>Огорожі металеві на кам- бет фундам вис-менше 1м</Layer>
        <Layer>Огорожі дротяні з колючого дроту</Layer>
        <Layer>Огорожі дротяні з гладкого дроту</Layer>
        <Layer>Огорожі дротяні “електропастухи”</Layer>
        <Layer>Обриви земляні без знаку</Layer>
        <Layer>Обриви скелясті в масштабі плану</Layer>
        <Layer>Непроїжджі вулиці</Layer>
        <Layer>Наземні частини підземних споруд</Layer>
        <Layer>Навіси та перекриття між будинками</Layer>
        <Layer>Навіси на стовпах</Layer>
        <Layer>Навіси-козирки</Layer>
        <Layer>Навіси для автомобільних ваг</Layer>
        <Layer>Нависаючі частини будинків</Layer>
        <Layer>Насипи без знаку</Layer>
        <Layer>Насипи</Layer>
        <Layer>Набережні без знаку</Layer>
        <Layer>Мостів опори</Layer>
        <Layer>Мости пішохідні зі східцями</Layer>
        <Layer>Мости пішохідні висячі</Layer>
        <Layer>Мости однопрогінні кам'яні, бетонні,залізобетонні</Layer>
        <Layer>Мости на плавучих опорах</Layer>
        <Layer>Мости малі кам'яні ЗБ мет</Layer>
        <Layer>Мости малі дерев'яні</Layer>
        <Layer>Мости довжиною до 1 м на автомобільних дорогах</Layer>
        <Layer>Мости багатопрогінні кам'яні, бетонні,залізобетонні</Layer>
        <Layer>Межі зміни покриття</Layer>
        <Layer>Межі землекористувань</Layer>
        <Layer>Межа проїздної частини автодоріг</Layer>
        <Layer>Лотки (їх борти)</Layer>
        <Layer>Лотки і жолоби для подачі води наземні</Layer>
        <Layer>Лотки на залізницях</Layer>
        <Layer>Лінії зв'язку повітряні дротяні на забудов території</Layer>
        <Layer>Лінії зв'язку повітряні дротяні на незабуд території</Layer>
        <Layer>Лінії електропередач повітряні кабельні</Layer>
        <Layer>Лінії берегові постійні</Layer>
        <Layer>Лінії берегові невизначені по болотах очерету</Layer>
        <Layer>Лінії берегові непостійні пересихаючі</Layer>
        <Layer>Лінія проїзду</Layer>
        <Layer>Лінія коричнева</Layer>
        <Layer>Лінія КОНТУР-ЗЕЛЕНИЙ</Layer>
        <Layer>Лінія КОНТУР</Layer>
        <Layer>Лінія чорна</Layer>
        <Layer>Лінія (2-1) зелена</Layer>
        <Layer>ЛЕП повітряні дротяні на незабудованій території низ напруги</Layer>
        <Layer>ЛЕП повітряні дротяні на незабудованій території вис напруги</Layer>
        <Layer>Кургани в масштабі плану без знаку</Layer>
        <Layer>Кургани в масштабі плану</Layer>
        <Layer>Козлові та мостові крани</Layer>
        <Layer>Косметичний шар чорний</Layer>
        <Layer>Косметичний шар білий</Layer>
        <Layer>Короб</Layer>
        <Layer>Колодязі</Layer>
        <Layer>Кар'єри площа</Layer>
        <Layer>Кар'єри без знаку</Layer>
        <Layer>Кар’єри</Layer>
        <Layer>Канави сухі</Layer>
        <Layer>Канали</Layer>
        <Layer>Камера на трубопроводі</Layer>
        <Layer>Кабель</Layer>
        <Layer>Інформаційні та рекламні стенди</Layer>
        <Layer>Грунтові дороги (край пунктир)</Layer>
        <Layer>Ґрунтові дороги(путівці)</Layer>
        <Layer>Газопровід</Layer>
        <Layer>Ганки закриті кам’яні</Layer>
        <Layer>Ганки закриті дерев’яні</Layer>
        <Layer>Ганки відкриті, східці наверх</Layer>
        <Layer>Галереї</Layer>
        <Layer>Фонтани(основа)</Layer>
        <Layer>Естакади технологічні та вантажні</Layer>
        <Layer>Естакади підйомних кранів</Layer>
        <Layer>Естакади для ремонту автомобілів</Layer>
        <Layer>Естакади</Layer>
        <Layer>Дороги підвісні</Layer>
        <Layer>Дерев'яні огорожі з капітальними опорами</Layer>
        <Layer>Дерев'яні огорожі на кам бет цегл фундаменту</Layer>
        <Layer>Дамби і вали позамасштабні</Layer>
        <Layer>Дамби і вали без знаку</Layer>
        <Layer>Дамби і вали</Layer>
        <Layer>Човнові станції</Layer>
        <Layer>Брандмауери</Layer>
        <Layer>Бункери саморозвантажні</Layer>
        <Layer>Берегові лінії</Layer>
        <Layer>Балкони на стовпах</Layer>
        <Layer>Басейни облицьовані</Layer>
        <Layer>Баштові та портальні крани</Layer>
        <Layer>Баки та цистерни, газгольдери в масштабі плану</Layer>
        <Layer>Баки та цистерни-газгольдери позамасштабні</Layer>
        <Layer>Автомобільні дороги з удоск-покрит- (точка)</Layer>
        <Layer>Автомобільні дороги(край узбіччя)</Layer>
        <Layer>Арки постійні на автомобільних дорогах</Layer>
        <Layer>Акведуки</Layer>
        <Layer>В'їзди під арками</Layer>
        <Layer>Переходи та галереї надземні між будинками</Layer>
        <Layer>Укоси укріплені в масштабі плану</Layer>
        <Layer>Укоси укріплені позамасштабні</Layer>
        <Layer>Укоси неукріплені в масштабі плану</Layer>
        <Layer>Укоси неукріплені позамасштабні</Layer>
        <Layer>Укоси без знаку</Layer>
        <Layer>Труби димохідні котельних</Layer>
        <Layer>Трамвай, лінія контактної мережі</Layer>
        <Layer>Трамвайні колії</Layer>
        <Layer>Шляхопроводи залізничні над автомобільною дорогою</Layer>
        <Layer>Шлагбаум</Layer>
        <Layer>Ряди дерев (алеї) на вулицях</Layer>
        <Layer>Позначки висот</Layer>
        <Layer>Огорожі металеві вис- 1 м і більше</Layer>
        <Layer>Огорожі металеві на кам- бет фундам вис- 1м і більше</Layer>
        <Layer>Лінія направлення звязку</Layer>
        <Layer>Огорожі дротяні з дротяної сітки (вольєри)</Layer>
        <Layer>Ліхтарі електричні  одинарні</Layer>
        <Layer>Ліхтарі електричні  двойні</Layer>
        <Layer>Ліхтарі електричні(декоративні)</Layer>
        <Layer>ЛЕП повітряні дротяні на забудованій території вис напруги</Layer>
        <Layer>ЛЕП повітряні дротяні на забудованій території низ напруги</Layer>
        <Layer>Колодязі оглядові (люки)</Layer>
        <Layer>Кам’яні, залізобетонні огорожі заввишки 1 м та більше</Layer>
        <Layer>Дерев'яні огорожі суцільні,штахетні</Layer>
        <Layer>Кам’яні, залізобетонні та глинобитні огорожі заввишки менше 1 м</Layer>
        <Layer>Дерева хвойні характеристика</Layer>
        <Layer>Дерева листяні характеристика</Layer>
        <Layer>Дерева що стоять окремо</Layer>
        <Layer>Дерева які мають значення орієнтира хвойні</Layer>
        <Layer>Стовп дер круглий</Layer>
        <Layer>Стовп фермовий</Layer>
        <Layer>Стовп(лінія відтяжки)</Layer>
        <Layer>Стовп мет круглий</Layer>
        <Layer>Стовп ЗБ круглий</Layer>
        <Layer>Човнові станції</Layer>
        <Layer>Віадуки пішохідні над залізницею</Layer>
        <Layer>Зсуви</Layer>
        <Layer>Зсуви (штриховий пунктир)</Layer>
        <Layer>Колонади</Layer>
        <Layer>Круг поворотний</Layer>
        <Layer>Межа без бортового каменю</Layer>
        <Layer>Межа присадибної ділянки</Layer>
        <Layer>Межа з бортовим каменем</Layer>
        <Layer>Моли, причали, пірси із похилими стінками</Layer>
        <Layer>Моли, причали, пірси із прямовисними стінками</Layer>
        <Layer>Мости пішохідні</Layer>
        <Layer>Набережні прямовисні</Layer>
        <Layer>Обриви земляні</Layer>
        <Layer>Обриви земляні позамасштабні</Layer>
        <Layer>Обриви скеляст без знаку</Layer>
        <Layer>Сходи біля будинків, ганків</Layer>
        <Layer>Сходи для підйому на різні споруди</Layer>
        <Layer>Фунікульори і бремсберги</Layer>
        <Layer>Фонтани</Layer>
        <Layer>Тротуари доріжки без покриття</Layer>
        <Layer>Заводські і фабричні труби, що не є орієнтирами</Layer>
        <Layer>Зупинки автобусів та тролейбусів за населеними пунктами</Layer>
        <Layer>Горизонталі (потовщені 2-1)</Layer>
        <Layer>Горизонталі потовщені</Layer>
        <Layer>Горизонталі (основні 2-1)</Layer>
        <Layer>Горизонталі додаткові(5-1)</Layer>
        <Layer>Горизонталі основні</Layer>
        <Layer>Ж/Д дорога, вулиці</Layer>
        <Layer>Залізниці вузькоколійні</Layer>
        <Layer>Залізниці розібрані</Layer>
        <Layer>Залізниці ширококолійні</Layer>
        <Layer>2000 продовження</Layer>
        <Layer>Пам’ятники та монументи</Layer>
        <Layer>Павільйони, альтанки в масштабі плану</Layer>
        <Layer>Вимощення будинків</Layer>
        <Layer>Виноградники</Layer>
        <Layer>Трибуни</Layer>
        <Layer>Смуги деревних насаджень шир від3 до 8мм та - 8мм і вис-4м та більше</Layer>
        <Layer>Трав'яна рослинність лук</Layer>
        <Layer>Смуги деревних насаджень шир від 3 до 8мм та - 8мм і вис- до 4м</Layer>
        <Layer>Переїзд через залізницю</Layer>
        <Layer>Фруктові сади з ягідниками</Layer>
        <Layer>Сади фруктові з виноградниками</Layer>
        <Layer>Фруктові і цитрусові сади</Layer>
        <Layer>Дощові ями і споруди для збору води</Layer>
        <Layer>Зарості очерету</Layer>
        <Layer>Чагарники звичайні зарості</Layer>
        <Layer>Чагарники колючі зарості</Layer>
        <Layer>Бункери та будки оглядові</Layer>
        <Layer>Загони для тварин</Layer>
        <Layer>Рослинність вологолюбива трав’яна</Layer>
        <Layer>Рослинність високотрав’яна</Layer>
        <Layer>Рідколісся високостовбурне</Layer>
        <Layer>Рідколісся низькоросле</Layer>
        <Layer>Рослинність трав’яна степова</Layer>
        <Layer>Внутрішньо квартальне озеленення</Layer>
        <Layer>Вуличне озеленення</Layer>
        <Layer>Газони</Layer>
        <Layer>Городи</Layer>
        <Layer>Канали наземні</Layer>
        <Layer>Скупчення каміння в масштабі плану</Layer>
        <Layer>Канави</Layer>
        <Layer>Струмки (площинний об'єкт)</Layer>
        <Layer>Вантажно-розв- площадки високі</Layer>
        <Layer>Вантажно- розв- площадки високі рампи при будівлях або спорудах</Layer>
        <Layer>Вантажно-розв площадки низькі з бортовим каменем або без нього</Layer>
        <Layer>Платформи відкриті</Layer>
        <Layer>Платформи криті з опорами</Layer>
        <Layer>Ферма металева</Layer>
        <Layer>Ділянки зі зритою поверхнею</Layer>
        <Layer>Рілля</Layer>
        <Layer>Каплиці</Layer>
        <Layer>5 000</Layer>
        <Layer>Будівлі</Layer>
        <Layer>Оранжереї, теплиці</Layer>
        <Layer>Синагоги</Layer>
        <Layer>Будки контрольно-розподільчі</Layer>
        <Layer>Будинки з колонами замість частини або всього першого поверху</Layer>
        <Layer>Будки трансформаторні</Layer>
        <Layer>Будинки зруйновані</Layer>
        <Layer>Будівлі виробничого призначення</Layer>
        <Layer>Церкви, костьоли, кірхи</Layer>
        <Layer>Станції метеорологічні</Layer>
        <Layer>Склади</Layer>
        <Layer>Споруди малі (гаражі, туалети та інші)</Layer>
        <Layer>Дитячі майданчики</Layer>
        <Layer>Могили братські</Layer>
        <Layer>Басейни</Layer>
        <Layer>Легкі придорожні споруди (павільйони тощо)</Layer>
        <Layer>Сільськогосподарські підприємства(ферми, станції, майстерні)</Layer>
        <Layer>Відстійники</Layer>
        <Layer>Відстійники та очисні споруди облицьовані</Layer>
        <Layer>Автостоянки в містах</Layer>
        <Layer>Покриття</Layer>
        <Layer>Електричні підстанції</Layer>
        <Layer>Інші промислові об’єкти</Layer>
        <Layer>Внутрішній двір</Layer>
        <Layer>Тротуари доріжки з твердим покриттям</Layer>
        <Layer>Проїзди до будинків</Layer>
        <Layer>Проїжджі частини з покриттям</Layer>
        <Layer>Проїжджі частини без покриття</Layer>
        <Layer>Неорганізована територія</Layer>
        <Layer>Поверхні гравійні та галькові</Layer>
        <Layer>Поверхні щебеневі та кам’янисті розсипи</Layer>
        <Layer>10 000</Layer>
        <Layer>Колективні сади (дачі)</Layer>
        <Layer>Автомобільні дороги з удосконаленим покриттям(в масштабі плану</Layer>
        <Layer>Автомобільні дороги з покриттям шосе (в масштабі плану)</Layer>
        <Layer>Водосховища</Layer>
        <Layer>Електростанції</Layer>
        <Layer>Звалища</Layer>
        <Layer>50 000</Layer>
        <Layer>Ринки</Layer>
        <Layer>Лісопосадки молоді, розсадники лісових і декоративних порід</Layer>
        <Layer>Ліси високостовбурні</Layer>
        <Layer>Ліси пригнічені низькорослі</Layer>
        <Layer>Спортивні споруди</Layer>
        <Layer>Пляжі (площинний об'єкт)</Layer>
        <Layer>Ставки</Layer>
        <Layer>Автомагістралі автостради</Layer>
        <Layer>Пустирі</Layer>
        <Layer>Заболочені землі</Layer>
        <Layer>Майданчики будівельні</Layer>
        <Layer>150 000</Layer>
        <Layer>Сквери, парки, бульвари</Layer>
        <Layer>Піски рівні</Layer>
        <Layer>Річки (площинний об'єкт)</Layer>
        <Layer>Озера</Layer>
        <Layer>Океани і моря</Layer>
        <Layer>Кладовища (доріжки на ньому)</Layer>
        <Layer>Кладовища без деревної рослинності</Layer>
        <Layer>Кладовища з густою дерев росл</Layer>
        <Layer>Кладовища з окремими деревами</Layer>
        <Layer>Кладовища з рідколіссям</Layer>
        <Layer>Виробнича територія</Layer>
        <Layer>Квартали</Layer>
        <Layer>Зона розміщення об’єкту</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>{ _ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_
    }</Text>
          <PosX>-5</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Адреса: {_ОписательныйАдрес_
    }</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD43MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Викопіювання з топооснови м.Одеси М { _масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjA8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-4.5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAOgAAAEQAbwB1AGIAbABlACAAQQBsAHQAZQByAG4AYQB0AGkAbgBnACAAUwBjAGEAbABlACAAQgBhAHIAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlABQABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAABAAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQEAAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8BAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAkQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAQAAAJABoIYBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAAAAAAAAACRAA+YUeZLI0BGLtggACe5OQQEA+eUUeZLI0BGLtggACe5OQQEAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAADwPwAAAAANAAAAAAAAAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAFlAAAAAAAAAAAAAAAAAAAAZPQEADQAAAAAAAAAAAAAAA+YUeZLI0BGLtggACe5OQQEA+eUUeZLI0BGLtggACe5OQQEAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAADwPwAAAAANAAAAAAAAAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAAAAAAAAAgD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAA+eUUeZLI0BGLtggACe5OQQEAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAADwPwAAAAANAAAAAAAAAAAAAAAAABxAAAAAAAAAFEAAAAAAAAAAAA==</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>2.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Генеральный план</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Реєстр вулиць</Layer>
        <Layer>ГенПлан</Layer>
        <Layer>граница города</Layer>
        <Layer>центры обслуживания (расчётный срок)</Layer>
        <Layer>здания общественных центров(расчётный срок)</Layer>
        <Layer>Genplan_Osnovne_Kresl.DBO.centrdom_rezerv</Layer>
        <Layer>дачная застройка</Layer>
        <Layer>эстакада (порт)</Layer>
        <Layer>канатная дорога</Layer>
        <Layer>котельная (проект)</Layer>
        <Layer>котельная(сущ)</Layer>
        <Layer>выборочная рекострукция(расчётный срок)</Layer>
        <Layer>объекты стационар.рекреации за счёт реконструкции терр.</Layer>
        <Layer>реконструкция под мн/квартирную застройку</Layer>
        <Layer>мн/квартирная застройка на свободных тер. (расчётный срок)</Layer>
        <Layer>объекты здравохранения и собес(расчётный срок)</Layer>
        <Layer>территории объектов здравохранения и собес</Layer>
        <Layer>зоопарк</Layer>
        <Layer>mega_line</Layer>
        <Layer>территории санаторно-оздоровит.учреждений</Layer>
        <Layer>набережная</Layer>
        <Layer>кр.линии магистральных улиц</Layer>
        <Layer>кр.линии жилых улиц</Layer>
        <Layer>территории стационарной рекреации</Layer>
        <Layer>багатоквартирні будівлі</Layer>
        <Layer>территории общественных центров (расчетный срок)</Layer>
        <Layer>порт</Layer>
        <Layer>порт(расчётный срок)</Layer>
        <Layer>мосты</Layer>
        <Layer>оч.сооружения ливневой канализации(расчет.срок)</Layer>
        <Layer>учреждения науки и образования(расчётный срок)</Layer>
        <Layer>парк-памятник</Layer>
        <Layer>парк-памятник(граница)</Layer>
        <Layer>специализированные парки(расчётный срок)</Layer>
        <Layer>парковые аллеи</Layer>
        <Layer>пирсы (расчётный срок)</Layer>
        <Layer>пляжи(расчётный срок)</Layer>
        <Layer>Genplan_Osnovne_Kresl.DBO.polosa</Layer>
        <Layer>полоса отвода ж.д.</Layer>
        <Layer>магистральные улицы</Layer>
        <Layer>промзоны</Layer>
        <Layer>резерв промтерриторий</Layer>
        <Layer>промздания</Layer>
        <Layer>предприятия стационарной рекреации (расчётный срок)</Layer>
        <Layer>резерв развития жилой и общественной застройки</Layer>
        <Layer>vodnaya povерхность</Layer>
        <Layer>резерв.территории для жилой и обществ.застройки</Layer>
        <Layer>усадебная застройка (расчётный срок)</Layer>
        <Layer>мусороперерабатывающий завод</Layer>
        <Layer>спортивные объекты (расчётный срок)</Layer>
        <Layer>специализированные зелёные зоны</Layer>
        <Layer>технопарк(расчётный срок)</Layer>
        <Layer>центры обслуживания(перспектива, за гор. чертой)</Layer>
        <Layer>железная дорога</Layer>
        <Layer>заказник ландшафтный </Layer>
        <Layer>зелёные зоны (расчётный срок)</Layer>
        <Layer>смешанная застройка</Layer>
        <Layer>гаражи</Layer>
        <Layer>территории</Layer>
        <Layer>Genplan_Osnovne_Kresl.DBO.gorod</Layer>
        <Layer>Зона розміщення об’єкту</Layer>
        <Layer>садібна забудова</Layer>
        <Layer>территорії санаторно-оздоров.установ</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: { _ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTE8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD43MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMTI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_
}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Генплан м.Одеси.М { _масштаб_ }</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-4.5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>2.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Генеральный план: планировочные ограничения</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Планувальні обмеження</Layer>
        <Layer>просветительский экологический центр (проект)</Layer>
        <Layer>ГРС 1</Layer>
        <Layer>ГРС 2</Layer>
        <Layer>ГРС 3</Layer>
        <Layer>пост мониторинга состояния грунтов</Layer>
        <Layer>пост мониторинга атмосферного воздуха</Layer>
        <Layer>пост мониторинга водной среды</Layer>
        <Layer>объекты ПЗФ (перспектива)</Layer>
        <Layer>объекты ПЗФ гос.значения</Layer>
        <Layer>объекты ПЗФ местн.значения</Layer>
        <Layer>ТЭЦ</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.naftoprovid</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.ohor_z_gas_lines</Layer>
        <Layer>прибережна захисна смуга(проект)</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.szz_grs_lines</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.szz_kladb_pro_lines</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.szz_ochistnih_pro_lines</Layer>
        <Layer>водоохоронна зона(проект)</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.z_d__podezdy_pr</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.z_d_pr</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.z_oh_kuy</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.z_oh_kuy2</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.z_oh_kuy3</Layer>
        <Layer>акустический режим зона Г(ограничения застройки)</Layer>
        <Layer>акустический режим зона Б В(регламентация застройки)</Layer>
        <Layer>ограничения по высотности застройки</Layer>
        <Layer>граница города</Layer>
        <Layer>1 санзона курорта ""Куяльник""</Layer>
        <Layer>2 санзона курорта ""Куяльник""</Layer>
        <Layer>3 санзона курорта ""Куяльник""</Layer>
        <Layer>межі історичних ареалів</Layer>
        <Layer>кр.линии жилых улиц</Layer>
        <Layer>кр.линии магистральных улиц</Layer>
        <Layer>набережная</Layer>
        <Layer>охранная зона ЛЭП (сущ)</Layer>
        <Layer>охранная зона ЛЭП(проект)</Layer>
        <Layer>охранная зона газ</Layer>
        <Layer>охранная зона нефтепровод</Layer>
        <Layer>комплексна охоронна зона</Layer>
        <Layer>зона регулируемой застройки(Молдаванка)</Layer>
        <Layer>зона охраняемого ландшафта(акватория)</Layer>
        <Layer>пляжи</Layer>
        <Layer>ВПП</Layer>
        <Layer>полоса отвода ж.д.</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.proezch_old</Layer>
        <Layer>СЗЗ 1 класу шкідливості(1 000 м)</Layer>
        <Layer>СЗЗ 2 класу шкідливості(500 м)</Layer>
        <Layer>СЗЗ 3 класу шкідливості(300 м)</Layer>
        <Layer>СЗЗ 4-5 класу шкідливості(100-50 м)</Layer>
        <Layer>СЗЗ ГРС</Layer>
        <Layer>СЗЗ залізниці</Layer>
        <Layer>СЗЗ кладбищ</Layer>
        <Layer>СЗЗ очистных сооружений</Layer>
        <Layer>GenPlan_Obmez_Proekt.DBO.voda_pr</Layer>
        <Layer>зона охраны археологического слоя</Layer>
        <Layer>заказник ландшафтный </Layer>
        <Layer>зеленые зоны общего пользования (проект)</Layer>
        <Layer>зона охраняемого ландшафта</Layer>
        <Layer>зона ограничения по высотности застройки</Layer>
        <Layer>Реєстр вулиць</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: { _ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTE8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMTI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Генплан м.Одеси.Планувальні обмеження.М { _масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text> {_ДатаЗаявки_}р.</Text>
          <PosX>-1</PosX>
          <PosY>-2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTA8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz45NzUwMDwvRm9udFNpemVMbz48VGV4dFBBcnNlckNsYXNzPntENzA5OUI5MS1FMjk4LTQ5OUUtOTc1Ny03MkJCMzlFNTVDRjN9PC9UZXh0UEFyc2VyQ2xhc3M+PC9uczpUZXh0U3ltYm9sQ2xhc3NfU2VyaWFsaXplZD4=</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-4.5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>4.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Детальный план: схема зонирования территорийй</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Реєстр вулиць</Layer>
        <Layer>ДПТ</Layer>
        <Layer>ДПТ (утвержденные)</Layer>
        <Layer>Проектний план</Layer>
        <Layer>Проектний план </Layer>
        <Layer>Проектний план </Layer>
        <Layer>ДПТ в межах вулиць: Миколи Гефта, Отамана Чепіги, 7-ма Пересипська</Layer>
        <Layer>План функціонального зонування території</Layer>
        <Layer>План функціонального зонування території</Layer>
        <Layer>ДПТ в межах вулиць: Миколи Гефта, Отамана Чепіги, 7-ма Пересипська</Layer>
        <Layer>ДПТ (в разработке)</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: {_ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTE8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMTI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Викопіювання з ДПТ {_ОписательныйАдрес_} 
у м.Одесі М { _масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-6.5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>6</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>2.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Детальный план: схема планировочные ограничения</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Зона розміщення об’єкту</Layer>
        <Layer>Реєстр вулиць</Layer>
        <Layer>ДПТ</Layer>
        <Layer>ДПТ (утвержденные)</Layer>
        <Layer>Планувальні обмеження</Layer>
        <Layer>Планувальні обмеження</Layer>
        <Layer>План червоних ліній</Layer>
        <Layer>План червоних ліній</Layer>
        <Layer>Планувальні обмеження</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: { _ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTE8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMTI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Планувальні обмеження(згідно ДПТ)  М {_масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>3.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Границы участков смежных землепользователей</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Реєстр вулиць</Layer>
        <Layer>Земельні ділянки</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: { _ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTE8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMTI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTQ8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xNDI1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Межі ділянок суміжних землекористувачів
(витяг з бази даних УЗР ДКВ ОМР)</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>станом на 01.05.2016 р.</Text>
          <PosX>-1</PosX>
          <PosY>-3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD43MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-5.5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>3.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Містобудівні умови і обмеження (МУіО)</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Містобудівні умови та обмеження(МБУ і О)</Layer>
        <Layer>Будівельні паспорти(БП)</Layer>
        <Layer>Реєстр вулиць</Layer>
        <Layer>границы админ. районов</Layer>
        <Layer>граница города</Layer>
        <Layer>2000 вектор</Layer>
        <Layer>Annotation</Layer>
        <Layer>Характеристика</Layer>
        <Layer>Номер_буд</Layer>
        <Layer>Пояснення</Layer>
        <Layer>Знаки</Layer>
        <Layer>Берегові навігаційні знаки</Layer>
        <Layer>Чагарники колючі окремі групи знак</Layer>
        <Layer>Чагарники колючі зарості знак</Layer>
        <Layer>Чагарники колючі зарості знак</Layer>
        <Layer>Ділянки, покриті відходами пром-підпр-знак</Layer>
        <Layer>Ділянки зі зритою поверхнею(знак)</Layer>
        <Layer>Фруктові і цитрусові сади знак</Layer>
        <Layer>Газони знак</Layer>
        <Layer>Скульптури стели- тури знак</Layer>
        <Layer>Характеристика глибини обриву</Layer>
        <Layer>Характеристики каналів, канав</Layer>
        <Layer>Характеристики сухих канав</Layer>
        <Layer>Хрести і знаки з релігійним зображенням</Layer>
        <Layer>Інші знаки дорожні</Layer>
        <Layer>Кабельні стовпчики - сторожки</Layer>
        <Layer>Камені-орієнтири окремі</Layer>
        <Layer>Кладовища знак</Layer>
        <Layer>Клумби знак</Layer>
        <Layer>Ліс високостовб- знак</Layer>
        <Layer>Ліси низькор- знак</Layer>
        <Layer>Пам'ятники Вічний вогонь знак</Layer>
        <Layer>Памятники знак</Layer>
        <Layer>Рідка поросль знак</Layer>
        <Layer>Рідколісся низькоросле знак</Layer>
        <Layer>Рідколісся знак</Layer>
        <Layer>Росл трав степу знак</Layer>
        <Layer>Рослин- високот- знак</Layer>
        <Layer>Рослин- волог- знак</Layer>
        <Layer>Синагоги знак</Layer>
        <Layer>Сквери, парки, бульвари, знак</Layer>
        <Layer>Телевіз- знак</Layer>
        <Layer>Трав росл лук знак</Layer>
        <Layer>Виноградники знак</Layer>
        <Layer>Заболочені землі знак</Layer>
        <Layer>Зарості очерету знак</Layer>
        <Layer>Землі засол знак</Layer>
        <Layer>Знаки нівелірні репери стінні</Layer>
        <Layer>Кран знак</Layer>
        <Layer>Могили братські знак</Layer>
        <Layer>Карта</Layer>
        <Layer>2 000 </Layer>
        <Layer>Заводські і фабричні труби, що є орієнтирами</Layer>
        <Layer>Ями позамасштабні</Layer>
        <Layer>Ями і траншеї позамасштабні</Layer>
        <Layer>Висота від землі до низу труби (арочн- перех)</Layer>
        <Layer>Вишки легкого типу позамасштабні</Layer>
        <Layer>Входи в підземні частини будівель (напрямок)</Layer>
        <Layer>Трансформатори на стовпах позамасштабні</Layer>
        <Layer>Світлофори на стовпах</Layer>
        <Layer>Світлофори карликові</Layer>
        <Layer>Свердловини з механічним підйомом води</Layer>
        <Layer>Свердловини глибокого буріння</Layer>
        <Layer>Свердловини артезіанські</Layer>
        <Layer>Стовп ЗБ квадратний</Layer>
        <Layer>Стовп мет квадратний</Layer>
        <Layer>Споруди баштового типу капітальні</Layer>
        <Layer>Скупчення каміння позамасштабний</Layer>
        <Layer>Скупчення каміння(окремий камінь)</Layer>
        <Layer>Скелі останці позамасштабні</Layer>
        <Layer>Скелі надводні</Layer>
        <Layer>Семафори</Layer>
        <Layer>Пункти геодезичних мереж згущення</Layer>
        <Layer>Пункти геодез- мереж згущення у стінах будівель</Layer>
        <Layer>Пункти державної геодезичної мережі</Layer>
        <Layer>Прожектори карликові постійні</Layer>
        <Layer>Прожектори</Layer>
        <Layer>Позначки висот підпірної стінки</Layer>
        <Layer>Позначки урізів води</Layer>
        <Layer>Покажчик напрямку схилів (бергштрихи)</Layer>
        <Layer>Покажчик напрямку схилу(бергштрихи ізобат)</Layer>
        <Layer>Пляжі обладнані</Layer>
        <Layer>Підпис</Layer>
        <Layer>Павільйони, альтанки позамасштабні</Layer>
        <Layer>Окремі кущі уздовж доріг річок</Layer>
        <Layer>Напрям течії</Layer>
        <Layer>Могили окремі</Layer>
        <Layer>Мочарі з очеретом і тростиною</Layer>
        <Layer>Місця переходу від повітряних ліній зв’язку до кабельних підзем</Layer>
        <Layer>Місця переходу від повітряних ЛЕП до кабельних підземних ЛЕП</Layer>
        <Layer>Межі аркових переходів трубопроводів</Layer>
        <Layer>Маяки</Layer>
        <Layer>ЛЕП направлення вис напруги</Layer>
        <Layer>ЛЕП направлення низ напруги</Layer>
        <Layer>Кущі що ростуть окремо</Layer>
        <Layer>Кургани позамасштабні</Layer>
        <Layer>Крани підйомні( кран-балки)</Layer>
        <Layer>Колонки водозбірні</Layer>
        <Layer>Колодязі з механічним підйомом води(колодязі)</Layer>
        <Layer>Колодязі з ручним насосом</Layer>
        <Layer>Колодязі з корбою на стовпах</Layer>
        <Layer>Колодязі артезіанські</Layer>
        <Layer>Кінці колій з упорами</Layer>
        <Layer>Кінці колій без упорів</Layer>
        <Layer>Камені у водоймах обсихаючі окремі</Layer>
        <Layer>Камені у водоймах надводні розташ- групами</Layer>
        <Layer>Камені у водоймах надводні окремі</Layer>
        <Layer>Горби позамасштабні</Layer>
        <Layer>Гідранти пожежні полівальні</Layer>
        <Layer>Факели газові</Layer>
        <Layer>Джерела необладнані</Layer>
        <Layer>Чагарники окремі групи</Layer>
        <Layer>Будки трансформаторні позамасштабні</Layer>
        <Layer>Блискавковідводи</Layer>
        <Layer>Камені у водоймах надводні окремі</Layer>
        <Layer>Бензоколонки, колонки дизельного пального</Layer>
        <Layer>Зсуви недіючі</Layer>
        <Layer>Зсуви діючі</Layer>
        <Layer>Землі засолені виходами солі на поверхню</Layer>
        <Layer>Заводські і фабричні труби, їх основа</Layer>
        <Layer>Залізобетонна стіна</Layer>
        <Layer>Залізниці лінія контактної мережі</Layer>
        <Layer>Заболоченість</Layer>
        <Layer>Яри без знаку</Layer>
        <Layer>Ями в масштабі плану</Layer>
        <Layer>Ями і траншеї бетоновані</Layer>
        <Layer>Ями і траншеї</Layer>
        <Layer>Ями без знаку</Layer>
        <Layer>В'їзди на 2-й поверх</Layer>
        <Layer>Вузькі смуги дерев висотою до 4 м</Layer>
        <Layer>Вузькі смуги дерев висотою 4 м і більше</Layer>
        <Layer>Вулиці</Layer>
        <Layer>Ворота габаритні</Layer>
        <Layer>Водовипуски із заслонками</Layer>
        <Layer>Водосховища підземні</Layer>
        <Layer>Водна рослинність</Layer>
        <Layer>Виїмки без знаку</Layer>
        <Layer>Вишки легкого типу в масштабі плану</Layer>
        <Layer>Вирви карстові і псевдокарстові в масштабі плану</Layer>
        <Layer>Входи закриті в підземні частини будівель</Layer>
        <Layer>Входи відкриті в підземні частини будівель</Layer>
        <Layer>Веранди та тераси</Layer>
        <Layer>Вентилятори наземні</Layer>
        <Layer>Трубопроводи на опорах</Layer>
        <Layer>Трубопроводи на грунті</Layer>
        <Layer>Трубопроводи без розподілу за призначенням</Layer>
        <Layer>Труби під дорогами (вихід)</Layer>
        <Layer>Труби під дорогами</Layer>
        <Layer>Труби на річках каналах(вихід)</Layer>
        <Layer>Труби на річках, каналах</Layer>
        <Layer>Терикони без знаку</Layer>
        <Layer>Тераси полів закріплені</Layer>
        <Layer>Тераси без знаку</Layer>
        <Layer>Телевізійні та радіощогли, ретранслятори</Layer>
        <Layer>Струмки з пересихаючою береговою лінією</Layer>
        <Layer>Струмки з невизначеною береговою лінією</Layer>
        <Layer>Струмки(лінійний об'єкт)</Layer>
        <Layer>Стенди, меморіальні дошки</Layer>
        <Layer>Стародавні і історичні стіни</Layer>
        <Layer>Станційні колії</Layer>
        <Layer>Спуски та сходи на набережних</Layer>
        <Layer>Споруди башт- типу капітальні в м-бі плану</Layer>
        <Layer>Смуги деревних насаджень шир - 3мм і висотою до 4м</Layer>
        <Layer>Смуги деревних насаджень шир - 3мм і висотою 4м та більше</Layer>
        <Layer>Смуги чагарників шириною від 3 до 8мм</Layer>
        <Layer>Смуги чагарників шириною - 3мм (живоплоти)</Layer>
        <Layer>Скелі останці її основа</Layer>
        <Layer>Скелі останці в масштабі плану</Layer>
        <Layer>Шляхопроводи автодорожні над залізницею</Layer>
        <Layer>Селища сільського типу</Layer>
        <Layer>Ряди окремих дерев уздовж доріг, річок</Layer>
        <Layer>Розподільний металевий блок</Layer>
        <Layer>Розподільний бетонний блок</Layer>
        <Layer>Рейки підйомних кранів</Layer>
        <Layer>Промоїни</Layer>
        <Layer>Приямки</Layer>
        <Layer>Порти, пристані з обладнаними причалами</Layer>
        <Layer>Польові та лісові дороги (край пунктир)</Layer>
        <Layer>Польові та лісові дороги</Layer>
        <Layer>Покращені грунтові дороги 0,3</Layer>
        <Layer>Покращені грунтові дороги 0,1</Layer>
        <Layer>Погреби і овочесховища(лінія)</Layer>
        <Layer>Площадки</Layer>
        <Layer>Пішохідні стежки</Layer>
        <Layer>Підпірно-регулюючі споруди</Layer>
        <Layer>Підпірні стінки прямовисні</Layer>
        <Layer>Підпірні стінки похилі в масштабі плану</Layer>
        <Layer>Підпірні стінки похилі</Layer>
        <Layer>Переходи підземні</Layer>
        <Layer>Парники</Layer>
        <Layer>Парапети в масштабі плану</Layer>
        <Layer>Парапети</Layer>
        <Layer>Опорні стовпи та ферми</Layer>
        <Layer>Огорожі, трельяжі</Layer>
        <Layer>Огорожі шиферні на фундаменті</Layer>
        <Layer>Огорожі металеві вис- менше 1 м</Layer>
        <Layer>Огорожі металеві на кам- бет фундам вис-менше 1м</Layer>
        <Layer>Огорожі дротяні з колючого дроту</Layer>
        <Layer>Огорожі дротяні з гладкого дроту</Layer>
        <Layer>Огорожі дротяні “електропастухи”</Layer>
        <Layer>Обриви земляні без знаку</Layer>
        <Layer>Обриви скелясті в масштабі плану</Layer>
        <Layer>Непроїжджі вулиці</Layer>
        <Layer>Наземні частини підземних споруд</Layer>
        <Layer>Навіси та перекриття між будинками</Layer>
        <Layer>Навіси на стовпах</Layer>
        <Layer>Навіси-козирки</Layer>
        <Layer>Навіси для автомобільних ваг</Layer>
        <Layer>Нависаючі частини будинків</Layer>
        <Layer>Насипи без знаку</Layer>
        <Layer>Насипи</Layer>
        <Layer>Набережні без знаку</Layer>
        <Layer>Мостів опори</Layer>
        <Layer>Мости пішохідні зі східцями</Layer>
        <Layer>Мости пішохідні висячі</Layer>
        <Layer>Мости однопрогінні кам'яні, бетонні,залізобетонні</Layer>
        <Layer>Мости на плавучих опорах</Layer>
        <Layer>Мости малі кам'яні ЗБ мет</Layer>
        <Layer>Мости малі дерев'яні</Layer>
        <Layer>Мости довжиною до 1 м на автомобільних дорогах</Layer>
        <Layer>Мости багатопрогінні кам'яні, бетонні,залізобетонні</Layer>
        <Layer>Межі зміни покриття</Layer>
        <Layer>Межі землекористувань</Layer>
        <Layer>Межа проїздної частини автодоріг</Layer>
        <Layer>Лотки (їх борти)</Layer>
        <Layer>Лотки і жолоби для подачі води наземні</Layer>
        <Layer>Лотки на залізницях</Layer>
        <Layer>Лінії зв'язку повітряні дротяні на забудов території</Layer>
        <Layer>Лінії зв'язку повітряні дротяні на незабуд території</Layer>
        <Layer>Лінії електропередач повітряні кабельні</Layer>
        <Layer>Лінії берегові постійні</Layer>
        <Layer>Лінії берегові невизначені по болотах очерету</Layer>
        <Layer>Лінії берегові непостійні пересихаючі</Layer>
        <Layer>Лінія проїзду</Layer>
        <Layer>Лінія коричнева</Layer>
        <Layer>Лінія КОНТУР-ЗЕЛЕНИЙ</Layer>
        <Layer>Лінія КОНТУР</Layer>
        <Layer>Лінія чорна</Layer>
        <Layer>Лінія (2-1) зелена</Layer>
        <Layer>ЛЕП повітряні дротяні на незабудованій території низ напруги</Layer>
        <Layer>ЛЕП повітряні дротяні на незабудованій території вис напруги</Layer>
        <Layer>Кургани в масштабі плану без знаку</Layer>
        <Layer>Кургани в масштабі плану</Layer>
        <Layer>Козлові та мостові крани</Layer>
        <Layer>Косметичний шар чорний</Layer>
        <Layer>Косметичний шар білий</Layer>
        <Layer>Короб</Layer>
        <Layer>Колодязі</Layer>
        <Layer>Кар'єри площа</Layer>
        <Layer>Кар'єри без знаку</Layer>
        <Layer>Кар’єри</Layer>
        <Layer>Канави сухі</Layer>
        <Layer>Канали</Layer>
        <Layer>Камера на трубопроводі</Layer>
        <Layer>Кабель</Layer>
        <Layer>Інформаційні та рекламні стенди</Layer>
        <Layer>Грунтові дороги (край пунктир)</Layer>
        <Layer>Ґрунтові дороги(путівці)</Layer>
        <Layer>Газопровід</Layer>
        <Layer>Ганки закриті кам’яні</Layer>
        <Layer>Ганки закриті дерев’яні</Layer>
        <Layer>Ганки відкриті, східці наверх</Layer>
        <Layer>Галереї</Layer>
        <Layer>Фонтани(основа)</Layer>
        <Layer>Естакади технологічні та вантажні</Layer>
        <Layer>Естакади підйомних кранів</Layer>
        <Layer>Естакади для ремонту автомобілів</Layer>
        <Layer>Естакади</Layer>
        <Layer>Дороги підвісні</Layer>
        <Layer>Дерев'яні огорожі з капітальними опорами</Layer>
        <Layer>Дерев'яні огорожі на кам бет цегл фундаменту</Layer>
        <Layer>Дамби і вали позамасштабні</Layer>
        <Layer>Дамби і вали без знаку</Layer>
        <Layer>Дамби і вали</Layer>
        <Layer>Човнові станції</Layer>
        <Layer>Брандмауери</Layer>
        <Layer>Бункери саморозвантажні</Layer>
        <Layer>Берегові лінії</Layer>
        <Layer>Балкони на стовпах</Layer>
        <Layer>Басейни облицьовані</Layer>
        <Layer>Баштові та портальні крани</Layer>
        <Layer>Баки та цистерни, газгольдери в масштабі плану</Layer>
        <Layer>Баки та цистерни-газгольдери позамасштабні</Layer>
        <Layer>Автомобільні дороги з удоск-покрит- (точка)</Layer>
        <Layer>Автомобільні дороги(край узбіччя)</Layer>
        <Layer>Арки постійні на автомобільних дорогах</Layer>
        <Layer>Акведуки</Layer>
        <Layer>В'їзди під арками</Layer>
        <Layer>Переходи та галереї надземні між будинками</Layer>
        <Layer>Укоси укріплені в масштабі плану</Layer>
        <Layer>Укоси укріплені позамасштабні</Layer>
        <Layer>Укоси неукріплені в масштабі плану</Layer>
        <Layer>Укоси неукріплені позамасштабні</Layer>
        <Layer>Укоси без знаку</Layer>
        <Layer>Труби димохідні котельних</Layer>
        <Layer>Трамвай, лінія контактної мережі</Layer>
        <Layer>Трамвайні колії</Layer>
        <Layer>Шляхопроводи залізничні над автомобільною дорогою</Layer>
        <Layer>Шлагбаум</Layer>
        <Layer>Ряди дерев (алеї) на вулицях</Layer>
        <Layer>Позначки висот</Layer>
        <Layer>Огорожі металеві вис- 1 м і більше</Layer>
        <Layer>Огорожі металеві на кам- бет фундам вис- 1м і більше</Layer>
        <Layer>Лінія направлення звязку</Layer>
        <Layer>Огорожі дротяні з дротяної сітки (вольєри)</Layer>
        <Layer>Ліхтарі електричні  одинарні</Layer>
        <Layer>Ліхтарі електричні  двойні</Layer>
        <Layer>Ліхтарі електричні(декоративні)</Layer>
        <Layer>ЛЕП повітряні дротяні на забудованій території вис напруги</Layer>
        <Layer>ЛЕП повітряні дротяні на забудованій території низ напруги</Layer>
        <Layer>Колодязі оглядові (люки)</Layer>
        <Layer>Кам’яні, залізобетонні огорожі заввишки 1 м та більше</Layer>
        <Layer>Дерев'яні огорожі суцільні,штахетні</Layer>
        <Layer>Кам’яні, залізобетонні та глинобитні огорожі заввишки менше 1 м</Layer>
        <Layer>Дерева хвойні характеристика</Layer>
        <Layer>Дерева листяні характеристика</Layer>
        <Layer>Дерева що стоять окремо</Layer>
        <Layer>Дерева які мають значення орієнтира хвойні</Layer>
        <Layer>Стовп дер круглий</Layer>
        <Layer>Стовп фермовий</Layer>
        <Layer>Стовп(лінія відтяжки)</Layer>
        <Layer>Стовп мет круглий</Layer>
        <Layer>Стовп ЗБ круглий</Layer>
        <Layer>Човнові станції</Layer>
        <Layer>Віадуки пішохідні над залізницею</Layer>
        <Layer>Зсуви</Layer>
        <Layer>Зсуви (штриховий пунктир)</Layer>
        <Layer>Колонади</Layer>
        <Layer>Круг поворотний</Layer>
        <Layer>Межа без бортового каменю</Layer>
        <Layer>Межа присадибної ділянки</Layer>
        <Layer>Межа з бортовим каменем</Layer>
        <Layer>Моли, причали, пірси із похилими стінками</Layer>
        <Layer>Моли, причали, пірси із прямовисними стінками</Layer>
        <Layer>Мости пішохідні</Layer>
        <Layer>Набережні прямовисні</Layer>
        <Layer>Обриви земляні</Layer>
        <Layer>Обриви земляні позамасштабні</Layer>
        <Layer>Обриви скеляст без знаку</Layer>
        <Layer>Сходи біля будинків, ганків</Layer>
        <Layer>Сходи для підйому на різні споруди</Layer>
        <Layer>Фунікульори і бремсберги</Layer>
        <Layer>Фонтани</Layer>
        <Layer>Тротуари доріжки без покриття</Layer>
        <Layer>Заводські і фабричні труби, що не є орієнтирами</Layer>
        <Layer>Зупинки автобусів та тролейбусів за населеними пунктами</Layer>
        <Layer>Горизонталі (потовщені 2-1)</Layer>
        <Layer>Горизонталі потовщені</Layer>
        <Layer>Горизонталі (основні 2-1)</Layer>
        <Layer>Горизонталі додаткові(5-1)</Layer>
        <Layer>Горизонталі основні</Layer>
        <Layer>Ж/Д дорога, вулиці</Layer>
        <Layer>Залізниці вузькоколійні</Layer>
        <Layer>Залізниці розібрані</Layer>
        <Layer>Залізниці ширококолійні</Layer>
        <Layer>2000 продовження</Layer>
        <Layer>Пам’ятники та монументи</Layer>
        <Layer>Павільйони, альтанки в масштабі плану</Layer>
        <Layer>Вимощення будинків</Layer>
        <Layer>Виноградники</Layer>
        <Layer>Трибуни</Layer>
        <Layer>Смуги деревних насаджень шир від3 до 8мм та - 8мм і вис-4м та більше</Layer>
        <Layer>Трав'яна рослинність лук</Layer>
        <Layer>Смуги деревних насаджень шир від 3 до 8мм та - 8мм і вис- до 4м</Layer>
        <Layer>Переїзд через залізницю</Layer>
        <Layer>Фруктові сади з ягідниками</Layer>
        <Layer>Сади фруктові з виноградниками</Layer>
        <Layer>Фруктові і цитрусові сади</Layer>
        <Layer>Дощові ями і споруди для збору води</Layer>
        <Layer>Зарості очерету</Layer>
        <Layer>Чагарники звичайні зарості</Layer>
        <Layer>Чагарники колючі зарості</Layer>
        <Layer>Бункери та будки оглядові</Layer>
        <Layer>Загони для тварин</Layer>
        <Layer>Рослинність вологолюбива трав’яна</Layer>
        <Layer>Рослинність високотрав’яна</Layer>
        <Layer>Рідколісся високостовбурне</Layer>
        <Layer>Рідколісся низькоросле</Layer>
        <Layer>Рослинність трав’яна степова</Layer>
        <Layer>Внутрішньо квартальне озеленення</Layer>
        <Layer>Вуличне озеленення</Layer>
        <Layer>Газони</Layer>
        <Layer>Городи</Layer>
        <Layer>Канали наземні</Layer>
        <Layer>Скупчення каміння в масштабі плану</Layer>
        <Layer>Канави</Layer>
        <Layer>Струмки (площинний об'єкт)</Layer>
        <Layer>Вантажно-розв- площадки високі</Layer>
        <Layer>Вантажно- розв- площадки високі рампи при будівлях або спорудах</Layer>
        <Layer>Вантажно-розв площадки низькі з бортовим каменем або без нього</Layer>
        <Layer>Платформи відкриті</Layer>
        <Layer>Платформи криті з опорами</Layer>
        <Layer>Ферма металева</Layer>
        <Layer>Ділянки зі зритою поверхнею</Layer>
        <Layer>Рілля</Layer>
        <Layer>Каплиці</Layer>
        <Layer>5 000</Layer>
        <Layer>Будівлі</Layer>
        <Layer>Оранжереї, теплиці</Layer>
        <Layer>Синагоги</Layer>
        <Layer>Будки контрольно-розподільчі</Layer>
        <Layer>Будинки з колонами замість частини або всього першого поверху</Layer>
        <Layer>Будки трансформаторні</Layer>
        <Layer>Будинки зруйновані</Layer>
        <Layer>Будівлі виробничого призначення</Layer>
        <Layer>Церкви, костьоли, кірхи</Layer>
        <Layer>Станції метеорологічні</Layer>
        <Layer>Склади</Layer>
        <Layer>Споруди малі (гаражі, туалети та інші)</Layer>
        <Layer>Дитячі майданчики</Layer>
        <Layer>Могили братські</Layer>
        <Layer>Басейни</Layer>
        <Layer>Легкі придорожні споруди (павільйони тощо)</Layer>
        <Layer>Сільськогосподарські підприємства(ферми, станції, майстерні)</Layer>
        <Layer>Відстійники</Layer>
        <Layer>Відстійники та очисні споруди облицьовані</Layer>
        <Layer>Автостоянки в містах</Layer>
        <Layer>Покриття</Layer>
        <Layer>Електричні підстанції</Layer>
        <Layer>Інші промислові об’єкти</Layer>
        <Layer>Внутрішній двір</Layer>
        <Layer>Тротуари доріжки з твердим покриттям</Layer>
        <Layer>Проїзди до будинків</Layer>
        <Layer>Проїжджі частини з покриттям</Layer>
        <Layer>Проїжджі частини без покриття</Layer>
        <Layer>Неорганізована територія</Layer>
        <Layer>Поверхні гравійні та галькові</Layer>
        <Layer>Поверхні щебеневі та кам’янисті розсипи</Layer>
        <Layer>10 000</Layer>
        <Layer>Колективні сади (дачі)</Layer>
        <Layer>Автомобільні дороги з удосконаленим покриттям(в масштабі плану</Layer>
        <Layer>Автомобільні дороги з покриттям шосе (в масштабі плану)</Layer>
        <Layer>Водосховища</Layer>
        <Layer>Електростанції</Layer>
        <Layer>Звалища</Layer>
        <Layer>50 000</Layer>
        <Layer>Ринки</Layer>
        <Layer>Лісопосадки молоді, розсадники лісових і декоративних порід</Layer>
        <Layer>Ліси високостовбурні</Layer>
        <Layer>Ліси пригнічені низькорослі</Layer>
        <Layer>Спортивні споруди</Layer>
        <Layer>Пляжі (площинний об'єкт)</Layer>
        <Layer>Ставки</Layer>
        <Layer>Автомагістралі автостради</Layer>
        <Layer>Пустирі</Layer>
        <Layer>Заболочені землі</Layer>
        <Layer>Майданчики будівельні</Layer>
        <Layer>150 000</Layer>
        <Layer>Сквери, парки, бульвари</Layer>
        <Layer>Піски рівні</Layer>
        <Layer>Річки (площинний об'єкт)</Layer>
        <Layer>Озера</Layer>
        <Layer>Океани і моря</Layer>
        <Layer>Кладовища (доріжки на ньому)</Layer>
        <Layer>Кладовища без деревної рослинності</Layer>
        <Layer>Кладовища з густою дерев росл</Layer>
        <Layer>Кладовища з окремими деревами</Layer>
        <Layer>Кладовища з рідколіссям</Layer>
        <Layer>Виробнича територія</Layer>
        <Layer>Квартали</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: { _ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD43MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>__________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHALeft</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Містобудівні умови та обмеження(станом на 01.04.2014 р.)</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASEAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANwAAAAAAAAAAAAAAAAAAAAAAEhAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAAAAUwcAA1LjC5GPzhGd4wCqAEu4UQEAAACQAQBTBwAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>3.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <Scale_Manual>0</Scale_Manual>
      <ScaleMode>0</ScaleMode>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Об'єкти культурної спадщини</Caption>
      <Enable>true</Enable>
      <Layers>
        <Layer>Зона розміщення об’єкту</Layer>
        <Layer>Реєстр вулиць</Layer>
        <Layer>УООКН</Layer>
        <Layer>Памятники археологии</Layer>
        <Layer>Памятники арх-ры национал. значения</Layer>
        <Layer>Памятники арх-ры национал. и истории местного значения</Layer>
        <Layer>Пам’ятки арх-ри місц. значення</Layer>
        <Layer>Пам.арх-ри та історії місц. значення</Layer>
        <Layer>Памятники истории национал.значения</Layer>
        <Layer>Пам’ятки історії місц.значення</Layer>
        <Layer>Памятник монумент. искусства национал. значения</Layer>
        <Layer>Памятник монумент. искусства местн. значения</Layer>
        <Layer>Памятник монумент. искусства местн. значения 2</Layer>
        <Layer>Пам’ятки монумент. містецтва та історії</Layer>
        <Layer>Памятники истории местн.значения (АМФ)</Layer>
        <Layer>Мосты-памятники</Layer>
        <Layer>Памятник инженерного искусства</Layer>
        <Layer>Парк - памятник архитектуры и градостроительства</Layer>
        <Layer>Объекты ПЗФ</Layer>
        <Layer>Подпорные стены - памятники</Layer>
        <Layer>Подпорные ступени</Layer>
        <Layer>Зона охраняемого ландшафта</Layer>
        <Layer>Зона регулируемой застройки  ""Молдаванка""</Layer>
        <Layer>Заповедник ""Старая Одесса""</Layer>
        <Layer>Комплексна охранна зона</Layer>
        <Layer>Істор.ареал ""Історичний центр""</Layer>
        <Layer>Істор.ареал ""Французський бульвар""</Layer>
        <Layer>Зона охраны археолог.культурного слоя</Layer>
        <Layer>Охранная зона акватории</Layer>
        <Layer>Портовая зона регулирования застройки</Layer>
      </Layers>
      <TextElements>
        <TextElement>
          <Text>Адреса: { _ОписательныйАдрес_}</Text>
          <PosX>-1</PosX>
          <PosY>4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>{_ДолжностьРуководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>_________________ {_ФИОруководителя_}</Text>
          <PosX>-1</PosX>
          <PosY>2</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVABottom</PagePosVertical>
          <AncorHorizontal>esriTHALeft</AncorHorizontal>
          <AncorVertical>esriTVABottom</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4xMjAwMDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>Об’єкти культурної спадщини</Text>
          <PosX>0</PosX>
          <PosY>-1.5</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD43MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>-5</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVATop</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>5</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
  </Pages>
  <Body_Template>
    <string>по адрессу { _ОписательныйАдрес_ } </string>
  </Body_Template>
</CadastralReferenceData>";

        }
    }
}



