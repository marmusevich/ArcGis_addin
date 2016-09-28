using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SharedClasses;
using ESRI.ArcGIS.Display;

namespace CadastralReference
{
    //CadastralReference
    [Serializable]
    [XmlRootAttribute("CadastralReferenceData", IsNullable = true)]
    public class CadastralReferenceData
    {
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

        private void addStandartTekst(OnePageDescriptions opd)
        {
            opd.DataFrameSyze_Down = 5;
            opd.DataFrameSyze_Up = 2.5;
            opd.DataFrameSyze_Left = 1;
            opd.DataFrameSyze_Right = 1;

            OneTextElementDescription oted;

            oted = new OneTextElementDescription();
            oted.Text = @"Адреса: {_ОписательныйАдрес_}";
            oted.PosX = -1;
            oted.PosY = -4;
            oted.PagePosHorizontal = esriTextHorizontalAlignment.esriTHARight;
            oted.PagePosVertical = esriTextVerticalAlignment.esriTVABottom;
            oted.AncorHorizontal = esriTextHorizontalAlignment.esriTHARight;
            oted.AncorVertical = esriTextVerticalAlignment.esriTVABottom;
            //oted.TextSymbolClass = new TextSymbolClass();
            //((IFormattedTextSymbol)oted.TextSymbolClass).CharacterWidth = 10;
            ////oted.TextSymbolClass.Size = 10;
            opd.TextElements.Add(oted);

            oted = new OneTextElementDescription();
            oted.Text = @"{_ДолжностьРуководителя_}";
            oted.PosX = -1;
            oted.PosY = -3;
            oted.PagePosHorizontal = esriTextHorizontalAlignment.esriTHARight;
            oted.PagePosVertical = esriTextVerticalAlignment.esriTVABottom;
            oted.AncorHorizontal = esriTextHorizontalAlignment.esriTHARight;
            oted.AncorVertical = esriTextVerticalAlignment.esriTVABottom;
            //oted.TextSymbolClass = new TextSymbolClass();
            //oted.TextSymbolClass.Size = 14;
            opd.TextElements.Add(oted);

            oted = new OneTextElementDescription();
            oted.Text = @"__________________ {_ФИОруководителя_}";
            oted.PosX = -1;
            oted.PosY = -2;
            oted.PagePosHorizontal = esriTextHorizontalAlignment.esriTHARight;
            oted.PagePosVertical = esriTextVerticalAlignment.esriTVABottom;
            oted.AncorHorizontal = esriTextHorizontalAlignment.esriTHARight;
            oted.AncorVertical = esriTextVerticalAlignment.esriTVABottom;
            //oted.TextSymbolClass = new TextSymbolClass();
            //oted.TextSymbolClass.Size = 14;
            opd.TextElements.Add(oted);
        }

        #endregion // по умолчанию

        [XmlIgnore]
        public const string ObjectLayerName = "Зона розміщення об’єкту ";
        [XmlIgnore]
        public const string ObjectTableName = "Кадастровая_справка.DBO.KS_OBJ_FOR_ALEX";

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region внутренние переменные

        List<OnePageDescriptions> m_Pages = null;

        private int m_ZayavkaID = -1;
        private int m_MapObjectID = -1;
        private bool m_IsReferenceClose = false;
        private Dictionary<string, object> m_ZayavkaData = null;

        private string m_AllRTF = "";
        private string m_TitulRTF_Template = "";
        private string m_Page1RTF_Template = "";
        private StringCollection m_DinamicRTF_Template = null;
        private StringCollection m_DinamicRTF = null;
        private string m_ConstRTF_Template = "";
        private string m_RaspiskaRTF_Template = "";
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
        /// <summary>
        /// справка закрыта для редактирования
        /// </summary>
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
        /// <summary>
        /// Данные заявки
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, object> ZayavkaData { get { return m_ZayavkaData; } set { m_ZayavkaData = value; } }

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

        public string RukovoditelDoljnost = @"Начальник служби м\б кадастру";
        public string RukovoditelFIO = "Греков О.С.";

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

        public CadastralReferenceData(CadastralReferenceData crd):this()
        {
            CopySetingFrom(crd);
        }

        //скопировать настройки
        public void CopySetingFrom(CadastralReferenceData crd)
        {
            if (crd == null) return;
            
            this.TitulRTF_Template = crd.TitulRTF_Template;
            this.Page1RTF_Template = crd.Page1RTF_Template; 
            this.ConstRTF_Template = crd.ConstRTF_Template; 
            this.RaspiskaRTF_Template = crd.RaspiskaRTF_Template; 

            if(crd.DinamicRTF_Template != null)
            {
                this.DinamicRTF_Template = new StringCollection();
                foreach (string s in crd.DinamicRTF_Template)
                    this.DinamicRTF_Template.Add(s);
            }
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
        }

        public void ClearData()
        {
            ZayavkaID = -1;
            MapObjectID = -1;
            IsReferenceClose = false;
            ZayavkaData = null;
            AllRTF = "";
            DinamicRTF = new StringCollection();

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
        /// <summary>
        /// смена заявления
        /// </summary>
        public event EventHandler<EventArgs> ZayavkaID_Change;

        /// <summary>
        /// смена объекта
        /// </summary>
        public event EventHandler<EventArgs> ObjektInMapID_Change;
        
        /// <summary>
        /// запрет редактирования
        /// </summary>
        public event EventHandler<EventArgs> IsReferenceClose_Change;
        
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



        string getDeffaultSettingXMl()
        {
            return @"<?xml version=""1.0"" encoding=""utf-16""?>     <CadastralReferenceData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <RukovoditelDoljnost>Начальник служби м\б кадастру</RukovoditelDoljnost>
  <RukovoditelFIO>Греков О.С.</RukovoditelFIO>
  <TitulRTF_Template />
  <Page1RTF_Template />
  <DinamicRTF_Template />
  <ConstRTF_Template />
  <RaspiskaRTF_Template />
  <Pages>
    <Page>
      <DataFrameSyze_Down>5</DataFrameSyze_Down>
      <DataFrameSyze_Up>2.5</DataFrameSyze_Up>
      <DataFrameSyze_Left>1</DataFrameSyze_Left>
      <DataFrameSyze_Right>1</DataFrameSyze_Right>
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Выкопировка с топологии</Caption>
      <Enable>true</Enable>
      <Layers />
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
          <Text>Викопіювання з топооснови м. Одеси М {_масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>станом на {_ДатаЗаявки_}р.</Text>
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
      <NorthArrow_PosY>1</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVABottom</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>1</ScaleBar_PosY>
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
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Генеральный план</Caption>
      <Enable>true</Enable>
      <Layers />
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
          <Text>Генплан м. Одеси. М {_масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>станом на {_ДатаЗаявки_}р.</Text>
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
      <NorthArrow_PosY>1</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVABottom</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>1</ScaleBar_PosY>
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
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Генеральный план: планировочные ограничения</Caption>
      <Enable>true</Enable>
      <Layers />
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
          <Text>Генплан м. Одеси. Планувальні обмеження. М {_масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>станом на {_ДатаЗаявки_}р.</Text>
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
      <NorthArrow_PosY>1</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVABottom</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>1</ScaleBar_PosY>
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
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Детальный план: схема зонирования территорийй</Caption>
      <Enable>true</Enable>
      <Layers />
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
          <Text>Викопіювання з детального плану території
{_ОписательныйАдрес_} 
у м. Одесі М {_масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>станом на {_ДатаЗаявки_}р.</Text>
          <PosX>-1</PosX>
          <PosY>-4</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTA8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz45NzUwMDwvRm9udFNpemVMbz48VGV4dFBBcnNlckNsYXNzPntENzA5OUI5MS1FMjk4LTQ5OUUtOTc1Ny03MkJCMzlFNTVDRjN9PC9UZXh0UEFyc2VyQ2xhc3M+PC9uczpUZXh0U3ltYm9sQ2xhc3NfU2VyaWFsaXplZD4=</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>1</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVABottom</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>1</ScaleBar_PosY>
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
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Детальный план: схема планировочные ограничения</Caption>
      <Enable>true</Enable>
      <Layers />
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
          <Text>Викопіювання з топооснови м. Одеси М {_масштаб_}</Text>
          <PosX>0</PosX>
          <PosY>-1</PosY>
          <PagePosHorizontal>esriTHACenter</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHACenter</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MjI8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz4yMTc1MDA8L0ZvbnRTaXplTG8+PFRleHRQQXJzZXJDbGFzcz57RDcwOTlCOTEtRTI5OC00OTlFLTk3NTctNzJCQjM5RTU1Q0YzfTwvVGV4dFBBcnNlckNsYXNzPjwvbnM6VGV4dFN5bWJvbENsYXNzX1NlcmlhbGl6ZWQ+</TextSymbolClass_Serialized_innerUse>
        </TextElement>
        <TextElement>
          <Text>станом на {_ДатаЗаявки_}р.</Text>
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
      <NorthArrow_PosY>1</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVABottom</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>1</ScaleBar_PosY>
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
      <IsHasNorthArrow>true</IsHasNorthArrow>
      <IsHasScaleBar>true</IsHasScaleBar>
      <TypeScaleBarName>Scale Line</TypeScaleBarName>
      <Caption>Границы участков смежных землепользователей</Caption>
      <Enable>true</Enable>
      <Layers />
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
          <Text>станом на {_ДатаЗаявки_}р.</Text>
          <PosX>-1</PosX>
          <PosY>-3</PosY>
          <PagePosHorizontal>esriTHARight</PagePosHorizontal>
          <PagePosVertical>esriTVATop</PagePosVertical>
          <AncorHorizontal>esriTHARight</AncorHorizontal>
          <AncorVertical>esriTVATop</AncorVertical>
          <TextSymbolClass_Serialized_innerUse>PG5zOlRleHRTeW1ib2xDbGFzc19TZXJpYWxpemVkIHhzaTp0eXBlPSd0eXBlbnM6VGV4dFN5bWJvbCcgeG1sbnM6bnM9J09uZVRleHRFbGVtZW50RGVzY3JpcHRpb24nIHhtbG5zOnhzaT0naHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UnIHhtbG5zOnhzPSdodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYScgeG1sbnM6dHlwZW5zPSdodHRwOi8vd3d3LmVzcmkuY29tL3NjaGVtYXMvQXJjR0lTLzEwLjQnPjxuczpDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpDb2xvcj48QnJlYWtDaGFySW5kZXg+LTE8L0JyZWFrQ2hhckluZGV4PjxWZXJ0aWNhbEFsaWdubWVudD5lc3JpVFZBQmFzZWxpbmU8L1ZlcnRpY2FsQWxpZ25tZW50PjxIb3Jpem9udGFsQWxpZ25tZW50PmVzcmlUSEFDZW50ZXI8L0hvcml6b250YWxBbGlnbm1lbnQ+PENsaXA+ZmFsc2U8L0NsaXA+PFJpZ2h0VG9MZWZ0PmZhbHNlPC9SaWdodFRvTGVmdD48QW5nbGU+MDwvQW5nbGU+PFhPZmZzZXQ+MDwvWE9mZnNldD48WU9mZnNldD4wPC9ZT2Zmc2V0PjxuczpTaGFkb3dDb2xvciB4c2k6dHlwZT0ndHlwZW5zOlJnYkNvbG9yJz48VXNlV2luZG93c0RpdGhlcmluZz5mYWxzZTwvVXNlV2luZG93c0RpdGhlcmluZz48QWxwaGFWYWx1ZT4yNTU8L0FscGhhVmFsdWU+PFJlZD4wPC9SZWQ+PEdyZWVuPjA8L0dyZWVuPjxCbHVlPjA8L0JsdWU+PC9uczpTaGFkb3dDb2xvcj48U2hhZG93WE9mZnNldD4wPC9TaGFkb3dYT2Zmc2V0PjxTaGFkb3dZT2Zmc2V0PjA8L1NoYWRvd1lPZmZzZXQ+PFRleHRQb3NpdGlvbj5lc3JpVFBOb3JtYWw8L1RleHRQb3NpdGlvbj48VGV4dENhc2U+ZXNyaVRDTm9ybWFsPC9UZXh0Q2FzZT48Q2hhcmFjdGVyU3BhY2luZz4wPC9DaGFyYWN0ZXJTcGFjaW5nPjxDaGFyYWN0ZXJXaWR0aD4xMDA8L0NoYXJhY3RlcldpZHRoPjxXb3JkU3BhY2luZz4xMDA8L1dvcmRTcGFjaW5nPjxLZXJuaW5nPnRydWU8L0tlcm5pbmc+PExlYWRpbmc+MDwvTGVhZGluZz48VGV4dERpcmVjdGlvbj5lc3JpVERIb3Jpem9udGFsPC9UZXh0RGlyZWN0aW9uPjxGbGlwQW5nbGU+MDwvRmxpcEFuZ2xlPjxUeXBlU2V0dGluZz50cnVlPC9UeXBlU2V0dGluZz48VGV4dFBhdGhDbGFzcz57QjY1QTNFNzYtMjk5My0xMUQxLTlBNDMtMDA4MEM3RUM1Qzk2fTwvVGV4dFBhdGhDbGFzcz48VGV4dD48L1RleHQ+PFNpemU+MTA8L1NpemU+PE1hc2tTdHlsZT5lc3JpTVNOb25lPC9NYXNrU3R5bGU+PE1hc2tTaXplPjI8L01hc2tTaXplPjxGb250TmFtZT5BcmlhbDwvRm9udE5hbWU+PEZvbnRJdGFsaWM+ZmFsc2U8L0ZvbnRJdGFsaWM+PEZvbnRVbmRlcmxpbmU+ZmFsc2U8L0ZvbnRVbmRlcmxpbmU+PEZvbnRTdHJpa2V0aHJvdWdoPmZhbHNlPC9Gb250U3RyaWtldGhyb3VnaD48Rm9udFdlaWdodD40MDA8L0ZvbnRXZWlnaHQ+PEZvbnRDaGFyc2V0PjIwNDwvRm9udENoYXJzZXQ+PEZvbnRTaXplSGk+MDwvRm9udFNpemVIaT48Rm9udFNpemVMbz45NzUwMDwvRm9udFNpemVMbz48VGV4dFBBcnNlckNsYXNzPntENzA5OUI5MS1FMjk4LTQ5OUUtOTc1Ny03MkJCMzlFNTVDRjN9PC9UZXh0UEFyc2VyQ2xhc3M+PC9uczpUZXh0U3ltYm9sQ2xhc3NfU2VyaWFsaXplZD4=</TextSymbolClass_Serialized_innerUse>
        </TextElement>
      </TextElements>
      <NorthArrow_PosX>1</NorthArrow_PosX>
      <NorthArrow_PosY>1</NorthArrow_PosY>
      <NorthArrow_PagePosHorizontal>esriTHALeft</NorthArrow_PagePosHorizontal>
      <NorthArrow_PagePosVertical>esriTVABottom</NorthArrow_PagePosVertical>
      <NorthArrow_Serialized_innerUse>AgAEAAMAGAAAAE4AbwByAHQAaAAgAEEAcgByAG8AdwAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAPh/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUkAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAS1w9TDO/QEYOgCAAJuZbMAwAGAAIAAAAAAA4AAABNAGEAcgBrAGUAcgAAAAAA//8AAAAAAAAAAAQAAAAA5hR5ksjQEYu2CAAJ7k5BBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC4AAAAAAAAAAAAAAAAAAAAAAFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPA/AAAAAAAA8D8NAAAAAAAAAP//FgAAAEUAUwBSAEkAIABOAG8AcgB0AGgAAAAAAAAAAAAAAAAAAAAAAAAAkAEAAAAAAACA/AoAA1LjC5GPzhGd4wCqAEu4UQEAAACQAYD8CgAKRVNSSSBOb3J0aEHLpQDaUtARqPIAYIyF7eUCABQAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAA+H8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=</NorthArrow_Serialized_innerUse>
      <ScaleBar_PosX>-1</ScaleBar_PosX>
      <ScaleBar_PosY>1</ScaleBar_PosY>
      <ScaleBar_Height>0.75</ScaleBar_Height>
      <ScaleBar_Width>6.65</ScaleBar_Width>
      <ScaleBar_PagePosHorizontal>esriTHARight</ScaleBar_PagePosHorizontal>
      <ScaleBar_PagePosVertical>esriTVABottom</ScaleBar_PagePosVertical>
      <ScaleBar_AncorHorizontal>esriTHARight</ScaleBar_AncorHorizontal>
      <ScaleBar_AncorVertical>esriTVABottom</ScaleBar_AncorVertical>
      <ScaleBar_Serialized_innerUse>AQAGAAMAFgAAAFMAYwBhAGwAZQAgAEwAaQBuAGUAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAD4fwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAgABAAQACgAAABYAAABLAGkAbABvAG0AZQB0AGUAcgBzAAAAAgAAAAAAAAAAAAhAdD5atpMp0RGaQwCAx+xclgQAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD/////AgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0AAAAAAAAAlsTpfiPR0BGDgwgACbmWzAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFlAAAAAAAAAWUABAAAAAAAAAAAAAAAAAAAAAAAAAAABdj5atpMp0RGaQwCAx+xclgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAKEAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAA1LjC5GPzhGd4wCqAEu4UQHMAACQAcDUAQAFQXJpYWz//5GbCdeY4p5Jl1dyuznlXPMAAAQAAAAAAAAAAAAAAAAACEB0Plq2kynREZpDAIDH7FyWBACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP////8CAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADQAAAAAAAACWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWUAAAAAAAABZQAEAAAAAAAAAAAAAAAAAAAAAAAAAAAF2Plq2kynREZpDAIDH7FyWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAoQAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAADUuMLkY/OEZ3jAKoAS7hRAcwAAJABwNQBAAVBcmlhbP//kZsJ15jinkmXV3K7OeVc8wAAGUdPflSO0hGq2AAAAAAAAAEAAAAAAAIAAAABAAAADAAAAP//AAAAAAEAAAAAAAAAAAAAAAAAAAABAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAACAPnlFHmSyNARi7YIAAnuTkEBAJbE6X4j0dARg4MIAAm5lswBAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAA8D8AAAAADQAAAAAAAAD55RR5ksjQEYu2CAAJ7k5BAQCWxOl+I9HQEYODCAAJuZbMAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAPA/AAAAAA0AAAAAAAAAAAAAAAAAHEAAAAAAAAAUQAAAAAAFAAAA</ScaleBar_Serialized_innerUse>
    </Page>
  </Pages>
</CadastralReferenceData>
";

        }
    }
}



